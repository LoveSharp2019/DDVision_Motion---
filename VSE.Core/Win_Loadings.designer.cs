namespace VSE.Core
{
    partial class frmLoadings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoadings));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelMsg = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.labelSpan = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(180, 59);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(253, 205);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBar.Location = new System.Drawing.Point(147, 287);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(333, 23);
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;
            // 
            // labelMsg
            // 
            this.labelMsg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelMsg.AutoSize = true;
            this.labelMsg.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelMsg.ForeColor = System.Drawing.Color.Lime;
            this.labelMsg.Location = new System.Drawing.Point(147, 264);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new System.Drawing.Size(93, 20);
            this.labelMsg.TabIndex = 3;
            this.labelMsg.Text = "加载进度提示";
            this.labelMsg.Visible = false;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 300;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // labelSpan
            // 
            this.labelSpan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelSpan.AutoSize = true;
            this.labelSpan.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelSpan.ForeColor = System.Drawing.Color.Yellow;
            this.labelSpan.Location = new System.Drawing.Point(147, 317);
            this.labelSpan.Name = "labelSpan";
            this.labelSpan.Size = new System.Drawing.Size(68, 20);
            this.labelSpan.TabIndex = 4;
            this.labelSpan.Text = "单项时间:";
            this.labelSpan.Visible = false;
            // 
            // frmLoadings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(615, 365);
            this.Controls.Add(this.labelMsg);
            this.Controls.Add(this.labelSpan);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLoadings";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Loading";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmLoadings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelMsg;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label labelSpan;
        #endregion
    }
}