using HalconDotNet;
using LXCSystem.Files;
using LXCSystem.Frm;
using Microsoft.Win32;
using ShareMemNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using VControls;
using VSE.Core;
using VSE.Properties;
using WinFormsUI.Docking;


namespace VSE
{
    internal partial class Win_Main : Form
    {
        internal Win_Main()
        {
            deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            //初始化窗体控件
            try
            {
                InitializeComponent();
                dockPanel.DefaultFloatWindowSize = new System.Drawing.Size(180, 500);
                LoadTheme(this.DarkTheme1);
                Control.CheckForIllegalCrossThreadCalls = false;
               
                VER.Text = System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToString("VER yyyy MMdd-HHmm");

            }
            catch
            {
                Win_MessageBox.Instance.MessageBoxShow("启动失败，Halcon已过期或者系统平台不正确");
                Process.GetCurrentProcess().Kill();
            }
            窗体缩放label.Text= Method.GetDay();
        }
        private void LoadTheme(ThemeBase tb)
        {
            this.dockPanel.Theme = tb;
            this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2015, tb);
        }

        private void EnableVSRenderer(VisualStudioToolStripExtender.VsVersion version, ThemeBase theme)
        {
            ToolStripExtender1.SetStyle(menuStrip1, version, theme);
           // ToolStripExtender1.SetStyle(toolStrip1, version, theme);
            ToolStripExtender1.SetStyle(statusStrip1, version, theme);
            ToolStripExtender1.SetStyle(Win_Job.Instance.toolStrip1, version, theme);
            ToolStripExtender1.SetStyle(Win_Log.Instance.toolStrip1, version, theme);
            ToolStripExtender1.SetStyle(Win_Log.Instance.contextMenuStrip1, version, theme);
            ToolStripExtender1.SetStyle(Win_MorphologyTool.Instance.toolStrip2, version, theme);
            ToolStripExtender1.SetStyle(Win_MorphologyTool.Instance.contextMenuStrip1, version, theme);
            ToolStripExtender1.SetStyle(Job.rightClickMenuAtBlank, version, theme);
            ToolStripExtender1.SetStyle(Job.rightClickMenu, version, theme); 
      
        }

