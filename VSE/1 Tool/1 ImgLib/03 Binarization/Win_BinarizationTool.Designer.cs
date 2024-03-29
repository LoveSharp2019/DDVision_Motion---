namespace VSE
{
    partial class Win_BinarizationTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_BinarizationTool));
            this.GrayV = new VControls.VNumericUpDown();
            this.lbl_deg = new System.Windows.Forms.Label();
            this.vLinkImage1 = new VControls.VLinkImage();
            this.btn_OrgImg = new System.Windows.Forms.Button();
            this.Ppanel2.SuspendLayout();
            this.Ppanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xxxxxxxxxxxxxxxxxxxxxxx)).BeginInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.SuspendLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.SuspendLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.SuspendLayout();
            this.SuspendLayout();
            // 
            // PImageWin
            // 
            this.PImageWin.Size = new System.Drawing.Size(649, 534);
            // 
            // Ppanel2
            // 
            this.Ppanel2.Controls.Add(this.btn_OrgImg);
            this.Ppanel2.Controls.Add(this.vLinkImage1);
            this.Ppanel2.Controls.Add(this.GrayV);
            this.Ppanel2.Controls.Add(this.lbl_deg);
            this.Ppanel2.Location = new System.Drawing.Point(649, 0);
            this.Ppanel2.Size = new System.Drawing.Size(344, 534);
            // 
            // Pbtn_close
            // 
            this.Pbtn_close.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.Pbtn_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Pbtn_close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Pbtn_close.Click += new System.EventHandler(this.Pbtn_close_Click);
            // 
            // Pbtn_runTool
            // 
            this.Pbtn_runTool.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.Pbtn_runTool.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Pbtn_runTool.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Pbtn_runTool.Click += new System.EventHandler(this.Pbtn_runTool_Click);
            // 
            // Ppanel3
            // 
            this.Ppanel3.Location = new System.Drawing.Point(0, 534);
            this.Ppanel3.Size = new System.Drawing.Size(993, 55);
            // 
            // XXXXXXXXXXXXXXXXXXXXXXpictureBox1
            // 
            this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("XXXXXXXXXXXXXXXXXXXXXXpictureBox1.Image")));
            // 
            // XXXXXXXXXXXXXlbl_title
            // 
            this.XXXXXXXXXXXXXlbl_title.Size = new System.Drawing.Size(961, 25);
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx
            // 
            this.xxxxxxxxxxxxxxxxxxxxxxx.Size = new System.Drawing.Size(993, 625);
            // 
            // GrayV
            // 
            this.GrayV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.GrayV.DecimalPlaces = 0;
            this.GrayV.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GrayV.Incremeent = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GrayV.Location = new System.Drawing.Point(69, 69);
            this.GrayV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GrayV.MaximumSize = new System.Drawing.Size(300, 26);
            this.GrayV.MaxValue = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.GrayV.MinimumSize = new System.Drawing.Size(50, 26);
            this.GrayV.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GrayV.Name = "GrayV";
            this.GrayV.Size = new System.Drawing.Size(155, 26);
            this.GrayV.TabIndex = 1;
            this.GrayV.Value = 0D;
            this.GrayV.ValueChanged += new VControls.DValueChanged(this.GrayV_ValueChanged);
            // 
            // lbl_deg
            // 
            this.lbl_deg.AutoSize = true;
            this.lbl_deg.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_deg.ForeColor = System.Drawing.Color.White;
            this.lbl_deg.Location = new System.Drawing.Point(12, 74);
            this.lbl_deg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_deg.Name = "lbl_deg";
            this.lbl_deg.Size = new System.Drawing.Size(56, 17);
            this.lbl_deg.TabIndex = 123;
            this.lbl_deg.Text = "阈      值";
            // 
            // vLinkImage1
            // 
            this.vLinkImage1.AutoSize = true;
            this.vLinkImage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.vLinkImage1.Font = new System.Drawing.Font("微软雅黑 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.vLinkImage1.ImageText = "";
            this.vLinkImage1.Location = new System.Drawing.Point(9, 19);
            this.vLinkImage1.Margin = new System.Windows.Forms.Padding(4);
            this.vLinkImage1.Name = "vLinkImage1";
            this.vLinkImage1.Size = new System.Drawing.Size(273, 31);
            this.vLinkImage1.TabIndex = 124;
            // 
            // btn_OrgImg
            // 
            this.btn_OrgImg.BackColor = System.Drawing.Color.Transparent;
            this.btn_OrgImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_OrgImg.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btn_OrgImg.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_OrgImg.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_OrgImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OrgImg.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_OrgImg.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_OrgImg.Location = new System.Drawing.Point(275, 20);
            this.btn_OrgImg.Name = "btn_OrgImg";
            this.btn_OrgImg.Size = new System.Drawing.Size(65, 30);
            this.btn_OrgImg.TabIndex = 125;
            this.btn_OrgImg.Text = "原图";
            this.btn_OrgImg.UseVisualStyleBackColor = false;
            this.btn_OrgImg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_OrgImg_MouseDown);
            this.btn_OrgImg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_OrgImg_MouseUp);
            // 
            // Win_BinarizationTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 633);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LockBox = true;
            this.MaximizeBox = false;
            this.Name = "Win_BinarizationTool";
            this.Text = "Win_BinarizationTool";
            this.VisibleChanged += new System.EventHandler(this.Win_BinarizationTool_VisibleChanged);
            this.Ppanel2.ResumeLayout(false);
            this.Ppanel2.PerformLayout();
            this.Ppanel3.ResumeLayout(false);
            this.Ppanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1)).EndInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.ResumeLayout(false);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.PerformLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xxxxxxxxxxxxxxxxxxxxxxx)).EndInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public VControls.VNumericUpDown GrayV;
        private System.Windows.Forms.Label lbl_deg;
        internal System.Windows.Forms.Button btn_OrgImg;
        public VControls.VLinkImage vLinkImage1;
    }
}