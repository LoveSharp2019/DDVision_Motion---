using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using VSE.Core;
using VControls;

namespace VSE
{
    [Serializable]
    class Scaner
    {
        internal Scaner(string name)
        {
            this.Name = name;
            SerialPort serialPort = new SerialPort();
            L_serialPort.Add(name, serialPort);
        }

        /// <summary>
        /// 串口号
        /// </summary>
        internal string portName = "COM1";
        /// <summary>
        /// 波特率
        /// </summary>
        internal int baudRate = 9600;
        /// <summary>
        /// 数据位
        /// </summary>
        internal int dataBit = 8;
        /// <summary>
        /// 停止位
        /// </summary>
        internal StopBits stopBit = (StopBits)Enum.Parse(typeof(StopBits), "One");
        /// <summary>
        /// 奇偶效验位
        /// </summary>
        internal Parity parity = (Parity)Enum.Parse(typeof(Parity), "Odd");
        /// <summary>
        /// 触发命令
        /// </summary>
        internal string TrigCmd = "T";
        /// <summary>
        /// 失败重扫次数
        /// </summary>
        internal int failNum = 3;
        /// <summary>
        /// 扫描到的条码
        /// </summary>
        internal string ResultStr = string.Empty;
        /// <summary>
        /// 客户端名称
        /// </summary>
        internal string Name = string.Empty;
        /// <summary>
        /// 结束符
        /// </summary>
        internal string endChar = string.Empty;
        /// <summary>  
        /// SerialPort集合  因SerialPort类不能被序列化，所以声明一个静态的集合来存储    键：名称   值：SerialPort对象
        /// </summary>
        internal static Dictionary<string, SerialPort> L_serialPort = new Dictionary<string, SerialPort>();


        /// <summary>
        /// 通过名称来查找对应的SerialPort对象
        /// </summary>
        /// <param name="name">通讯设备名</param>
        /// <returns></returns>
        internal SerialPort FindSerialPortByName()
        {
            try
            {
                foreach (KeyValuePair<string, SerialPort> item in L_serialPort)
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
        internal void Init()
        {
            try
            {
                if (!FindSerialPortByName().IsOpen)
                {
                    //FindSerialPortByName().NewLine = "\r\n";
                    FindSerialPortByName().RtsEnable = false;
                    FindSerialPortByName().PortName = portName;
                    FindSerialPortByName().BaudRate = baudRate;
                    FindSerialPortByName().DataBits = dataBit;
                    FindSerialPortByName().StopBits = stopBit;
                    FindSerialPortByName().Parity = parity;
                    try
                    {
                        FindSerialPortByName().Open();
                        //////FindSerialPortByName().DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
                    }
                    catch
                    {
                        Win_MessageBox.Instance.MessageBoxShow(string.Format("\r\n[{0}] 打开端口失败，可能原因：端口不存在或已被占用", Name), TipType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 收到消息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (!FindSerialPortByName().IsOpen)
                {
                    Win_MessageBox.Instance.MessageBoxShow("\r\n串口未打开，请打开后重试");
                    return;
                }
                string receiveStr = FindSerialPortByName().ReadExisting();
                if (receiveStr != "" && receiveStr.EndsWith(endChar))
                {
                    receiveStr.TrimEnd(endChar.ToCharArray());
                    Win_Scaner.Instance.tbx_output.Text += DateTime.Now.ToString("HH:mm:ss") + "<-  :" + receiveStr + Environment.NewLine;
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
        internal string Scan()
        {
            try
            {
                if (!FindSerialPortByName().IsOpen)
                {
                    Win_MessageBox.Instance.MessageBoxShow("\r\n发送失败，端口未打开", TipType.Error);
                    return string.Empty;
                }

                if (Win_TCPClient.Instance.Visible)
                {
                    string curTime = DateTime.Now.ToString("HH:mm:ss");
                    Win_TCPClient.Instance.tbx_log.AppendText(curTime + "<-  : " + TrigCmd + "\r\n");
                }

                FindSerialPortByName().ReadExisting();
                for (int i = 0; i < failNum ; i++)
                {
                    FindSerialPortByName().WriteLine(TrigCmd);
                    Thread.Sleep(100);
                    ResultStr = FindSerialPortByName().ReadExisting();
                    if (Win_Scaner.Instance.Visible)
                    {
                        Win_Scaner.Instance.tbx_output.Text += DateTime.Now.ToString("HH:mm:ss") + "<-  :" + ResultStr + Environment.NewLine;
                        Application.DoEvents();
                    }
                    if (ResultStr != "NG" && ResultStr != string.Empty)
                        return ResultStr;
                    Thread.Sleep(100);
                }
                Win_MessageBox.Instance.MessageBoxShow("\r\n扫描条码失败");
                return string.Empty;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return string.Empty;
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
                if (!FindSerialPortByName().IsOpen)
                {
                    Win_MessageBox.Instance.MessageBoxShow("\r\n发送失败，端口未打开");
                    return;
                }
                if (Win_Scaner.Instance.Visible)
                {
                    string curTime = DateTime.Now.ToString("HH:mm:ss");
                    Win_Scaner.Instance.tbx_output.AppendText(curTime + "<-  :" + msg + "\r\n");
                }
                FindSerialPortByName().ReadExisting();
                FindSerialPortByName().WriteLine(msg + endChar);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        internal void Close()
        {
            try
            {
                FindSerialPortByName().Close();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
    }
}
