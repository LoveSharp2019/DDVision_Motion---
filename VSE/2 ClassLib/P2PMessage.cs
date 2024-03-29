using System;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using HalconDotNet;

namespace VSE
{
        /// <summary>  
        /// 发送消息处理程序类
        /// 调用该方法“SendMessageToTargetWindow”发送  
        /// </summary>  
        public class P2PMessage
        {
            /// <summary>  
            /// 系统定义的消息
            /// </summary>  
            private const int WM_COPYDATA = 0x004A;


            /// <summary>  
            /// 用户己定义消息
            /// </summary>  
            private const int WM_DATA_TRANSFER = 0x0437;


            // 使用Windows API的FindWindow方法
            [DllImport("User32.dll", EntryPoint = "FindWindow")]
            private static extern int FindWindow(string lpClassName, string lpWindowName);


            //使用Windows API的IsWindow方法
            [DllImport("User32.dll", EntryPoint = "IsWindow")]
            private static extern bool IsWindow(int hWnd);


            // 使用Windows API的SendMessage方法
            [DllImport("User32.dll", EntryPoint = "SendMessage")]
            private static extern int SendMessage(int hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);


            // 使用Windows API的SendMessage方法
            [DllImport("User32.dll", EntryPoint = "SendMessageA")]
            private static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);


            /// <summary>  
            /// 要发信息数据结构 
            /// </summary>  
            private struct COPYDATASTRUCT
            {
                public IntPtr dwData;
                public int cbData;//信息的长度
                public IntPtr lpData;
            }


            /// <summary>  
            /// 向目标窗口发送消息
            /// </summary>  
            /// <param name="wndName">查找目标窗体名称</param>  
            /// <param name="msg">要发送的消息，字符串</param>  
            /// <returns>返回的数据是否成功或则失败</returns>  
            public static bool SendMessageToTargetWindow(string wndName, string msg)
            {
                int iHWnd = FindWindow(null, wndName);
                if (iHWnd == 0)
                {

#if DEBUG
                    string strError = string.Format("SendMessageToTargetWindow: 没有发现{0}窗体!", wndName);
                    MessageBox.Show(strError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.WriteLine(strError);
#endif
                    return false;
                }
                else
                {
                    byte[] bytData = null;
                    bytData = Encoding.Default.GetBytes(msg);
                    COPYDATASTRUCT cdsBuffer;
                    cdsBuffer.dwData = (IntPtr)100;
                    cdsBuffer.cbData = bytData.Length;
                    cdsBuffer.lpData = Marshal.AllocHGlobal(bytData.Length);
                    Marshal.Copy(bytData, 0, cdsBuffer.lpData, bytData.Length);
                    // 使用系统定义的消息wmcopydata来发送消息。 
                    int iReturn = SendMessage(iHWnd, WM_COPYDATA, 0, ref cdsBuffer);
                    if (iReturn < 0)
                    {
#if DEBUG
                        string strError = string.Format("SendMessageToTargetWindow: 没有发现{0}窗体!", wndName);
                        MessageBox.Show(strError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Debug.WriteLine(strError);
#endif
                        return false;
                    }
                    return true;
                }
            }

        /// <summary>  
        /// 向目标窗口发送消息
        /// </summary>  
        /// <param name="wndName">查找目标窗体名称</param>  
        /// <param name="wParam">整型数据</param>  
        /// <param name="lParam">整型数据</param>  
        /// <returns>数据发送成或者失败</returns>  
        public static bool SendMessageToTargetWindow(string wndName, int wParam, int lParam)
            {
                int iHWnd = FindWindow(null, wndName);
                if (iHWnd == 0)
                {
#if DEBUG
                    string strError = string.Format("SendMessageToTargetWindow: The target window [{0}] was not found!", wndName);
                    MessageBox.Show(strError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.WriteLine(strError);
#endif
                    return false;
                }
                else
                {
                    //使用定义的消息wmdatatransfer来发送消息。（测试传int数据正常接收，传string数据接收有问题）
                    int iReturn = SendMessage(iHWnd, WM_DATA_TRANSFER, wParam, lParam);
                    if (iReturn < 0)
                    {
#if DEBUG
                        string strError = string.Format("SendMessageToTargetWindow: Send message to the target window [{0}] failed!", wndName);
                        MessageBox.Show(strError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Debug.WriteLine(strError);
#endif
                        return false;
                    }
                    return true;
                }
            }
        }
    }
