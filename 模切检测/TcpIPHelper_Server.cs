using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using VSE;
using VSE.Core;
using VControls;

namespace Start
{
    internal class TcpIPHelper_Server
    {
        internal TcpIPHelper_Server(string severName)
        {
            this.Name = severName;
            this.stcpSever.severName = severName;
            this.stcpSever.SeverObj = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.stcpSever.L_Client = new Dictionary<string, Socket>();

            this.ClientConnectedList.Clear();
        }

        /// <summary>
        /// 服务器对象
        /// </summary>
        STCPSever stcpSever = new STCPSever();

        /// <summary>
        /// 是否已监听
        /// </summary>
        internal bool listened = false;

        /// <summary>
        /// 接收到的消息
        /// </summary>
        internal string receivedStr = string.Empty;

        /// <summary>
        /// 服务器名称
        /// </summary>
        internal string Name = string.Empty;

        /// <summary>
        /// 服务器IP地址
        /// </summary>
        internal string SeverIP = "127.0.0.1";

        /// <summary>
        /// 服务器端口号
        /// </summary>
        internal Int32 SeverPort = 8001;

        /// <summary>
        /// 程序开启后自动监听
        /// </summary>
        public bool AutoListenAfterStart = true;

        /// <summary>
        /// 程序关闭前自动断开服务器
        /// </summary>
        public bool AutoDisconnectBeforeClose = true;

        /// <summary>
        /// 监听状态
        /// </summary>
        public string strTcpIPStatus = "";

        public List<string> ClientConnectedList = new List<string>();

        public delegate void UpdateSocketStatus(UpdateSocketStatusEventArgs e);
        public delegate void ReceiveClientMessage(ReceiveClientMessageEventArgs e);
        public static event UpdateSocketStatus updateSocketStatus; 
        public static event ReceiveClientMessage receiveClientMessage; 

        /// <summary>
        /// 监听
        /// </summary>
        internal void Listen()
        {
            try
            {
                Thread th = new Thread(() =>
                {

                    try
                    {
                        IPAddress ip = IPAddress.Parse(SeverIP);
                        IPEndPoint point = new IPEndPoint(ip, SeverPort);

                        stcpSever.SeverObj.Bind(point);
                        stcpSever.SeverObj.Listen(10);
                        strTcpIPStatus = "停止监听";
                        OnUpdateSockectStatus(new UpdateSocketStatusEventArgs(true));
                    }
                    catch
                    {
                        OnUpdateSockectStatus(new UpdateSocketStatusEventArgs(false));
                        Win_MessageBox.Instance.MessageBoxShow("\r\n服务端在监听时出错！(错误代码：001)\r\n\r\n可能原因：所监听的IP地址有误");
                        return;
                    }

                    while (true)
                    {
                        Socket socket = stcpSever.SeverObj.Accept();
                        Thread th_receive = new Thread(Recieve);
                        th_receive.IsBackground = true;
                        th_receive.Start(socket);

                        stcpSever.L_Client.Add(socket.RemoteEndPoint.ToString(), socket);
                        Win_Log.Instance.OutputMsg(string.Format("客户端已连接，信息: {0}", socket.RemoteEndPoint.ToString()), Win_Log.InfoType.tip);
                        ClientConnectedList.Add(socket.RemoteEndPoint.ToString());  
                        //Win_TCPServer.Instance.lbx_connectedList.Items.Add(socket.RemoteEndPoint.ToString());
                        //Win_TCPServer.Instance.cbx_connectedList.Items.Add(socket.RemoteEndPoint.ToString());
                        //if (Win_TCPServer.Instance.cbx_connectedList.Items.Count > 0)
                        //    Win_TCPServer.Instance.cbx_connectedList.SelectedIndex = 0;
                    }
                });
                th.IsBackground = true;
                th.Start();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg">消息</param>
        internal void Send(string clientStr, string msg)
        {
            try
            {
                byte[] buffer = Encoding.Default.GetBytes(msg);
                foreach (KeyValuePair<string, Socket> item in stcpSever.L_Client)
                {
                    if (item.Key == clientStr)
                        item.Value.Send(buffer);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        /// <summary>
        /// 接收一次消息
        /// </summary>
        internal string RecieveOnce(object obj)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int length = 0;
                try
                {
                    length = ((Socket)obj).Receive(buffer);
                }
                catch { }
                string result = Encoding.Default.GetString(buffer, 0, length);
                if (length > 0)
                {
                    OnReceiveClientMessage(new ReceiveClientMessageEventArgs(result));
                    return result;
                }
                else
                {
                    //连接断开
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        private void Recieve(object obj)
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int length = 0;
                    try
                    {
                        length = ((Socket)obj).Receive(buffer);
                    }
                    catch { }
                    string result = Encoding.Default.GetString(buffer, 0, length);
                    if (length > 0)
                    {
                        receivedStr = result;
                        OnReceiveClientMessage(new ReceiveClientMessageEventArgs(result));
                    }
                    else
                    {
                        Win_MessageBox.Instance.MessageBoxShow("客户端xxx连接已断开连接");
                        ClientConnectedList.Clear();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        /// <summary>
        /// 与服务器断开连接
        /// </summary>
        internal void Close()
        {
            try
            {
                //断开所有客户端
                foreach (KeyValuePair<string, Socket> item in stcpSever.L_Client)
                {
                    if (item.Value.Connected)
                        item.Value.Disconnect(false);
                    item.Value.Close();
                }

                //断开服务器
                if (stcpSever.SeverObj.Connected)
                    stcpSever.SeverObj.Disconnect(false);
                stcpSever.SeverObj.Close();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        public static void OnUpdateSockectStatus(UpdateSocketStatusEventArgs e)
        {
            if (updateSocketStatus != null)
            {
                updateSocketStatus(e);
            }
        }

        public static void OnReceiveClientMessage(ReceiveClientMessageEventArgs e)
        {
            if (receiveClientMessage != null)
            {
                receiveClientMessage(e);
            }
        }
    }

    internal struct STCPSever
    {
        /// <summary>
        /// 服务器名称
        /// </summary>
        internal string severName;
        /// <summary>
        /// Socket对象
        /// </summary>
        internal Socket SeverObj;
        /// <summary>
        /// 连接到此服务器的客户端集合
        /// </summary>
        internal Dictionary<string, Socket> L_Client;
    }

    /// <summary>
    /// 
    /// </summary>
    public class UpdateSocketStatusEventArgs : EventArgs
    {
        private bool _Connect;

        public bool Connect
        {
            get { return this._Connect; }
        }

        public UpdateSocketStatusEventArgs(bool isConnect)
        {
            this._Connect = isConnect;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class ReceiveClientMessageEventArgs : EventArgs
    {
        private string _Message;

        public string Message
        {
            get { return this._Message; }
        }

        public ReceiveClientMessageEventArgs(string strMessage)
        {
            this._Message = strMessage;

        }
    }
}
