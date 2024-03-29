namespace VControls
{
    partial class Win_ConfirmBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_ConfirmBox));
            this.btn_cancel = new System.Windows.Forms.Button();
            this.lbl_info = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_confirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xxxxxxxxxxxxxxxxxxxxxxx)).BeginInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.SuspendLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.SuspendLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // XXXXXXXXXXXXXXXXXXXXXXpictureBox1
            // 
            this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("XXXXXXXXXXXXXXXXXXXXXXpictureBox1.Image")));
            // 
            // XXXXXXXXXXXXXlbl_title
            // 
            this.XXXXXXXXXXXXXlbl_title.BackColor = System.Drawing.Color.Transparent;
            this.XXXXXXXXXXXXXlbl_title.Size = new System.Drawing.Size(439, 25);
            this.XXXXXXXXXXXXXlbl_title.Text = "提示信息";
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx
            // 
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx.Panel2
            // 
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.Controls.Add(this.panel2);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Size = new System.Drawing.Size(471, 235);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_cancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btn_cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cancel.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_cancel.Location = new System.Drawing.Point(316, 157);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(143, 30);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbl_info.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lbl_info.Location = new System.Drawing.Point(14, 15);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(42, 21);
            this.lbl_info.TabIndex = 2;
            this.lbl_info.Text = "信息";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.button1.Location = new System.Drawing.Point(161, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 30);
            this.button1.TabIndex = 7;
            this.button1.Text = "否";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Gray;
            this.panel5.Location = new System.Drawing.Point(9, 148);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(450, 3);
            this.panel5.TabIndex = 105;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.lbl_info);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.btn_cancel);
            this.panel2.Controls.Add(this.btn_confirm);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(471, 199);
            this.panel2.TabIndex = 106;
            // 
            // btn_confirm
            // 
            this.btn_confirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_confirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_confirm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btn_confirm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_confirm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_confirm.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_confirm.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_confirm.Location = new System.Drawing.Point(9, 157);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(142, 30);
            this.btn_confirm.TabIndex = 7;
            this.btn_confirm.Text = "是";
            this.btn_confirm.UseVisualStyleBackColor = false;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // Win_ConfirmBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ClientSize = new System.Drawing.Size(475, 243);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Win_ConfirmBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Win_ConfirmBox";
            this.Load += new System.EventHandler(this.Win_ConfirmBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1)).EndInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.ResumeLayout(false);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.PerformLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xxxxxxxxxxxxxxxxxxxxxxx)).EndInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btn_cancel;
        internal System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label lbl_info;
        internal System.Windows.Forms.Button btn_confirm;
    }
}