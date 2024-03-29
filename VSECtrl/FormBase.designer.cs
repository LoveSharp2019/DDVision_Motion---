namespace VControls
{
    partial class FormBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBase));
            this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1 = new System.Windows.Forms.PictureBox();
            this.XXXXXXXXXXXXXlbl_title = new System.Windows.Forms.Label();
            this.xxxxxxxxxxxxxxxxxxxxxxx = new System.Windows.Forms.SplitContainer();
            this.controlBox1 = new VControls.ControlBox();
            this.XXXXXXXXXXXXXXXXXXXXXlabel1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xxxxxxxxxxxxxxxxxxxxxxx)).BeginInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.SuspendLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.SuspendLayout();
            this.SuspendLayout();
            // 
            // XXXXXXXXXXXXXXXXXXXXXXpictureBox1
            // 
            this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("XXXXXXXXXXXXXXXXXXXXXXpictureBox1.Image")));
            this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1.Location = new System.Drawing.Point(3, 4);
            this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1.Name = "XXXXXXXXXXXXXXXXXXXXXXpictureBox1";
            this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1.Size = new System.Drawing.Size(23, 23);
            this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1.TabIndex = 1;
            this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1.TabStop = false;
            // 
            // XXXXXXXXXXXXXlbl_title
            // 
            this.XXXXXXXXXXXXXlbl_title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XXXXXXXXXXXXXlbl_title.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.XXXXXXXXXXXXXlbl_title.ForeColor = System.Drawing.Color.White;
            this.XXXXXXXXXXXXXlbl_title.Location = new System.Drawing.Point(30, 5);
            this.XXXXXXXXXXXXXlbl_title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.XXXXXXXXXXXXXlbl_title.Name = "XXXXXXXXXXXXXlbl_title";
            this.XXXXXXXXXXXXXlbl_title.Size = new System.Drawing.Size(337, 25);
            this.XXXXXXXXXXXXXlbl_title.TabIndex = 1;
            this.XXXXXXXXXXXXXlbl_title.Text = "VSE";
            this.XXXXXXXXXXXXXlbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.XXXXXXXXXXXXXlbl_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.setForm_MouseDown);
            this.XXXXXXXXXXXXXlbl_title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.setForm_MouseMove);
            this.XXXXXXXXXXXXXlbl_title.MouseUp += new System.Windows.Forms.MouseEventHandler(this.setForm_MouseUp);
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx
            // 
            this.xxxxxxxxxxxxxxxxxxxxxxx.BackColor = System.Drawing.Color.Transparent;
            this.xxxxxxxxxxxxxxxxxxxxxxx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xxxxxxxxxxxxxxxxxxxxxxx.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.xxxxxxxxxxxxxxxxxxxxxxx.IsSplitterFixed = true;
            this.xxxxxxxxxxxxxxxxxxxxxxx.Location = new System.Drawing.Point(2, 6);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Margin = new System.Windows.Forms.Padding(0);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Name = "xxxxxxxxxxxxxxxxxxxxxxx";
            this.xxxxxxxxxxxxxxxxxxxxxxx.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx.Panel1
            // 
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.Controls.Add(this.controlBox1);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.Controls.Add(this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.Controls.Add(this.XXXXXXXXXXXXXlbl_title);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.Padding = new System.Windows.Forms.Padding(30, 5, 2, 5);
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx.Panel2
            // 
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xxxxxxxxxxxxxxxxxxxxxxx.Size = new System.Drawing.Size(369, 261);
            this.xxxxxxxxxxxxxxxxxxxxxxx.SplitterDistance = 35;
            this.xxxxxxxxxxxxxxxxxxxxxxx.SplitterWidth = 1;
            this.xxxxxxxxxxxxxxxxxxxxxxx.TabIndex = 4;
            // 
            // controlBox1
            // 
            this.controlBox1.AutoSize = true;
            this.controlBox1.BackColor = System.Drawing.Color.Transparent;
            this.controlBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.controlBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.controlBox1.Location = new System.Drawing.Point(245, 5);
            this.controlBox1.LockEnable = false;
            this.controlBox1.Margin = new System.Windows.Forms.Padding(4);
            this.controlBox1.MaxEnable = true;
            this.controlBox1.MinEnable = false;
            this.controlBox1.MouseHoverColor = System.Drawing.Color.Gray;
            this.controlBox1.Name = "controlBox1";
            this.controlBox1.Size = new System.Drawing.Size(122, 25);
            this.controlBox1.TabIndex = 2;
            // 
            // XXXXXXXXXXXXXXXXXXXXXlabel1
            // 
            this.XXXXXXXXXXXXXXXXXXXXXlabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(106)))), ((int)(((byte)(175)))));
            this.XXXXXXXXXXXXXXXXXXXXXlabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.XXXXXXXXXXXXXXXXXXXXXlabel1.Location = new System.Drawing.Point(2, 1);
            this.XXXXXXXXXXXXXXXXXXXXXlabel1.Name = "XXXXXXXXXXXXXXXXXXXXXlabel1";
            this.XXXXXXXXXXXXXXXXXXXXXlabel1.Size = new System.Drawing.Size(369, 5);
            this.XXXXXXXXXXXXXXXXXXXXXlabel1.TabIndex = 5;
            // 
            // FormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ClientSize = new System.Drawing.Size(373, 269);
            this.Controls.Add(this.xxxxxxxxxxxxxxxxxxxxxxx);
            this.Controls.Add(this.XXXXXXXXXXXXXXXXXXXXXlabel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormBase";
            this.Padding = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.Text = "VSE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_ToolBase_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1)).EndInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.ResumeLayout(false);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xxxxxxxxxxxxxxxxxxxxxxx)).EndInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.PictureBox XXXXXXXXXXXXXXXXXXXXXXpictureBox1;
        public System.Windows.Forms.Label XXXXXXXXXXXXXlbl_title;
        private System.Windows.Forms.Label XXXXXXXXXXXXXXXXXXXXXlabel1;
        public System.Windows.Forms.SplitContainer xxxxxxxxxxxxxxxxxxxxxxx;
        private ControlBox controlBox1;
    }
}