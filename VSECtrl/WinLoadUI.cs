//************************************************************************************
//     Filename:      WinLoadUI.cs
//      Version:      V1.0.0.0
//       Create:      Lxc
//  Create time:      2018-10-8 17:06
//   Descrption:      窗体载入界面定义
//
//************************************************************************************
using LXCSystem.Control.Base.Common;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace VControls
{  /// <summary>
   /// 窗体载入界面定义
   /// </summary>
    public partial class WinLoadUI : Form
    {
        public WinLoadUI()
        {
            if (VSE.Core.Dog.CheckActive() != 0)
            {
                return;
            }
            InitializeComponent();
            
            
        }
        /// <summary>
        /// 运行参数
        /// </summary>
        private static class RunParametereter
        {
            public static string Status { get; set; }
            public static int StartNum { get; set; }
            public static int EndNum { get; set; }
            public static int CostTime { get; set; }
            public static float CurrentNum { get; set; }
        }
        private Color mColor = Color.White;
        private void SetPerPixelBitmapFilename()
        {
                Graphics g =this.CreateGraphics();
                g.FillRectangle(new SolidBrush(Color.PaleGreen), new Rectangle(ProgressBarPosition.X, ProgressBarPosition.Y, 900, 30));
                g.Dispose();
              
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00080000; // This form has to have the WS_EX_LAYERED extended style
                return cp;
            }
        }

        /// <summary>
        /// 响应鼠标事件
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0084 /*WM_NCHITTEST*/)
            {
                m.Result = (IntPtr)2;	// HTCLIENT
                return;
            }
            base.WndProc(ref m);
        }

        #region 函数


        #region 与主界面数据互动
        public void UpdateProgress(string status, int startNum, int endNum, int costTime)
        {
            RunParametereter.Status = status;
            RunParametereter.StartNum = startNum;
            RunParametereter.EndNum = endNum;
            RunParametereter.CostTime = costTime;
            UpdateProgressBackground();
        }

        private void UpdateProgressBackground()
        {
            int frame = 0;
            if (RunParametereter.EndNum - RunParametereter.StartNum < 0)
            {
                int temp = RunParametereter.StartNum;
                RunParametereter.StartNum = RunParametereter.EndNum;
                RunParametereter.EndNum = temp;
            }
            if (RunParametereter.StartNum == RunParametereter.EndNum)
            {
                frame = 1;
            }
            else
            {
                frame = (int)(RunParametereter.CostTime / (RunParametereter.EndNum - RunParametereter.StartNum) * 0.8);
            }
            for (int i = RunParametereter.StartNum; i <= RunParametereter.EndNum; i++)
            {
                UpdateStatusToForm(RunParametereter.Status, i, frame);
            }
        }
        Point ProgressBarPosition = new Point(2, 614);
        private void UpdateStatusToForm(string status, int currentNum, int waitTime)
        {

            Bitmap temp = (Bitmap)this.BackgroundImage.Clone();
            Graphics g = Graphics.FromImage(temp);
            g.DrawString(currentNum.ToString("0'%'"), GetFont(48), new SolidBrush(mColor), new PointF(450, 490));
            g.DrawString(status, GetFont(18), new SolidBrush(mColor), new PointF(ProgressBarPosition.X+415, ProgressBarPosition.Y - 45));
            Color c = new Color();
            if (currentNum < 30)
            {
                c = Color.Gray;
            }
            else if (currentNum <80)
            {
                c = Color.FromArgb(192, 255, 192);
            }
            else
            {
                c = Color.FromArgb(0, 192, 192);
            }
          
            g.FillRectangle(new SolidBrush(c), new Rectangle(ProgressBarPosition.X, ProgressBarPosition.Y, 900 * currentNum / 100, 5));
            
            g.Dispose();

            SetBitmap(temp);
            if (temp != null)
                temp.Dispose();

            System.Threading.Thread.Sleep(waitTime);
            Application.DoEvents();
        }
        #endregion
        #region 设置背景图像
        private void SetBitmap(Bitmap bitmap)
        {
            SetBitmap(bitmap, 255);
        }
        private void SetBitmap(Bitmap bitmap, byte opacity)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");

            // The idea of this is very simple,
            // 1. Create a compatible DC with screen;
            // 2. Select the bitmap with 32bpp with alpha-channel in the compatible DC;
            // 3. Call the UpdateLayeredWindow.

            IntPtr screenDc = GDI.GetDC(IntPtr.Zero);
            IntPtr memDc = GDI.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));  // grab a GDI handle from this GDI+ bitmap
                oldBitmap = GDI.SelectObject(memDc, hBitmap);

                GDI.Size size = new GDI.Size(bitmap.Width, bitmap.Height);
                GDI.Point pointSource = new GDI.Point(0, 0);
                GDI.Point topPos = new GDI.Point(Left, Top);
                GDI.BLENDFUNCTION blend = new GDI.BLENDFUNCTION();
                blend.BlendOp = GDI.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = opacity;
                blend.AlphaFormat = GDI.AC_SRC_ALPHA;

                GDI.UpdateLayeredWindow(Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, GDI.ULW_ALPHA);
            }
            finally
            {
                GDI.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    GDI.SelectObject(memDc, oldBitmap);
                    //Windows.DeleteObject(hBitmap);
                    // The documentation says that we have to use the Windows.DeleteObject... but since there is no such method I use the normal DeleteObject from GDI GDI and it's working fine without any resource leak.
                    GDI.DeleteObject(hBitmap);
                }
                GDI.DeleteDC(memDc);
            }
        }
        #endregion
        #region 设置字体
        private Font GetFont(float size)
        {
            return new System.Drawing.Font("微软雅黑", size, GraphicsUnit.Pixel);
        }
        #endregion

        #endregion

        private void WinLoadUI_Load(object sender, EventArgs e)
        {
            SetPerPixelBitmapFilename();
        }
    }
}
