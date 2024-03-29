using System;
using System.Windows.Forms;
using VSE;
using VSE.Core;
using VControls;

namespace Start
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //此处首先读取一次配置，因为程序启动时就需要知道当前语言选择，用于下面的提示信息的语言类型
                //Ini ini = new Ini(Application.StartupPath + @"\Config\Config.ini");
                //string language = ini.IniReadConfig("Language");
                

                System.Threading.Mutex mutex = new System.Threading.Mutex(false, "ThisShouldOnlyRunOnce");
                bool running = true;
              
                    running = !mutex.WaitOne(0, false);           
               
                Form1 RunForm = new Form1();    
                if (running)
                {
                    Win_ConfirmBox.Instance.lbl_info.Text = "      已经运行了一个实例（或旧实例尚未完全关闭），是否再开启\r\n一个实例？";
                    Win_ConfirmBox.Instance.ShowDialog();
                    if (Win_ConfirmBox.Instance.Result == ConfirmBoxResult.Yes)
                    {
                        VSE.Start.Init(RunForm);
                    }
                }
                else
                {
                    VSE.Start.Init(RunForm);
                }
            }
            catch(Exception ex)
            {
                Win_MessageBox.Instance.MessageBoxShow("启动失败（错误代码：00001）", TipType.Error);
            }
        }

    }
}
