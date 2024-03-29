namespace VControls
{
    partial class ToolWinBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolWinBase));
            this.PImageWin = new Lxc.VisionPlus.ImageView.ImgView();
            this.Ppanel2 = new System.Windows.Forms.Panel();
            this.Plbl_runTime = new System.Windows.Forms.Label();
            this.Plbl_toolTip = new System.Windows.Forms.Label();
            this.Pbtn_close = new System.Windows.Forms.Button();
            this.Pbtn_runTool = new System.Windows.Forms.Button();
            this.Ppanel3 = new System.Windows.Forms.Panel();
            this.xxxxxxxxxxxxxxxxxxxxxxxxxxlabel2 = new System.Windows.Forms.Label();
            this.xxxxxxxxxxxxxxxxxxxswitchButton1 = new LXCSystem.Control.SwitchButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Pbtn_help = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xxxxxxxxxxxxxxxxxxxxxxx)).BeginInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.SuspendLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.SuspendLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.SuspendLayout();
            this.Ppanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // XXXXXXXXXXXXXlbl_title
            // 
            this.XXXXXXXXXXXXXlbl_title.Size = new System.Drawing.Size(964, 25);
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx
            // 
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx.Panel2
            // 
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.Controls.Add(this.PImageWin);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.Controls.Add(this.Ppanel2);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.Controls.Add(this.Ppanel3);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Size = new System.Drawing.Size(996, 622);
            // 
            // PImageWin
            // 
            this.PImageWin.BackColor = System.Drawing.Color.Transparent;
            this.PImageWin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PImageWin.Caption = "label2";
            this.PImageWin.CaptionVisible = false;
            this.PImageWin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PImageWin.DrawModel = false;
            this.PImageWin.EditModel = Lxc.VisionPlus.ImageView.ImgView.winMode.Menu;
            this.PImageWin.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PImageWin.Location = new System.Drawing.Point(0, 0);
            this.PImageWin.Margin = new System.Windows.Forms.Padding(1);
            this.PImageWin.Name = "PImageWin";
            this.PImageWin.Size = new System.Drawing.Size(652, 531);
            this.PImageWin.StaticWnd = false;
            this.PImageWin.TabIndex = 90;
            // 
            // Ppanel2
            // 
            this.Ppanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.Ppanel2.Location = new System.Drawing.Point(652, 0);
            this.Ppanel2.Margin = new System.Windows.Forms.Padding(1);
            this.Ppanel2.Name = "Ppanel2";
            this.Ppanel2.Padding = new System.Windows.Forms.Padding(1);
            this.Ppanel2.Size = new System.Drawing.Size(344, 531);
            this.Ppanel2.TabIndex = 89;
            // 
            // Plbl_runTime
            // 
            this.Plbl_runTime.AutoSize = true;
            this.Plbl_runTime.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Plbl_runTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Plbl_runTime.Location = new System.Drawing.Point(188, 6);
            this.Plbl_runTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Plbl_runTime.Name = "Plbl_runTime";
            this.Plbl_runTime.Size = new System.Drawing.Size(78, 20);
            this.Plbl_runTime.TabIndex = 115;
            this.Plbl_runTime.Text = "耗时：0ms";
            this.Plbl_runTime.Click += new System.EventHandler(this.Plbl_runTime_Click);
            // 
            // Plbl_toolTip
            // 
            this.Plbl_toolTip.AutoSize = true;
            this.Plbl_toolTip.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Plbl_toolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Plbl_toolTip.Location = new System.Drawing.Point(188, 28);
            this.Plbl_toolTip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Plbl_toolTip.Name = "Plbl_toolTip";
            this.Plbl_toolTip.Size = new System.Drawing.Size(65, 20);
            this.Plbl_toolTip.TabIndex = 114;
            this.Plbl_toolTip.Text = "状态：无";
            // 
            // Pbtn_close
            // 
            this.Pbtn_close.BackColor = System.Drawing.Color.Transparent;
            this.Pbtn_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pbtn_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Pbtn_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.Pbtn_close.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.Pbtn_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Pbtn_close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Pbtn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Pbtn_close.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Pbtn_close.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Pbtn_close.Location = new System.Drawing.Point(195, 4);
            this.Pbtn_close.Margin = new System.Windows.Forms.Padding(4);
            this.Pbtn_close.Name = "Pbtn_close";
            this.Pbtn_close.Padding = new System.Windows.Forms.Padding(2);
            this.Pbtn_close.Size = new System.Drawing.Size(96, 43);
            this.Pbtn_close.TabIndex = 111;
            this.Pbtn_close.Text = "关闭";
            this.Pbtn_close.UseVisualStyleBackColor = false;
            // 
            // Pbtn_runTool
            // 
            this.Pbtn_runTool.BackColor = System.Drawing.Color.Transparent;
            this.Pbtn_runTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pbtn_runTool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Pbtn_runTool.Dock = System.Windows.Forms.DockStyle.Right;
            this.Pbtn_runTool.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.Pbtn_runTool.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Pbtn_runTool.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Pbtn_runTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Pbtn_runTool.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Pbtn_runTool.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Pbtn_runTool.Location = new System.Drawing.Point(99, 4);
            this.Pbtn_runTool.Margin = new System.Windows.Forms.Padding(4);
            this.Pbtn_runTool.Name = "Pbtn_runTool";
            this.Pbtn_runTool.Padding = new System.Windows.Forms.Padding(2);
            this.Pbtn_runTool.Size = new System.Drawing.Size(96, 43);
            this.Pbtn_runTool.TabIndex = 0;
            this.Pbtn_runTool.TabStop = false;
            this.Pbtn_runTool.Text = "试运行";
            this.Pbtn_runTool.UseVisualStyleBackColor = false;
            // 
            // Ppanel3
            // 
            this.Ppanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Ppanel3.Controls.Add(this.xxxxxxxxxxxxxxxxxxxxxxxxxxlabel2);
            this.Ppanel3.Controls.Add(this.xxxxxxxxxxxxxxxxxxxswitchButton1);
            this.Ppanel3.Controls.Add(this.panel2);
            this.Ppanel3.Controls.Add(this.Plbl_toolTip);
            this.Ppanel3.Controls.Add(this.Plbl_runTime);
            this.Ppanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Ppanel3.Location = new System.Drawing.Point(0, 531);
            this.Ppanel3.Margin = new System.Windows.Forms.Padding(0);
            this.Ppanel3.Name = "Ppanel3";
            this.Ppanel3.Padding = new System.Windows.Forms.Padding(1);
            this.Ppanel3.Size = new System.Drawing.Size(996, 55);
            this.Ppanel3.TabIndex = 91;
            // 
            // xxxxxxxxxxxxxxxxxxxxxxxxxxlabel2
            // 
            this.xxxxxxxxxxxxxxxxxxxxxxxxxxlabel2.AutoSize = true;
            this.xxxxxxxxxxxxxxxxxxxxxxxxxxlabel2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xxxxxxxxxxxxxxxxxxxxxxxxxxlabel2.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.xxxxxxxxxxxxxxxxxxxxxxxxxxlabel2.Location = new System.Drawing.Point(5, 19);
            this.xxxxxxxxxxxxxxxxxxxxxxxxxxlabel2.Name = "xxxxxxxxxxxxxxxxxxxxxxxxxxlabel2";
            this.xxxxxxxxxxxxxxxxxxxxxxxxxxlabel2.Size = new System.Drawing.Size(37, 19);
            this.xxxxxxxxxxxxxxxxxxxxxxxxxxlabel2.TabIndex = 117;
            this.xxxxxxxxxxxxxxxxxxxxxxxxxxlabel2.Text = "启用";
            // 
            // xxxxxxxxxxxxxxxxxxxswitchButton1
            // 
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.Appearance = System.Windows.Forms.Appearance.Button;
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.BackColor = System.Drawing.Color.Transparent;
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("xxxxxxxxxxxxxxxxxxxswitchButton1.BackgroundImage")));
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.CheckImage = ((System.Drawing.Image)(resources.GetObject("xxxxxxxxxxxxxxxxxxxswitchButton1.CheckImage")));
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.FlatAppearance.BorderSize = 0;
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.Location = new System.Drawing.Point(49, 9);
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.Margin = new System.Windows.Forms.Padding(4);
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.Name = "xxxxxxxxxxxxxxxxxxxswitchButton1";
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.Size = new System.Drawing.Size(97, 39);
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.TabIndex = 116;
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.TabStop = false;
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.UnCheckImage = ((System.Drawing.Image)(resources.GetObject("xxxxxxxxxxxxxxxxxxxswitchButton1.UnCheckImage")));
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.UseVisualStyleBackColor = false;
            this.xxxxxxxxxxxxxxxxxxxswitchButton1.Click += new System.EventHandler(this.switchButton1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.panel2.Controls.Add(this.Pbtn_help);
            this.panel2.Controls.Add(this.Pbtn_runTool);
            this.panel2.Controls.Add(this.Pbtn_close);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(700, 1);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.panel2.Size = new System.Drawing.Size(293, 51);
            this.panel2.TabIndex = 112;
            // 
            // Pbtn_help
            // 
            this.Pbtn_help.BackColor = System.Drawing.Color.Transparent;
            this.Pbtn_help.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pbtn_help.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Pbtn_help.Dock = System.Windows.Forms.DockStyle.Right;
            this.Pbtn_help.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.Pbtn_help.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Pbtn_help.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Pbtn_help.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Pbtn_help.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Pbtn_help.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Pbtn_help.Location = new System.Drawing.Point(3, 4);
            this.Pbtn_help.Margin = new System.Windows.Forms.Padding(4);
            this.Pbtn_help.Name = "Pbtn_help";
            this.Pbtn_help.Padding = new System.Windows.Forms.Padding(2);
            this.Pbtn_help.Size = new System.Drawing.Size(96, 43);
            this.Pbtn_help.TabIndex = 112;
            this.Pbtn_help.TabStop = false;
            this.Pbtn_help.Text = "帮助";
            this.Pbtn_help.UseVisualStyleBackColor = false;
            // 
            // ToolWinBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1000, 630);
            this.Name = "ToolWinBase";
            this.Load += new System.EventHandler(this.ToolWinBase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1)).EndInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.ResumeLayout(false);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.PerformLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xxxxxxxxxxxxxxxxxxxxxxx)).EndInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.ResumeLayout(false);
            this.Ppanel3.ResumeLayout(false);
            this.Ppanel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public Lxc.VisionPlus.ImageView.ImgView PImageWin;
        public System.Windows.Forms.Panel Ppanel2;
        public System.Windows.Forms.Label Plbl_runTime;
        public System.Windows.Forms.Label Plbl_toolTip;
        public System.Windows.Forms.Button Pbtn_close;
        public System.Windows.Forms.Button Pbtn_runTool;
        public System.Windows.Forms.Panel Ppanel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label xxxxxxxxxxxxxxxxxxxxxxxxxxlabel2;
        private LXCSystem.Control.SwitchButton xxxxxxxxxxxxxxxxxxxswitchButton1;
        public System.Windows.Forms.Button Pbtn_help;
    }
}