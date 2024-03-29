namespace VSE
{
    partial class Win_Log
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_Log));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.历史日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_tip = new System.Windows.Forms.ToolStripButton();
            this.tsb_warn = new System.Windows.Forms.ToolStripButton();
            this.tsb_error = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem,
            this.停止刷新ToolStripMenuItem,
            this.历史日志ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 70);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.删除ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.删除ToolStripMenuItem.Text = "清除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.清除ToolStripMenuItem_Click);
            // 
            // 停止刷新ToolStripMenuItem
            // 
            this.停止刷新ToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.停止刷新ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.停止刷新ToolStripMenuItem.Name = "停止刷新ToolStripMenuItem";
            this.停止刷新ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.停止刷新ToolStripMenuItem.Text = "停止刷新";
            this.停止刷新ToolStripMenuItem.Click += new System.EventHandler(this.停止刷新ToolStripMenuItem_Click);
            // 
            // 历史日志ToolStripMenuItem
            // 
            this.历史日志ToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.历史日志ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.历史日志ToolStripMenuItem.Name = "历史日志ToolStripMenuItem";
            this.历史日志ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.历史日志ToolStripMenuItem.Text = "历史日志";
            this.历史日志ToolStripMenuItem.Click += new System.EventHandler(this.历史日志ToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_tip,
            this.tsb_warn,
            this.tsb_error,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(654, 31);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStrip1_Paint);
            // 
            // tsb_tip
            // 
            this.tsb_tip.CheckOnClick = true;
            this.tsb_tip.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.tsb_tip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tsb_tip.Image = ((System.Drawing.Image)(resources.GetObject("tsb_tip.Image")));
            this.tsb_tip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_tip.Name = "tsb_tip";
            this.tsb_tip.Size = new System.Drawing.Size(83, 28);
            this.tsb_tip.Text = "提示(0)";
            this.tsb_tip.Click += new System.EventHandler(this.tsb_tip_Click);
            // 
            // tsb_warn
            // 
            this.tsb_warn.CheckOnClick = true;
            this.tsb_warn.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.tsb_warn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tsb_warn.Image = ((System.Drawing.Image)(resources.GetObject("tsb_warn.Image")));
            this.tsb_warn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_warn.Name = "tsb_warn";
            this.tsb_warn.Size = new System.Drawing.Size(83, 28);
            this.tsb_warn.Text = "警告(0)";
            this.tsb_warn.Click += new System.EventHandler(this.tsb_warn_Click);
            // 
            // tsb_error
            // 
            this.tsb_error.CheckOnClick = true;
            this.tsb_error.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.tsb_error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tsb_error.Image = ((System.Drawing.Image)(resources.GetObject("tsb_error.Image")));
            this.tsb_error.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_error.Name = "tsb_error";
            this.tsb_error.Size = new System.Drawing.Size(83, 28);
            this.tsb_error.Text = "错误(0)";
            this.tsb_error.Click += new System.EventHandler(this.tsb_error_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.CheckOnClick = true;
            this.toolStripButton1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.toolStripButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(83, 28);
            this.toolStripButton1.Text = "报警(0)";
            this.toolStripButton1.ToolTipText = "报警历史";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.HideSelection = false;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(0, 31);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(654, 266);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "时间";
            this.columnHeader1.Width = 140;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "信息";
            this.columnHeader2.Width = 300;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(-2, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(657, 32);
            this.label1.TabIndex = 4;
            // 
            // Win_Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.ClientSize = new System.Drawing.Size(654, 301);
            this.CloseButtonVisible = false;
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Win_Log";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.Text = "日志";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Win_Log_FormClosed);
            this.SizeChanged += new System.EventHandler(this.Win_Log_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsb_tip;
        private System.Windows.Forms.ToolStripButton tsb_warn;
        private System.Windows.Forms.ToolStripButton tsb_error;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStripMenuItem 历史日志ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止刷新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}