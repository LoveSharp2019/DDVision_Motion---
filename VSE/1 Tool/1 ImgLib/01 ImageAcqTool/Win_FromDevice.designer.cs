using VControls;
namespace VSE
{
    partial class Win_FromDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_FromDevice));
            this.label147 = new System.Windows.Forms.Label();
            this.label123 = new System.Windows.Forms.Label();
            this.label148 = new System.Windows.Forms.Label();
            this.btn_saveImage = new VControls.VButton();
            this.btn_displayImage = new VControls.VButton();
            this.cbx_deviceList = new LXCSystem.Control.CommonCtrl.LxcComboBox();
            this.tbx_exposure = new VControls.VNumericUpDown();
            this.SuspendLayout();
            // 
            // label147
            // 
            this.label147.AutoSize = true;
            this.label147.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label147.Location = new System.Drawing.Point(2, 83);
            this.label147.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(68, 17);
            this.label147.TabIndex = 94;
            this.label147.Text = "曝光时间：";
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label123.Location = new System.Drawing.Point(5, 7);
            this.label123.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(56, 17);
            this.label123.TabIndex = 98;
            this.label123.Text = "设备列表";
            // 
            // label148
            // 
            this.label148.AutoSize = true;
            this.label148.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label148.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label148.Location = new System.Drawing.Point(185, 83);
            this.label148.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(25, 17);
            this.label148.TabIndex = 97;
            this.label148.Text = "ms";
            // 
            // btn_saveImage
            // 
            this.btn_saveImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_saveImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_saveImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_saveImage.Location = new System.Drawing.Point(78, 146);
            this.btn_saveImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_saveImage.Name = "btn_saveImage";
            this.btn_saveImage.Size = new System.Drawing.Size(67, 28);
            this.btn_saveImage.TabIndex = 106;
            this.btn_saveImage.TextStr = "图像另存";
            // 
            // btn_displayImage
            // 
            this.btn_displayImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_displayImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_displayImage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_displayImage.Location = new System.Drawing.Point(5, 146);
            this.btn_displayImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_displayImage.Name = "btn_displayImage";
            this.btn_displayImage.Size = new System.Drawing.Size(67, 28);
            this.btn_displayImage.TabIndex = 105;
            this.btn_displayImage.TextStr = "实时";
            // 
            // cbx_deviceList
            // 
            this.cbx_deviceList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbx_deviceList.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbx_deviceList.BackColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbx_deviceList.BorderColor = System.Drawing.Color.DimGray;
            this.cbx_deviceList.ComboxFont = new System.Drawing.Font("微软雅黑", 12F);
            this.cbx_deviceList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_deviceList.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbx_deviceList.ItemDrawMode = LXCSystem.Control.Base.Common.ListDrawMode.AutoDraw;
            this.cbx_deviceList.ListScrollAppearance.BackColorHover = System.Drawing.Color.Empty;
            this.cbx_deviceList.ListScrollAppearance.BackColorNormal = System.Drawing.Color.Empty;
            this.cbx_deviceList.ListScrollAppearance.BackColorPressed = System.Drawing.Color.Empty;
            this.cbx_deviceList.ListScrollAppearance.ButtonColorHover = System.Drawing.Color.Empty;
            this.cbx_deviceList.ListScrollAppearance.ButtonColorNormal = System.Drawing.Color.Empty;
            this.cbx_deviceList.ListScrollAppearance.ButtonColorPressed = System.Drawing.Color.Empty;
            this.cbx_deviceList.Location = new System.Drawing.Point(5, 32);
            this.cbx_deviceList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_deviceList.Name = "cbx_deviceList";
            this.cbx_deviceList.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.cbx_deviceList.SelectedItemFontColor = System.Drawing.Color.White;
            this.cbx_deviceList.Size = new System.Drawing.Size(301, 25);
            this.cbx_deviceList.TabIndex = 104;
            this.cbx_deviceList.Text = null;
            this.cbx_deviceList.SelectedIndexChanged += new System.EventHandler(this.cbx_deviceList_SelectedIndexChanged);
            // 
            // tbx_exposure
            // 
            this.tbx_exposure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbx_exposure.DecimalPlaces = 3;
            this.tbx_exposure.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_exposure.Incremeent = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.tbx_exposure.Location = new System.Drawing.Point(75, 74);
            this.tbx_exposure.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_exposure.MaximumSize = new System.Drawing.Size(300, 26);
            this.tbx_exposure.MaxValue = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.tbx_exposure.MinimumSize = new System.Drawing.Size(50, 26);
            this.tbx_exposure.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.tbx_exposure.Name = "tbx_exposure";
            this.tbx_exposure.Size = new System.Drawing.Size(231, 26);
            this.tbx_exposure.TabIndex = 103;
            this.tbx_exposure.Value = 20D;
            this.tbx_exposure.ValueChanged += new VControls.DValueChanged(this.tbx_exposure_ValueChanged);
            // 
            // Win_FromDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(344, 238);
            this.Controls.Add(this.btn_saveImage);
            this.Controls.Add(this.btn_displayImage);
            this.Controls.Add(this.cbx_deviceList);
            this.Controls.Add(this.tbx_exposure);
            this.Controls.Add(this.label148);
            this.Controls.Add(this.label147);
            this.Controls.Add(this.label123);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Win_FromDevice";
            this.Text = "Win_AcquistionFromDevice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label147;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.Label label148;
        internal VNumericUpDown tbx_exposure;
        internal LXCSystem.Control.CommonCtrl.LxcComboBox cbx_deviceList;
        internal VButton btn_displayImage;
        internal VButton btn_saveImage;
    }
}