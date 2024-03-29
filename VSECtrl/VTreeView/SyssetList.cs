using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace System.Windows.Forms
{
    public class SyssetList : TreeView
    {
        public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler WMScroll;

        protected void OnWMScroll(object sender, EventArgs e)
        {
            if (WMScroll != null)
                WMScroll(sender, e);
        }

       
        public SyssetList()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();

        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SyssetList
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HotTracking = true;
            this.LineColor = System.Drawing.Color.Black;
            this.ShowLines = false;
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
        private SolidBrush brush1 = new SolidBrush(Color.FromArgb(70, 70, 70));//填充颜色
        private Pen pen1 = new Pen(Color.FromArgb(70, 70, 70), 1);//边框颜色
        /*3 MouseMove的时候 画光标所在的节点的背景*/
        private SolidBrush brush3 = new SolidBrush(Color.FromArgb(50, 50, 50));
        private Pen pen3 = new Pen(Color.FromArgb(55, 55, 55), 1);
        private Pen pen4 = new Pen(Color.FromArgb(40, 40, 40), 1);
        private SolidBrush brushS = new SolidBrush(Color.MediumSpringGreen);//填充颜色
        #endregion

        private void TV_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            #region 1     选中的节点背景=========================================
            Rectangle nodeRect = new Rectangle(1,
                                                e.Bounds.Top,
                                                e.Bounds.Width - 3,
                                                e.Bounds.Height -1);
            Rectangle nodeRect1 = new Rectangle(e.Bounds.Width - 10,
                                                e.Bounds.Top,
                                                e.Bounds.Width - 3,
                                                e.Bounds.Height);
            if (e.Node.IsSelected)
            {
                //TreeView有焦点的时候 画选中的节点
               
                    e.Graphics.FillRectangle(brush1, nodeRect);
                    e.Graphics.DrawRectangle(pen1, nodeRect);
                    e.Graphics.FillRectangle(brushS, nodeRect1);
            }
            else if ((e.State & TreeNodeStates.Hot) != 0 && e.Node.Text != "")//|| currentMouseMoveNode == e.Node)
            {
                e.Graphics.FillRectangle(brush3, nodeRect);
                e.Graphics.DrawRectangle(pen3, nodeRect);
            }
            else
            {
                
               e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(35, 35, 35)), e.Bounds);
               e.Graphics.DrawRectangle(pen4, nodeRect);
               
               
            }
            #endregion

            

            #region 2 画节点文本=========================================
            Rectangle nodeTextRect = new Rectangle(
                                                    e.Node.Bounds.Left+10,
                                                    e.Node.Bounds.Top +10,
                                                    e.Node.Bounds.Width + 100,
                                                    e.Node.Bounds.Height
                                                    );


            e.Graphics.DrawString(e.Node.Text,
                                  e.Node.TreeView.Font,
                                  new SolidBrush(this.ForeColor),
                                  nodeTextRect);
            
                #endregion

            //    #region 4   画IImageList 中的图标===================================================================

            //    int currt_X = e.Node.Bounds.X;
            //if (this.ImageList != null && this.ImageList.Images.Count > 0)
            //{
            //    //图标大小16*16
            //    Rectangle imagebox = new Rectangle(
            //        e.Node.Bounds.X - 3 - 24,
            //        e.Node.Bounds.Y + 3,
            //        this.ImageList.ImageSize.Width,//IMAGELIST IMAGE WIDTH
            //       this.ImageList.ImageSize.Height);//HEIGHT
            //    int k = 0;
            //    if (e.Node.ImageIndex!=-1)
            //    {
            //        k = e.Node.ImageIndex;
            //    }
                
      
            //        e.Graphics.DrawImage(this.ImageList.Images[k], imagebox);
                
            //    currt_X -= 19;

            //    /*测试 画IMAGELIST的矩形*/
            //    //if (e.Node.ImageIndex > 0)
            //    //    e.Graphics.DrawRectangle(new Pen(Color.Black, 1), imagebox);
            //}
            //#endregion
  

        }
      
    
        #endregion
       
    }
}