        #region 变量定义

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_Main _instance;
        public static Win_Main Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_Main();
                return _instance;
            }
        }

        /// <summary>
        /// 指示是否允许拖动和缩放
        /// </summary>
        public static bool allowScaleAndZoom = false;


        /// <summary>
        /// 反序列化Dock控件对象
        /// </summary>
        internal DeserializeDockContent deserializeDockContent;

        /// <summary>
        /// 注册码
        /// </summary>
        public string regiestCode;
        /// <summary>
        /// 累计时间
        /// </summary>
        public int elapsedTime = 0;
        /// <summary>
        /// 虚拟键盘进程
        /// </summary>
        internal Process processKeyBoard;
        /// <summary>
        /// 计算器进程
        /// </summary>
        internal Process processCalculator;

        internal static bool locked = false;
        /// <summary>
        /// 保存项目标识位
        /// </summary>
        internal static bool openProject = false;
        #endregion

        #region 函数定义

        /// <summary>
        /// 整体保存
        /// </summary>
        internal void SaveAll()
        {
            try
            {
                //foreach (TabPage item in Win_Job.Instance.tbc_jobs.TabPages)
                //{
                //    //如果本地没有此流程，则可能是临时读取的流程，返回，不保存
                //    if (Win_Job.Instance.tbc_jobs.TabCount > 0)
                //        Save(item.Text);
                //}
                Project.SaveProject();
                //Project .Instance .configuration .Save();
                //////Win_Job.Instance.Dock = DockStyle.Fill;
                //////Win_Job.Instance.Show();
                //////Win_Job.Instance.Dock = DockStyle.Fill;
                //  Win_Main.Instance.OutputMsg("保存项目成功",Win_Log.InfoType.tip);
                dockPanel.SaveAsXml(Project.Instance.configuration.layoutFilePath);
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="persistString"></param>
        /// <returns></returns>
        private IDockContent GetContentFromPersistString(string persistString)
        {
            try
            {
                string[] parsedStrings = persistString.Split(new char[] { ';' });
                persistString = parsedStrings[0];
                if (persistString == typeof(Win_Job).ToString())
                    return Win_Job.Instance;
                else if (persistString == typeof(Win_ToolBox).ToString())
                    return Win_ToolBox.Instance;
                else if (persistString == typeof(Win_Log).ToString())
                    return Win_Log.Instance;
                else if (persistString == typeof(Win_Monitor).ToString())
                    return Win_Monitor.Instance;
                else
                {
                    if (parsedStrings[0] != typeof(Win_ImageWindow).ToString())
                        return null;
                    //Win_ImageWindow dummyDoc = new Win_ImageWindow();
                    if (Project.Instance.configuration.imageWindowName.Count == 0)
                    {
                        Project.Instance.configuration.imageWindowName.Add("图像");
                    }
                  
                    return Win_ImageWindow.Instance;
                }
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return null;
            }
        }

        /// <summary>
        /// 开机自启动
        /// </summary>
        /// <param name="isAuto">是否启用</param>
        internal static void Auto_Start(bool isAuto)
        {
            try
            {
                if (isAuto == true)
                {
                    RegistryKey R_local = Registry.LocalMachine;        //RegistryKey R_local = Registry.CurrentUser;
                    RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    R_run.SetValue("应用名称", Application.ExecutablePath);
                    R_run.Close();
                    R_local.Close();
                }

                else
                {
                    RegistryKey R_local = Registry.LocalMachine;        //RegistryKey R_local = Registry.CurrentUser;
                    RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    R_run.DeleteValue("应用名称", false);
                    R_run.Close();
                    R_local.Close();
                }
            }
            catch (Exception)
            {
                Win_MessageBox.Instance.MessageBoxShow("\r\n开机程序自启动设置失败，请以管理员权限运行此程序后重新尝试");
            }
        }
        private object obj = new object();
        public delegate void ShowTestData();
        /// <summary>
        /// 信息输出
        /// </summary>
        /// <param name="msg">要输出的信息</param>
        /// <param name="color">背景颜色</param>
        public void OutputMsg(string msg,Win_Log.InfoType type)
        {
            try
            {
                ShowTestData showTestData = delegate()
                {
                    Win_Log.Instance.OutputMsg(msg, type);
                    elapsedTime = 0;
                    //lbl_output.Text = DateTime.Now.ToString("HH:mm:ss") + "    " + msg;
                    //if (color == Color.Red)
                    //{
                    //    lbl_output.ForeColor = color;
                    //}
                    //else
                    //{
                    //    lbl_output.ForeColor = color;
                    //}
                };
                statusStrip1.Invoke(showTestData);
                Application.DoEvents();
                VSE.Core.Log.SaveLog(Project.Instance.configuration.dataPath,LogType.Operate, msg);
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }


        /// <summary>
        /// 整体保存
        /// </summary>
        internal static string Save()
        {
            try
            {
                if (Win_Job.Instance.listView1.SelectedNode==null)
                {
                    return "";
                }

                string jobName = Win_Job.Instance.JobEdit_JobName.Text.Substring(5);
                Job job = Job.FindJobByName(jobName);

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(Application.StartupPath + "\\Config\\Project\\Vision\\Job\\" + job.jobName + ".job", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, job);
                stream.Close();

                //更新结果下拉框
                ////// GetImageWindowControl(Win_ToolBox.Instance.listView1.SelectedItems[0].Text).Update_Last_Run_Result_Image_List();
                VSE.Core.Log.SaveLog(Project.Instance.configuration.dataPath, LogType.Operate,"程序保存成功");
                Win_Main.Instance.OutputMsg("流程保存成功",Win_Log.InfoType.tip);
                return jobName;
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return "";
            }
        }

        /// <summary>
        /// 把一个目录下的文件拷贝的目标目录下
        /// </summary>
        /// <param name="sourceFolder">源目录</param>
        /// <param name="targerFolder">目标目录</param>
        /// <param name="removePrefix">移除文件名部分路径</param>
        internal static void CopyFiles(string sourceFolder, string targerFolder, string removePrefix = "")
        {
            try
            {
                if (string.IsNullOrEmpty(removePrefix))
                {
                    removePrefix = sourceFolder;
                }
                if (!Directory.Exists(targerFolder))
                {
                    Directory.CreateDirectory(targerFolder);
                }
                DirectoryInfo directory = new DirectoryInfo(sourceFolder);
                //获取目录下的文件
                FileInfo[] files = directory.GetFiles();
                foreach (FileInfo item in files)
                {
                    if (item.Name == "Thumbs.db")
                    {
                        continue;
                    }
                    string tempPath = item.FullName.Replace(removePrefix, string.Empty);
                    tempPath = targerFolder + tempPath;
                    FileInfo fileInfo = new FileInfo(tempPath);
                    if (!fileInfo.Directory.Exists)
                    {
                        fileInfo.Directory.Create();
                    }
                    File.Delete(tempPath);
                    item.CopyTo(tempPath, true);
                }
                //获取目录下的子目录
                DirectoryInfo[] directors = directory.GetDirectories();
                foreach (var item in directors)
                {
                    CopyFiles(item.FullName, targerFolder, removePrefix);
                }
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        public static void SetPermission()
        {
            switch (Permission.CurrentPermission)
            {
                case PermissionLevel.NoPermission:

                   Win_Main.Instance.系统ToolStripMenuItem.Visible = false;
                    break;
                case PermissionLevel.Operator:

                    Win_Main.Instance.系统ToolStripMenuItem.Visible= false;
                    break;
                case PermissionLevel.Admin:
                    Win_Main.Instance.系统ToolStripMenuItem.Visible = true;
                    break;
                case PermissionLevel.Developer:
                    Win_Main.Instance.系统ToolStripMenuItem.Visible = true;
                    break;
 
            }
           
        }
        #endregion

        #region 相关事件
        private void tss_permissionInfo_Click(object sender, EventArgs e)
        {
            Win_Login.Instance.ShowDialog();
        }
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.Opacity = 0;
                try
                {
                    SDK_HIKVision.CloseAllCamera();
                }
                catch { }
                try
                {
                    SDK_Basler.CloseAllCamera();
                }
                catch { }
                try
                {
                    SDK_PointGrey.CloseAllCamera();
                }
                catch { }
                 Win_CloseTip Win_closeTip = new Win_CloseTip();
                Win_closeTip.Show();
                try
                {
                    //ShareMemProH.PH_Exit();
                }
                catch { }

                //关闭设备
                for (int i = 0; i < Project.Instance.L_lightController.Count; i++)
                {

                    if (Project.Instance.L_lightController[i].CloseAllChBeforeClose)
                        Project.Instance.L_lightController[i].CloseAllChannel();
                }
                for (int i = 0; i < Project.Instance.L_TCPClient.Count; i++)
                {

                    if (Project.Instance.L_TCPClient[i].AutoDisconnectBeforeClose)
                        Project.Instance.L_TCPClient[i].Close();
                }
                for (int i = 0; i < Project.Instance.L_Scaner.Count; i++)
                {
                    Project.Instance.L_Scaner[i].Close();
                }

                //清除图像窗体里面的ROI
                ////// GetImageWindowControl(Win_ToolBox.Instance.listView1.SelectedItems[0].Text).hwc_imageWindow.viewWindow.resetWindowImage();
                ////// GetImageWindowControl(Win_ToolBox.Instance.listView1.SelectedItems[0].Text).hwc_imageWindow.ClearWindow();
                ////// GetImageWindowControl(Win_ToolBox.Instance.listView1.SelectedItems[0].Text).hwc_imageWindow.viewWindow._hWndControl.roiManager.reset();
                ////// GetImageWindowControl(Win_ToolBox.Instance.listView1.SelectedItems[0].Text).hwc_imageWindow.viewWindow._hWndControl.roiManager.ROIList.Clear();

                Application.DoEvents();
                this.ShowInTaskbar = false;
                //关闭已连接的Socket
                //////if (Win_TCPClient.Instance.socket != null && Win_TCPClient.Instance.socket.Connected)
                //////{
                //////    Win_TCPClient.Instance.socket.Disconnect(false);
                //////}
                //////if (Win_TCPServer.Instance.commSkt != null && Win_TCPServer.Instance.commSkt.Connected)
                //////{
                //////    Win_TCPServer.Instance.commSkt.Disconnect(false);
                //////}
                //保存配置信息
                //Project .Instance .configuration .Save();

                //要返回到作业编辑界面以下，否则会有一些修改过得参数不能保存


                //////if (Win_TCPClient.Instance.socket != null && Win_TCPClient.Instance.socket.Connected)
                //////{
                //////    Win_TCPClient.Instance.socket.Disconnect(false);
                //////    Win_TCPClient.Instance.socket.Close();
                //////}
                try
                {
                    //ImageAcqTool.Close_All_Camera();
                }
                catch { }
                VSE.Core.Log.SaveLog(Project.Instance.configuration.dataPath, LogType.Operate, "程序关闭\r\n");
                Win_closeTip.Close();
                Application.DoEvents();
                Process.GetCurrentProcess().Kill();
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void Win_Main_Shown(object sender, EventArgs e)
        {
            try
            {
                //hzy20220425,打开窗口前Job处理
                Job.SetCamerasInJobTriggerMode();
                //Job.SetSoftwareLoadMode();
                Start.isFirstLoadJob = true;
                Job.RunCurJob();
                foreach (var item in Start.UserImage)
                {
                    item.viewWindow.ClearWindow(false);
                }
               
         
                Job.ClearCamerasBufferInJob();
                ///////////////////////////////////
                Job.SetCameraExposure();
                Application.DoEvents();
               
                this.Opacity = 1; 
                Win_Log.Instance.ClearLog();
                Thread.Sleep(200);
                if (Project.Instance.configuration.autoRunAfterStart)
                {
                    启动toolStripButton_Click_1(null, null);
                }
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }

        }

        bool restart = false;

        private void Win_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (restart)
                {
                    e.Cancel = true;
                    return;
                }
                if (Project.Instance.configuration.saveWhenExit)
                {
                    Win_ConfirmBox.Instance.lbl_info.Text = ( "\r\n退出前是否需要保存项目？");
                    Win_ConfirmBox.Instance.ShowDialog();
                    if (Win_ConfirmBox.Instance.Result == ConfirmBoxResult.Yes)
                    {
                        Project.SaveProject();
                        // SaveAll();
                        Project.Instance.configuration.Save();

                        dockPanel.SaveAsXml(Project.Instance.configuration.layoutFilePath);
                    }
                    else if (Win_ConfirmBox.Instance.Result == ConfirmBoxResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    Win_ConfirmBox.Instance.lbl_info.Text = ( "\r\n确定要退出吗？");
                    Win_ConfirmBox.Instance.ShowDialog();
                    if (Win_ConfirmBox.Instance.Result != ConfirmBoxResult.Yes)
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                Machine.willExit = true;
                
                if (processKeyBoard != null && !processKeyBoard.HasExited)
                    processKeyBoard.Kill();
                if (processCalculator != null && !processCalculator.HasExited)
                    processCalculator.Kill();
                Project.Instance.configuration.mainFormWidth = this.Size.Width;
                Project.Instance.configuration.mainFormHeight = this.Size.Height;
                Win_Job.Instance.Hide();

            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        public static List<HWindowControl> d = new List<HWindowControl>();
        public void SetCompanyName()
        {
            toolStripStatusLabel2.Text = Project.Instance.configuration.CompanyName; 
        }
        public void SetCompanyAddress()
        {
            toolStripStatusLabel3.Text = Project.Instance.configuration.CompanyAddress+"--";
        }
        public void SetLogo()
        {
            pictureBox1.Image = Project.Instance.configuration.Logo ;
        }
        private void Win_Main_Load(object sender, EventArgs e)
        {
            try
            {
                toolStripStatusLabel2.Text = Project.Instance.configuration.CompanyName;
                Machine.curFormMode = FormMode.None;
                Machine.SwitchFrom(FormMode.MainForm);
                //switch (Project.Instance.configuration.defaultForm)
                //{
                //    case FormMode.MainForm:

                //        break;
                //    case FormMode.VisionForm:
                //        Machine.SwitchFrom(FormMode.VisionForm);
                //        break;
                //    case FormMode.MotionForm:
                //        Machine.SwitchFrom(FormMode.MotionForm);
                //        break;
                //}
                this.WindowState = FormWindowState.Maximized;
                try
                {
                    if (File.Exists(Application.StartupPath + "\\" + Project.Instance.configuration.layoutFilePath))
                        Win_Main.Instance.dockPanel.LoadFromXml(Application.StartupPath + "\\" + Project.Instance.configuration.layoutFilePath, Win_Main.Instance.deserializeDockContent);
                    else
                        Win_Main.Instance.dockPanel.LoadFromXml(Application.StartupPath + "\\Config\\Resources\\Layout\\" + "系统布局.config", Win_Main.Instance.deserializeDockContent);
                }
                catch (Exception) 
                {
                }


                //加载完成，相机取图片
                for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
                {
                    for (int j = 0; j < Project.Instance.curEngine.L_jobList[i].L_toolList.Count; j++)
                    {
                        if (Project.Instance.curEngine.L_jobList[i].L_toolList[j].toolType == ToolType.ImageAcq)
                        {
                            //Project.Instance.curEngine.L_jobList[i].L_toolList[j].tool.Run(true, false, string.Empty);
                        }
                    }
                }


                ToolTip toolTip = new ToolTip
                {
                    AutoPopDelay = 5000,
                    InitialDelay = 10,
                    ReshowDelay = 10,
                    ShowAlways = true
                };

                dockPanel.AllowEndUserDocking = !Project.Instance.configuration.lockLayout;
                panel2.Controls.Add(Start.Main);
                Start.Main.Show();

               

                //  if (Win_ImageWindow.Instance.DockState == DockState.Hidden)
                Win_ImageWindow.Instance.SetCaption();
                if (Project.Instance.configuration.imageWindowName.Count == 0)
                {
                    Win_ImageWindow.Instance.Show(dockPanel);
                }
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
      
        private static string imageSavePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
      

     

        #endregion

        #region  窗体缩放
        private const int WM_NCHITTEST = 0x0084; //鼠标在窗体客户区（除标题栏和边框以外的部分）时发送的信息
        const int HTLEFT = 10;  //左变
        const int HTRIGHT = 11;  //右边
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;  //左上
        const int HTTOPRIGHT = 14; //右上
        const int HTBOTTOM = 15;  //下
        const int HTBOTTOMLEFT = 0x10;  //左下
        const int HTBOTTOMRIGHT = 17;  //右下
        System.Drawing.Point vPoint = System.Drawing.Point.Empty;
        //自定义边框拉伸
        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
                switch (m.Msg)
                {
                    case WM_NCHITTEST:
                        vPoint = new System.Drawing.Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                        vPoint = PointToClient(vPoint);
                        if (vPoint.X <= 5)
                            if (vPoint.Y <= 5)
                                m.Result = (IntPtr)HTTOPLEFT;  //左上
                            else if (vPoint.Y >= this.ClientSize.Height - 5)
                                m.Result = (IntPtr)HTBOTTOMLEFT; //左下
                            else
                                m.Result = (IntPtr)HTLEFT;  //左边
                        else if (vPoint.X >= this.ClientSize.Width - 5)
                            if (vPoint.Y <= 5)
                                m.Result = (IntPtr)HTTOPRIGHT;  //右上
                            else if (vPoint.Y >= this.ClientSize.Height - 5)
                                m.Result = (IntPtr)HTBOTTOMRIGHT;  //右下
                            else
                                m.Result = (IntPtr)HTRIGHT;  //右
                        else if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOP;  //上
                        else if (vPoint.Y >= this.ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOM; //下

                        else
                        {
                            base.WndProc(ref m);//如果去掉这一行代码,窗体将失去MouseMove..等事件
                            System.Drawing.Point lpint = new System.Drawing.Point((int)m.LParam);//可以得到鼠标坐标,这样就可以决定怎么处理这个消息了,是移动窗体,还是缩放,以及向哪向的缩放

                            m.Result = (IntPtr)0x2;//托动HTCAPTION=2 <0x2>
                        }
                        break;
                }
            }
            catch { }
        }
        #endregion

        #region 窗体拖动
        private static bool IsDrag = false;
        private int enterX;
        private int enterY;
        private void setForm_MouseDown(object sender, MouseEventArgs e)
        {
            IsDrag = true;
            enterX = e.Location.X;
            enterY = e.Location.Y;
        }
        private void setForm_MouseUp(object sender, MouseEventArgs e)
        {
            IsDrag = false;
            enterX = 0;
            enterY = 0;
        }
        private void setForm_MouseLeave(object sender, EventArgs e)
        {
            IsDrag = false;
            enterX = 0;
            enterY = 0;
        }
        private void setForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDrag)
            {
                Left += e.Location.X - enterX;
                Top += e.Location.Y - enterY;
            }
        }
        #endregion

      
       
        private void time_recordTime_Tick(object sender, EventArgs e)
        {
            
            if (Machine.willExit)
                return;
            窗体缩放label.Text = Method.GetDay();
            if (Machine.machineRunStatu == MachineRunStatu.Running)
            {
                Machine.runTime += DateTime.Now - Machine.lastTime;
                Machine.lastTime = DateTime.Now;
                toolStripStatusLabel1.Text = string.Format("运行时长：{0}H", Math.Round(Machine.runTime.TotalHours, 2).ToString());
            }
            else
            {
                //if (Win_MotionControl.Instance.Visible)
                //    Machine.UpdateIO();
            }
        }


        private void Win_Main_SizeChanged(object sender, EventArgs e)
        {
           
        }


        private void 窗体缩放label_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
                this.WindowState = FormWindowState.Maximized;

            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

      

    
        private void 关闭软件lxcImageButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region  启动/暂停/停止/复位/软件颜色变化

        int colorHappy = 0;
        bool falg = true;

        private void timer1_Tick(object sender, EventArgs e)
        {
            Color sourceColor = Color.FromArgb(192, 64, 0);//Color.Blue;
            Color destColor = Color.FromArgb(64, 53, 130);
            int redSpace = destColor.R - sourceColor.R;
            int greenSpace = destColor.G - sourceColor.G;
            int blueSpace = destColor.B - sourceColor.B;
            if (falg)
            {
                colorHappy += 3;
                if (colorHappy > 100)
                {
                    colorHappy = 100;
                    falg = false;
                }
            }
            else
            {
                colorHappy -= 3;
                if (colorHappy < 0)
                {
                    colorHappy = 0;
                    falg = true;
                }
            }

            Color vColor = Color.FromArgb(
                sourceColor.R + (int)((double)colorHappy / 100 * redSpace),
                sourceColor.G + (int)((double)colorHappy / 100 * greenSpace),
                sourceColor.B + (int)((double)colorHappy / 100 * blueSpace));
            statusStrip1.BackColor = vColor;//获取一种颜色
        }

        public void StartPre(bool flag)
        {
            timer1.Enabled = !flag;
            if (flag)
            {
                statusStrip1.BackColor = Color.FromArgb(0, 122, 204);//获取一种颜色
            }
        }

        public void MachineState(state MacState)
        {
            switch (MacState)
            {
                case state.start:
                    启动toolStripButton_Click(null, null);
                    break;
                case state.stop:
                    停止toolStripButton_Click(null, null);
                    break;
                case state.pause:
                    暂停toolStripButton_Click(null, null);
                    break;
                case state.reset:
                    复位toolStripButton_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void 启动toolStripButton_Click(object sender, EventArgs e)
        {

            if (Machine.machineRunStatu == MachineRunStatu.Running)
            { return; }

            //Start.JobRunMarkM = false;
            //Start.JobRunMark1 = false;

            启动toolStripButton.Enabled = false;

            暂停toolStripButton.Enabled = true;
            Machine.machineRunStatu = MachineRunStatu.Running;
            启动toolStripButton.Image = Resources.启动A;
            停止toolStripButton.Image = Resources.停止B;
            暂停toolStripButton.Image = Resources.暂停B;
            启动toolStripButton.Text = "已启动";
            停止toolStripButton.Text = "停止";
            暂停toolStripButton.Text = "暂停";
            EnableRun(true, false);

            Machine.StartRun();
            Start.OnMachineStateChange(new MachineStateChangeEventArgs(state.start));

            Configuration.SpeedMode = true;
        }

        private void 启动toolStripButton_Click_1(object sender, EventArgs e)
        {
            //////if (!Permission.CheckPermission(PermissionLevel.Operator))
            //////    return;
            //Start.JobRunMarkM = false;
            //Start.JobRunMark1 = false;

            暂停toolStripButton.Enabled = true;
            Machine.machineRunStatu = MachineRunStatu.Running;

            启动toolStripButton.Image = Resources.启动A;
            复位toolStripButton.Image = Resources.复位B;
            停止toolStripButton.Image = Resources.停止B;
            暂停toolStripButton.Image = Resources.暂停B;

            Machine.StartRun();

            启动toolStripButton.Text = "已启动";
            停止toolStripButton.Text = "停止";
            复位toolStripButton.Text = "复位";
            暂停toolStripButton.Text = "暂停";
            复位toolStripButton.Enabled = false;
            停止toolStripButton.Enabled = true;
            Configuration.SpeedMode = true;
            新建方案ToolStripMenuItem.Enabled = false;
        }

        private void 暂停toolStripButton_Click(object sender, EventArgs e)
        {
            if (Machine.machineRunStatu == MachineRunStatu.Pause)
            { return; }

            //toolStripButton36.Enabled = false;
            启动toolStripButton.Enabled = true;

            Start.JobRunMarkM = true;
            //Start.JobRunMark1 = true;//此处不可以，只能等主站Job运行完毕之后，才能去关闭从站Job

            Machine.StopRun();
            Start.OnMachineStateChange(new MachineStateChangeEventArgs(state.pause));
            Machine.machineRunStatu = MachineRunStatu.Pause;
            停止toolStripButton.Image = Resources.停止B;
            启动toolStripButton.Image = Resources.启动B;
            暂停toolStripButton.Image = Resources.暂停A;
            暂停toolStripButton.Text = "已暂停";
            停止toolStripButton.Text = "停止";
            启动toolStripButton.Text = "启动";
            EnableRun(false, false);
        }

        private void 停止toolStripButton_Click(object sender, EventArgs e)
        {
            if (Machine.machineRunStatu == MachineRunStatu.Stop)
            {
                return;
            }
            暂停toolStripButton.Enabled = false;
            启动toolStripButton.Enabled = true;

            Start.JobRunMarkM = true;
            //Start.JobRunMark1 = true;//此处不可以，只能等主站Job运行完毕之后，才能去关闭从站Job

            //toolStripButton3.Enabled = false;

            Machine.StopRun();
            Start.OnMachineStateChange(new MachineStateChangeEventArgs(state.stop));
            Machine.machineRunStatu = MachineRunStatu.Stop;
            停止toolStripButton.Image = Resources.停止A;
            启动toolStripButton.Image = Resources.启动B;
            暂停toolStripButton.Image = Resources.暂停B;
            停止toolStripButton.Text = "已停止";
            启动toolStripButton.Text = "启动";
            暂停toolStripButton.Text = "暂停";
            EnableRun(false, false);
            Thread th = new Thread(() =>
            {
                Thread.Sleep(1000);      //保证所有的流程最后一次运行都执行完毕
                Configuration.SpeedMode = false;
            })
            {
                IsBackground = true
            };
            th.Start();
        }

        private void 复位toolStripButton_Click(object sender, EventArgs e)
        {
            Start.OnMachineStateChange(new MachineStateChangeEventArgs(state.reset));
            //toolStripButton8.Enabled = false;
            //Win_UserForm.Instance.AlarmClear();
        }

        // 禁用UI功能 运行分单流程运行【Job界面控制】，和多流程【工程】运行【主界面控制】
        //共同禁用项
        //1.禁止拖拽 禁止隐藏[取消隐藏按钮] 工具箱 流程编辑器 LOG记录器 变量监控器
        //2.禁止切换界面
        //3.禁止菜单栏 禁止工具箱 禁止流程编辑器除菜单栏以外的地方
        //4.禁止状态栏 登录 切换工程
        public void EnableRun(bool flag, bool IsJobRun)
        {
            //1============================================================================================================
            dockPanel.LxcAllowDrag = !flag;
            Win_ToolBox.Instance.HideButtonVisible = !flag;
            Win_Job.Instance.HideButtonVisible = !flag;
            Win_Log.Instance.HideButtonVisible = !flag;
            Win_Monitor.Instance.HideButtonVisible = !flag;
            //2============================================================================================================
            pictureBox1.Enabled = !flag;
            //3============================================================================================================
            menuStrip1.Enabled = !flag;
            Win_ToolBox.Instance.Enabled = !flag;
            //4============================================================================================================
            lbl_curEngine.Enabled = !flag;
            tss_permissionInfo.Enabled = !flag;
            //============================================================================================================

            if (IsJobRun)
            {
                启动toolStripButton.Enabled = !flag;
                暂停toolStripButton.Enabled = !flag;
                停止toolStripButton.Enabled = !flag;
                复位toolStripButton.Enabled = !flag;
            }

            Win_Job.Instance.EnableRun(flag, IsJobRun);
        }

        #endregion
        #region 方案菜单栏

        private void 方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 新建方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Scheme.CreateScheme();
        }

        private void 近期方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            //
        }

        private void 打开方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Scheme.OpenScheme();
        }

        private void 导出方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Project.ExportProject();
        }

        private void 导入方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openProject = false;
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Project.InportProject();
        }

        private void 克隆方案toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Scheme.CloneScheme();
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.SaveProject();
            // SaveAll();
            Project.Instance.configuration.Save();
        }

        private void 删除方案toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Scheme.DeleteScheme1();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion
        #region 流程菜单栏

        private void 新建流程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Job.CreateJob();
        }

        private void 导出流程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;
                Job.ExportJob();
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        private void 导入流程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;
                if (Project.Instance.L_engineList.Count == 0)
                {
                    Win_MessageBox.Instance.MessageBoxShow("\r\n未创建方案，请先创建方案");
                    Win_EngineManager.Instance.Show();
                    return;
                }

                System.Windows.Forms.OpenFileDialog dig_openImage = new System.Windows.Forms.OpenFileDialog
                {
                    FileName = "",
                    Title = ("请选择流程文件"),
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                    Filter = ("流程文件(*.job)|*.job")
                };
                dig_openImage.ShowDialog();
                if (dig_openImage.FileName == string.Empty)
                {
                    return;
                }
                try
                {
                    Job job = Job.LoadJob(dig_openImage.FileName);
                    //File.Copy(dig_openImage.FileName, Application.StartupPath + "\\Config\\Project\\Vision\\Job\\" + job.jobName + ".job");
                }
                catch { }

                VSE.Core.Log.SaveLog(Project.Instance.configuration.dataPath, LogType.Operate, "导入了新流程");
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        private void 克隆流程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Job.CloneJob();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Developer))
                    return;

                if (Win_Job.Instance.listView1.Nodes.Count < 1)
                {
                    Win_Log.Instance.OutputMsg("当前项目中未添加任何方案", Win_Log.InfoType.error);
                    return;
                }
                Win_ConfirmBox.Instance.lbl_info.Text = "确定要删除当前流程吗？";
                Win_ConfirmBox.Instance.ShowDialog();
                if (Win_ConfirmBox.Instance.Result != ConfirmBoxResult.Yes)
                {
                    return;
                }
                string jobName = Win_Job.Instance.JobEdit_JobName.Text.Substring(5);
                Job.RemoveJobByName(jobName);
                for (int i = 0; i < Win_Job.Instance.listView1.Nodes.Count; i++)
                {
                    if (Win_Job.Instance.listView1.Nodes[i].Text == jobName)
                    {
                        Win_Job.Instance.listView1.Nodes.RemoveAt(i);
                    }
                }
                if (File.Exists(Application.StartupPath + "\\Config\\Project\\Vision\\Job\\" + jobName + ".job"))
                    File.Delete(Application.StartupPath + "\\Config\\Project\\Vision\\Job\\" + jobName + ".job");
                Win_Main.Instance.OutputMsg("流程删除成功", Win_Log.InfoType.tip);
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }


        #endregion
        #region 视图菜单栏

        private void 全局变量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Win_GlobalVariable.Instance.Show();
            Win_GlobalVariable.Instance.WindowState = FormWindowState.Normal;
        }

        private void 输出监视ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Developer))
                    return;
                Win_Monitor.Instance.Show(dockPanel, DockState.Document);


                //需要重新保存一下布局
                // File.Delete(Application.StartupPath + "\\" + Project.Instance.configuration.layoutFilePath);
                // dockPanel.SaveAsXml(Project.Instance.configuration.layoutFilePath);
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        private void 工具箱ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Win_ToolBox.Instance.Show(dockPanel, Win_ToolBox.lastDockState);
        }


        private void 系统日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Win_Log.Instance.DockState == DockState.Hidden || Win_Log.Instance.DockState == DockState.Unknown)
                Win_Log.Instance.Show(Win_Main.Instance.dockPanel, DockState.DockRight);
        }

        private void 流程编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Win_Job.Instance.DockState == DockState.Hidden || Win_Job.Instance.DockState == DockState.Unknown)
                Win_Job.Instance.Show(Win_Main.Instance.dockPanel, DockState.DockRight);
        }

        #endregion 
        #region 工具菜单栏
        private void 虚拟键盘ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processKeyBoard = System.Diagnostics.Process.Start("osk.exe");
        }

        private void 截屏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                VSE.Core.Method.Screenshot();
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        private void 计算机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processCalculator = System.Diagnostics.Process.Start("calc.exe");
        }

        #endregion
        #region 系统菜单栏
        private void 系统重置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Developer))
                    return;

                Win_ConfirmBox.Instance.lbl_info.Text = ("确定要将程序恢复到初始状态吗？这将清除所有配置及流程\r\n文件，使程序恢复到安装完毕时的初始状态！");
                Win_ConfirmBox.Instance.ShowDialog();
                if (Win_ConfirmBox.Instance.Result != ConfirmBoxResult.Yes)
                {
                    return;
                }


                //删除所有配置文件
                if (Directory.Exists(Application.StartupPath + "\\Config\\Project"))
                    Directory.Delete(Application.StartupPath + "\\Config\\Project", true);
                if (File.Exists(Application.StartupPath + "\\Config\\Configuration.ini"))
                    File.Delete(Application.StartupPath + "\\Config\\Configuration.ini");
                Win_MessageBox.Instance.MessageBoxShow("\r\n重置成功!（重启后生效，确定后程序将自动关闭）");
                Process.GetCurrentProcess().Kill();
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        private void 重置布局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Developer))
                return;

            Win_ConfirmBox.Instance.lbl_info.Text = ("确定要将布局恢复到初始状态吗？");
            Win_ConfirmBox.Instance.ShowDialog();
            if (Win_ConfirmBox.Instance.Result != ConfirmBoxResult.Yes)
            {
                return;
            }
            try
            {
                File.Copy(Application.StartupPath + "\\Config\\Resources\\Layout\\backup\\系统布局.config", Application.StartupPath + "\\Config\\Resources\\Layout\\系统布局.config", true);

            }
            catch { }
            Win_MessageBox.Instance.MessageBoxShow("\r\n重置成功!（重启后生效，确定后程序将自动重启）");
            restart = true;
            Application.Restart();
            Process.GetCurrentProcess().Kill();
        }

        private void 数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Project.Instance.configuration.dataPath + "\\Log");
        }

        private void 系统设定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            VSE.Core.Log.SaveLog(Project.Instance.configuration.dataPath, LogType.Operate, "打开设置页面");
            Win_Setting.Instance.Show();
        }
        #endregion 
        #region 帮助菜单栏
        private void 激活ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 示例项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Process.Start(Application.StartupPath + "\\Config\\Resources\\Help.html");
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Win_About.Instance.ShowDialog();
        }

        #endregion


        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;
                if (Win_Job.Instance.listView1.Nodes.Count== 0)
                {
                    Win_Main.Instance.OutputMsg("没有可运行的流程",Win_Log.InfoType.tip);
                    return;
                }
                Win_Job.Instance.btn_runOnce.Enabled = false;
                string jobName = Win_Job.Instance.JobEdit_JobName.Text.Substring(5);
                Job job = Job.FindJobByName(jobName);
                job.Run();
                Win_Job.Instance.btn_runOnce.Enabled = true;
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Permission.CheckPermission(PermissionLevel.Admin))
                    return;

                if (Win_Job.Instance.btn_runLoop.Text == "连续运行")
                    Job.FindJobByName(Win_Job.Instance.JobEdit_JobName.Text.Substring(5)).LoopRun(true);
                else
                    Job.FindJobByName(Win_Job.Instance.JobEdit_JobName.Text.Substring(5)).LoopRun(false);
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
       
        private void 解锁ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;

            dockPanel.AllowEndUserDocking = !dockPanel.AllowEndUserDocking;
          
            VSE.Core.Log.SaveLog(Project.Instance.configuration.dataPath, LogType.Operate,"界面锁定启用");
        }

        private void 登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Win_Login.Instance.ShowDialog();
        }

        private void 运行一次ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton11_Click(null, null);
        }

        private void 连续运行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton12_Click(null, null);
        }

        private void lbl_curEngine_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.RemoveAll();
            toolTip1.ToolTipTitle = string.Empty;
            toolTip1.Show("点击可切换方案", statusStrip1);
        }

        private void tss_permissionInfo_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.RemoveAll();
            toolTip1.ToolTipTitle = string.Empty;
            toolTip1.Show("点击可登录用户", statusStrip1);
        }

        private void lbl_curEngine_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                //if (lbl_curEngine.Text.Substring(5) != e.ClickedItem.Text)
                if (lbl_curEngine.Text != e.ClickedItem.Text)
                {
                    lbl_curEngine.Text = e.ClickedItem.Text;
                    List<string> list = new List<string>();
                    int iJobCount = Project.Instance.FindEngineByName(e.ClickedItem.Text).L_jobList.Count;
                    for (int i = 0; i < iJobCount; i++)
                    {
                        list.Add(Project.Instance.FindEngineByName(e.ClickedItem.Text).L_jobList[i].jobName);
                    }
                    string[] strJobNmaeList = list.ToArray();
                    Start.OnUpdateJobNameList(new JobNameListEventArgs(strJobNmaeList));
                    Start.isFirstLoadJob = true;
                    Scheme.SwitchScheme(lbl_curEngine.Text);
                    Start.OnSwitchProject(lbl_curEngine.Text);
                }
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        public static HImage TempImage = new HImage();
        public static string TempJobName = "";
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
          
            if (Machine.curFormMode== FormMode.VisionForm)
            {
                Loding.StartMainLoading();
                Machine.SwitchFrom(FormMode.MainForm);
                
                toolTip1.SetToolTip(pictureBox1, "操作员账户 点击切换到管理员账户");
               
                //toolStripButton1.Image = User.Images[1];

                Loding.StopMainLoading();
            }
            else
            {
                
                //LXCSystem.Frm.PasswordInputForm PassWin = new PasswordInputForm("123");
                //if (PassWin.ShowDialog() == DialogResult.OK)
                //{
                 
                    Loding.StartMainLoading();
                    toolTip1.SetToolTip(pictureBox1, "管理员账户 点击切换到操作员账户");
                   
                    Machine.SwitchFrom(FormMode.VisionForm);
                    Loding.StopMainLoading();
                if (TempJobName!="")
                {
                    foreach (KeyValuePair<string, Lxc.VisionPlus.ImageView.ImgView> item in Win_ImageWindow.HDisplayCtrs)
                    {
                        if (item.Key == Job.FindJobByName(TempJobName).debugImageWindow)
                        {
                            item.Value.Image = TempImage.CopyImage();

                        }
                    }
                    TempJobName = "";
                }
                

                // }
            }
            dockPanel.LxcAllowDrag = (Machine.curFormMode == FormMode.VisionForm);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
        }

        private void 系统ToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if ((Permission.CurrentPermission == PermissionLevel.NoPermission || Permission.CurrentPermission == PermissionLevel.Operator) )
            {
                //系统ToolStripMenuItem.DropDown.HideDropDown();
            }
        }

      
    }
}
