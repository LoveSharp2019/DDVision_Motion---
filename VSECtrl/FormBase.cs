using System;
using System.Drawing;
using System.Windows.Forms;
using VControls.Properties;

namespace VControls
{
    public partial class FormBase : Form
    {
        public FormBase()
        {
            InitializeComponent();
            this.BackColor = VControls.VUI.WinTitleBarBackColor;
            xxxxxxxxxxxxxxxxxxxxxxx.Panel2.BackColor = VControls.VUI.WinBackColor;
            this.TopMost = false;
            controlBox1.ButtonCloseClick += ButtonClose_Click;
            controlBox1.ButtonMaxClick += ButtonMax_Click;
            controlBox1.ButtonMinClick += ButtonMin_Click;
            controlBox1.ButtonLockClick += ButtonLock_Click;
            controlBox1.MouseHoverColor = Color.FromArgb(this.BackColor.R+15, this.BackColor.G+15, this.BackColor.B+15);
        }
        protected override CreateParams CreateParams
        {
            get
            {

                CreateParams paras = base.CreateParams;
                paras.ExStyle |= 0x02000000;
                return paras;
            }

        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014)
                return;
            base.WndProc(ref m);
        }
        public new bool MinimizeBox
        {
            get { return base.MinimizeBox; }
            set { 

                base.MinimizeBox = value;
                controlBox1.MinEnable = value;
                this.Invalidate();
            }
        }
        public new bool MaximizeBox
        {
            get { return base.MaximizeBox; }
            set
            {

                base.MaximizeBox = value;
                controlBox1.MaxEnable = value;
                this.Invalidate();
            }
        }
        public bool LockBox
        {
            get { return controlBox1.LockEnable; }
            set
            {
                controlBox1.LockEnable = value;
                this.Invalidate();
            }
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
      //  #region  窗体缩放
      //  private const int WM_NCHITTEST = 0x0084; //鼠标在窗体客户区（除标题栏和边框以外的部分）时发送的信息
      //  const int HTLEFT = 10;  //左变
      //  const int HTRIGHT = 11;  //右边
      //  const int HTTOP = 12;
      //  const int HTTOPLEFT = 13;  //左上
      //  const int HTTOPRIGHT = 14; //右上
      //  const int HTBOTTOM = 15;  //下
      //  const int HTBOTTOMLEFT = 0x10;  //左下
      //  const int HTBOTTOMRIGHT = 17;  //右下
      //  System.Drawing.Point vPoint = System.Drawing.Point.Empty;
      ////  自定义边框拉伸
      //  protected override void WndProc(ref Message m)
      //  {
      //      try
      //      {
      //          base.WndProc(ref m);
      //          switch (m.Msg)
      //          {
      //              case WM_NCHITTEST:
      //                  vPoint = new System.Drawing.Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
      //                  vPoint = PointToClient(vPoint);
      //                  if (vPoint.X <= 5)
      //                      if (vPoint.Y <= 5)
      //                          m.Result = (IntPtr)HTTOPLEFT;  //左上
      //                      else if (vPoint.Y >= this.ClientSize.Height - 5)
      //                          m.Result = (IntPtr)HTBOTTOMLEFT; //左下
      //                      else
      //                          m.Result = (IntPtr)HTLEFT;  //左边
      //                  else if (vPoint.X >= this.ClientSize.Width - 5)
      //                      if (vPoint.Y <= 5)
      //                          m.Result = (IntPtr)HTTOPRIGHT;  //右上
      //                      else if (vPoint.Y >= this.ClientSize.Height - 5)
      //                          m.Result = (IntPtr)HTBOTTOMRIGHT;  //右下
      //                      else
      //                          m.Result = (IntPtr)HTRIGHT;  //右
      //                  else if (vPoint.Y <= 5)
      //                      m.Result = (IntPtr)HTTOP;  //上
      //                  else if (vPoint.Y >= this.ClientSize.Height - 5)
      //                      m.Result = (IntPtr)HTBOTTOM; //下

      //                  else
      //                  {
      //                      base.WndProc(ref m);//如果去掉这一行代码,窗体将失去MouseMove..等事件
      //                      System.Drawing.Point lpint = new System.Drawing.Point((int)m.LParam);//可以得到鼠标坐标,这样就可以决定怎么处理这个消息了,是移动窗体,还是缩放,以及向哪向的缩放

      //                      m.Result = (IntPtr)0x2;//托动HTCAPTION=2 <0x2>
      //                  }
      //                  break;
      //          }
      //      }
      //      catch { }
      //  }
      //  #endregion
   


        private void Frm_ToolBase_FormClosing(object sender, FormClosingEventArgs e)
        { 
          
           
        }

    private void ButtonLock_Click(object sender, EventArgs e)
        {
            if (this.TopMost)
            {
                this.TopMost = false;
            }
            else
            {
                this.TopMost = true;
            }
            XXXXXXXXXXXXXlbl_title.Focus();
        }

        private void ButtonMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        
        private void ButtonMax_Click(object sender, EventArgs e)
        {
            if (this.MinimumSize == this.MaximumSize && this.MinimumSize != new Size(0, 0))
            {
                controlBox1.MaxEnable = false;
                return;
            }

            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;

            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;

            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
        
            this.Close();
        }
    }
}
