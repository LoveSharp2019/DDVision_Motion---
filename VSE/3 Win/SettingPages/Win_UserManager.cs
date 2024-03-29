using System;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    internal partial class Win_UserManager : Form
    {
        internal Win_UserManager()
        {
            InitializeComponent();
            this.BackColor = VUI.WinBackColor;
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        private Ini ini = new Ini(Application.StartupPath + "\\Config.ini");
        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Win_UserManager _instance;
        public static Win_UserManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_UserManager();
                return _instance;
            }
        }

        /// <summary>
       // / 输入检查
       // / </summary>
       // / <returns></returns>
        private bool InputCheck()
        {
            try
            {
                if (NewPassword.Text == string.Empty)
                {
                    NewPassword.Text = string.Empty;
                    AgainPassword.Text = string.Empty;
                    NewPassword.Focus();
                    NewPasswordTip.Text = "密码不能为空 ，请重新输入";
                    return false;
                }
                if (NewPassword.Text.Length < 1)
                {
                    NewPassword.Text = string.Empty;
                    AgainPassword.Text = string.Empty;
                    NewPassword.Focus();
                    NewPasswordTip.Text = "密码长度不能小于1，请重新输入";
                    return false;
                }
                if (NewPassword.Text != AgainPassword.Text)
                {
                    AgainPasswordTip.Text = "两次输入的密码不一致，请重新输入";
                    NewPassword.Text = string.Empty;
                    AgainPassword.Text = string.Empty;
                    NewPassword.Focus();
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            OrgPassword.PasswordChar = default;
        }

        private void label4_MouseUp(object sender, MouseEventArgs e)
        {
            OrgPassword.PasswordChar = char.Parse("※");
        }

        private void label6_MouseUp(object sender, MouseEventArgs e)
        {
            NewPassword.PasswordChar = char.Parse("※");
        }

        private void label6_MouseDown(object sender, MouseEventArgs e)
        {
            NewPassword.PasswordChar = default;
        }

        private void label7_MouseUp(object sender, MouseEventArgs e)
        {
            AgainPassword.PasswordChar = char.Parse("※");
        }

        private void label7_MouseDown(object sender, MouseEventArgs e)
        {
            AgainPassword.PasswordChar = default;
        }

        private void lxcButton2_Click(object sender, EventArgs e)
        {

            try
            {
                string currentMD5 = LXCSystem.SysCore.SoftBasic.GetMD5(OrgPassword.Text.Trim());
                if (lxcRadioButton4.Checked)
                {
                    string localMD5 = Project.Instance.configuration.adminPassword;
                    if (currentMD5 == localMD5)
                    {
                        if (InputCheck())
                        {
                            string passwordMD5 = LXCSystem.SysCore.SoftBasic.GetMD5(NewPassword.Text);
                            Project.Instance.configuration.adminPassword = passwordMD5;
                            MessageBox.Show("密码修改成功", "Tip:");
                            NewPassword.Text = string.Empty;
                            AgainPassword.Text = string.Empty;
                            OrgPassword.Text = string.Empty;
                        }
                    }
                    else
                    {
                        OrgPassword.Text = string.Empty;
                        OrgPassword.Focus();
                        OrgPasswordTip.Text = "初始密码错误，请重新输入";
                    }
                }
                else
                {
                    string localMD5 = Project.Instance.configuration.OperPassword;
                    if (currentMD5 == localMD5)
                    {
                        if (InputCheck())
                        {
                            string passwordMD5 = LXCSystem.SysCore.SoftBasic.GetMD5(NewPassword.Text);
                            Project.Instance.configuration.OperPassword = passwordMD5;
                            MessageBox.Show("密码修改成功", "Tip:");
                            NewPassword.Text = string.Empty;
                            AgainPassword.Text = string.Empty;
                            OrgPassword.Text = string.Empty;
                        }
                    }
                    else
                    {
                        OrgPassword.Text = string.Empty;
                        OrgPassword.Focus();
                        OrgPasswordTip.Text = "初始密码错误，请重新输入";
                    }
                }
            }
            catch (Exception ex)
            {
                VSE.Core.Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
            
        }

        private void lxcButton1_Click(object sender, EventArgs e)
        {
            Win_ConfirmBox.Instance.lbl_info.Text ="确定要重置密码吗？";
            Win_ConfirmBox.Instance.StartPosition = FormStartPosition.CenterParent;
            
            Win_ConfirmBox.Instance.ShowDialog();
            if (Win_ConfirmBox.Instance.Result == ConfirmBoxResult.Yes)
            {
                if (lxcRadioButton4.Checked)
                {
                    Project.Instance.configuration.adminPassword = Configuration.adminPasswordconst;
                }
                else
                {
                    Project.Instance.configuration.OperPassword = "";
                }
            }
            Win_MessageBox.Instance.MessageBoxShow("密码重置成功！");

        }
    }
}
