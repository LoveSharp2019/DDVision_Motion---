namespace VSE
{
    partial class Win_ComConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_ComConfig));
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_communcationType = new System.Windows.Forms.ComboBox();
            this.dgv_comConfig = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_deleteItem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xxxxxxxxxxxxxxxxxxxxxxx)).BeginInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.SuspendLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_comConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // XXXXXXXXXXXXXlbl_title
            // 
            this.XXXXXXXXXXXXXlbl_title.BackColor = System.Drawing.Color.Transparent;
            this.XXXXXXXXXXXXXlbl_title.Size = new System.Drawing.Size(1079, 25);
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx
            // 
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx.Panel1
            // 
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.xxxxxxxxxxxxxxxxxxxxxxx.Size = new System.Drawing.Size(1111, 732);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(6, 70);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "通讯方式：";
            // 
            // cbx_communcationType
            // 
            this.cbx_communcationType.BackColor = System.Drawing.Color.DarkGray;
            this.cbx_communcationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_communcationType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbx_communcationType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_communcationType.FormattingEnabled = true;
            this.cbx_communcationType.Items.AddRange(new object[] {
            "无",
            "以太网客户端",
            "以太网服务端",
            "串口",
            "IO信号"});
            this.cbx_communcationType.Location = new System.Drawing.Point(70, 67);
            this.cbx_communcationType.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbx_communcationType.Name = "cbx_communcationType";
            this.cbx_communcationType.Size = new System.Drawing.Size(180, 25);
            this.cbx_communcationType.TabIndex = 1;
            // 
            // dgv_comConfig
            // 
            this.dgv_comConfig.AllowUserToDeleteRows = false;
            this.dgv_comConfig.AllowUserToResizeRows = false;
            this.dgv_comConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_comConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column7,
            this.Column5,
            this.Column4,
            this.Column6,
            this.Column3});
            this.dgv_comConfig.Location = new System.Drawing.Point(10, 104);
            this.dgv_comConfig.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgv_comConfig.Name = "dgv_comConfig";
            this.dgv_comConfig.RowHeadersVisible = false;
            this.dgv_comConfig.RowTemplate.Height = 23;
            this.dgv_comConfig.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_comConfig.Size = new System.Drawing.Size(1094, 628);
            this.dgv_comConfig.TabIndex = 13;
            this.dgv_comConfig.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_comConfig_EditingControlShowing);
            this.dgv_comConfig.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgv_comConfig_RowsAdded);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "编号";
            this.Column1.Name = "Column1";
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "接收指令";
            this.Column2.Name = "Column2";
            // 
            // Column7
            // 
            this.Column7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Column7.HeaderText = "流程";
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column7.Width = 150;
            // 
            // Column5
            // 
            this.Column5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Column5.HeaderText = "返回项";
            this.Column5.Name = "Column5";
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column5.Width = 300;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "NG返回值";
            this.Column4.Name = "Column4";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "前缀";
            this.Column6.Name = "Column6";
            this.Column6.Width = 115;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "后缀";
            this.Column3.Name = "Column3";
            this.Column3.Width = 110;
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_save.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_save.Location = new System.Drawing.Point(895, 59);
            this.btn_save.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(101, 39);
            this.btn_save.TabIndex = 15;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_deleteItem
            // 
            this.btn_deleteItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_deleteItem.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btn_deleteItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_deleteItem.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_deleteItem.Location = new System.Drawing.Point(1001, 59);
            this.btn_deleteItem.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_deleteItem.Name = "btn_deleteItem";
            this.btn_deleteItem.Size = new System.Drawing.Size(101, 39);
            this.btn_deleteItem.TabIndex = 17;
            this.btn_deleteItem.Text = "删除项";
            this.btn_deleteItem.UseVisualStyleBackColor = false;
            this.btn_deleteItem.Click += new System.EventHandler(this.btn_deleteItem_Click);
            // 
            // Win_ComConfig
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ClientSize = new System.Drawing.Size(1115, 740);
            this.Controls.Add(this.btn_deleteItem);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.dgv_comConfig);
            this.Controls.Add(this.cbx_communcationType);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Win_ComConfig";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通讯配置";
            this.Load += new System.EventHandler(this.Win_ComConfig_Load);
            this.Shown += new System.EventHandler(this.Win_ComConfig_Shown);
            this.Controls.SetChildIndex(this.xxxxxxxxxxxxxxxxxxxxxxx, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cbx_communcationType, 0);
            this.Controls.SetChildIndex(this.dgv_comConfig, 0);
            this.Controls.SetChildIndex(this.btn_save, 0);
            this.Controls.SetChildIndex(this.btn_deleteItem, 0);
            ((System.ComponentModel.ISupportInitialize)(this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1)).EndInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.ResumeLayout(false);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xxxxxxxxxxxxxxxxxxxxxxx)).EndInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_comConfig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView dgv_comConfig;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox cbx_communcationType;
        internal System.Windows.Forms.Button btn_save;
        internal System.Windows.Forms.Button btn_deleteItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column7;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}