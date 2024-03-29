using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace System.Windows.Forms
{
    public class TaskEdit : TreeView
    {
        public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler WMScroll;

        protected void OnWMScroll(object sender, EventArgs e)
        {
            if (WMScroll != null)
                WMScroll(sender, e);
        }


        public TaskEdit()
        {
            // Enable default double buffering processing
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();

        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskEdit));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
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
            // TaskEdit
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FullRowSelect = true;
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
#pragma warning disable CS0649 // 从未对字段“TaskEdit.treeNode1”赋值，字段将一直保持其默认值 null
        TreeNode treeNode1;
#pragma warning restore CS0649 // 从未对字段“TaskEdit.treeNode1”赋值，字段将一直保持其默认值 null
#pragma warning disable CS0649 // 从未对字段“TaskEdit.pt”赋值，字段将一直保持其默认值
        Point pt;
#pragma warning restore CS0649 // 从未对字段“TaskEdit.pt”赋值，字段将一直保持其默认值
        #region 三种不同状态的颜色
        /*1节点被选中 ,TreeView有焦点*/
        private SolidBrush brush1 = new SolidBrush(Color.FromArgb(80, 80, 80));//填充颜色
        private Pen pen1 = new Pen(Color.FromArgb(90, 90, 90), 1);//边框颜色

        /*2节点被选中 ,TreeView没有焦点*/
        private SolidBrush brush2 = new SolidBrush(Color.FromArgb(45, 45, 45));
        private Pen pen2 = new Pen(Color.FromArgb(50, 50, 50), 1);

        /*3 MouseMove的时候 画光标所在的节点的背景*/
        private SolidBrush brush3 = new SolidBrush(Color.FromArgb(50, 50, 50));
        private ImageList imageList1;
        private System.ComponentModel.IContainer components;
        private Pen pen3 = new Pen(Color.FromArgb(55, 55, 55), 1);

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
                    e.Graphics.DrawLine(pen1, new Point(1, e.Bounds.Bottom), new Point(e.Bounds.Width - 2, e.Bounds.Bottom));

                    /*测试 画聚焦的边框*/
                    //ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds, Color.Black, SystemColors.Highlight);
                }
                /*TreeView失去焦点的时候 */
                else
                {
                    e.Graphics.FillRectangle(brush2, nodeRect);
                    e.Graphics.DrawLine(pen2, new Point(1, e.Bounds.Bottom), new Point(e.Bounds.Width - 2, e.Bounds.Bottom));
                }
            }
            else if ((e.State & TreeNodeStates.Hot) != 0 && e.Node.Text != "")//|| currentMouseMoveNode == e.Node)
            {
                e.Graphics.FillRectangle(brush3, nodeRect);
                e.Graphics.DrawLine(pen3, new Point(1, e.Bounds.Bottom), new Point(e.Bounds.Width - 2, e.Bounds.Bottom));
            }
            else
            {
                if (e.Node.Level == 0)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(35, 35, 35)), e.Bounds);
                    e.Graphics.DrawLine(pen3, new Point(1, e.Bounds.Bottom-1), new Point(e.Bounds.Width - 2, e.Bounds.Bottom-1));
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.Bounds);
                }

            }
            #endregion

            #region 2     +-号绘制=========================================
            Rectangle plusRect = new Rectangle(e.Node.Bounds.Left - 42, nodeRect.Top + 4, 16, 16); // +-号的大小 是9 * 9
            if (e.Node.IsExpanded && e.Node.Nodes.Count > 0)
            {
                //treeView1.Refresh();
                for (int i = 0; i < this.Nodes.Count; i++)
                    e.Graphics.DrawImage(imageList1.Images[0], plusRect);
            }
            else if (e.Node.IsExpanded == false && e.Node.Nodes.Count > 0)
            {
                //treeView1.Refresh();
                for (int i = 0; i < this.Nodes.Count; i++)
                {

                    e.Graphics.DrawImage(imageList1.Images[2], plusRect);

                }

            }

            TreeViewHitTestInfo info = this.HitTest(pt);
            if (treeNode1 != null && info.Location == TreeViewHitTestLocations.PlusMinus && treeNode1 == e.Node)
            {
                if (treeNode1.IsExpanded && treeNode1.Nodes.Count > 0)
                    e.Graphics.DrawImage(imageList1.Images[1], plusRect);
                else if (treeNode1.IsExpanded == false && treeNode1.Nodes.Count > 0)
                    e.Graphics.DrawImage(imageList1.Images[3], plusRect);
            }


            /*测试用 画出+-号出现的矩形*/
            //if (e.Node.Nodes.Count > 0)
            //    e.Graphics.DrawRectangle(new Pen(Color.Red), plusRect);
            #endregion

            #region 3     画节点文本=========================================
            Rectangle nodeTextRect = new Rectangle(
                                                    e.Node.Bounds.Left,
                                                    e.Node.Bounds.Top + 4,
                                                    e.Node.Bounds.Width + 2,
                                                    e.Node.Bounds.Height
                                                    );
            nodeTextRect.Width += 4;
            nodeTextRect.Height -= 4;

            e.Graphics.DrawString(e.Node.Text,
                                  e.Node.TreeView.Font,
                                  new SolidBrush(this.ForeColor),
                                  nodeTextRect);


            ////画子节点上文件的个数 (111)
            if (e.Node.Text != "")
            {

                nodeTextRect.Width += 4;
                nodeTextRect.Height -= 4;
                //画子节点个数 (111)
                if (e.Node.Level == 0 && e.Node.Text != "" && nodeTextRect.Right > 20)
                {
                    if (e.Node.ToolTipText != null)
                    {
                        string[] str = e.Node.ToolTipText.Split('：');
                        if (str.Length>3)
                        {
                            if (str[1].Contains("成功"))
                            {
                                e.Graphics.DrawString(string.Format("✅  {0}", str[2].Split('\r')[0]),
                                            new Font("Arial", 10),
                                            Brushes.LightGray,
                                            e.Bounds.Width - 80,
                                            nodeTextRect.Top + 2);
                            }
                            else
                            {
                                e.Graphics.DrawString(string.Format("❌  {0}ms", e.Node.Tag == null ? "0" : e.Node.Tag.ToString()),
                                            new Font("Arial", 10),
                                            Brushes.Red,
                                            e.Bounds.Width - 80,
                                            nodeTextRect.Top + 2);
                            }

                        }
                        else
                        {
                            if (str[0] == "未启用")
                            {
                                e.Graphics.DrawString(string.Format("※未启用"),
                                           new Font("宋体", 10),
                                           Brushes.IndianRed,
                                           e.Bounds.Width - 80,
                                           nodeTextRect.Top + 2);
                            }
                            else
                            {
                                e.Graphics.DrawString(string.Format("❌  0ms"),
                                           new Font("Arial", 10),
                                           Brushes.Red,
                                           e.Bounds.Width - 80,
                                           nodeTextRect.Top + 2);
                            }
                           
                        }
                    }
                    
                }
                //System.Diagnostics.Debug.WriteLine(nodeTextRect.Right - 4);
            }

            Rectangle r = e.Node.Bounds;
            r.Height -= 2;
            ///*测试用，画文字出现的矩形*/
            //if (e.Node.Text != "")
            //    e.Graphics.DrawRectangle(new Pen(Color.Blue), r);
            #endregion

            #region 4   画IImageList 中的图标===================================================================

            int currt_X = e.Node.Bounds.X;
            if (this.ImageList != null && this.ImageList.Images.Count > 0)
            {
                //图标大小16*16
                Rectangle imagebox = new Rectangle(
                    e.Node.Bounds.X - 3 - 16,
                    e.Node.Bounds.Y + 3,
                    16,//IMAGELIST IMAGE WIDTH
                    16);//HEIGHT


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
            ;
            if (false)
            {
#pragma warning disable CS0162 // 检测到无法访问的代码
                DrawLine(this.CreateGraphics());
#pragma warning restore CS0162 // 检测到无法访问的代码
            }


        }
        private void numericUpDown1_MouseWheel(object sender, MouseEventArgs e)
        {
           
                HandledMouseEventArgs h = e as HandledMouseEventArgs;
                if (h != null)
                {
                    h.Handled = true;
                }


        } 
        /// <summary>
        /// 正在绘制输入输出指向线
        /// </summary>
        public bool isDrawing = false;
        /// <summary>
        /// 流程树中节点的最大长度
        /// </summary>
