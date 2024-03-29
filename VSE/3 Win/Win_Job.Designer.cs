namespace VSE
{
    partial class Win_Job
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_Job));
            this.panel1 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.TaskList();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ToolBoxEdit_ProjectName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_createJob = new System.Windows.Forms.ToolStripButton();
            this.tsb_deleteJob = new System.Windows.Forms.ToolStripButton();
            this.tsb_jobInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_foldJob = new System.Windows.Forms.ToolStripButton();
            this.tsb_expandJob = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_runOnce = new System.Windows.Forms.ToolStripButton();
            this.btn_runLoop = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tbc_jobs = new System.Windows.Forms.Panel();
            this.JobEdit_JobName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Controls.Add(this.ToolBoxEdit_ProjectName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(627, 135);
            this.panel1.TabIndex = 100;
            this.panel1.TabStop = true;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.listView1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.HotTracking = true;
            this.listView1.ImageList = this.imageList1;
            this.listView1.Indent = 30;
            this.listView1.ItemHeight = 30;
            this.listView1.Location = new System.Drawing.Point(0, 27);
            this.listView1.Name = "listView1";
            this.listView1.ShowLines = false;
            this.listView1.Size = new System.Drawing.Size(627, 108);
            this.listView1.TabIndex = 105;
            this.listView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.listView1_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "作业.png");
            // 
            // ToolBoxEdit_ProjectName
            // 
            this.ToolBoxEdit_ProjectName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ToolBoxEdit_ProjectName.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolBoxEdit_ProjectName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ToolBoxEdit_ProjectName.ForeColor = System.Drawing.Color.Gray;
            this.ToolBoxEdit_ProjectName.Location = new System.Drawing.Point(0, 0);
            this.ToolBoxEdit_ProjectName.Name = "ToolBoxEdit_ProjectName";
            this.ToolBoxEdit_ProjectName.Size = new System.Drawing.Size(627, 27);
            this.ToolBoxEdit_ProjectName.TabIndex = 104;
            this.ToolBoxEdit_ProjectName.Text = "xxx";
            this.ToolBoxEdit_ProjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(622, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 26);
            this.label2.TabIndex = 57;
            this.label2.Text = "label2";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 282;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_createJob,
            this.tsb_deleteJob,
            this.tsb_jobInfo,
            this.toolStripSeparator1,
            this.tsb_foldJob,
            this.tsb_expandJob,
            this.toolStripSeparator2,
            this.btn_runOnce,
            this.btn_runLoop});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(10, 3, 0, 0);
            this.toolStrip1.Size = new System.Drawing.Size(627, 50);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_createJob
            // 
            this.tsb_createJob.AutoSize = false;
            this.tsb_createJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_createJob.Image = ((System.Drawing.Image)(resources.GetObject("tsb_createJob.Image")));
            this.tsb_createJob.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_createJob.Name = "tsb_createJob";
            this.tsb_createJob.Size = new System.Drawing.Size(40, 38);
            this.tsb_createJob.Text = "新建流程";
            this.tsb_createJob.Click += new System.EventHandler(this.tsb_createJob_Click);
            // 
            // tsb_deleteJob
            // 
            this.tsb_deleteJob.AutoSize = false;
            this.tsb_deleteJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_deleteJob.Image = ((System.Drawing.Image)(resources.GetObject("tsb_deleteJob.Image")));
            this.tsb_deleteJob.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_deleteJob.Name = "tsb_deleteJob";
            this.tsb_deleteJob.Size = new System.Drawing.Size(40, 38);
            this.tsb_deleteJob.Text = "删除流程";
            this.tsb_deleteJob.Click += new System.EventHandler(this.tsb_deleteJob_Click);
            // 
            // tsb_jobInfo
            // 
            this.tsb_jobInfo.AutoSize = false;
            this.tsb_jobInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_jobInfo.Image = ((System.Drawing.Image)(resources.GetObject("tsb_jobInfo.Image")));
            this.tsb_jobInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_jobInfo.Name = "tsb_jobInfo";
            this.tsb_jobInfo.Size = new System.Drawing.Size(40, 38);
            this.tsb_jobInfo.Text = "流程属性";
            this.tsb_jobInfo.Click += new System.EventHandler(this.tsb_jobInfo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.BackColor = System.Drawing.Color.Red;
            this.toolStripSeparator1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // tsb_foldJob
            // 
            this.tsb_foldJob.AutoSize = false;
            this.tsb_foldJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_foldJob.Image = ((System.Drawing.Image)(resources.GetObject("tsb_foldJob.Image")));
            this.tsb_foldJob.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_foldJob.Name = "tsb_foldJob";
            this.tsb_foldJob.Size = new System.Drawing.Size(40, 38);
            this.tsb_foldJob.Text = "折叠流程";
            this.tsb_foldJob.Click += new System.EventHandler(this.tsb_foldJob_Click);
            // 
            // tsb_expandJob
            // 
            this.tsb_expandJob.AutoSize = false;
            this.tsb_expandJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_expandJob.Image = ((System.Drawing.Image)(resources.GetObject("tsb_expandJob.Image")));
            this.tsb_expandJob.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_expandJob.Name = "tsb_expandJob";
            this.tsb_expandJob.Size = new System.Drawing.Size(40, 38);
            this.tsb_expandJob.Text = "展开流程";
            this.tsb_expandJob.Click += new System.EventHandler(this.tsb_expandJob_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // btn_runOnce
            // 
            this.btn_runOnce.AutoSize = false;
            this.btn_runOnce.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_runOnce.Image = ((System.Drawing.Image)(resources.GetObject("btn_runOnce.Image")));
            this.btn_runOnce.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_runOnce.Name = "btn_runOnce";
            this.btn_runOnce.Size = new System.Drawing.Size(42, 38);
            this.btn_runOnce.Text = "toolStripButton1";
            this.btn_runOnce.ToolTipText = "单次运行";
            this.btn_runOnce.Click += new System.EventHandler(this.btn_runOnce_Click);
            // 
            // btn_runLoop
            // 
            this.btn_runLoop.AutoSize = false;
            this.btn_runLoop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_runLoop.Image = ((System.Drawing.Image)(resources.GetObject("btn_runLoop.Image")));
            this.btn_runLoop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_runLoop.Name = "btn_runLoop";
            this.btn_runLoop.Size = new System.Drawing.Size(42, 38);
            this.btn_runLoop.Text = "连续运行";
            this.btn_runLoop.ToolTipText = "连续运行";
            this.btn_runLoop.Click += new System.EventHandler(this.btn_jobLoopRun_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(627, 2);
            this.label1.TabIndex = 11;
            // 
            // tbc_jobs
            // 
            this.tbc_jobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbc_jobs.Location = new System.Drawing.Point(0, 189);
            this.tbc_jobs.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.tbc_jobs.Name = "tbc_jobs";
            this.tbc_jobs.Size = new System.Drawing.Size(627, 539);
            this.tbc_jobs.TabIndex = 101;
            // 
            // JobEdit_JobName
            // 
            this.JobEdit_JobName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.JobEdit_JobName.Dock = System.Windows.Forms.DockStyle.Top;
            this.JobEdit_JobName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.JobEdit_JobName.ForeColor = System.Drawing.Color.Gray;
            this.JobEdit_JobName.Location = new System.Drawing.Point(0, 0);
            this.JobEdit_JobName.Name = "JobEdit_JobName";
            this.JobEdit_JobName.Size = new System.Drawing.Size(627, 2);
            this.JobEdit_JobName.TabIndex = 102;
            this.JobEdit_JobName.Text = "xxx";
            this.JobEdit_JobName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.JobEdit_JobName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 187);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(627, 2);
            this.panel2.TabIndex = 0;
            // 
            // Win_Job
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(627, 728);
            this.CloseButtonVisible = false;
            this.Controls.Add(this.tbc_jobs);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Win_Job";
            this.Text = "流程编辑器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Win_Job_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Win_Job_FormClosed);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton tsb_createJob;
        internal System.Windows.Forms.ToolStripButton tsb_deleteJob;
        private System.Windows.Forms.ToolStripButton tsb_jobInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsb_foldJob;
        private System.Windows.Forms.ToolStripButton tsb_expandJob;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton btn_runOnce;
        public System.Windows.Forms.ToolStripButton btn_runLoop;
        public System.Windows.Forms.Panel tbc_jobs;
        public System.Windows.Forms.Label JobEdit_JobName;
        public System.Windows.Forms.Label ToolBoxEdit_ProjectName;
        public System.Windows.Forms.TaskList listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ImageList imageList1;
    }
}