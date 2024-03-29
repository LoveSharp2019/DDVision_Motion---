namespace VSE
{
    partial class Win_FromLocal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_FromLocal));
            this.tbx_imagePath = new System.Windows.Forms.TextBox();
            this.btn_lastImage = new LXCSystem.Control.SwitchButton();
            this.btn_nextImage = new LXCSystem.Control.SwitchButton();
            this.tbx_imageDirectoryPath = new System.Windows.Forms.TextBox();
            this.pnl_multImage = new System.Windows.Forms.Panel();
            this.btn_browseImage = new VControls.VButton();
            this.btn_selectImageDirectoryPath = new VControls.VButton();
            this.btn_selectImagePath = new VControls.VButton();
            this.pnl_multImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbx_imagePath
            // 
            this.tbx_imagePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbx_imagePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbx_imagePath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_imagePath.ForeColor = System.Drawing.Color.White;
            this.tbx_imagePath.Location = new System.Drawing.Point(6, 18);
            this.tbx_imagePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_imagePath.Multiline = true;
            this.tbx_imagePath.Name = "tbx_imagePath";
            this.tbx_imagePath.Size = new System.Drawing.Size(326, 88);
            this.tbx_imagePath.TabIndex = 1;
            this.tbx_imagePath.TextChanged += new System.EventHandler(this.tbx_imagePath_TextChanged);
            // 
            // btn_lastImage
            // 
            this.btn_lastImage.Appearance = System.Windows.Forms.Appearance.Button;
            this.btn_lastImage.BackColor = System.Drawing.Color.Transparent;
            this.btn_lastImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_lastImage.BackgroundImage")));
            this.btn_lastImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_lastImage.CheckImage = ((System.Drawing.Image)(resources.GetObject("btn_lastImage.CheckImage")));
            this.btn_lastImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_lastImage.FlatAppearance.BorderSize = 0;
            this.btn_lastImage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_lastImage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_lastImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_lastImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_lastImage.Location = new System.Drawing.Point(5, 104);
            this.btn_lastImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_lastImage.Name = "btn_lastImage";
            this.btn_lastImage.Size = new System.Drawing.Size(33, 28);
            this.btn_lastImage.TabIndex = 44;
            this.btn_lastImage.TabStop = false;
            this.btn_lastImage.UnCheckImage = ((System.Drawing.Image)(resources.GetObject("btn_lastImage.UnCheckImage")));
            this.btn_lastImage.UseVisualStyleBackColor = false;
            this.btn_lastImage.Click += new System.EventHandler(this.btn_lastImage_Click);
            // 
            // btn_nextImage
            // 
            this.btn_nextImage.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btn_nextImage.Appearance = System.Windows.Forms.Appearance.Button;
            this.btn_nextImage.BackColor = System.Drawing.Color.Transparent;
            this.btn_nextImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_nextImage.BackgroundImage")));
            this.btn_nextImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_nextImage.CheckImage = ((System.Drawing.Image)(resources.GetObject("btn_nextImage.CheckImage")));
            this.btn_nextImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_nextImage.FlatAppearance.BorderSize = 0;
            this.btn_nextImage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_nextImage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_nextImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_nextImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_nextImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_nextImage.Location = new System.Drawing.Point(66, 104);
            this.btn_nextImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_nextImage.Name = "btn_nextImage";
            this.btn_nextImage.Size = new System.Drawing.Size(33, 28);
            this.btn_nextImage.TabIndex = 0;
            this.btn_nextImage.TabStop = false;
            this.btn_nextImage.UnCheckImage = ((System.Drawing.Image)(resources.GetObject("btn_nextImage.UnCheckImage")));
            this.btn_nextImage.UseVisualStyleBackColor = false;
            this.btn_nextImage.Click += new System.EventHandler(this.btn_nextImage_Click);
            // 
            // tbx_imageDirectoryPath
            // 
            this.tbx_imageDirectoryPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbx_imageDirectoryPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbx_imageDirectoryPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_imageDirectoryPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbx_imageDirectoryPath.Location = new System.Drawing.Point(5, 10);
            this.tbx_imageDirectoryPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_imageDirectoryPath.Multiline = true;
            this.tbx_imageDirectoryPath.Name = "tbx_imageDirectoryPath";
            this.tbx_imageDirectoryPath.Size = new System.Drawing.Size(327, 88);
            this.tbx_imageDirectoryPath.TabIndex = 5;
            this.tbx_imageDirectoryPath.TabStop = false;
            this.tbx_imageDirectoryPath.TextChanged += new System.EventHandler(this.tbx_imageDirectoryPath_TextChanged);
            // 
            // pnl_multImage
            // 
            this.pnl_multImage.BackColor = System.Drawing.Color.Transparent;
            this.pnl_multImage.Controls.Add(this.btn_browseImage);
            this.pnl_multImage.Controls.Add(this.btn_selectImageDirectoryPath);
            this.pnl_multImage.Controls.Add(this.tbx_imageDirectoryPath);
            this.pnl_multImage.Controls.Add(this.btn_nextImage);
            this.pnl_multImage.Controls.Add(this.btn_lastImage);
            this.pnl_multImage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnl_multImage.Location = new System.Drawing.Point(0, 8);
            this.pnl_multImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnl_multImage.Name = "pnl_multImage";
            this.pnl_multImage.Size = new System.Drawing.Size(342, 148);
            this.pnl_multImage.TabIndex = 87;
            this.pnl_multImage.Visible = false;
            // 
            // btn_browseImage
            // 
            this.btn_browseImage.BackColor = System.Drawing.Color.Transparent;
            this.btn_browseImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_browseImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_browseImage.Location = new System.Drawing.Point(159, 104);
            this.btn_browseImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_browseImage.Name = "btn_browseImage";
            this.btn_browseImage.Size = new System.Drawing.Size(68, 26);
            this.btn_browseImage.TabIndex = 116;
            this.btn_browseImage.TextStr = "浏览图像";
            // 
            // btn_selectImageDirectoryPath
            // 
            this.btn_selectImageDirectoryPath.BackColor = System.Drawing.Color.Transparent;
            this.btn_selectImageDirectoryPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_selectImageDirectoryPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_selectImageDirectoryPath.Location = new System.Drawing.Point(264, 104);
            this.btn_selectImageDirectoryPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_selectImageDirectoryPath.Name = "btn_selectImageDirectoryPath";
            this.btn_selectImageDirectoryPath.Size = new System.Drawing.Size(68, 26);
            this.btn_selectImageDirectoryPath.TabIndex = 117;
            this.btn_selectImageDirectoryPath.TextStr = "选择路径";
            this.btn_selectImageDirectoryPath.Load += new System.EventHandler(this.btn_selectImageDirectoryPath_Load);
            // 
            // btn_selectImagePath
            // 
            this.btn_selectImagePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_selectImagePath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_selectImagePath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_selectImagePath.Location = new System.Drawing.Point(264, 112);
            this.btn_selectImagePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_selectImagePath.Name = "btn_selectImagePath";
            this.btn_selectImagePath.Size = new System.Drawing.Size(68, 26);
            this.btn_selectImagePath.TabIndex = 118;
            this.btn_selectImagePath.TextStr = "选择图像";
            // 
            // Win_FromLocal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(344, 294);
            this.Controls.Add(this.pnl_multImage);
            this.Controls.Add(this.tbx_imagePath);
            this.Controls.Add(this.btn_selectImagePath);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Win_FromLocal";
            this.pnl_multImage.ResumeLayout(false);
            this.pnl_multImage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox tbx_imagePath;
        internal LXCSystem.Control.SwitchButton btn_lastImage;
        internal LXCSystem.Control.SwitchButton btn_nextImage;
        internal System.Windows.Forms.TextBox tbx_imageDirectoryPath;
        internal System.Windows.Forms.Panel pnl_multImage;
        internal VControls.VButton btn_selectImagePath;
        internal VControls.VButton btn_browseImage;
        internal VControls.VButton btn_selectImageDirectoryPath;

    }
}