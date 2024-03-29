using LXCSystem.Frm;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VSE.Core;
using System.Threading;
using MvCamCtrl.NET;

namespace VSE
{
    internal class Machine
    {

        /// <summary>
        /// 表示程序启动时是否初始化成功
        /// </summary>
        internal static bool initSucceed = true;
        /// <summary>
        /// 是否处于生产模式，分前生产模式和调试模式
        /// </summary>
        internal static bool productionMode = true;
        /// <summary>
        /// 记录旧的时间
        /// </summary>
        internal static DateTime lastTime = DateTime.Now;
        /// <summary>
        /// 当前窗体模式
        /// </summary>
        internal static FormMode curFormMode = FormMode.MainForm;
        /// <summary>
        /// 总生产时间
        /// </summary>
        internal static TimeSpan runTime;
        /// <summary>
        /// 总待机时间
        /// </summary>
      //  internal static TimeSpan waitTime;
        /// <summary>
        /// 总报警时间
        /// </summary>
        //internal static TimeSpan alarmTime;
        /// <summary>
        /// 资源锁
        /// </summary>
        internal static object lock_resources = new object();
        /// <summary>
        /// 是否正在启动
        /// </summary>
        internal static bool loading = true;
        /// <summary>
        /// 资源锁
        /// </summary>
        private static object obj = new object();
        /// <summary>
        /// 程序即将退出
        /// </summary>
        internal static bool willExit = false;
        /// <summary>
        /// 设备运行状态
        /// </summary>
        internal static MachineRunStatu machineRunStatu = MachineRunStatu.WaitReset;

