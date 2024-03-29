using VControls;
namespace VSE
{
    partial class Win_GeneralSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_GeneralSettings));
            this.label3 = new System.Windows.Forms.Label();
            this.Chinese = new System.Windows.Forms.RadioButton();
            this.English = new System.Windows.Forms.RadioButton();
            this.lxcGroupBox3 = new LXCSystem.Control.CommonCtrl.LxcGroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lxcGroupBox1 = new LXCSystem.Control.CommonCtrl.LxcGroupBox();
            this.lxcRadioButton2 = new LXCSystem.Control.CommonCtrl.LxcRadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.lxcRadioButton1 = new LXCSystem.Control.CommonCtrl.LxcRadioButton();
            this.lxcRadioButton4 = new LXCSystem.Control.CommonCtrl.LxcRadioButton();
            this.lxcGroupBox4 = new LXCSystem.Control.CommonCtrl.LxcGroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.vTextBox1 = new VControls.VTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbx_companyName = new VControls.VTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lxcGroupBox3.SuspendLayout();
            this.lxcGroupBox1.SuspendLayout();
            this.lxcGroupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(61, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "公司名称：";
            // 
            // Chinese
            // 
            this.Chinese.Appearance = System.Windows.Forms.Appearance.Button;
            this.Chinese.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Chinese.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Chinese.BackgroundImage")));
            this.Chinese.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Chinese.Checked = true;
            this.Chinese.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Chinese.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.Chinese.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.Chinese.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.Chinese.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Chinese.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.Chinese.ForeColor = System.Drawing.Color.Black;
            this.Chinese.Location = new System.Drawing.Point(59, 22);
            this.Chinese.Name = "Chinese";
            this.Chinese.Size = new System.Drawing.Size(65, 42);
            this.Chinese.TabIndex = 26;
            this.Chinese.TabStop = true;
            this.Chinese.Tag = "";
            this.Chinese.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Chinese.UseVisualStyleBackColor = false;
            this.Chinese.Click += new System.EventHandler(this.Chinese_Click);
            // 
            // English
            // 
            this.English.Appearance = System.Windows.Forms.Appearance.Button;
            this.English.BackColor = System.Drawing.Color.Black;
            this.English.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("English.BackgroundImage")));
            this.English.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.English.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.English.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.English.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.English.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.English.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.English.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.English.ForeColor = System.Drawing.Color.Black;
            this.English.Location = new System.Drawing.Point(130, 22);
            this.English.Name = "English";
            this.English.Size = new System.Drawing.Size(65, 42);
            this.English.TabIndex = 25;
            this.English.Tag = "";
            this.English.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.English.UseVisualStyleBackColor = false;
            this.English.Click += new System.EventHandler(this.Chinese_Click);
            // 
            // lxcGroupBox3
            // 
            this.lxcGroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.lxcGroupBox3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lxcGroupBox3.BorderStyle = LXCSystem.Control.Base.Common.EnumBorderStyle.XStyle;
            this.lxcGroupBox3.CaptionColor = System.Drawing.Color.Silver;
            this.lxcGroupBox3.CaptionFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lxcGroupBox3.Controls.Add(this.Chinese);
            this.lxcGroupBox3.Controls.Add(this.label4);
            this.lxcGroupBox3.Controls.Add(this.English);
            this.lxcGroupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lxcGroupBox3.Location = new System.Drawing.Point(12, 4);
            this.lxcGroupBox3.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.lxcGroupBox3.Name = "lxcGroupBox3";
            this.lxcGroupBox3.Padding = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.lxcGroupBox3.Size = new System.Drawing.Size(720, 80);
            this.lxcGroupBox3.TabIndex = 97;
            this.lxcGroupBox3.TabStop = false;
            this.lxcGroupBox3.Text = "语       言";
            this.lxcGroupBox3.TextMargin = 6;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(201, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "此版本只支持中文";
            // 
            // lxcGroupBox1
            // 
            this.lxcGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.lxcGroupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lxcGroupBox1.BorderStyle = LXCSystem.Control.Base.Common.EnumBorderStyle.XStyle;
            this.lxcGroupBox1.CaptionColor = System.Drawing.Color.Silver;
            this.lxcGroupBox1.CaptionFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lxcGroupBox1.Controls.Add(this.lxcRadioButton2);
            this.lxcGroupBox1.Controls.Add(this.label5);
            this.lxcGroupBox1.Controls.Add(this.lxcRadioButton1);
            this.lxcGroupBox1.Controls.Add(this.lxcRadioButton4);
            this.lxcGroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lxcGroupBox1.Location = new System.Drawing.Point(12, 96);
            this.lxcGroupBox1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.lxcGroupBox1.Name = "lxcGroupBox1";
            this.lxcGroupBox1.Padding = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.lxcGroupBox1.Size = new System.Drawing.Size(720, 61);
            this.lxcGroupBox1.TabIndex = 97;
            this.lxcGroupBox1.TabStop = false;
            this.lxcGroupBox1.Text = "主       题";
            this.lxcGroupBox1.TextMargin = 6;
            // 
            // lxcRadioButton2
            // 
            this.lxcRadioButton2.AutoSize = true;
            this.lxcRadioButton2.BorderColor = System.Drawing.Color.White;
            this.lxcRadioButton2.CheckBoxBackColor = System.Drawing.Color.Black;
            this.lxcRadioButton2.CheckColor = System.Drawing.Color.Lime;
            this.lxcRadioButton2.InnerCircleRadius = 4;
            this.lxcRadioButton2.Location = new System.Drawing.Point(213, 22);
            this.lxcRadioButton2.MinimumSize = new System.Drawing.Size(22, 22);
            this.lxcRadioButton2.Name = "lxcRadioButton2";
            this.lxcRadioButton2.OuterCircleRadius = 8;
            this.lxcRadioButton2.Size = new System.Drawing.Size(50, 22);
            this.lxcRadioButton2.TabIndex = 4;
            this.lxcRadioButton2.Text = "蓝色";
            this.lxcRadioButton2.UseVisualStyleBackColor = true;
            this.lxcRadioButton2.Click += new System.EventHandler(this.lxcRadioButton4_Click);
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(286, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "此版本只支持深色";
            // 
            // lxcRadioButton1
            // 
            this.lxcRadioButton1.AutoSize = true;
            this.lxcRadioButton1.BorderColor = System.Drawing.Color.White;
            this.lxcRadioButton1.CheckBoxBackColor = System.Drawing.Color.Black;
            this.lxcRadioButton1.CheckColor = System.Drawing.Color.Lime;
            this.lxcRadioButton1.InnerCircleRadius = 4;
            this.lxcRadioButton1.Location = new System.Drawing.Point(136, 22);
            this.lxcRadioButton1.MinimumSize = new System.Drawing.Size(22, 22);
            this.lxcRadioButton1.Name = "lxcRadioButton1";
            this.lxcRadioButton1.OuterCircleRadius = 8;
            this.lxcRadioButton1.Size = new System.Drawing.Size(50, 22);
            this.lxcRadioButton1.TabIndex = 4;
            this.lxcRadioButton1.Text = "浅色";
            this.lxcRadioButton1.UseVisualStyleBackColor = true;
            this.lxcRadioButton1.Click += new System.EventHandler(this.lxcRadioButton4_Click);
            // 
            // lxcRadioButton4
            // 
            this.lxcRadioButton4.AutoSize = true;
            this.lxcRadioButton4.BorderColor = System.Drawing.Color.White;
            this.lxcRadioButton4.CheckBoxBackColor = System.Drawing.Color.Black;
            this.lxcRadioButton4.CheckColor = System.Drawing.Color.Lime;
            this.lxcRadioButton4.Checked = true;
            this.lxcRadioButton4.InnerCircleRadius = 4;
            this.lxcRadioButton4.Location = new System.Drawing.Point(59, 22);
            this.lxcRadioButton4.MinimumSize = new System.Drawing.Size(22, 22);
            this.lxcRadioButton4.Name = "lxcRadioButton4";
            this.lxcRadioButton4.OuterCircleRadius = 8;
            this.lxcRadioButton4.Size = new System.Drawing.Size(50, 22);
            this.lxcRadioButton4.TabIndex = 4;
            this.lxcRadioButton4.TabStop = true;
            this.lxcRadioButton4.Text = "深色";
            this.lxcRadioButton4.UseVisualStyleBackColor = true;
            this.lxcRadioButton4.Click += new System.EventHandler(this.lxcRadioButton4_Click);
            // 
            // lxcGroupBox4
            // 
            this.lxcGroupBox4.BackColor = System.Drawing.Color.Transparent;
            this.lxcGroupBox4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lxcGroupBox4.BorderStyle = LXCSystem.Control.Base.Common.EnumBorderStyle.XStyle;
            this.lxcGroupBox4.CaptionColor = System.Drawing.Color.Silver;
            this.lxcGroupBox4.CaptionFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lxcGroupBox4.Controls.Add(this.panel1);
            this.lxcGroupBox4.Controls.Add(this.label1);
            this.lxcGroupBox4.Controls.Add(this.label2);
            this.lxcGroupBox4.Controls.Add(this.vTextBox1);
            this.lxcGroupBox4.Controls.Add(this.label6);
            this.lxcGroupBox4.Controls.Add(this.label3);
            this.lxcGroupBox4.Controls.Add(this.tbx_companyName);
            this.lxcGroupBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lxcGroupBox4.Location = new System.Drawing.Point(12, 159);
            this.lxcGroupBox4.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.lxcGroupBox4.Name = "lxcGroupBox4";
            this.lxcGroupBox4.Padding = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.lxcGroupBox4.Size = new System.Drawing.Size(720, 120);
            this.lxcGroupBox4.TabIndex = 97;
            this.lxcGroupBox4.TabStop = false;
            this.lxcGroupBox4.Text = "定制设定";
            this.lxcGroupBox4.TextMargin = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(372, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(65, 65);
            this.panel1.TabIndex = 12;
            this.panel1.DoubleClick += new System.EventHandler(this.panel1_DoubleClick);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(441, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "双击更改图标";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(441, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 34);
            this.label2.TabIndex = 11;
            this.label2.Text = "Logo\r\n[64*64 PNG]";
            // 
            // vTextBox1
            // 
            this.vTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.vTextBox1.DefaultText = "请输入公司地址";
            this.vTextBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.vTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.vTextBox1.Location = new System.Drawing.Point(127, 60);
            this.vTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.vTextBox1.MaximumSize = new System.Drawing.Size(400, 22);
            this.vTextBox1.MinimumSize = new System.Drawing.Size(20, 22);
            this.vTextBox1.Name = "vTextBox1";
            this.vTextBox1.PasswordChar = false;
            this.vTextBox1.ReadOnly = false;
            this.vTextBox1.Size = new System.Drawing.Size(239, 22);
            this.vTextBox1.TabIndex = 21;
            this.vTextBox1.TextStr = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label6.Location = new System.Drawing.Point(61, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "公司地址：";
            // 
            // tbx_companyName
            // 
            this.tbx_companyName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tbx_companyName.DefaultText = "请输入公司名称";
            this.tbx_companyName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_companyName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbx_companyName.Location = new System.Drawing.Point(127, 30);
            this.tbx_companyName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_companyName.MaximumSize = new System.Drawing.Size(400, 22);
            this.tbx_companyName.MinimumSize = new System.Drawing.Size(20, 22);
            this.tbx_companyName.Name = "tbx_companyName";
            this.tbx_companyName.PasswordChar = false;
            this.tbx_companyName.ReadOnly = false;
            this.tbx_companyName.Size = new System.Drawing.Size(239, 22);
            this.tbx_companyName.TabIndex = 21;
            this.tbx_companyName.TextStr = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "PNG|*.png";
            // 
            // Win_GeneralSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(766, 427);
            this.Controls.Add(this.lxcGroupBox4);
            this.Controls.Add(this.lxcGroupBox1);
            this.Controls.Add(this.lxcGroupBox3);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Win_GeneralSettings";
            this.Text = "Win_GeneralSettings";
            this.VisibleChanged += new System.EventHandler(this.Win_GeneralSettings_VisibleChanged);
            this.lxcGroupBox3.ResumeLayout(false);
            this.lxcGroupBox1.ResumeLayout(false);
            this.lxcGroupBox1.PerformLayout();
            this.lxcGroupBox4.ResumeLayout(false);
            this.lxcGroupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Label label3;
        public VTextBox tbx_companyName;
        private System.Windows.Forms.RadioButton Chinese;
        private System.Windows.Forms.RadioButton English;
        private LXCSystem.Control.CommonCtrl.LxcGroupBox lxcGroupBox3;
        private LXCSystem.Control.CommonCtrl.LxcGroupBox lxcGroupBox1;
        private LXCSystem.Control.CommonCtrl.LxcRadioButton lxcRadioButton2;
        private LXCSystem.Control.CommonCtrl.LxcRadioButton lxcRadioButton1;
        private LXCSystem.Control.CommonCtrl.LxcRadioButton lxcRadioButton4;
        private LXCSystem.Control.CommonCtrl.LxcGroupBox lxcGroupBox4;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label5;
        public VTextBox vTextBox1;
        internal System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}