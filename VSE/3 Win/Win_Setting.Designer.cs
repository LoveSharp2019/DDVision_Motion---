namespace VSE
{
    partial class Win_Setting
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
           
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_Setting));
            this.pnl_window = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.syssetList1 = new System.Windows.Forms.SyssetList();
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
            this.XXXXXXXXXXXXXlbl_title.Size = new System.Drawing.Size(944, 25);
            this.XXXXXXXXXXXXXlbl_title.Text = "系统设置";
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx
            // 
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx.Panel2
            // 
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.Controls.Add(this.panel2);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Size = new System.Drawing.Size(976, 653);
            // 
            // pnl_window
            // 
            this.pnl_window.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_window.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnl_window.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnl_window.Location = new System.Drawing.Point(146, 0);
            this.pnl_window.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnl_window.Name = "pnl_window";
            this.pnl_window.Size = new System.Drawing.Size(830, 617);
            this.pnl_window.TabIndex = 20;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.pnl_window);
            this.panel2.Controls.Add(this.syssetList1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(976, 617);
            this.panel2.TabIndex = 106;
            // 
            // syssetList1
            // 
            this.syssetList1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.syssetList1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.syssetList1.Dock = System.Windows.Forms.DockStyle.Left;
            this.syssetList1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.syssetList1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.syssetList1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.syssetList1.FullRowSelect = true;
            this.syssetList1.HotTracking = true;
            this.syssetList1.ItemHeight = 40;
            this.syssetList1.Location = new System.Drawing.Point(0, 0);
            this.syssetList1.Name = "syssetList1";
         
            
            this.syssetList1.ShowLines = false;
            this.syssetList1.ShowRootLines = false;
            this.syssetList1.Size = new System.Drawing.Size(146, 617);
            this.syssetList1.TabIndex = 152;
            this.syssetList1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.syssetList1_NodeMouseClick);
            // 
            // Win_Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ClientSize = new System.Drawing.Size(980, 661);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(0, 525);
            this.Name = "Win_Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.Win_Setting_Load);
            this.VisibleChanged += new System.EventHandler(this.Win_Setting_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1)).EndInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.ResumeLayout(false);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.PerformLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xxxxxxxxxxxxxxxxxxxxxxx)).EndInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnl_window;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SyssetList syssetList1;
    }
}