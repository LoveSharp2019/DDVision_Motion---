using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using VSE.Core;
using VControls;

namespace VSE
{
    [Serializable]
    class Serial
    {
        internal Serial(string name)
        {
            this.Name = name;
            SerialPort serialPort = new SerialPort();
            L_serial.Add(name, serialPort);
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
        /// 以十六进制形式通讯
        /// </summary>
        internal bool CommHex = false;
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
        internal static Dictionary<string, SerialPort> L_serial = new Dictionary<string, SerialPort>();


        /// <summary>
        /// 通过名称来查找对应的SerialPort对象
        /// </summary>
        /// <param name="name">通讯设备名</param>
        /// <returns></returns>
        internal SerialPort FindSerialPortByName()
        {
            try
            {
                foreach (KeyValuePair<string, SerialPort> item in L_serial)
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
                        ////////FindSerialPortByName().DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
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
                    receiveStr = receiveStr.TrimEnd(endChar.ToCharArray());

                    //此处临时添加，为了处理激光位移传感器的返回数据的
                    string temp = string.Empty;
                    temp = StringToHexString(temp, Encoding.Default);
                    temp = receiveStr.Substring(4, 6);
                    Convert.ToInt32("065A", 16);

                    Win_Scaner.Instance.tbx_output.Text += DateTime.Now.ToString("HH:mm:ss") + "<-  :" + receiveStr + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        /// <summary>
        /// 16进制字符转换为字符串
        /// </summary>
        /// <param name="hs"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        private string HexStringToString(string hs, Encoding encode)
        {
            string strTemp = "";
            byte[] b = new byte[hs.Length / 2];
            for (int i = 0; i < hs.Length / 2; i++)
            {
                strTemp = hs.Substring(i * 2, 2);
                b[i] = Convert.ToByte(strTemp, 16);
            }
            //按照指定编码将字节数组变为字符串
            return encode.GetString(b);
        }
        /// <summary>
        /// 字符串转换为16进制字符
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        private string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符
            {
                if (i != 0)
                    result += " ";
                result += Convert.ToString(b[i], 16);
            }
            return result;
        }
        private static byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += "";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return returnBytes;
        }

        private static string ByteToHexStr(byte[] buffer)
        {
            string Str = string.Empty;
            for (int i = 0; i < buffer.Length; i++)
            {
                Str += buffer[i].ToString("X2");
            }
            return Str;
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
                for (int i = 0; i < 3; i++)
                {
                    ResultStr = string.Empty;

                    if (CommHex)
                    {
                        // temp = StringToHexString(TrigCmd , Encoding.Default);
                        //temp = receiveStr.Substring(4, 6);
                        //Convert.ToInt32("065A", 16);
                        byte[] temp = strToHexByte(TrigCmd);


                        FindSerialPortByName().Write(temp, 0, temp.Length);
                        //  FindSerialPortByName().WriteLine(temp);
                        Thread.Sleep(100);

                        byte[] buffer = new byte[6];
                        FindSerialPortByName().Read(buffer, 0, 6);
                        string ttt = ByteToHexStr(buffer);
                        ttt = ttt.Substring(4, 4);
                        Int32 iii = Convert.ToInt32(ttt, 16);

                        ResultStr = (iii / 100.0).ToString();
                    }
                    else
                    {

                    }
                    if (Win_Serial.Instance.Visible)
                        Win_Serial.Instance.tbx_output.Text += DateTime.Now.ToString("HH:mm:ss") + "<-  :" + ResultStr + Environment.NewLine;

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
