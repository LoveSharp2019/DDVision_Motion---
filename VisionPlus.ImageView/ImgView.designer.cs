namespace Lxc.VisionPlus.ImageView
{
    partial class ImgView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                mCtrl_HWindow.MouseMove -= HWindowControl_HMouseMove;

            }
            if (disposing && hv_image != null)
            {
                hv_image.Dispose();
            }
            if (disposing && hv_window != null)
            {
                hv_window.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImgView));
            this.m_CtrlHStatusLabelCtrl = new System.Windows.Forms.Label();
            this.m_CtrlImageList = new System.Windows.Forms.ImageList(this.components);
            this.mCtrl_HWindow = new System.Windows.Forms.PictureBox();
            this.hv_MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.指针ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.平移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtn_fitWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtn_OriginSize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_showInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.区域灰度直方图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测量距离ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开图像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_saveOriginImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtn_saveDumpWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.imgviewCaptionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mCtrl_HWindow)).BeginInit();
            this.hv_MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_CtrlHStatusLabelCtrl
            // 
            this.m_CtrlHStatusLabelCtrl.AutoSize = true;
            this.m_CtrlHStatusLabelCtrl.BackColor = System.Drawing.Color.Transparent;
            this.m_CtrlHStatusLabelCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_CtrlHStatusLabelCtrl.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_CtrlHStatusLabelCtrl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.m_CtrlHStatusLabelCtrl.Location = new System.Drawing.Point(0, 0);
            this.m_CtrlHStatusLabelCtrl.Margin = new System.Windows.Forms.Padding(4);
            this.m_CtrlHStatusLabelCtrl.Name = "m_CtrlHStatusLabelCtrl";
            this.m_CtrlHStatusLabelCtrl.Size = new System.Drawing.Size(63, 19);
            this.m_CtrlHStatusLabelCtrl.TabIndex = 1;
            this.m_CtrlHStatusLabelCtrl.Text = "123456";
            this.m_CtrlHStatusLabelCtrl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_CtrlImageList
            // 
            this.m_CtrlImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_CtrlImageList.ImageStream")));
            this.m_CtrlImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.m_CtrlImageList.Images.SetKeyName(0, "TableIcon.png");
            this.m_CtrlImageList.Images.SetKeyName(1, "PicturesIcon.png");
            // 
            // mCtrl_HWindow
            // 
            this.mCtrl_HWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mCtrl_HWindow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mCtrl_HWindow.ContextMenuStrip = this.hv_MenuStrip;
            this.mCtrl_HWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mCtrl_HWindow.Image = ((System.Drawing.Image)(resources.GetObject("mCtrl_HWindow.Image")));
            this.mCtrl_HWindow.Location = new System.Drawing.Point(0, 25);
            this.mCtrl_HWindow.Margin = new System.Windows.Forms.Padding(4);
            this.mCtrl_HWindow.Name = "mCtrl_HWindow";
            this.mCtrl_HWindow.Size = new System.Drawing.Size(391, 254);
            this.mCtrl_HWindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.mCtrl_HWindow.TabIndex = 2;
            this.mCtrl_HWindow.TabStop = false;
            this.mCtrl_HWindow.SizeChanged += new System.EventHandler(this.mCtrl_HWindow_SizeChanged);
            this.mCtrl_HWindow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mCtrl_HWindow_MouseDown);
            this.mCtrl_HWindow.MouseEnter += new System.EventHandler(this.mCtrl_HWindow_MouseEnter);
            this.mCtrl_HWindow.MouseLeave += new System.EventHandler(this.mCtrl_HWindow_MouseLeave);
            this.mCtrl_HWindow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mCtrl_HWindow_MouseUp);
            // 
            // hv_MenuStrip
            // 
            this.hv_MenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.hv_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.指针ToolStripMenuItem,
            this.平移ToolStripMenuItem,
            this.toolStripSeparator3,
            this.toolStripMenuItem2,
            this.tsbtn_fitWindow,
            this.tsbtn_OriginSize,
            this.toolStripSeparator1,
            this.tsbtn_showInfo,
            this.toolStripMenuItem1,
            this.toolStripSeparator2,
            this.tsbtn_saveOriginImage,
            this.tsbtn_saveDumpWindow});
            this.hv_MenuStrip.Name = "hv_MenuStrip";
            this.hv_MenuStrip.Size = new System.Drawing.Size(161, 220);
            // 
            // 指针ToolStripMenuItem
            // 
            this.指针ToolStripMenuItem.Checked = true;
            this.指针ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.指针ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("指针ToolStripMenuItem.Image")));
            this.指针ToolStripMenuItem.Name = "指针ToolStripMenuItem";
            this.指针ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.指针ToolStripMenuItem.Text = "指针";
            this.指针ToolStripMenuItem.Click += new System.EventHandler(this.指针ToolStripMenuItem_Click);
            // 
            // 平移ToolStripMenuItem
            // 
            this.平移ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("平移ToolStripMenuItem.Image")));
            this.平移ToolStripMenuItem.Name = "平移ToolStripMenuItem";
            this.平移ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.平移ToolStripMenuItem.Text = "平移缩放";
            this.平移ToolStripMenuItem.Click += new System.EventHandler(this.平移ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(157, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem2.Text = "全屏";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // tsbtn_fitWindow
            // 
            this.tsbtn_fitWindow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tsbtn_fitWindow.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_fitWindow.Image")));
            this.tsbtn_fitWindow.Name = "tsbtn_fitWindow";
            this.tsbtn_fitWindow.Size = new System.Drawing.Size(160, 22);
            this.tsbtn_fitWindow.Text = "适应窗口";
            this.tsbtn_fitWindow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbtn_fitWindow.Click += new System.EventHandler(this.tsbtn_fitWindow_Click);
            // 
            // tsbtn_OriginSize
            // 
            this.tsbtn_OriginSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tsbtn_OriginSize.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_OriginSize.Image")));
            this.tsbtn_OriginSize.Name = "tsbtn_OriginSize";
            this.tsbtn_OriginSize.Size = new System.Drawing.Size(160, 22);
            this.tsbtn_OriginSize.Text = "原始大小";
            this.tsbtn_OriginSize.Click += new System.EventHandler(this.tsbtn_OriginSize_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // tsbtn_showInfo
            // 
            this.tsbtn_showInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tsbtn_showInfo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_showInfo.Image")));
            this.tsbtn_showInfo.Name = "tsbtn_showInfo";
            this.tsbtn_showInfo.Size = new System.Drawing.Size(160, 22);
            this.tsbtn_showInfo.Text = "设定";
            this.tsbtn_showInfo.Click += new System.EventHandler(this.tsbtn_showInfo_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.区域灰度直方图ToolStripMenuItem,
            this.测量距离ToolStripMenuItem,
            this.打开图像ToolStripMenuItem});
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem1.Text = "辅助";
            // 
            // 区域灰度直方图ToolStripMenuItem
            // 
            this.区域灰度直方图ToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.区域灰度直方图ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.区域灰度直方图ToolStripMenuItem.Name = "区域灰度直方图ToolStripMenuItem";
            this.区域灰度直方图ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.区域灰度直方图ToolStripMenuItem.Text = "区域灰度直方图";
            this.区域灰度直方图ToolStripMenuItem.Click += new System.EventHandler(this.区域灰度直方图ToolStripMenuItem_Click);
            // 
            // 测量距离ToolStripMenuItem
            // 
            this.测量距离ToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.测量距离ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.测量距离ToolStripMenuItem.Name = "测量距离ToolStripMenuItem";
            this.测量距离ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.测量距离ToolStripMenuItem.Text = "量测两点距离";
            this.测量距离ToolStripMenuItem.Click += new System.EventHandler(this.测量距离ToolStripMenuItem_Click);
            // 
            // 打开图像ToolStripMenuItem
            // 
            this.打开图像ToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.打开图像ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.打开图像ToolStripMenuItem.Name = "打开图像ToolStripMenuItem";
            this.打开图像ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.打开图像ToolStripMenuItem.Text = "打开图像";
            this.打开图像ToolStripMenuItem.Click += new System.EventHandler(this.打开图像ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // tsbtn_saveOriginImage
            // 
            this.tsbtn_saveOriginImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tsbtn_saveOriginImage.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_saveOriginImage.Image")));
            this.tsbtn_saveOriginImage.Name = "tsbtn_saveOriginImage";
            this.tsbtn_saveOriginImage.Size = new System.Drawing.Size(160, 22);
            this.tsbtn_saveOriginImage.Text = "保存原始图片";
            this.tsbtn_saveOriginImage.Click += new System.EventHandler(this.tsbtn_saveOriginImage_Click);
            // 
            // tsbtn_saveDumpWindow
            // 
            this.tsbtn_saveDumpWindow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tsbtn_saveDumpWindow.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_saveDumpWindow.Image")));
            this.tsbtn_saveDumpWindow.Name = "tsbtn_saveDumpWindow";
            this.tsbtn_saveDumpWindow.Size = new System.Drawing.Size(160, 22);
            this.tsbtn_saveDumpWindow.Text = "保存窗口缩略图";
            this.tsbtn_saveDumpWindow.Click += new System.EventHandler(this.tsbtn_saveDumpWindow_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "111";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 279);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_CtrlHStatusLabelCtrl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(391, 20);
            this.splitContainer1.SplitterDistance = 230;
            this.splitContainer1.TabIndex = 5;
            this.splitContainer1.Visible = false;
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // imgviewCaptionLabel
            // 
            this.imgviewCaptionLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgviewCaptionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.imgviewCaptionLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imgviewCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.imgviewCaptionLabel.Location = new System.Drawing.Point(0, 0);
            this.imgviewCaptionLabel.Name = "imgviewCaptionLabel";
            this.imgviewCaptionLabel.Size = new System.Drawing.Size(391, 25);
            this.imgviewCaptionLabel.TabIndex = 6;
            this.imgviewCaptionLabel.Text = "label2";
            this.imgviewCaptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.imgviewCaptionLabel.Visible = false;
            // 
            // ImgView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.mCtrl_HWindow);
            this.Controls.Add(this.imgviewCaptionLabel);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ImgView";
            this.Size = new System.Drawing.Size(391, 299);
            this.Load += new System.EventHandler(this.HWindow_Final_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mCtrl_HWindow)).EndInit();
            this.hv_MenuStrip.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_CtrlHStatusLabelCtrl;
        private System.Windows.Forms.ImageList m_CtrlImageList;
        private System.Windows.Forms.PictureBox mCtrl_HWindow;
        private System.Windows.Forms.ContextMenuStrip hv_MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 指针ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 平移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsbtn_fitWindow;
        private System.Windows.Forms.ToolStripMenuItem tsbtn_OriginSize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsbtn_showInfo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 测量距离ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsbtn_saveOriginImage;
        private System.Windows.Forms.ToolStripMenuItem tsbtn_saveDumpWindow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem 打开图像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 区域灰度直方图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label imgviewCaptionLabel;
    }
}
