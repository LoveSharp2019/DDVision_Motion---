
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    internal partial class Win_GeneralSettings : Form
    {
        internal Win_GeneralSettings()
        {
            InitializeComponent();
            this.BackColor = VUI.WinBackColor;

        }

        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Win_GeneralSettings _instance;
        public static Win_GeneralSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_GeneralSettings();
                return _instance;
            }
        }
        private void Chinese_Click(object sender, EventArgs e)
        {
            if (Chinese.Checked)
            {
                Project.Instance.configuration.SystemLanguage = Language.Chinese;
                //切换中文
            }
            else if (English.Checked)
            {
                Project.Instance.configuration.SystemLanguage = Language.English;
                //切换英文
            }
        }

        private void lxcRadioButton4_Click(object sender, EventArgs e)
        {
            if (lxcRadioButton4.Checked)
            {
                Project.Instance.configuration.SystemTheme = Theme.Dark;
                //切换深色主题
            }
            else if (lxcRadioButton1.Checked)
            {
                Project.Instance.configuration.SystemTheme = Theme.Light;
                //切换浅色主题
            }
            else if (lxcRadioButton2.Checked)
            {
                Project.Instance.configuration.SystemTheme = Theme.Blue;
                //切换蓝色主题
            }
        }

        private void tbx_companyName_TextStrChanged(string textStr)
        {
            Project.Instance.configuration.CompanyName = tbx_companyName.TextStr;
            Project.Instance.configuration.CompanyAddress = vTextBox1.TextStr;
            //保存公司名称 地址
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog()== DialogResult.OK)
            {
                Project.Instance.configuration.Logo = Image.FromFile(openFileDialog1.FileName);
                panel1.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
            }
            
            
       
            //切换图标
        }

        private void Win_GeneralSettings_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.vTextBox1.TextStrChanged -= new VControls.DTextStrChanged(this.tbx_companyName_TextStrChanged);
                this.tbx_companyName.TextStrChanged -= new VControls.DTextStrChanged(this.tbx_companyName_TextStrChanged);
                tbx_companyName.TextStr= Project.Instance.configuration.CompanyName;
                vTextBox1.TextStr = Project.Instance.configuration.CompanyAddress;
                switch (Project.Instance.configuration.SystemTheme)
                {
                    case Theme.Dark:
                        lxcRadioButton4.Checked = true;
                        break;
                    case Theme.Light:
                        lxcRadioButton1.Checked = true;
                        break;
                    case Theme.Blue:
                        lxcRadioButton2.Checked = true;
                        break;
                    default:
                        break;
                }
                switch (Project.Instance.configuration.SystemLanguage)
                {
                    case Language.Chinese:
                        Chinese.Checked = true;
                        break;
                    case Language.English:
                        English.Checked = true;
                        break;
                  
                    default:
                        break;
                }
                panel1.BackgroundImage = Project.Instance.configuration.Logo;
                this.vTextBox1.TextStrChanged += new VControls.DTextStrChanged(this.tbx_companyName_TextStrChanged);
                this.tbx_companyName.TextStrChanged += new VControls.DTextStrChanged(this.tbx_companyName_TextStrChanged);
            }
        }
    }
}
