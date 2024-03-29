namespace VSE
{
    partial class Win_ToolBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_ToolBox));
            this.lbl_toolInfo = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.折叠所有ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.展开所有ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tvw_tools = new System.Windows.Forms.ToolList();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_toolInfo
            // 
            this.lbl_toolInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_toolInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_toolInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_toolInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_toolInfo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_toolInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lbl_toolInfo.ImageIndex = 0;
            this.lbl_toolInfo.Location = new System.Drawing.Point(1, 678);
            this.lbl_toolInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_toolInfo.MaximumSize = new System.Drawing.Size(2333, 56);
            this.lbl_toolInfo.Name = "lbl_toolInfo";
            this.lbl_toolInfo.Size = new System.Drawing.Size(279, 56);
            this.lbl_toolInfo.TabIndex = 21;
            this.lbl_toolInfo.Text = "说明：无";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.折叠所有ToolStripMenuItem,
            this.展开所有ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(100, 48);
            // 
            // 折叠所有ToolStripMenuItem
            // 
            this.折叠所有ToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.折叠所有ToolStripMenuItem.Name = "折叠所有ToolStripMenuItem";
            this.折叠所有ToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.折叠所有ToolStripMenuItem.Text = "折叠所有";
            this.折叠所有ToolStripMenuItem.Click += new System.EventHandler(this.折叠所有ToolStripMenuItem_Click);
            // 
            // 展开所有ToolStripMenuItem
            // 
            this.展开所有ToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.展开所有ToolStripMenuItem.Name = "展开所有ToolStripMenuItem";
            this.展开所有ToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.展开所有ToolStripMenuItem.Text = "展开所有";
            this.展开所有ToolStripMenuItem.Click += new System.EventHandler(this.展开所有ToolStripMenuItem_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "C2.png");
            this.imageList.Images.SetKeyName(1, "O2.png");
            this.imageList.Images.SetKeyName(2, "输入.png");
            this.imageList.Images.SetKeyName(3, "输出.png");
            this.imageList.Images.SetKeyName(4, "101采集图像.png");
            this.imageList.Images.SetKeyName(5, "102保存图像.png");
            this.imageList.Images.SetKeyName(6, "103二值化.png");
            this.imageList.Images.SetKeyName(7, "104形态学.png");
            this.imageList.Images.SetKeyName(8, "105滤波.png");
            this.imageList.Images.SetKeyName(9, "106图像分解.png");
            this.imageList.Images.SetKeyName(10, "107查找区域.png");
            this.imageList.Images.SetKeyName(11, "108查找区域.png");
            this.imageList.Images.SetKeyName(12, "109区域检测.png");
            this.imageList.Images.SetKeyName(13, "201棋盘格.png");
            this.imageList.Images.SetKeyName(14, "202标定.png");
            this.imageList.Images.SetKeyName(15, "203空间转换.png");
            this.imageList.Images.SetKeyName(16, "301匹配.png");
            this.imageList.Images.SetKeyName(17, "302斑点.png");
            this.imageList.Images.SetKeyName(18, "303边.png");
            this.imageList.Images.SetKeyName(19, "304圆.png");
            this.imageList.Images.SetKeyName(20, "305拐角.png");
            this.imageList.Images.SetKeyName(21, "401两点距离.png");
            this.imageList.Images.SetKeyName(22, "402点圆距离.png");
            this.imageList.Images.SetKeyName(23, "403点线距离.png");
            this.imageList.Images.SetKeyName(24, "404线线夹角.png");
            this.imageList.Images.SetKeyName(25, "405线线交点.png");
            this.imageList.Images.SetKeyName(26, "406线圆交点.png");
            this.imageList.Images.SetKeyName(27, "407圆圆交点.png");
            this.imageList.Images.SetKeyName(28, "410区域灰度比较.png");
            this.imageList.Images.SetKeyName(29, "字符.png");
            this.imageList.Images.SetKeyName(30, "扫码枪 (1).png");
            this.imageList.Images.SetKeyName(31, "逻辑运算.png");
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "展开树.png");
            // 
            // tvw_tools
            // 
            this.tvw_tools.AllowDrop = true;
            this.tvw_tools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.tvw_tools.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvw_tools.ContextMenuStrip = this.contextMenuStrip1;
            this.tvw_tools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvw_tools.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.tvw_tools.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvw_tools.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tvw_tools.FullRowSelect = true;
            this.tvw_tools.HotTracking = true;
            this.tvw_tools.ImageIndex = 0;
            this.tvw_tools.ImageList = this.imageList;
            this.tvw_tools.Indent = 30;
            this.tvw_tools.ItemHeight = 30;
            this.tvw_tools.LineColor = System.Drawing.Color.White;
            this.tvw_tools.Location = new System.Drawing.Point(1, 1);
            this.tvw_tools.Margin = new System.Windows.Forms.Padding(4, 14, 4, 4);
            this.tvw_tools.Name = "tvw_tools";
            this.tvw_tools.SelectedImageIndex = 0;
            this.tvw_tools.ShowLines = false;
            this.tvw_tools.ShowRootLines = false;
            this.tvw_tools.Size = new System.Drawing.Size(279, 677);
            this.tvw_tools.TabIndex = 23;
            this.tvw_tools.Tag = "100";
            this.tvw_tools.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvw_tools_AfterCollapse);
            this.tvw_tools.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvw_tools_BeforeExpand);
            this.tvw_tools.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvw_tools_AfterExpand);
            this.tvw_tools.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvw_tools_ItemDrag);
            this.tvw_tools.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvw_job_AfterSelect);
            this.tvw_tools.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvw_tools_NodeMouseClick);
            this.tvw_tools.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvw_tools_DragDrop);
            this.tvw_tools.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvw_tools_DragEnter);
            this.tvw_tools.DoubleClick += new System.EventHandler(this.tvw_job_DoubleClick);
            this.tvw_tools.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvw_tools_MouseDown);
            // 
            // Win_ToolBox
            // 
            this.AutoHidePortion = 0.4D;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(281, 735);
            this.CloseButtonVisible = false;
            this.ConfigButtonVisible = false;
            this.Controls.Add(this.tvw_tools);
            this.Controls.Add(this.lbl_toolInfo);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Win_ToolBox";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工具箱";
            this.DockStateChanged += new System.EventHandler(this.Win_Tools_DockStateChanged);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Win_Tools_FormClosed);
            this.Load += new System.EventHandler(this.Win_Tools_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_toolInfo;
        private System.Windows.Forms.ToolList tvw_tools;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 折叠所有ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 展开所有ToolStripMenuItem;
        public System.Windows.Forms.ImageList imageList;
        public System.Windows.Forms.ImageList imageList1;
    }
}