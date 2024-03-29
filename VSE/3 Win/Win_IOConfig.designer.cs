namespace VSE
{
    partial class Win_IOConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_IOConfig));
            this.btn_cancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBox1 = new LXCSystem.Control.CommonCtrl.LxcComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xxxxxxxxxxxxxxxxxxxxxxx)).BeginInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.SuspendLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.SuspendLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // XXXXXXXXXXXXXlbl_title
            // 
            this.XXXXXXXXXXXXXlbl_title.Size = new System.Drawing.Size(470, 25);
            this.XXXXXXXXXXXXXlbl_title.Text = "编辑终端";
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx
            // 
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx.Panel2
            // 
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.Controls.Add(this.panel2);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Size = new System.Drawing.Size(502, 371);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_cancel.BackgroundImage")));
            this.btn_cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cancel.ForeColor = System.Drawing.Color.White;
            this.btn_cancel.Location = new System.Drawing.Point(426, 295);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(65, 30);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "关闭";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.treeView1);
            this.panel2.Controls.Add(this.btn_cancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(502, 335);
            this.panel2.TabIndex = 106;
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.White;
            this.comboBox1.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.comboBox1.BackColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboBox1.BorderColor = System.Drawing.Color.Gray;
            this.comboBox1.ComboxFont = new System.Drawing.Font("微软雅黑", 12F);
            this.comboBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.comboBox1.ItemDrawMode = LXCSystem.Control.Base.Common.ListDrawMode.AutoDraw;
            this.comboBox1.ListScrollAppearance.BackColorHover = System.Drawing.Color.Empty;
            this.comboBox1.ListScrollAppearance.BackColorNormal = System.Drawing.Color.Empty;
            this.comboBox1.ListScrollAppearance.BackColorPressed = System.Drawing.Color.Empty;
            this.comboBox1.ListScrollAppearance.ButtonColorHover = System.Drawing.Color.Empty;
            this.comboBox1.ListScrollAppearance.ButtonColorNormal = System.Drawing.Color.Empty;
            this.comboBox1.ListScrollAppearance.ButtonColorPressed = System.Drawing.Color.Empty;
            this.comboBox1.Location = new System.Drawing.Point(46, 6);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.comboBox1.SelectedItemFontColor = System.Drawing.Color.White;
            this.comboBox1.Size = new System.Drawing.Size(130, 25);
            this.comboBox1.TabIndex = 111;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 109;
            this.label1.Text = "工具：";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.treeView1.Location = new System.Drawing.Point(12, 37);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(479, 252);
            this.treeView1.TabIndex = 106;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // Win_IOConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ClientSize = new System.Drawing.Size(506, 379);
            this.Name = "Win_IOConfig";
            this.Text = "Win_ConfirmBox";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Win_IOConfig_FormClosing);
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
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label1;
        internal LXCSystem.Control.CommonCtrl.LxcComboBox comboBox1;
    }
}