using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Start
{
    internal class IOCardHelper_SY
    {
        #region API函数声明
        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key,
          string val, string filePath);
        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key,
          string def, StringBuilder retVal, int size, string filePath);
        #endregion

        int boardType = 16;            //板卡类型 IO模块：0X10
        long[] hESMTP = new long[32];  //模块句柄
        string allIP;                  //自动搜索模块函数的结果
        string[] ip;                   //每个模块的IP
        uint[] DI_Data = new uint[32]; //每个模块的DI信号
        uint[] DO_Data = new uint[32]; //每个模块的DO信号
        int index;                     //模块序号
        bool bStartTest = false;       //启动DO测试
        Thread TestThread;

        /// <summary>
        /// 查找IO卡
        /// </summary>
        private bool SearchCards()
        {
            int CardNO = 0;
            ip = null;
            bool bRet = false;
            IntPtr a = ESMTPHeader.SearchDevice_1(ref CardNO);
            if (CardNO > 0)
            {
                allIP = Marshal.PtrToStringAnsi(a);
                ip = allIP.Split(',');
                for (int i = 0; i < CardNO; i++)
                {
                    if (ip[i] != "")
                    {
                        //comboBox1.Items.Add(ip[i]);
                    }
                }
                //comboBox1.SelectedIndex = 0;
                bRet = true;    
            }
            else
            {
                //MessageBox.Show("没有搜索到模块！");
                bRet = false;
            }
            return bRet;    
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        private bool ConnectIOCard()
        {
            bool bRet = false;  
            IntPtr iptemp = Marshal.StringToHGlobalAnsi(ip[index]);
            hESMTP[index] = ESMTPHeader.Connect(boardType, iptemp);
            if (hESMTP[index] < 0)
            {
                bRet = false;//连接失败
                //InitialCheckBox();
            }
            else
            {
                bRet = true;//连接成功
                //setDO(index);
            }
            return bRet;
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        private void DisconnectIOCard()
        {
            ushort rc;
            rc = ESMTPHeader.Disconnect(boardType, hESMTP[index]);
            if (rc == 0)
            {
                hESMTP[index] = 0;
               
                MessageBox.Show(ip[index].ToString() + "\n连接已断开！", "提示");
              
            }
            else
            {
              
                MessageBox.Show(ip[index].ToString() + "\n断开失败！", "提示");
            }
        }

    }
}
