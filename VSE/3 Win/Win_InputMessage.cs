using VSE.Properties;
using System;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    internal partial class Win_InputMessage : FormBase
    {
        internal Win_InputMessage()
        {
            InitializeComponent();
           this.BackColor = VUI.WinTitleBarBackColor;
            btn_confirm.FlatAppearance.BorderColor = VUI.ButtonBorderColor;
        }
        /// <summary>
        /// 是否以密码的形式输入
        /// </summary>
        internal bool passwordChar = false;
        /// <summary>
        /// 信息输入窗体输入的信息
        /// </summary>
        internal  string input = string.Empty;
        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_InputMessage _instance;
        public static Win_InputMessage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_InputMessage();
                return _instance;
            }
        }

        bool IsOK = false;
        private void btn_confirm_Click(object sender, EventArgs e)
        {
            input = txt_input.TextStr.Trim();
            IsOK = true;
            this.Close();
        }
    
        private void Win_InputMessage_Load(object sender, EventArgs e)
        {
            try
            {
                txt_input.Select();

                this.TopMost = true;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void Win_InputMessage_Shown(object sender, EventArgs e)
        {
            if (txt_input.TextStr == string.Empty)
            {
                txt_input.TextStr = string.Empty;
                txt_input.TextStr = "1";
                txt_input.TextStr = string.Empty;
                input = string.Empty;
            }
            else
            {
                input = txt_input.TextStr;
            }
        }

        private void Win_InputMessage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsOK)
            {
                input = "";
            }
           
        }

        private void Win_InputMessage_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                IsOK = false;
            }
            
           
        }
    }
}
