using VSE.Properties;
using System;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    internal partial class Win_Setting :FormBase
    {
        internal Win_Setting():base()
        {
            InitializeComponent();
            this.BackColor = VUI.WinTitleBarBackColor;
           // tvw_setting.BackColor = VUI.WinBackColor;
            panel2.BackColor = VUI.WinBackColor;
            TopMost = true;
            treeNode1.Name = "节点0";
            treeNode1.Text = "常规";
            treeNode2.Name = "节点1";
            treeNode2.Text = "项目";
            treeNode3.Name = "节点2";
            treeNode3.Text = "机种";
            treeNode4.Name = "节点3";
            treeNode4.Text = "插件";
            treeNode5.Name = "节点4";
            treeNode5.Text = "账户";
            treeNode6.Name = "节点5";
            treeNode6.Text = "高级";
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_Setting _instance;
        public static Win_Setting Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_Setting();
                return _instance;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);  
            e.Cancel = true;
            this.Hide();
        }
        private void Win_Setting_Load(object sender, EventArgs e)
        {
            try
            {
                //tvw_setting.SelectedNode = tvw_setting.Nodes[0];

                //Win_GeneralSettings.Instance.tbx_companyName.Text = Project.Instance.configuration.CompanyName;
                //Win_GeneralSettings.Instance.cbo_lanuage.Text ="简体中文";
                //Win_GeneralSettings.Instance.tbx_dataPath.Text = Project.Instance.configuration.dataPath;

                //Win_ProjetSettings.Instance.tbx_programTitle.Text = Project.Instance.configuration.ProgramTitle;
                //Win_ProjetSettings.Instance.cbx_cardType.Text = Project.Instance.configuration.cardType.ToString();
                //Win_ProjetSettings.Instance.ckb_vitualCard.Checked = Project.Instance.configuration.vitualCard;
                //switch (Project.Instance.configuration.defaultForm)
                //{
                //    case FormMode.MainForm:
                //        Win_ProjetSettings.Instance.radioButton1.Checked = true;
                //        break;
                //    case FormMode.VisionForm:
                //        Win_ProjetSettings.Instance.radioButton2.Checked = true;
                //        break;
                //    case FormMode.MotionForm:
                //        Win_ProjetSettings.Instance.radioButton3.Checked = true;
                //        break;
                //}

                ////////Win_RunSettings.Instance.tbx_autoRunVel.Text = Project.Instance.configuration.autoRunVel.ToString();
                //Win_RunSettings.Instance.tbx_jobsRunPouseTime.Value = Project.Instance.configuration.timeBetweenJobRun;
                //Win_StartSetting.Instance.cCheckBox1.Checked = Project.Instance.configuration.autoRunAfterStart;
                //Win_StartSetting.Instance.ckb_autoConnect.Checked = Project.Instance.configuration.autoConnectAfterStart;
                //Win_StartSetting.Instance.ckb_switchedToAutoRunMode.Checked = Project.Instance.configuration.switchedToAutoMode;
                //Win_StartSetting.Instance.ckb_autoLock.Checked = Project.Instance.configuration.autoLockAfterStart;
                //Win_StartSetting.Instance.ckb_maxSizeAfterStart.Checked = Project.Instance.configuration.maxSizeAfterStart;
                //Win_StartSetting.Instance.ckb_displayLine.Checked = Project.Instance.configuration.displayLine;
                //Win_RunSettings.Instance.ckb_displayLine.Checked = Project.Instance.configuration.failStop;
                //Win_RunSettings.Instance.cCheckBox1.Checked = Project.Instance.configuration.endStop;
                //Win_StartSetting.Instance.ckb_allowResizeFormSize.Checked = Project.Instance.configuration.allowResizeForm;
                //Win_StartSetting.Instance.ckb_saveWhileExit.Checked = Project.Instance.configuration.saveWhenExit;
                //Win_StartSetting.Instance.ckb_autoStartAfterStartup.Checked = Project.Instance.configuration.autoStartAfterStartup;

                //Win_StartSetting.Instance.ckb_enablePermissionControl.Checked = Project.Instance.configuration.enablePermissionControl;

                //Win_ProjetSettings.Instance.checkBox2.Checked = Project.Instance.configuration.EnableVisionForm;

                //Win_GeneralSettings.Instance.ckb_dataSaveDays.Value = Project.Instance.configuration.dataSaveDays;

            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void btn_saveSetting_Click(object sender, EventArgs e)
        {

        }
        private void tvw_setting_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                //TreeNode node = tvw_setting.SelectedNode;
                //switch (node.Text)
                //{
                //    case "功能":
                //        pnl_window.Controls.Clear();
                //        Win_StartSetting.Instance.TopLevel = false;
                //        Win_StartSetting.Instance.Parent = pnl_window;
                //        Win_StartSetting.Instance.Show();
                //        windowType = 3;
                //        break;
                //    case "项目":
                //        pnl_window.Controls.Clear();
                //        Win_ProjetSettings.Instance.TopLevel = false;
                //        Win_ProjetSettings.Instance.Parent = pnl_window;
                //        Win_ProjetSettings.Instance.Show();
                //        windowType = 1;
                //        break;
                //    case "方案":
                //        pnl_window.Controls.Clear();
                //        Win_EngineManager.Instance.TopLevel = false;
                //        Win_EngineManager.Instance.Parent = pnl_window;
                //        Win_EngineManager.Instance.Show();
                //        windowType = 2;
                //        break;
                //    case "运行":
                //        pnl_window.Controls.Clear();
                //        Win_RunSettings.Instance.TopLevel = false;
                //        Win_RunSettings.Instance.Parent = pnl_window;
                //        Win_RunSettings.Instance.Show();
                //        windowType = 5;
                //        break;
                //    case "常规":
                //        pnl_window.Controls.Clear();
                //        Win_GeneralSettings.Instance.TopLevel = false;
                //        Win_GeneralSettings.Instance.Parent = pnl_window;
                //        Win_GeneralSettings.Instance.Show();
                //        windowType = 0;
                //        break;
                //    case "安全":
                //    case "用户管理":
                //        if (windowType != 4)
                //        {
                //            pnl_window.Controls.Clear();
                //            Win_UserManager.Instance.TopLevel = false;
                //            Win_UserManager.Instance.Parent = pnl_window;
                //            Win_UserManager.Instance.Show();
                //            windowType = 4;
                //        }
                //        break;
                //}
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Hide();
                //Project.Instance.configuration.autoConnectAfterStart = Win_StartSetting.Instance.ckb_autoConnect.Checked;
                ////////Project.Instance.configuration.autoRunVel = (short)Convert.ToInt32(Win_RunSettings.Instance.tbx_autoRunVel.Text);
                //Project.Instance.configuration.ProgramTitle = Win_ProjetSettings.Instance.tbx_programTitle.Text.Trim();
               
                //Project.Instance.configuration.EnableVisionForm = Win_ProjetSettings.Instance.checkBox2.Checked;

                //Project.Instance.configuration.timeBetweenJobRun = Convert.ToInt32(Win_RunSettings.Instance.tbx_jobsRunPouseTime.Value);
                //Project.Instance.configuration.switchedToAutoMode = Win_StartSetting.Instance.ckb_switchedToAutoRunMode.Checked;
                //Project.Instance.configuration.dataPath = Win_GeneralSettings.Instance.tbx_dataPath.Text.Trim();
                //Project.Instance.configuration.language = Win_GeneralSettings.Instance.cbo_lanuage.SelectedIndex == 0 ? Language.Chinese : Language.English;
                //Project.Instance.configuration.cardType = (Win_ProjetSettings.Instance.cbx_cardType.Text == string.Empty ? CardType.无 : (CardType)Enum.Parse(typeof(CardType), Win_ProjetSettings.Instance.cbx_cardType.Text));
                //Project.Instance.configuration.autoConnectAfterStart = Win_StartSetting.Instance.ckb_autoConnect.Checked;
                //Project.Instance.configuration.CompanyName = Win_GeneralSettings.Instance.tbx_companyName.Text.Trim();
                //Project.Instance.configuration.displayLine = Win_StartSetting.Instance.ckb_displayLine.Checked;
                //Project.Instance.configuration.autoRunAfterStart = Win_StartSetting.Instance.cCheckBox1.Checked;

                //Project.Instance.configuration.endStop = Win_RunSettings.Instance.cCheckBox1.Checked;
                //Project.Instance.configuration.failStop = Win_RunSettings.Instance.ckb_displayLine.Checked;
                //Project.Instance.configuration.dataSaveDays = Convert.ToInt16(Win_GeneralSettings.Instance.ckb_dataSaveDays.Value);
                //Project.Instance.configuration.autoLockAfterStart = Win_StartSetting.Instance.ckb_autoLock.Checked;
                //Project.Instance.configuration.maxSizeAfterStart = Win_StartSetting.Instance.ckb_maxSizeAfterStart.Checked;
                //Project.Instance.configuration.enablePermissionControl = Win_StartSetting.Instance.ckb_enablePermissionControl.Checked;
                //if (Win_StartSetting.Instance.ckb_autoStartAfterStartup.Checked != Project.Instance.configuration.autoStartAfterStartup)
                //{
                //    if (Win_StartSetting.Instance.ckb_autoStartAfterStartup.Checked)
                //        Win_Main.Auto_Start(!Project.Instance.configuration.autoStartAfterStartup);
                //    else
                //        Win_Main.Auto_Start(!Project.Instance.configuration.autoStartAfterStartup);
                //}
                //Project.Instance.configuration.autoStartAfterStartup = Win_StartSetting.Instance.ckb_autoStartAfterStartup.Checked;
                //Project.Instance.configuration.allowResizeForm = Win_StartSetting.Instance.ckb_allowResizeFormSize.Checked;
                //Project.Instance.configuration.vitualCard = Win_ProjetSettings.Instance.ckb_vitualCard.Checked;
                //Project.Instance.configuration.saveWhenExit = Win_StartSetting.Instance.ckb_saveWhileExit.Checked;
                //if (Win_ProjetSettings.Instance.radioButton1.Checked)
                //    Project.Instance.configuration.defaultForm = FormMode.MainForm;
                //else if (Win_ProjetSettings.Instance.radioButton2.Checked)
                //    Project.Instance.configuration.defaultForm = FormMode.VisionForm;
                //else
                //    Project.Instance.configuration.defaultForm = FormMode.MotionForm;

                //Project.SaveProject();
                //// SaveAll();
                //Project.Instance.configuration.Save();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void syssetList1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Node.Index)
            {
                case 0:
                    pnl_window.Controls.Clear();
                    Win_GeneralSettings.Instance.TopLevel = false;
                    Win_GeneralSettings.Instance.Parent = pnl_window;
                    Win_GeneralSettings.Instance.Show();
                    break;
                case 1:
                    pnl_window.Controls.Clear();
                    Win_ProjetSettings.Instance.TopLevel = false;
                    Win_ProjetSettings.Instance.Parent = pnl_window;
                    Win_ProjetSettings.Instance.Show();
                    break;
                case 2:
                    pnl_window.Controls.Clear();
                    Win_EngineManager.Instance.TopLevel = false;
                    Win_EngineManager.Instance.Parent = pnl_window;
                    Win_EngineManager.Instance.Show();
                    break;
                case 3:
                    pnl_window.Controls.Clear();
                    Win_PuginSettings.Instance.TopLevel = false;
                    Win_PuginSettings.Instance.Parent = pnl_window;
                    Win_PuginSettings.Instance.Show();
                    break;
                case 4:
                    pnl_window.Controls.Clear();
                    Win_UserManager.Instance.TopLevel = false;
                    Win_UserManager.Instance.Parent = pnl_window;
                    Win_UserManager.Instance.Show();
                    break;
                case 5:
                    if (Permission.CurrentPermission== PermissionLevel.Developer)
                    {
                        pnl_window.Controls.Clear();
                        Win_AdvancedSetting.Instance.TopLevel = false;
                        Win_AdvancedSetting.Instance.Parent = pnl_window;
                        Win_AdvancedSetting.Instance.Show();
                    }
                    
                   


                    break;
                default:
                    break;
            }
        }
        System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("常规");
        System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("项目");
        System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("机种");
        System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("插件");
        System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("账户");
        System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("高级");
           
        private void Win_Setting_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.syssetList1.Nodes.Clear();
                if (Permission.CurrentPermission== PermissionLevel.Developer)
                {
                    this.syssetList1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            this.treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
                }
                else
                {
                    this.syssetList1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            this.treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
                }
                pnl_window.Controls.Clear();
                Win_GeneralSettings.Instance.TopLevel = false;
                Win_GeneralSettings.Instance.Parent = pnl_window;
                Win_GeneralSettings.Instance.Show();
            }
        }
    }
}