        [DllImport("Kernel32.DLL ", SetLastError = true)]
        public static extern bool SetEnvironmentVariable(string lpName, string lpValue);
        static int OldValue = 0;
        //更新进度
        internal static void UpdateStep(int percentValue, string stepMsg, bool succeed)
        {
            try
            {
                Start.Win_Welcome.UpdateProgress(stepMsg+"...", OldValue, percentValue, 500);
                OldValue = percentValue;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        internal static void InitAll()
        {
            try
            {
                var t = Win_About.Instance;
              
                Parameter.Status = "初始化中...";
                Parameter.StatusNum = 0;
                Start.Win_Welcome.Show();
                
                System.Windows.Forms.Application.DoEvents();
                UpdateStep(2,"初始化", true);
                Project.Instance.configuration.Read();
                Win_Main.Instance.XXXXXXXXXXXXXlbl_title.Text = Project.Instance.configuration.ProgramTitle;
             
                Application.DoEvents();
                Win_Main.Instance.Opacity = 0;
                //初始化配置文件
                UpdateStep(8,"初始化配置文件", true);
                InitConfigDirctory();
                //通讯连接
                UpdateStep(16,"尝试通讯连接", true);
                if (Project.Instance.configuration.autoConnectAfterStart)
                {
                    //if (Project.Instance.configuration.communicationType == CommunicationType.Internet_Client)
                    //    ;//////Win_TCPClient.Instance.Connect();
                    //else if (Project.Instance.configuration.communicationType == CommunicationType.Internet_Sever)
                    //{ }
                    //////Win_TCPServer.Instance.Listen();
                    //////else if (Project.Instance.configuration.communicationType == CommunicationType.SerialPort)
                    //////    Win_SerialPort.Instance.btn_openPort_Click(null, null);
                }

                //枚举网络中的相机 
                UpdateStep(20,"枚举网络中的相机", true);

                SDK_Halcon.EnumCamera();       //枚举网络中的所有的相机
                try
                {
                    Win_SDKInfo.LoadState[0] = SDK_Basler.EnumCamrea();
                }
                catch { }
                try
                {
                    if (SDK_HIKVision.EnumCamrea())
                    {
                        if(SDK_HIKVision.deviceList.Count>0)
                        {
                            for (int i = 0; i < SDK_HIKVision.deviceList.Count; i++)
                            {
                                MyCamera myCamera;
                                DeviceInfo deviceInfo = SDK_HIKVision.deviceList[i];
                                SDK_HIKVision instance = new SDK_HIKVision(deviceInfo.cameraInfoStr);
                                instance.OpenCamera(deviceInfo, out myCamera);
                                SDK_HIKVision.D_cameras.Add(deviceInfo.cameraInfoStr, myCamera);
                                AcqImageTool.L_devices.Add(instance);
                                //AcqImageTool.L_devices[i].SetExposure(AcqImageTool.L_devicesExposure[deviceInfo.cameraInfoStr]);
                            }
                        }
                    }
                    else
                    { 
                       //Failed
                    }
                }
                catch { }
               
                try
                {
                    Win_SDKInfo.LoadState[5] = SDK_PointGrey.EnumCamera();
                }
                catch { }

                //加载标准图像
                UpdateStep(70,"加载标准图像", true);

                //显示生产界面
                UpdateStep(78,"初始化窗体", true);
                //if (Project.Instance.configuration.showProductionFormAfterStart)
                //    Machine.SwitchToProductForm();

                UpdateStep(80,"检查注册状态", true);
                Win_Main.Instance.regiestCode = Regiest.Get_RNum(Regiest.Get_MNum());

                if (Project.Instance.configuration.maxSizeAfterStart)
                    Win_Main.Instance.WindowState = FormWindowState.Maximized;

                //初始化各设备
                for (int i = 0; i < Project.Instance.L_lightController.Count; i++)
                {
                    Project.Instance.L_lightController[i].OpenController();
                }
                //加载流程
                UpdateStep(85,"加载流程文件", true);
                string[] files = Directory.GetFiles(Application.StartupPath + "\\Config\\Project\\Vision\\", "*.pjt");
                if (files.Length > 0)
                    Project.LoadProject(files[0]);

                //加载设备
                #region 禁用
                //UpdateStep(90,"正在尝试通讯连接", true);

                //for (int i = 0; i < Project.Instance.L_TCPClient.Count; i++)
                //{
                //    TCPClient tcpClient = new TCPClient(Project.Instance.L_TCPClient[i].Name);


                //    int idx = Win_DeviceManager.Instance.dgv_deviceList.Rows.Add();
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Tag = "TCPClient";
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Height = 30;
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Cells[1].Value = Resources.客户端;
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Cells[0].Value = Project.Instance.L_TCPClient[i].Name;
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Cells[2].Value = Resources.客户端;

                //    if (Project.Instance.L_TCPClient[i].AutoConnectAfterStart)
                //        Project.Instance.L_TCPClient[i].Connect();

                //    //状态显示图标
                //    ToolStripItem tsb = new ToolStripStatusLabel("", Resources.客户端);
                //    tsb.AutoSize = false;
                //    tsb.Width = 20;
                //    tsb.Name = Project.Instance.L_TCPClient[i].Name;
                //    //tsb.BorderSides = ToolStripStatusLabelBorderSides.Right;
                //    tsb.ToolTipText = string.Format("名称：{0}\r\n状态：{1}\r\nIP    : {2}\r\nPort : {3}", Project.Instance.L_TCPClient[i].Name, Project.Instance.L_TCPClient[i].FindSocketByName().Connected ? "已连接" : "未连接", Project.Instance.L_TCPClient[i].severIP, Project.Instance.L_TCPClient[i].severPort);
                //    Win_Main.Instance.statusStrip1.Items.Insert(1, tsb);

                //    tsb.Tag = Project.Instance.L_TCPClient[i];
                //    tsb.MouseEnter += tsb_MouseEnter;

                //}
                //for (int i = 0; i < Project.Instance.L_TCPSever.Count; i++)
                //{
                //    TCPSever tcpSever = new TCPSever(Project.Instance.L_TCPSever[i].Name);


                //    int idx = Win_DeviceManager.Instance.dgv_deviceList.Rows.Add();
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Tag = "TCPSever";
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Height = 30;
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Cells[1].Value = Resources.服务器;
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Cells[0].Value = Project.Instance.L_TCPSever[i].Name;
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Cells[2].Value = Resources.客户端;

                //    if (Project.Instance.L_TCPSever[i].AutoListenAfterStart)
                //        Project.Instance.L_TCPSever[i].Listen();
                //}

                //for (int i = 0; i < Project.Instance.L_lightController.Count; i++)
                //{
                //    int idx = Win_DeviceManager.Instance.dgv_deviceList.Rows.Add();
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Tag = "LightController";
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Height = 30;
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Cells[1].Value = Resources.光照度_允乐;
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Cells[0].Value = Project.Instance.L_lightController[i].Name;
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Cells[2].Value = Resources.客户端;

                //    if (Project.Instance.L_lightController[i].OpenAllChAfterStart)
                //        Project.Instance.L_lightController[i].OpenAllChannel();
                //}

                //for (int i = 0; i < Project.Instance.L_Scaner.Count; i++)
                //{
                //    Scaner scaner = new Scaner(Project.Instance.L_Scaner[i].Name);

                //    int idx = Win_DeviceManager.Instance.dgv_deviceList.Rows.Add();
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Tag = "Scaner";
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Height = 30;
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Cells[1].Value = Resources.光照度_允乐;
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Cells[0].Value = Project.Instance.L_Scaner[i].Name;
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Cells[2].Value = Resources.客户端;

                //    Project.Instance.L_Scaner[i].Init();
                //}

                //for (int i = 0; i < Project.Instance.L_Serial.Count; i++)
                //{
                //    Serial serial = new Serial(Project.Instance.L_Serial[i].Name);

                //    int idx = Win_DeviceManager.Instance.dgv_deviceList.Rows.Add();
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Tag = "Serial";
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Height = 30;
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Cells[1].Value = Resources.光照度_允乐;
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Cells[0].Value = Project.Instance.L_Serial[i].Name;
                //    Win_DeviceManager.Instance.dgv_deviceList.Rows[idx].Cells[2].Value = Resources.客户端;

                //    Project.Instance.L_Serial[i].Init();
                //}
                #endregion
                //添加最近打开过的文件列表
                int count = Project.Instance.configuration.L_recentlyOpendFile.Count;
                for (int i = 0; i < 5 - count; i++)
                {
                    Project.Instance.configuration.L_recentlyOpendFile.Add(string.Empty);
                }
                for (int i = 0; i < Project.Instance.configuration.L_recentlyOpendFile.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (Project.Instance.configuration.L_recentlyOpendFile[0] != string.Empty)
                            {
                                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem((i + 1) + ". " + Path.GetFileName(Project.Instance.configuration.L_recentlyOpendFile[i]))
                                {
                                    Tag = Project.Instance.configuration.L_recentlyOpendFile[i]
                                };
                                toolStripMenuItem.Click += toolStripMenuItem_Click;
                                toolStripMenuItem.BackColor = Color.White;
                                Win_Main.Instance.近期方案ToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
                            }
                            break;
                    }
                }

                UpdateStep(100,"启动成功", true);

                Application.DoEvents();
                if (Project.Instance.configuration.enablePermissionControl)
                    Win_Main.Instance.tss_permissionInfo.Text = "当前用户：未登录";
                else
                    Win_Main.Instance.tss_permissionInfo.Text = "当前用户：未登录";
                if (!Project.Instance.configuration.allowResizeForm)
                {
                    Win_Main.Instance.MinimumSize = Win_Main.Instance.Size;
                    Win_Main.Instance.MaximumSize = Win_Main.Instance.Size;
                }
                Log.SaveLog(Project.Instance.configuration.dataPath, LogType.Operate,"程序启动");
                if (Machine.initSucceed)
                {
                    Win_Main.Instance.OutputMsg("启动成功",Win_Log.InfoType.tip);
                }
                else
                {
                    Win_Main.Instance.OutputMsg("启动出错",Win_Log.InfoType.error);
                  
                }

                loading = false;
            }
            catch (Exception ex)
            {
                loading = false;
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        static void tsb_MouseEnter(object sender, EventArgs e)
        {
            TCPClient tcpClient = (TCPClient)(((ToolStripItem)sender).Tag);

            Win_Main.Instance.toolTip1.RemoveAll();
            Win_Main.Instance.toolTip1.ToolTipTitle = tcpClient.Name;
            string info = string.Format("状态 ：{0}\r\nIP    : {1}\r\nPort : {2}", tcpClient.FindSocketByName().Connected ? "已连接" : "未连接", tcpClient.severIP, tcpClient.severPort);
            Win_Main.Instance.toolTip1.Show(info, Win_Main.Instance.statusStrip1);
        }

        static void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.LoadProject(((ToolStripMenuItem)sender).Tag.ToString());
        }

        /// <summary>
        /// 开始自动运行
        /// </summary>
        internal static void StartRun()
        {
            try
            {
                if (machineRunStatu == MachineRunStatu.Homing)
                {
                    Win_Main.Instance.OutputMsg("复位中，请复位完成后开始",Win_Log.InfoType.error);
                    return;
                }
                else if (machineRunStatu == MachineRunStatu.WaitReset)
                {
                    Win_Main.Instance.OutputMsg("程序未复位，请复位成后开始",Win_Log.InfoType.error);
                    return;
                }
                else
                {
                    Win_Main.Instance.OutputMsg("开始运行",Win_Log.InfoType.tip);
                    Log.SaveLog(Project.Instance.configuration.dataPath, LogType.Operate, "开始运行");
                    machineRunStatu = MachineRunStatu.Running;
                    Win_Main.Instance.StartPre(true);

                    for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
                    {
                        if (Project.Instance.curEngine.L_jobList[i].jobRunMode == JobRunMode.LoopRunAfterStart)

                            Project.Instance.curEngine.L_jobList[i].LoopRun(true);
                    }

                    Win_Job.Instance.btn_runLoop.Enabled = false;
                    Win_Job.Instance.btn_runOnce.Enabled = false;
                   
                    //Win_UserForm.Instance.Start();
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        internal static void SwitchFrom(FormMode formMode)
        {
         
            if (Machine.curFormMode != formMode)
            {
                if (formMode== FormMode.MainForm)
                {
                    Machine.curFormMode = FormMode.MainForm;
                 
                    Win_Main.Instance.panel4.Visible = false;
                    Win_Main.Instance.dockPanel.Dock = DockStyle.None;
                    Win_Main.Instance.dockPanel.Height = 0;
                    Win_Main.Instance.panel2.Dock = DockStyle.Fill;
                    Win_Main.Instance.menuStrip1.Dock = DockStyle.None;
                    Win_Main.Instance.menuStrip1.Height = 0;
                    Win_Main.Instance.toolStrip1.Location = new Point(68, -3);
                }
                else
                {
                    Machine.curFormMode = FormMode.VisionForm;

                    Win_Main.Instance.panel4.Visible = false;
                    Win_Main.Instance.menuStrip1.Height = 40;


                    Win_Main.Instance.panel2.Dock = DockStyle.None;
                    Win_Main.Instance.panel2.Height = 0;
                    Win_Main.Instance.dockPanel.Dock = DockStyle.Fill;
                    Win_Main.Instance.toolStrip1.Location = new Point(433, -3);
                }


            }
         
        }
        /// <summary>
        /// 停止自动运行
        /// </summary>
        internal static void StopRun()
        {
            try
            {
                for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
                {
                    Project.Instance.curEngine.L_jobList[i].LoopRun(false);
                }
                if (machineRunStatu == MachineRunStatu.Running)
                {

                    Win_Main.Instance.OutputMsg("停止运行",Win_Log.InfoType.tip);
                    Log.SaveLog(Project.Instance.configuration.dataPath, LogType.Operate, "停止自动运行");
                    machineRunStatu = MachineRunStatu.Stop;

                    //Win_UserForm.Instance.Stop();
                    
                    Win_Job.Instance.btn_runLoop.Enabled = true ;
                    Win_Job.Instance.btn_runOnce.Enabled = true;
                    Win_Main.Instance.StartPre(false);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 检查各配置文件
        /// </summary>
        private static void InitConfigDirctory()
        {
            try
            {
                //主配置文件文件夹
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Config\\Project"))
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Config\\Project");

                //标准图像文件夹
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Config\\Project\\Vision\\StandardImage"))
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Config\\Project\\Vision\\StandardImage");

                //运动控制文件夹
                if (!Directory.Exists(Application.StartupPath + "\\Config\\Project\\Motion"))
                    Directory.CreateDirectory(Application.StartupPath + "\\Config\\Project\\Motion");

                //作业文件夹
                if (!Directory.Exists(Application.StartupPath + "\\Config\\Project\\Vision\\Job"))
                    Directory.CreateDirectory(Application.StartupPath + "\\Config\\Project\\Vision\\Job");

                //资源文件夹
                if (!Directory.Exists(Application.StartupPath + "\\Config\\Resources"))
                    Directory.CreateDirectory(Application.StartupPath + "\\Config\\Resources");

                //通讯记录文件夹
                if (!Directory.Exists(Application.StartupPath + "\\Config\\Log\\Comm"))
                    Directory.CreateDirectory(Application.StartupPath + "\\Config\\Log\\Comm");

                //操作记录文件夹
                if (!Directory.Exists(Application.StartupPath + "\\Config\\Log\\Comm"))
                    Directory.CreateDirectory(Application.StartupPath + "\\Config\\Log\\Operate");

                //错误信息保存文件夹
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Config\\Log\\Error"))
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Config\\Log\\Error");

                //主配置文件ini
                if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\Config\\Config.ini"))
                    File.Create(System.Windows.Forms.Application.StartupPath + "\\Config\\Config.ini").Close();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

      
        /// <summary>
        /// 定期清理
        /// </summary>
        private void RegularClear(int dayNum)
        {
            try
            {
                UpdateStep(5,"程序备份", true);
                if (Project.Instance.configuration.autoBackupProgram)
                {
                    try
                    {
                        if (Application.StartupPath.Substring(0, 6) == @"C:\VSE")       //我们不允许在备份的文件夹下启动
                        {
                            MessageBox.Show("Do not run this backup program directly, please copy to another path and run again");
                            Process.GetCurrentProcess().Kill();
                        }
                        string date = DateTime.Now.ToString("yyyy-MM-dd");
                        if (Directory.Exists(@"C:\VSE\" + date))
                        {
                            Directory.Delete(@"C:\VSE\" + date, true);
                            Directory.CreateDirectory(@"C:\VSE\" + date);
                        }
                        Win_Main.CopyFiles(Application.StartupPath, @"C:\VSE\" + date);

                        //清理30天以前的备份
                        DateTime now = DateTime.Now;
                        string[] fileList = Directory.GetDirectories(@"C:\VSE");
                        for (int i = 0; i < fileList.Length; i++)
                        {
                            DirectoryInfo dir = new System.IO.DirectoryInfo(fileList[i]);
                            DateTime dt = dir.CreationTime;
                            if ((now - dt).Days > 30)
                            {
                                File.Delete(fileList[i]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.SaveError(Project.Instance.configuration.dataPath,ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

    }
    /// <summary>
    /// 设备运行状态
    /// </summary>
    internal enum MachineRunStatu
    {
        WaitReset,
        Stop,
        Pause,
        Homing,
        WaitRun,
        Running,
        Alarm,
    }
    public enum FormMode
    {
        None,
        MainForm,
        VisionForm,
        MotionForm,
    }
}
