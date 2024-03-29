using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    // PLC类
    internal class PLCComm
    {
        public PLCComm(string IP, int port)
        {
            Connect(IP, port);
        }

        /// <summary>
        /// 通讯用Socket
        /// </summary>
        internal Socket socket;
       

        /// <summary>
        /// 连接PLC
        /// </summary>
        public void Connect(string socketIP, int socketPort)
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip;
                try
                {
                    ip = IPAddress.Parse(socketIP);
                }
                catch
                {
                    Win_MessageBox messageBox = new Win_MessageBox();
                    messageBox.MessageBoxShow("\r\nIP地址有误或IP不存在，请检查");

                    return;
                }
                IPEndPoint point = new IPEndPoint(ip, socketPort);
                try
                {
                    socket.Connect(point);
                }
                catch
                {
                    if (System.Windows.Forms.MessageBox.Show("连接失败，可能是第三方通讯设备未正常运行，请在第三方通讯设备正常运行后点击确定，或直接点击取消放弃连接。", "提示：", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {

                    }
                }
                if (socket.Connected)
                {
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 断开与PLC的连接
        /// </summary>
        private void Close()
        {
            try
            {
                socket.Close();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
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
                        length = socket.Receive(buffer);
                    }
                    catch { }
                    string result = Encoding.Default.GetString(buffer, 0, length);
                    if (length > 0)
                    {
                        if (Win_TCPClient.Instance.Visible)
                        { }
                        //////  ShowMsg("<- ：" + result);
                        //////Help11 help11 = new Help11();
                        //////help11.str1 = result;
                        //////help11.str2 = socket.RemoteEndPoint.ToString();
                        //////Win_Main.Protocol(help11);
                    }
                    else
                    {
                        if (socket != null)
                        {
                            try
                            {
                                socket.Disconnect(false);
                                socket.Close();
                            }
                            catch { }
                        }
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
        /// 读PLC位
        /// </summary>
        /// <param name="plcBrand">PLC类型</param>
        /// <param name="pos">位位置</param>
        private void ReadBit(PLCBrand plcBrand, int pos)
        {
            try
            {
                switch (plcBrand)
                {
                    case PLCBrand.Omron:

                        break;
                    case PLCBrand.Panasonic:

                        break;
                    case PLCBrand.Mitsubishi:

                        break;
                    case PLCBrand.Siemens:

                        break;
                    case PLCBrand.AB:

                        break;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 写PLC位
        /// </summary>
        /// <param name="plcBrand">PLC品牌</param>
        /// <param name="pos">位位置</param>
        private void WriteBit(PLCBrand plcBrand, int pos)
        {
            try
            {
                switch (plcBrand)
                {
                    case PLCBrand.Omron:

                        break;
                    case PLCBrand.Panasonic:

                        break;
                    case PLCBrand.Mitsubishi:

                        break;
                    case PLCBrand.Siemens:

                        break;
                    case PLCBrand.AB:

                        break;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 读PLC双字
        /// </summary>
        /// <param name="plcBrand">PLC类型</param>
        /// <param name="pos">双字位置</param>
        private void ReadDouble(PLCBrand plcBrand, int pos)
        {
            try
            {
                switch (plcBrand)
                {
                    case PLCBrand.Omron:

                        break;
                    case PLCBrand.Panasonic:

                        break;
                    case PLCBrand.Mitsubishi:

                        break;
                    case PLCBrand.Siemens:

                        break;
                    case PLCBrand.AB:

                        break;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 写PLC双字
        /// </summary>
        /// <param name="plcBrand">PLC品牌</param>
        /// <param name="pos">双字位置</param>
        private void WriteDouble(PLCBrand plcBrand, int pos)
        {
            try
            {
                switch (plcBrand)
                {
                    case PLCBrand.Omron:

                        break;
                    case PLCBrand.Panasonic:

                        break;
                    case PLCBrand.Mitsubishi:

                        break;
                    case PLCBrand.Siemens:

                        break;
                    case PLCBrand.AB:

                        break;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

    }
}
