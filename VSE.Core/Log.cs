using System;
using System.Diagnostics;
using System.IO;


namespace VSE.Core
{
    /// <summary>
    /// Log记录辅助类
    /// </summary>
    public class Log
    {
        
        /// <summary>
        /// 通讯文件锁，防止文件冲突
        /// </summary>
        private static object objComm = new object();
        /// <summary>
        /// 异常文件锁，防止文件冲突
        /// </summary>
        private static object objError = new object();
        /// <summary>
        /// 数据信息锁，防止文件冲突
        /// </summary>
        private static object objData = new object();
        /// <summary>
        /// 操作信息锁，防止文件冲突
        /// </summary>
        private static object objOperate = new object();

        /// <summary>
        /// Log保存函数
        /// </summary>
        /// <param name="logType">Log信息类型</param>
        /// <param name="message">信息内容</param>
        public static void SaveLog(string path,LogType logType, string message)
        {
            try
            {
                switch (logType)
                {
                    case LogType.Comm:
                        lock (objComm)
                        {
                            DateTime now = DateTime.Now;
                            string filePath = string.Format("{0}Log\\Comm\\{1}\\", path , now.ToString("yyyyMMdd"));
                            string fileName = now.ToString("HH时") + ".txt";         //每小时创建一个txt，防止通讯数据交换频率高，数据量大，文本文件过大
                            if (!Directory.Exists(filePath))
                                Directory.CreateDirectory(filePath);
                            if (!File.Exists(filePath + fileName))
                                File.Create(filePath + fileName).Close();
                            File.AppendAllText(filePath + fileName, DateTime.Now.ToString("yyyy/MM/dd mm:HH:ss    ") + message + Environment.NewLine);
                        }
                        break;
                    case LogType.Operate:
                        lock (objOperate)
                        {
                            DateTime now1 = DateTime.Now;
                            string filePath1 = string.Format("{0}\\Log\\Operate\\", path);
                            string fileName1 = now1.ToString("yy_MM_dd") + ".txt";
                            if (!Directory.Exists(filePath1))
                                Directory.CreateDirectory(filePath1);
                            if (!File.Exists(filePath1 + fileName1))
                                File.Create(filePath1 + fileName1).Close();
                            File.AppendAllText(filePath1 + fileName1, DateTime.Now.ToString("yyyy/MM/dd mm:HH:ss    ") + message + Environment.NewLine);

                        }
                        break;
                    case LogType.Data:
                        lock (objData)
                        {
                            //此处待添加
                        }
                        break;
                }
            }
            catch (Exception)
            {
                //////System.Windows.Forms.MessageBox.Show("存储日志文件异常\r\n" + es.ToString(), "提示：");
            }
        }
        /// <summary>
        /// Log保存函数
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void SaveError(string BasePath,Exception ex)
        {
            try
            {
                lock (objError)
                {
                    StackFrame tmpSF = new StackTrace(new StackFrame(true)).GetFrame(0);
                    string rowNo = "";
                    if (ex.ToString().Contains("行号") || ex.ToString().Contains("line"))
                    {
                        if (ex.ToString().Contains("行号"))
                        {
                            rowNo = ex.ToString().Split(new string[] { "行号" }, StringSplitOptions.RemoveEmptyEntries)[1];
                        }
                        else
                        {
                            rowNo = ex.ToString().Split(new string[] { "line" }, StringSplitOptions.RemoveEmptyEntries)[1];
                        }
                    }
                    else
                    {
                        rowNo = "未知";
                    }
                    string data = "----------     " + DateTime.Now.ToString() + "     ----------" + Environment.NewLine +
                                  "出错文件：" + tmpSF.GetFileName() + Environment.NewLine +
                                  "出错函数：" + tmpSF.GetMethod().Name + Environment.NewLine +
                                  "出错行号：" + rowNo + Environment.NewLine +
                                  "出错列号：" + tmpSF.GetFileColumnNumber() + Environment.NewLine +
                                  "出错信息：" + ex.Message.ToString() + Environment.NewLine;
                    data += Environment.NewLine;
                    string ErrorPath = BasePath+ "\\Config\\Log\\Error";
                    if (!Directory.Exists(ErrorPath))
                    {
                        Directory.CreateDirectory(ErrorPath);
                    }
                    string path = ErrorPath + "\\" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".txt";
                    if (!File.Exists(path))
                    {
                        File.Create(path).Close();
                    }
                    File.AppendAllText(path, data);
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "提示：");
                }
            }
            catch (Exception )
            {
                //////System.Windows.Forms.MessageBox.Show("存储日志文件异常\r\n" + es.ToString(), "提示：");
            }
        }

    }
    /// <summary>
    /// Log信息类型：通讯信息|异常信息|生产数据
    /// </summary>
    public enum LogType
    {
        Comm,
        Error,
        Data,
        Operate,
    }
}
