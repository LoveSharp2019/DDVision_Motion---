using System;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    public partial class Win_Regiest : FormBase 
    {
        public Win_Regiest()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 本机的机器码
        /// </summary>
        private string machineCode;
        /// <summary>
        /// 读取本地存储的注册码
        /// </summary>
        private Ini ini = new Ini(Application.StartupPath + "\\Config.ini");
        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_Regiest _instance;
        public static Win_Regiest Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_Regiest();
                return _instance;
            }
        }
      

        private void lnl_author_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Win_InputMessage inputMessage = new Win_InputMessage();
            inputMessage.XXXXXXXXXXXXXlbl_title.Text = "请输入密码";
            inputMessage.passwordChar = true;
            inputMessage.ShowDialog(this);
            if (inputMessage.txt_input.TextStr == "XXX")
            {
                Ini ini = new Ini(Application.StartupPath + "\\Config.ini");
                string machineCode =Regiest. Get_MNum();
                string RegiestCode =Regiest. Get_RNum(machineCode);
                ini.IniWriteValue("Regiest", "RegiestCode", RegiestCode);
                this.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Win_Regiest_Load(object sender, EventArgs e)
        {
            machineCode =Regiest . Get_MNum();
            txt_machineCode.Text = machineCode;
        }
        private void btn_regiest_Click(object sender, EventArgs e)
        {

            string machineCode =Regiest . Get_MNum();
            string RegiestCode =Regiest. Get_RNum(machineCode);
            if (txt_regiestCode.Text == RegiestCode)
            {
                ini.IniWriteValue("Regiest", "RegiestCode", txt_regiestCode.Text);
            }
            else
            {
                txt_regiestCode.Clear();
                Win_MessageBox messageBox = new Win_MessageBox();
                messageBox.MessageBoxShow("The registration code is incorrect. Please enter it again");
                txt_regiestCode.Focus();
            }
        }
        
    }
}
