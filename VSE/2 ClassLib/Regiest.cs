using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows;
using VSE.Core;

namespace VSE
{
    /// <summary>
    /// 注册类
    /// </summary>
    internal class Regiest
    {

        /// <summary>
        /// 存储密钥
        /// </summary>
        private static int[] intCode = new int[127];
        /// <summary>
        /// 存机器码的Ascii值
        /// </summary>
        private static int[] intNumber = new int[25];
        /// <summary>
        /// 存储机器码字
        /// </summary>
        private static char[] Charcode = new char[25];


        /// <summary>
        /// 获取硬盘序列号
        /// </summary>
        /// <returns></returns>
        private static string GetDiskVolumeSerialNumber()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
                disk.Get();
                return disk.GetPropertyValue("VolumeSerialNumber").ToString();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return string.Empty;
            }
        }
        /// <summary>
        /// 获取CPU序列哈
        /// </summary>
        /// <returns></returns>
        private static string GetCpu()
        {
            try
            {
                string strCpu = null;
                ManagementClass myCpu = new ManagementClass("win32_Processor");
                ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
                foreach (ManagementObject myObject in myCpuConnection)
                {
                    strCpu = myObject.Properties["Processorid"].Value.ToString();
                    break;
                }
                return strCpu;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return string.Empty;
            }
        }
        /// <summary>
        /// 给数组赋值小于10的数
        /// </summary>
        private static void SetIntCode()
        {
            for (int i = 1; i < intCode.Length; i++)
            {
                intCode[i] = i % 9;
            }
        }
        /// <summary>
        /// 获取本机序列号
        /// </summary>
        /// <returns></returns>
        internal static string Get_MNum()
        {
            try
            {
                string strNum = GetCpu() + GetDiskVolumeSerialNumber();
                string strMNum = strNum.Substring(0, 24);
                return strMNum;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return string.Empty;
            }
        }
        /// <summary>
        /// 根据机器码生成注册码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        internal static string Get_RNum(string machionCode)
        {
            try
            {
                SetIntCode();
                string MNum = machionCode;
                for (int i = 1; i < Charcode.Length; i++)
                {
                    Charcode[i] = Convert.ToChar(MNum.Substring(i - 1, 1));
                }
                for (int j = 1; j < intNumber.Length; j++)
                {
                    intNumber[j] = intCode[Convert.ToInt32(Charcode[j])] + Convert.ToInt32(Charcode[j]);
                }
                string strAsciiName = "";
                for (int j = 1; j < intNumber.Length; j++)
                {
                    if (intNumber[j] >= 48 && intNumber[j] <= 57)
                    {
                        strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                    }
                    else if (intNumber[j] >= 65 && intNumber[j] <= 90)
                    {
                        strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                    }
                    else if (intNumber[j] >= 97 && intNumber[j] <= 122)
                    {
                        strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                    }
                    else
                    {
                        if (intNumber[j] > 122)
                        {
                            strAsciiName += Convert.ToChar(intNumber[j] - 10).ToString();
                        }
                        else
                        {
                            strAsciiName += Convert.ToChar(intNumber[j] - 9).ToString();
                        }
                    }
                }
                return strAsciiName;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return null;
            }
        }

    }
}