#pragma warning disable CS0414 // 字段“TaskEdit.maxLength”已被赋值，但从未使用过它的值
        private int maxLength = 130;
#pragma warning restore CS0414 // 字段“TaskEdit.maxLength”已被赋值，但从未使用过它的值
        /// <summary>
        /// 记录起始节点和此节点的列坐标值
        /// </summary>
        private static Dictionary<TreeNode, Color> startNodeAndColor = new Dictionary<TreeNode, Color>();

        /// <summary>
        /// 每一个列坐标值对应一种颜色
        /// </summary>
        private Dictionary<int, Color> colValueAndColor = new Dictionary<int, Color>();
        /// <summary>
        /// 输入输出指向线的颜色数组
        /// </summary>
        private static Color[] color = new Color[] { Color.LightGreen, Color.Orange, Color.Green, Color.Red, Color.Green, Color.Brown, Color.LightGreen, Color.Green, Color.Red, Color.Green, Color.Orange, Color.Brown, Color.LightGreen, Color.Green, Color.Red, Color.Green, Color.Orange, Color.Brown, Color.LightGreen, Color.Green, Color.Red, Color.Green, Color.Orange, Color.Brown, Color.LightGreen, Color.Green, Color.Red, Color.Green, Color.Orange, Color.Brown };
        /// <summary>
        /// 需要连线的节点对，不停的画连线，注意键值对中第一个为连线的结束节点，第二个为起始节点，一个输出可能连接多个输入，而键值对中的键不能重复，所以把源作为值，输入作为键
        /// </summary>
        public Dictionary<TreeNode, TreeNode> D_itemAndSource = new Dictionary<TreeNode, TreeNode>();
        /// <summary>
        /// 记录前面的划线所跨越的列段，
        /// </summary>
        private Dictionary<int, Dictionary<TreeNode, TreeNode>> RecordLineCol = new Dictionary<int, Dictionary<TreeNode, TreeNode>>();
        private void CreateLine(TaskEdit treeview, TreeNode endNode, TreeNode startNode,Graphics graphics)
        {
            int maxLength = 150;
            try
            {
                //得到起始与结束节点之间所有节点的最大长度  ，保证画线不穿过节点
                int startNodeParantIndex = startNode.Parent.Index;
                int endNodeParantIndex = endNode.Parent.Index;
                int startNodeIndex = startNode.Index;
                int endNodeIndex = endNode.Index;
                int max = 0;

                if (!startNode.Parent.IsExpanded)
                {
                    max = startNode.Parent.Bounds.X + startNode.Parent.Bounds.Width;
                }
                else
                {
                    for (int i = startNodeIndex; i < startNode.Parent.Nodes.Count - 1; i++)
                    {
                        if (max < treeview.Nodes[startNodeParantIndex].Nodes[i].Bounds.X + treeview.Nodes[startNodeParantIndex].Nodes[i].Bounds.Width)
                            max = treeview.Nodes[startNodeParantIndex].Nodes[i].Bounds.X + treeview.Nodes[startNodeParantIndex].Nodes[i].Bounds.Width;
                    }
                }
                for (int i = startNodeParantIndex + 1; i < endNodeParantIndex; i++)
                {
                    if (!treeview.Nodes[i].IsExpanded)
                    {
                        if (max < treeview.Nodes[i].Bounds.X + treeview.Nodes[i].Bounds.Width)
                            max = treeview.Nodes[i].Bounds.X + treeview.Nodes[i].Bounds.Width;
                    }
                    else
                    {
                        for (int j = 0; j < treeview.Nodes[i].Nodes.Count; j++)
                        {
                            if (max < treeview.Nodes[i].Nodes[j].Bounds.X + treeview.Nodes[i].Nodes[j].Bounds.Width)
                                max = treeview.Nodes[i].Nodes[j].Bounds.X + treeview.Nodes[i].Nodes[j].Bounds.Width;
                        }
                    }
                }
                if (!endNode.Parent.IsExpanded)
                {
                    if (max < endNode.Parent.Bounds.X + endNode.Parent.Bounds.Width)
                        max = endNode.Parent.Bounds.X + endNode.Parent.Bounds.Width;
                }
                else
                {
                    for (int i = 0; i < endNode.Index; i++)
                    {
                        if (max < treeview.Nodes[endNodeParantIndex].Nodes[i].Bounds.X + treeview.Nodes[endNodeParantIndex].Nodes[i].Bounds.Width)
                            max = treeview.Nodes[endNodeParantIndex].Nodes[i].Bounds.X + treeview.Nodes[endNodeParantIndex].Nodes[i].Bounds.Width;
                    }
                }
                max += 20;        //箭头不能连着节点，

                if (!startNode.Parent.IsExpanded)
                    startNode = startNode.Parent;
                if (!endNode.Parent.IsExpanded)
                    endNode = endNode.Parent;

                if (endNode.Bounds.X + endNode.Bounds.Width + 20 > max)
                    max = endNode.Bounds.X + endNode.Bounds.Width + 20;
                if (startNode.Bounds.X + startNode.Bounds.Width + 20 > max)
                    max = startNode.Bounds.X + startNode.Bounds.Width + 20;

                //判断是否可以在当前处划线
                foreach (KeyValuePair<int, Dictionary<TreeNode, TreeNode>> item in RecordLineCol)
                {
                    if (Math.Abs(max - item.Key) < 10)
                    {
                        foreach (KeyValuePair<TreeNode, TreeNode> item1 in item.Value)
                        {
                            if (startNode != item1.Value)
                            {
                                if ((item1.Value.Bounds.X < maxLength && item1.Key.Bounds.X < maxLength) || (item1.Value.Bounds.X < maxLength && item1.Key.Bounds.X < maxLength))
                                {
                                    if (item1.Value.Bounds.Y > startNode.Bounds.Y || item1.Key.Bounds.Y > startNode.Bounds.Y)    //20200612加
                                        max += (10 - Math.Abs(max - item.Key));
                                }
                            }
                        }
                    }
                }

                Dictionary<TreeNode, TreeNode> temp = new Dictionary<TreeNode, TreeNode>();
                temp.Add(endNode, startNode);
                if (!RecordLineCol.ContainsKey(max))
                    RecordLineCol.Add(max, temp);
                else
                    RecordLineCol[max].Add(endNode, startNode);

                if (!startNodeAndColor.ContainsKey(startNode))
                    startNodeAndColor.Add(startNode, color[startNodeAndColor.Count]);

                Pen pen = new Pen(startNodeAndColor[startNode], 1);
                Brush brush = new SolidBrush(startNodeAndColor[startNode]);

                graphics.DrawLine(pen, startNode.Bounds.X + startNode.Bounds.Width,
                    startNode.Bounds.Y + startNode.Bounds.Height / 2,
                max,
                  startNode.Bounds.Y + startNode.Bounds.Height / 2);
                graphics.DrawLine(pen, max,
                   startNode.Bounds.Y + startNode.Bounds.Height / 2,
                   max,
                  endNode.Bounds.Y + endNode.Bounds.Height / 2);
                graphics.DrawLine(pen, max,
                   endNode.Bounds.Y + endNode.Bounds.Height / 2,
                   endNode.Bounds.X + endNode.Bounds.Width,
                     endNode.Bounds.Y + endNode.Bounds.Height / 2);
                graphics.DrawString("<", new Font("微软雅黑", 12F), brush, endNode.Bounds.X + endNode.Bounds.Width - 5,
                     endNode.Bounds.Y + endNode.Bounds.Height / 2 - 12);
                Application.DoEvents();
            }
            catch { }
        }
        internal void DrawLine(Graphics graphics)
        {
            try
            {
                if (this.ShowLines)
                {
                    
                        this.MouseWheel += new MouseEventHandler(numericUpDown1_MouseWheel);          //划线的时候不能滚动，否则画好了线，结果已经滚到其它地方了
                        maxLength = 150;
                        colValueAndColor.Clear();
                        startNodeAndColor.Clear();
                        RecordLineCol.Clear();
                       
                        

                        foreach (KeyValuePair<TreeNode, TreeNode> item in D_itemAndSource)
                        {
                            CreateLine(this, item.Key, item.Value, graphics);
                        }
                        Application.DoEvents();
                        this.MouseWheel -= new MouseEventHandler(numericUpDown1_MouseWheel);
                        //Job.GetJobTree(jobName).Refresh();
                       

                  
                }
            }
#pragma warning disable CS0168 // 声明了变量“ex”，但从未使用过
            catch (Exception ex)
#pragma warning restore CS0168 // 声明了变量“ex”，但从未使用过
            {
                //////Log.SaveError(ex);
            }
        }
        #endregion
       
    }
}