namespace VSE
{
    partial class Win_EngineManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_EngineManager));
            this.label1 = new System.Windows.Forms.Label();
            this.tbx_engineName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button44 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dgv_engineInfo = new VControls.DisplayTable();
            this.cbx_engineList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前方案：";
            // 
            // tbx_engineName
            // 
            this.tbx_engineName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbx_engineName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbx_engineName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbx_engineName.Location = new System.Drawing.Point(443, 445);
            this.tbx_engineName.Name = "tbx_engineName";
            this.tbx_engineName.Size = new System.Drawing.Size(268, 23);
            this.tbx_engineName.TabIndex = 8;
            this.tbx_engineName.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(402, 447);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "方案名：";
            this.label2.Visible = false;
            // 
            // btn_confirm
            // 
            this.btn_confirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_confirm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_confirm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RosyBrown;
            this.btn_confirm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_confirm.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_confirm.ForeColor = System.Drawing.Color.White;
            this.btn_confirm.Location = new System.Drawing.Point(653, 171);
            this.btn_confirm.Margin = new System.Windows.Forms.Padding(5);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(71, 30);
            this.btn_confirm.TabIndex = 106;
            this.btn_confirm.Text = "导出";
            this.btn_confirm.UseVisualStyleBackColor = true;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            // 
            // button44
            // 
            this.button44.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button44.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button44.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RosyBrown;
            this.button44.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button44.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button44.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button44.ForeColor = System.Drawing.Color.White;
            this.button44.Location = new System.Drawing.Point(653, 105);
            this.button44.Margin = new System.Windows.Forms.Padding(5);
            this.button44.Name = "button44";
            this.button44.Size = new System.Drawing.Size(71, 30);
            this.button44.TabIndex = 108;
            this.button44.Text = "克隆";
            this.button44.UseVisualStyleBackColor = true;
            this.button44.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RosyBrown;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(653, 204);
            this.button5.Margin = new System.Windows.Forms.Padding(5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(71, 30);
            this.button5.TabIndex = 109;
            this.button5.Text = "删除";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button7
            // 
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button7.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RosyBrown;
            this.button7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(653, 138);
            this.button7.Margin = new System.Windows.Forms.Padding(5);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(71, 30);
            this.button7.TabIndex = 111;
            this.button7.Text = "导入";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RosyBrown;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(653, 72);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 30);
            this.button1.TabIndex = 165;
            this.button1.Text = "新建";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "流程列表：";
            // 
            // dgv_engineInfo
            // 
            this.dgv_engineInfo.AutoScroll = true;
            this.dgv_engineInfo.BackColor = System.Drawing.Color.Transparent;
            this.dgv_engineInfo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgv_engineInfo.ForeColor = System.Drawing.Color.Silver;
            this.dgv_engineInfo.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgv_engineInfo.Location = new System.Drawing.Point(13, 72);
            this.dgv_engineInfo.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_engineInfo.Name = "dgv_engineInfo";
            this.dgv_engineInfo.RowCount = 0;
            this.dgv_engineInfo.RowHeight = 35;
            this.dgv_engineInfo.Size = new System.Drawing.Size(631, 440);
            this.dgv_engineInfo.TabIndex = 168;
            // 
            // cbx_engineList
            // 
            this.cbx_engineList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbx_engineList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbx_engineList.ForeColor = System.Drawing.SystemColors.Window;
            this.cbx_engineList.FormattingEnabled = true;
            this.cbx_engineList.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.cbx_engineList.Location = new System.Drawing.Point(89, 9);
            this.cbx_engineList.Name = "cbx_engineList";
            this.cbx_engineList.Size = new System.Drawing.Size(157, 25);
            this.cbx_engineList.TabIndex = 169;
            this.cbx_engineList.SelectedIndexChanged += new System.EventHandler(this.cbx_engineList_SelectedIndexChanged_1);
            // 
            // Win_EngineManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(763, 567);
            this.Controls.Add(this.cbx_engineList);
            this.Controls.Add(this.dgv_engineInfo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbx_engineName);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button44);
            this.Controls.Add(this.button5);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Win_EngineManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "方案管理";
            this.VisibleChanged += new System.EventHandler(this.Win_EngineManager_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btn_confirm;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button button44;
        public System.Windows.Forms.Button button5;
        public System.Windows.Forms.Button button7;
        internal System.Windows.Forms.TextBox tbx_engineName;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        public VControls.DisplayTable dgv_engineInfo;
        public System.Windows.Forms.ComboBox cbx_engineList;
    }
}