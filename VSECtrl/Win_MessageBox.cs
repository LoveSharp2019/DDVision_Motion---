using System;
using System.Windows.Forms;
using VControls.Properties;
using VSE.Core;

namespace VControls
{
    public partial class Win_MessageBox :FormBase
    {
        public Win_MessageBox()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_MessageBox _instance;
        public static Win_MessageBox Instance
        {
            get
            {
                _instance = new Win_MessageBox();
                return _instance;
            }
        }


      
        /// <summary>
        /// 弹框
        /// </summary>
        /// <param name="msg">要显示的信息</param>
        public  void MessageBoxShow(string msg, TipType tipType = TipType .Tip)
        {
            this.lbl_info.Text = msg;
            this.TopMost = true;
            switch (tipType)
            {
                case TipType.Tip:
                    XXXXXXXXXXXXXXXXXXXXXXpictureBox1.BackgroundImage = Resources.Tip;
                    XXXXXXXXXXXXXlbl_title.Text = "提示";
                    break;
                case TipType.Warn:
                    XXXXXXXXXXXXXXXXXXXXXXpictureBox1.BackgroundImage = Resources.Warn;
                    XXXXXXXXXXXXXlbl_title.Text = "警告";
                    break;
                case TipType.Error:
                    XXXXXXXXXXXXXXXXXXXXXXpictureBox1.BackgroundImage = Resources.Error;
                    XXXXXXXXXXXXXlbl_title.Text = "错误";
                    break;
            }
            Application.DoEvents();
            this.ShowDialog();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //base.OnFormClosing(e);
            //e.Cancel = true;
            //this.Hide();
        }
        private void btn_confim_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Win_MessageBox_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.TopLevel = true;
        }
    }
}
