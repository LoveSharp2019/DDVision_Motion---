namespace VSE
{
    partial class Win_Monitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_Monitor));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lxcRadioButton1 = new LXCSystem.Control.CommonCtrl.LxcRadioButton();
            this.lxcRadioButton4 = new LXCSystem.Control.CommonCtrl.LxcRadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.displayTable1 = new VControls.DisplayTable();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lxcRadioButton1
            // 
            this.lxcRadioButton1.AutoSize = true;
            this.lxcRadioButton1.BorderColor = System.Drawing.Color.White;
            this.lxcRadioButton1.CheckBoxBackColor = System.Drawing.Color.Black;
            this.lxcRadioButton1.CheckColor = System.Drawing.Color.Lime;
            this.lxcRadioButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lxcRadioButton1.InnerCircleRadius = 4;
            this.lxcRadioButton1.Location = new System.Drawing.Point(109, 3);
            this.lxcRadioButton1.MinimumSize = new System.Drawing.Size(22, 22);
            this.lxcRadioButton1.Name = "lxcRadioButton1";
            this.lxcRadioButton1.OuterCircleRadius = 8;
            this.lxcRadioButton1.Size = new System.Drawing.Size(74, 22);
            this.lxcRadioButton1.TabIndex = 120;
            this.lxcRadioButton1.Text = "全局变量";
            this.lxcRadioButton1.UseVisualStyleBackColor = true;
            this.lxcRadioButton1.CheckedChanged += new System.EventHandler(this.lxcRadioButton1_CheckedChanged);
            // 
            // lxcRadioButton4
            // 
            this.lxcRadioButton4.AutoSize = true;
            this.lxcRadioButton4.BorderColor = System.Drawing.Color.White;
            this.lxcRadioButton4.CheckBoxBackColor = System.Drawing.Color.Black;
            this.lxcRadioButton4.CheckColor = System.Drawing.Color.Lime;
            this.lxcRadioButton4.Checked = true;
            this.lxcRadioButton4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lxcRadioButton4.InnerCircleRadius = 4;
            this.lxcRadioButton4.Location = new System.Drawing.Point(3, 3);
            this.lxcRadioButton4.MinimumSize = new System.Drawing.Size(22, 22);
            this.lxcRadioButton4.Name = "lxcRadioButton4";
            this.lxcRadioButton4.OuterCircleRadius = 8;
            this.lxcRadioButton4.Size = new System.Drawing.Size(74, 22);
            this.lxcRadioButton4.TabIndex = 121;
            this.lxcRadioButton4.TabStop = true;
            this.lxcRadioButton4.Text = "流程输出";
            this.lxcRadioButton4.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lxcRadioButton4);
            this.panel1.Controls.Add(this.lxcRadioButton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(631, 29);
            this.panel1.TabIndex = 122;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(201, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 17);
            this.label1.TabIndex = 122;
            this.label1.Text = "调试专用-此版本不支持图形变量监控";
            // 
            // displayTable1
            // 
            this.displayTable1.AutoScroll = true;
            this.displayTable1.BackColor = System.Drawing.Color.Transparent;
            this.displayTable1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayTable1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.displayTable1.ForeColor = System.Drawing.Color.Green;
            this.displayTable1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.displayTable1.Location = new System.Drawing.Point(0, 29);
            this.displayTable1.Margin = new System.Windows.Forms.Padding(4);
            this.displayTable1.Name = "displayTable1";
            this.displayTable1.RowCount = 3;
            this.displayTable1.RowHeight = 35;
            this.displayTable1.Size = new System.Drawing.Size(631, 214);
            this.displayTable1.TabIndex = 0;
            // 
            // Win_Monitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(631, 243);
            this.CloseButtonVisible = false;
            this.Controls.Add(this.displayTable1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Win_Monitor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "输出监控";
            this.Load += new System.EventHandler(this.Win_Monitor_Load);
            this.VisibleChanged += new System.EventHandler(this.Win_Monitor_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private VControls.DisplayTable displayTable1;
        private LXCSystem.Control.CommonCtrl.LxcRadioButton lxcRadioButton1;
        private LXCSystem.Control.CommonCtrl.LxcRadioButton lxcRadioButton4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}