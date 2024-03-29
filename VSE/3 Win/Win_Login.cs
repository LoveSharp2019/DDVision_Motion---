using System;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    internal partial class Win_Login :FormBase
    {
        internal Win_Login()
        {
            InitializeComponent();
        }

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
        private void setForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDrag)
            {
                Left += e.Location.X - enterX;
                Top += e.Location.Y - enterY;
            }
        }
        #endregion
        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_Login _instance;
        public static Win_Login Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_Login();
                return _instance;
            }
        }


        private void Win_Login_Shown(object sender, EventArgs e)
        {
            label1.Text = Project.Instance.configuration.CompanyName;
            tbx_password.DefaultText = "请输入密码";
            tbx_password.Text = string.Empty;
            tbx_password.Focus();

            cbx_user.SelectedIndex = 0;
            tbx_password.Select();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                Permission.CurrentPermission = PermissionLevel.NoPermission;
                Win_Main.SetPermission();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbx_user.Text == ("开发人员"))
                {
                    string temp = LXCSystem.SysCore.SoftBasic.GetMD5(tbx_password.TextStr.Trim());
                    if (LXCSystem.SysCore.SoftBasic.GetMD5(tbx_password.TextStr.Trim()) == Project.Instance.configuration.developerPassword)
                    {
                        this.Close();
                        Permission.CurrentPermission = PermissionLevel.Developer;
                        Log.SaveLog(Project.Instance.configuration.dataPath, LogType.Operate, "用户登录成功，当前用户：Developer");
                    }
                    else
                    {
                        tbx_password.DefaultText = "密码错误，请重新输入";
                        tbx_password.TextStr = string.Empty;
                        //tbx_password.Focus();
                    }
                }
                else if (cbx_user.Text == ("管理员"))
                {
                    string currentMD5 = LXCSystem.SysCore.SoftBasic.GetMD5(tbx_password.TextStr.Trim());
                    if (currentMD5 == Project.Instance.configuration.adminPassword)
                    {
                        this.Close();
                        Log.SaveLog(Project.Instance.configuration.dataPath, LogType.Operate, "用户登录成功，当前用户：Admin");
                        Permission.CurrentPermission = PermissionLevel.Admin;
                    }
                    else
                    {
                        tbx_password.TextStr = "密码错误，请重新输入";
                        tbx_password.TextStr = string.Empty;
                     
                    }
                }
                else if (cbx_user.Text == ("操作员"))
                {
                    string currentMD5 = LXCSystem.SysCore.SoftBasic.GetMD5(tbx_password.TextStr.Trim());
                    if (currentMD5 == Project.Instance.configuration.adminPassword)
                    {
                        this.Close();
                        Permission.CurrentPermission = PermissionLevel.Operator;
                        Log.SaveLog(Project.Instance.configuration.dataPath, LogType.Operate, "用户登录成功，当前用户：Operator");
                    }
                    else
                    {
                        tbx_password.TextStr = "密码错误，请重新输入";
                        tbx_password.TextStr = string.Empty;
                        //////tbx_password.Focus();
                      
                    }
                }
                else
                {
                    this.Close();
                    Permission.CurrentPermission = PermissionLevel.NoPermission;
                }
                Win_Main.SetPermission();
                tbx_password.TextStr = string.Empty;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void Win_Login_Load(object sender, EventArgs e)
        {
            label1.Text = Project.Instance.configuration.CompanyName;
            tbx_password.DefaultText = "请输入密码";
            tbx_password.Text = string.Empty;
            tbx_password.Focus();

        }

    }
}
