using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VControls;
using VSE.Core;

namespace VSE
{
    [Serializable]
    internal class TCPSever
    {
        internal TCPSever(string severName)
        {
            this.Name = severName;
            STCPSever stcpSever = new STCPSever();
            stcpSever.severName = severName;
            stcpSever.SeverObj = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            stcpSever.L_Client = new Dictionary<string, Socket>();
            L_STCPSever.Add(stcpSever);
        }

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
        internal string SeverIP = "192.168.0.1";
        /// <summary>
        /// 服务器端口号
        /// </summary>
        internal Int32 SeverPort = 10004;
        /// <summary>  
        /// Socket集合  因Socket类不能被序列化，所以声明一个静态的集合来存储    键：通讯设备名   值：Socket对象
        /// </summary>
        internal static List<STCPSever> L_STCPSever = new List<STCPSever>();
        /// <summary>
        /// 程序开启后自动监听
        /// </summary>
        public bool AutoListenAfterStart = true;
        /// <summary>
        /// 程序关闭前自动断开服务器
        /// </summary>
        public bool AutoDisconnectBeforeClose = true;

        /// <summary>
        /// 通过通讯设备名来查找对应的Socket对象
        /// </summary>
        /// <param name="name">通讯设备名</param>
        /// <returns></returns>
        internal Socket FindSocketByName()
        {
            try
            {
                for (int i = 0; i < L_STCPSever.Count; i++)
                {
                    if (L_STCPSever[i].severName == Name)
                        return L_STCPSever[i].SeverObj;
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return null;
            }
        }
        /// <summary>
        /// 监听
        /// </summary>
        internal void Listen()
        {
            try
            {
                Thread th = new Thread(() =>
                {
                    for (int i = 0; i < L_STCPSever.Count; i++)
                    {
                        if (L_STCPSever[i].severName == Name)
                        {
                            try
                            {
                                IPAddress ip = IPAddress.Parse(SeverIP);
                                IPEndPoint point = new IPEndPoint(ip, SeverPort);

                                L_STCPSever[i].SeverObj.Bind(point);
                                L_STCPSever[i].SeverObj.Listen(10);
                                Win_TCPServer.Instance.btn_listen.Text = "停止监听";
                            }
                            catch
                            {
                                Win_MessageBox.Instance.MessageBoxShow("\r\n服务端在监听时出错！(错误代码：001)\r\n\r\n可能原因：所监听的IP地址有误");
                                return;
                            }

                            while (true)
                            {
                                Socket socket = L_STCPSever[i].SeverObj.Accept();
                                Thread th_receive = new Thread(Recieve);
                                th_receive.IsBackground = true;
                                th_receive.Start(socket);

                                L_STCPSever[i].L_Client.Add(socket.RemoteEndPoint.ToString(), socket);
                                Win_Log.Instance.OutputMsg(string.Format("客户端已连接，信息: {0}", socket.RemoteEndPoint.ToString()),Win_Log.InfoType.tip);
                                Win_TCPServer.Instance.lbx_connectedList.Items.Add(socket.RemoteEndPoint.ToString());
                                Win_TCPServer.Instance.cbx_connectedList.Items.Add(socket.RemoteEndPoint.ToString());
                                if (Win_TCPServer.Instance.cbx_connectedList.Items.Count > 0)
                                    Win_TCPServer.Instance.cbx_connectedList.SelectedIndex = 0;
                            }
                        }
                    }
                });
                th.IsBackground = true;
                th.Start();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
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
                if (Win_TCPServer.Instance.Visible)
                {
                    string curTime = DateTime.Now.ToString("HH:mm:ss");
                    Win_TCPServer.Instance.tbx_log.AppendText(curTime + "<-  : " + msg + "\r\n");
                }
                byte[] buffer = Encoding.Default.GetBytes(msg);
                for (int i = 0; i < L_STCPSever.Count; i++)
                {
                    if (L_STCPSever[i].severName == Name)
                    {
                        foreach (KeyValuePair<string, Socket> item in L_STCPSever[i].L_Client)
                        {
                            if (item.Key == clientStr)
                                item.Value.Send(buffer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
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
                    if (Win_TCPServer.Instance.Visible)
                    {
                        string curTime = DateTime.Now.ToString("HH:mm:ss");
                        Win_TCPServer.Instance.tbx_log.AppendText(curTime + "->  : " + result + "\r\n");
                    }
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
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
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
                        if (Win_TCPServer.Instance.Visible)
                        {
                            string curTime = DateTime.Now.ToString("HH:mm:ss");
                            Win_TCPServer.Instance.tbx_log.AppendText(curTime + "->  : " + result + "\r\n");
                        }
                        receivedStr = result;
                    }
                    else
                    {
                        if (FindSocketByName() != null)
                        {
                            try
                            {
                                FindSocketByName().Disconnect(false);
                                FindSocketByName().Close();

                            }
                            catch { }
                        }

                        if (Win_TCPServer.Instance.Visible)
                        {
                            if (Win_DeviceManager.Instance.dgv_deviceList.SelectedRows[0].Cells[0].Value.ToString() == Name)
                            {
                                Win_TCPServer.Instance.btn_listen.Text = "连接";
                                Win_DeviceManager.Instance.lbl_tip.Text = "连接已断开";
                            }
                        }
                        Win_Main.Instance.OutputMsg("客户端xxx连接已断开连接",Win_Log.InfoType.error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 与服务器断开连接
        /// </summary>
        internal void Close()
        {
            try
            {
                for (int i = 0; i < L_STCPSever.Count; i++)
                {
                    //断开所有客户端
                    foreach (KeyValuePair<string, Socket> item in L_STCPSever[i].L_Client)
                    {
                        if (item.Value.Connected)
                            item.Value.Disconnect(false);
                        item.Value.Close();
                    }

                    //断开服务器
                    if (L_STCPSever[i].SeverObj.Connected)
                        L_STCPSever[i].SeverObj.Disconnect(false);
                    L_STCPSever[i].SeverObj.Close();
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
    }
    /// <summary>
    /// 服务器类型
    /// </summary>
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
}
