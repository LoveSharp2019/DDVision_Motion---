using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    [Serializable]
    internal class TCPClient
    {
        internal TCPClient(string name)
        {
            this.Name = name;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            L_socket.Add(name, socket);
        }

        /// <summary>
        /// 接收到的消息
        /// </summary>
        internal string receivedStr = string.Empty;
        /// <summary>
        /// 客户端名称
        /// </summary>
        internal string Name = string.Empty;
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        internal string severIP = "192.168.0.1";
        /// <summary>
        /// 服务器端口号
        /// </summary>
        internal Int32 severPort = 10004;
        /// <summary>  
        /// Socket集合  因Socket类不能被序列化，所以声明一个静态的集合来存储    键：通讯设备名   值：Socket对象
        /// </summary>
        internal static Dictionary<string, Socket> L_socket = new Dictionary<string, Socket>();
        /// <summary>
        /// 程序开启后自动连接服务器
        /// </summary>
        public bool AutoConnectAfterStart = true;
        /// <summary>
        /// 程序关闭前自动断开服务器
        /// </summary>
        public bool AutoDisconnectBeforeClose = true;
        /// <summary>
        /// 断开时是否自动连接
        /// </summary>
        public bool AutoConnect = true;

        /// <summary>
        /// 通过通讯设备名来查找对应的Socket对象
        /// </summary>
        /// <param name="name">通讯设备名</param>
        /// <returns></returns>
        internal Socket FindSocketByName()
        {
            try
            {
                foreach (KeyValuePair<string, Socket> item in L_socket)
                {
                    if (item.Key == Name)
                        return item.Value;
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
        /// 连接服务端
        /// </summary>
        internal void Connect()
        {
            try
            {
            Again:
                for (int i = 0; i < L_socket.Count; i++)
                {
                    if (L_socket.Keys.ToArray()[i] == Name)
                        L_socket[L_socket.Keys.ToArray()[i]] = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
                IPAddress ip;
                try
                {
                    ip = IPAddress.Parse(severIP);
                }
                catch
                {
                    Win_MessageBox.Instance.MessageBoxShow("\r\nIP地址有误或IP不存在，连接失败，请检查");
                    return;
                }
                IPEndPoint point = new IPEndPoint(ip, severPort);
                try
                {
                    FindSocketByName().Connect(point);
                }
                catch
                {
                    Win_ConfirmBox.Instance.lbl_info.Text = (string.Format("      客户端 [{0}] 连接失败，可能是服务器端未监听，点击\r\n是放弃连接，或在服务器端监听后点击否再次连接。", this.Name));
                    Win_ConfirmBox.Instance.ShowDialog();
                    if (Win_ConfirmBox.Instance.Result == ConfirmBoxResult.No)
                    {
                        goto Again;
                    }
                }
                if (FindSocketByName().Connected)
                {
                    Thread th_recieve = new Thread(Recieve);
                    th_recieve.IsBackground = true;
                    th_recieve.Start();
                }
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
        internal void Send(string msg)
        {
            try
            {
                if (Win_TCPClient.Instance.Visible)
                {
                    string curTime = DateTime.Now.ToString("HH:mm:ss");
                    Win_TCPClient.Instance.tbx_log.AppendText(curTime + "<-  : " + msg + "\r\n");
                }
                byte[] buffer = Encoding.Default.GetBytes(msg);
                FindSocketByName().Send(buffer);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 接收一次消息
        /// </summary>
        internal string RecieveOnce()
        {
            try
            {
                byte[] buffer = new byte[1024];
                int length = 0;
                try
                {
                    length = FindSocketByName().Receive(buffer);
                }
                catch { }
                string result = Encoding.Default.GetString(buffer, 0, length);
                if (length > 0)
                {
                    if (Win_TCPClient.Instance.Visible)
                    {
                        string curTime = DateTime.Now.ToString("HH:mm:ss");
                        Win_TCPClient.Instance.tbx_log.AppendText(curTime + "->  : " + result + "\r\n");
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
        private void Recieve()
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int length = 0;
                    try
                    {
                        length = FindSocketByName().Receive(buffer);
                    }
                    catch { }
                    string result = Encoding.Default.GetString(buffer, 0, length);
                    if (length > 0)
                    {
                        if (Win_TCPClient.Instance.Visible)
                        {
                            string curTime = DateTime.Now.ToString("HH:mm:ss");
                            Win_TCPClient.Instance.tbx_log.AppendText(curTime + "->  : " + result + "\r\n");
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

                        if (Win_TCPClient.Instance.Visible)
                        {
                            if (Win_DeviceManager.Instance.dgv_deviceList.SelectedRows[0].Cells[0].Value.ToString() == Name)
                            {
                                Win_TCPClient.Instance.btn_connect.Text = "连接";
                                Win_DeviceManager.Instance.lbl_tip.Text = "连接已断开";
                            }
                        }
                        Win_Main.Instance.OutputMsg("服务器连接已中断，已启动自动重连...",Win_Log.InfoType.error);

                        //断开后自动重连
                        //////Thread th = new Thread(() =>
                        //////{
                        for (int i = 0; i < L_socket.Count; i++)
                        {
                            if (L_socket.Keys.ToArray()[i] == Name)
                            {
                                L_socket[L_socket.Keys.ToArray()[i]] = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            }
                        }
                        while (!FindSocketByName().Connected)
                        {
                            IPAddress ip;
                            try
                            {
                                ip = IPAddress.Parse(severIP);
                            }
                            catch
                            {
                                Win_MessageBox.Instance.MessageBoxShow("\r\nIP地址有误或IP不存在，连接失败，请检查");
                                return;
                            }
                            IPEndPoint point = new IPEndPoint(ip, severPort);
                            try
                            {
                                FindSocketByName().Connect(point);

                                Thread th_recieve = new Thread(Recieve);
                                th_recieve.IsBackground = true;
                                th_recieve.Start();
                            }
                            catch { }
                            Thread.Sleep(1000);
                        }

                        if (Win_TCPClient.Instance.Visible)
                        {
                            if (Win_DeviceManager.Instance.dgv_deviceList.SelectedRows[0].Cells[0].Value.ToString() == Name)
                            {
                                Win_TCPClient.Instance.btn_connect.Text = "断开";
                                Win_DeviceManager.Instance.lbl_tip.Text = "连接已断开";
                            }

                        }
                        //////});
                        //////th.IsBackground = true;
                        //////th.Start();

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
                if (FindSocketByName().Connected)
                    FindSocketByName().Disconnect(false);
                FindSocketByName().Close();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

    }
}
