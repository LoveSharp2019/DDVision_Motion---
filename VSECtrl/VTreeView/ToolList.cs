using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace System.Windows.Forms
{
    public class ToolList : TreeView
    {
        public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler WMScroll;

        protected void OnWMScroll(object sender, EventArgs e)
        {
            if (WMScroll != null)
                WMScroll(sender, e);
        }


        public ToolList()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();

        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolList));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.vScrollbarComponent1 = new VControls.VScrollBar.VScrollbarComponent(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "O1.png");
            this.imageList1.Images.SetKeyName(1, "O2.png");
            this.imageList1.Images.SetKeyName(2, "C1.png");
            this.imageList1.Images.SetKeyName(3, "C2.png");
            // 
            // ToolList
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HotTracking = true;
            this.LineColor = System.Drawing.Color.Black;
            this.ShowLines = false;
           // this.vScrollbarComponent1.SetUserCustomScrollbar(this, true);
            this.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.TV_DrawNode);
            this.ResumeLayout(false);

        }
        #region 双缓存重绘


        private void UpdateExtendedStyles()
        {
            int Style = 0;

            if (DoubleBuffered)
                Style |= TVS_EX_DOUBLEBUFFER;

            if (Style != 0)
                NativeInterop.SendMessage(Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)Style);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateExtendedStyles();
            if (!NativeInterop.IsWinXP)
                NativeInterop.SendMessage(Handle, TVM_SETBKCOLOR, IntPtr.Zero, (IntPtr)ColorTranslator.ToWin32(BackColor));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint))
            {
                Message m = new Message();
                m.HWnd = Handle;
                m.Msg = NativeInterop.WM_PRINTCLIENT;
                m.WParam = e.Graphics.GetHdc();
                m.LParam = (IntPtr)NativeInterop.PRF_CLIENT;
                DefWndProc(ref m);
                e.Graphics.ReleaseHdc(m.WParam);
            }
            base.OnPaint(e);
        }
        #endregion

        protected override void WndProc(ref Message m)
        {
            if (Win32API.IsHorizontalScrollBarVisible(this))
                Win32API.ShowScrollBar(this.Handle, 0, false);

            if (m.Msg == WM_VSCROLL || m.Msg == WM_HSCROLL || m.Msg == WM_MOUSEWHEEL)
                OnWMScroll(new object(), new EventArgs());
            base.WndProc(ref m);
        }



        private const int WM_VSCROLL = 0x0115;
        private const int WM_HSCROLL = 0x0114;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int TV_FIRST = 0x1100;
        private const int TVM_SETBKCOLOR = TV_FIRST + 29;
        private const int TVM_SETEXTENDEDSTYLE = TV_FIRST + 44;
        private const int TVS_EX_DOUBLEBUFFER = 0x0004;
        #region 节点重绘
        #region 三种不同状态的颜色
        /*1节点被选中 ,TreeView有焦点*/
        private SolidBrush brush1 = new SolidBrush(Color.FromArgb(60, 60, 60));//填充颜色
        private Pen pen1 = new Pen(Color.FromArgb(80, 80, 80), 1);//边框颜色

        /*2节点被选中 ,TreeView没有焦点*/
        private SolidBrush brush2 = new SolidBrush(Color.FromArgb(45, 45, 45));
        private Pen pen2 = new Pen(Color.FromArgb(60, 60, 60), 1);

        /*3 MouseMove的时候 画光标所在的节点的背景*/
        private SolidBrush brush3 = new SolidBrush(Color.FromArgb(50, 50, 50));
        private ImageList imageList1;
        private System.ComponentModel.IContainer components;
        private Pen pen3 = new Pen(Color.FromArgb(65, 65, 65), 1);
        private VControls.VScrollBar.VScrollbarComponent vScrollbarComponent1;
        private Pen pen4 = new Pen(Color.FromArgb(50, 50, 50), 1);
        #endregion

        private void TV_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            #region 1     选中的节点背景=========================================
            Rectangle nodeRect = new Rectangle(1,
                                                e.Bounds.Top,
                                                e.Bounds.Width - 2,
                                                e.Bounds.Height - 1);

            if (e.Node.IsSelected)
            {
                //TreeView有焦点的时候 画选中的节点
                if (this.Focused)
                {
                    e.Graphics.FillRectangle(brush1, nodeRect);
                    e.Graphics.DrawLine(pen1, new Point(1, e.Bounds.Bottom),new Point(e.Bounds.Width - 3, e.Bounds.Bottom));

                    /*测试 画聚焦的边框*/
                    //ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds, Color.Black, SystemColors.Highlight);
                }
                /*TreeView失去焦点的时候 */
                else
                {
                    e.Graphics.FillRectangle(brush2, nodeRect);
                    e.Graphics.DrawLine(pen2, new Point(1, e.Bounds.Bottom), new Point(e.Bounds.Width - 3, e.Bounds.Bottom));
                }
            }
            else if ((e.State & TreeNodeStates.Hot) != 0 && e.Node.Text != "")//|| currentMouseMoveNode == e.Node)
            {
                e.Graphics.FillRectangle(brush3, nodeRect);
                e.Graphics.DrawLine(pen3, new Point(1, e.Bounds.Bottom), new Point(e.Bounds.Width - 3, e.Bounds.Bottom));
            }
            else
            {
                if (e.Node.Level==0)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(35, 35, 35)), e.Bounds);
                    e.Graphics.DrawLine(pen4, new Point(1, e.Bounds.Bottom-1), new Point(e.Bounds.Width - 3, e.Bounds.Bottom-1));
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.Bounds);
                }
               
            }
            #endregion

            

            #region 3     画节点文本=========================================
            Rectangle nodeTextRect = new Rectangle(
                                                    e.Node.Bounds.Left,
                                                    e.Node.Bounds.Top +4,
                                                    e.Node.Bounds.Width + 2,
                                                    e.Node.Bounds.Height
                                                    );
            nodeTextRect.Width += 4;
            nodeTextRect.Height -= 4;

            e.Graphics.DrawString(e.Node.Text,
                                  e.Node.TreeView.Font,
                                  new SolidBrush(this.ForeColor),
                                  nodeTextRect);

            #endregion

            #region 4   画IImageList 中的图标===================================================================

            int currt_X = e.Node.Bounds.X;
            if (this.ImageList != null && this.ImageList.Images.Count > 0)
            {
                //图标大小16*16
                Rectangle imagebox = new Rectangle(
                    e.Node.Bounds.X - 3 - 24,
                    e.Node.Bounds.Y + 3,
                    24,//IMAGELIST IMAGE WIDTH
                    24);//HEIGHT


                int index = e.Node.ImageIndex;
                string imagekey = e.Node.ImageKey;
                if (imagekey != "" && this.ImageList.Images.ContainsKey(imagekey))
                    e.Graphics.DrawImage(this.ImageList.Images[imagekey], imagebox);
                else
                {
                    if (e.Node.ImageIndex < 0)
                        index = 0;
                    else if (index > this.ImageList.Images.Count - 1)
                        index = 0;
                    e.Graphics.DrawImage(this.ImageList.Images[index], imagebox);
                }
                currt_X -= 19;

                /*测试 画IMAGELIST的矩形*/
                //if (e.Node.ImageIndex > 0)
                //    e.Graphics.DrawRectangle(new Pen(Color.Black, 1), imagebox);
            }
            #endregion
  

        }
      
    
        #endregion
       
    }
}