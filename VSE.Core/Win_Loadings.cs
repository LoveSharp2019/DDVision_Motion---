using System;
using System.Drawing;
using System.Windows.Forms;

namespace VSE.Core
{
    public partial class frmLoadings : Form
    {
        public frmLoadings()
        {
            this.InitializeComponent();
        }
        public frmLoadings(Point locationValue, Size frmSizeValue)
        {
            this.InitializeComponent();
            this._locationValue = locationValue;
            this._frmSizeValue = frmSizeValue;
            this.WindowState = FormWindowState.Maximized;
        }
        private void frmLoadings_Load(object sender, EventArgs e)
        {
            try
            {
                base.Size = this._frmSizeValue;
                base.Location = this._locationValue;
            }
            catch
            {
            }
        }
      
        private void CallInvoker(MethodInvoker invoker)
        {
            if (base.InvokeRequired)
            {
                base.Invoke(invoker);
                return;
            }
            invoker();
        }
        public void LoadingFinish()
        {
            MethodInvoker invoker = delegate ()
            {
                base.Close();
            };
            this.CallInvoker(invoker);
        }
        public void SetProcess(int percent, string msg)
        {
            this.start = DateTime.Now;
            MethodInvoker invoker = delegate ()
            {
                this.progressBar.Value = percent;
                this.labelMsg.Text = msg;
            };
            this.CallInvoker(invoker);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now - this.start;
            MethodInvoker invoker = delegate ()
            {
                this.labelSpan.Text = string.Format("单项时间:{0}", span.ToString());
            };
            this.CallInvoker(invoker);
        }
        //private void LodingExit()
        //{
        //    Helper.Loding.Exit();
        //}
        private readonly Point _locationValue;
        private readonly Size _frmSizeValue;
        private DateTime start = DateTime.Now;

    }
}
