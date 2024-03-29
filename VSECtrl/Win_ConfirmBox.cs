using System;

namespace VControls
{
    public partial class Win_ConfirmBox : FormBase
    {
        public Win_ConfirmBox()
        {
            InitializeComponent();
            this.BackColor = VUI.WinTitleBarBackColor;
            panel2.BackColor = VUI.WinBackColor;
        }


        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_ConfirmBox _instance;
        public static Win_ConfirmBox Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_ConfirmBox();
                return _instance;
            }
        }
        /// <summary>
        /// 选择结果
        /// </summary>
        public ConfirmBoxResult Result = ConfirmBoxResult.Cancel;



        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Result = ConfirmBoxResult.Cancel;
            this.Close();
        }
        private void btn_confirm_Click(object sender, EventArgs e)
        {
            Result = ConfirmBoxResult.Yes;
            this.Close();
        }
        private void Win_ConfirmBox_Load(object sender, EventArgs e)
        {
            Result = ConfirmBoxResult.Cancel;
            Win_ConfirmBox.Instance.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Result = ConfirmBoxResult.No;
            this.Close();
        }
    }
    public enum ConfirmBoxResult
    {
        Cancel,
        Yes,
        No,
    }
}
