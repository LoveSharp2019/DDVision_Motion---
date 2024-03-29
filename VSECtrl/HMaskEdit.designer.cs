namespace VControls
{
    partial class HMaskEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HMaskEdit));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.矩形 = new System.Windows.Forms.ToolStripMenuItem();
            this.仿射矩形 = new System.Windows.Forms.ToolStripMenuItem();
            this.椭圆 = new System.Windows.Forms.ToolStripMenuItem();
            this.笔刷 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.hWindow_Final1 = new Lxc.VisionPlus.ImageView.ImgView();
            this.vButton2 = new VControls.VButton();
            this.vButton1 = new VControls.VButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(8, 2, 2, 2);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripSeparator2,
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripButton8,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1115, 56);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.矩形,
            this.仿射矩形,
            this.椭圆,
            this.笔刷});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Margin = new System.Windows.Forms.Padding(8, 1, 0, 2);
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(45, 53);
            this.toolStripDropDownButton1.Text = "笔刷";
            this.toolStripDropDownButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripDropDownButton1.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripDropDownButton1_DropDownItemClicked);
            // 
            // 矩形
            // 
            this.矩形.Image = ((System.Drawing.Image)(resources.GetObject("矩形.Image")));
            this.矩形.Name = "矩形";
            this.矩形.Size = new System.Drawing.Size(164, 38);
            this.矩形.Text = "矩形选择";
            this.矩形.Click += new System.EventHandler(this.矩形_Click);
            // 
            // 仿射矩形
            // 
            this.仿射矩形.Image = ((System.Drawing.Image)(resources.GetObject("仿射矩形.Image")));
            this.仿射矩形.Name = "仿射矩形";
            this.仿射矩形.Size = new System.Drawing.Size(164, 38);
            this.仿射矩形.Text = "仿射矩形选择";
            this.仿射矩形.Click += new System.EventHandler(this.仿射矩形_Click);
            // 
            // 椭圆
            // 
            this.椭圆.Image = ((System.Drawing.Image)(resources.GetObject("椭圆.Image")));
            this.椭圆.Name = "椭圆";
            this.椭圆.Size = new System.Drawing.Size(164, 38);
            this.椭圆.Text = "椭圆选择";
            this.椭圆.Click += new System.EventHandler(this.椭圆_Click);
            // 
            // 笔刷
            // 
            this.笔刷.Image = ((System.Drawing.Image)(resources.GetObject("笔刷.Image")));
            this.笔刷.Name = "笔刷";
            this.笔刷.Size = new System.Drawing.Size(164, 38);
            this.笔刷.Text = "笔刷";
            this.笔刷.Click += new System.EventHandler(this.笔刷_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 56);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 53);
            this.toolStripLabel1.Text = "笔刷大小：";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 53);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "最小笔划";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 53);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "小笔划";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Checked = true;
            this.toolStripButton3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 53);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.ToolTipText = "大笔划";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 53);
            this.toolStripButton4.Text = "toolStripButton4";
            this.toolStripButton4.ToolTipText = "最大笔划";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 56);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(48, 53);
            this.toolStripButton5.Text = "橡皮擦";
            this.toolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton5.ToolTipText = "橡皮擦";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Checked = true;
            this.toolStripButton6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(36, 53);
            this.toolStripButton6.Text = "掩膜";
            this.toolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton6.ToolTipText = "掩膜";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(6, 56);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(84, 53);
            this.toolStripButton8.Text = "填充当前选项";
            this.toolStripButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton8.Visible = false;
            this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 56);
            this.toolStripSeparator3.Visible = false;
            // 
            // hWindow_Final1
            // 
            this.hWindow_Final1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hWindow_Final1.BackColor = System.Drawing.Color.Transparent;
            this.hWindow_Final1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hWindow_Final1.Caption = "label2";
            this.hWindow_Final1.CaptionVisible = false;
            this.hWindow_Final1.DrawModel = false;
            this.hWindow_Final1.EditModel = Lxc.VisionPlus.ImageView.ImgView.winMode.MoveZoom;
            this.hWindow_Final1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hWindow_Final1.Location = new System.Drawing.Point(0, 55);
            this.hWindow_Final1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.hWindow_Final1.Name = "hWindow_Final1";
            this.hWindow_Final1.Size = new System.Drawing.Size(1115, 599);
            this.hWindow_Final1.StaticWnd = false;
            this.hWindow_Final1.TabIndex = 1;
            this.hWindow_Final1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.hWindow_Final1_MouseDown);
            this.hWindow_Final1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.hWindow_Final1_MouseUp);
            // 
            // vButton2
            // 
            this.vButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.vButton2.BackColor = System.Drawing.Color.White;
            this.vButton2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.vButton2.Location = new System.Drawing.Point(1003, 657);
            this.vButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.vButton2.Name = "vButton2";
            this.vButton2.Size = new System.Drawing.Size(107, 38);
            this.vButton2.TabIndex = 5;
            this.vButton2.TextStr = "取消";
            this.vButton2.Clicked += new VControls.DClicked(this.vButton2_Clicked);
            // 
            // vButton1
            // 
            this.vButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.vButton1.BackColor = System.Drawing.Color.White;
            this.vButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.vButton1.Location = new System.Drawing.Point(890, 657);
            this.vButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.vButton1.Name = "vButton1";
            this.vButton1.Size = new System.Drawing.Size(107, 38);
            this.vButton1.TabIndex = 5;
            this.vButton1.TextStr = "确定";
            this.vButton1.Clicked += new VControls.DClicked(this.vButton1_Clicked);
            // 
            // HMaskEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1115, 698);
            this.Controls.Add(this.vButton2);
            this.Controls.Add(this.vButton1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.hWindow_Final1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "HMaskEdit";
            this.Text = "掩膜编辑器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HMaskEdit_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.VisibleChanged += new System.EventHandler(this.HMaskEdit_VisibleChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Lxc.VisionPlus.ImageView.ImgView hWindow_Final1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem 矩形;
        private System.Windows.Forms.ToolStripMenuItem 仿射矩形;
        private System.Windows.Forms.ToolStripMenuItem 椭圆;
        private System.Windows.Forms.ToolStripMenuItem 笔刷;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private VButton vButton1;
        private VButton vButton2;
    }
}