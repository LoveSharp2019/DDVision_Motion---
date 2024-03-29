namespace VControls
{
    partial class ControlBox
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlBox));
            this.ButtonLock = new System.Windows.Forms.Button();
            this.ButtonMin = new System.Windows.Forms.Button();
            this.ButtonMax = new System.Windows.Forms.Button();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // ButtonLock
            // 
            this.ButtonLock.AutoSize = true;
            this.ButtonLock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ButtonLock.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonLock.BackgroundImage")));
            this.ButtonLock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonLock.FlatAppearance.BorderSize = 0;
            this.ButtonLock.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.ButtonLock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.ButtonLock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.ButtonLock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonLock.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ButtonLock.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.ButtonLock.Location = new System.Drawing.Point(4, 0);
            this.ButtonLock.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonLock.Name = "ButtonLock";
            this.ButtonLock.Size = new System.Drawing.Size(24, 24);
            this.ButtonLock.TabIndex = 3;
            this.ButtonLock.TabStop = false;
            this.ButtonLock.UseVisualStyleBackColor = false;
            this.ButtonLock.Click += new System.EventHandler(this.ButtonLock_Click);
            // 
            // ButtonMin
            // 
            this.ButtonMin.AutoSize = true;
            this.ButtonMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ButtonMin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonMin.BackgroundImage")));
            this.ButtonMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonMin.Enabled = false;
            this.ButtonMin.FlatAppearance.BorderSize = 0;
            this.ButtonMin.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.ButtonMin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.ButtonMin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.ButtonMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonMin.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ButtonMin.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.ButtonMin.Location = new System.Drawing.Point(34, 0);
            this.ButtonMin.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonMin.Name = "ButtonMin";
            this.ButtonMin.Size = new System.Drawing.Size(24, 24);
            this.ButtonMin.TabIndex = 4;
            this.ButtonMin.TabStop = false;
            this.ButtonMin.UseVisualStyleBackColor = false;
            this.ButtonMin.Click += new System.EventHandler(this.ButtonMin_Click);
            // 
            // ButtonMax
            // 
            this.ButtonMax.AutoSize = true;
            this.ButtonMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ButtonMax.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonMax.BackgroundImage")));
            this.ButtonMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonMax.FlatAppearance.BorderSize = 0;
            this.ButtonMax.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.ButtonMax.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.ButtonMax.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.ButtonMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonMax.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ButtonMax.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.ButtonMax.Location = new System.Drawing.Point(64, 0);
            this.ButtonMax.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonMax.Name = "ButtonMax";
            this.ButtonMax.Size = new System.Drawing.Size(24, 24);
            this.ButtonMax.TabIndex = 5;
            this.ButtonMax.TabStop = false;
            this.ButtonMax.UseVisualStyleBackColor = false;
            this.ButtonMax.Click += new System.EventHandler(this.ButtonMax_Click);
            // 
            // ButtonClose
            // 
            this.ButtonClose.AutoSize = true;
            this.ButtonClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ButtonClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonClose.BackgroundImage")));
            this.ButtonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonClose.FlatAppearance.BorderSize = 0;
            this.ButtonClose.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.ButtonClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.ButtonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonClose.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ButtonClose.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.ButtonClose.Location = new System.Drawing.Point(94, 0);
            this.ButtonClose.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(24, 24);
            this.ButtonClose.TabIndex = 6;
            this.ButtonClose.TabStop = false;
            this.ButtonClose.UseVisualStyleBackColor = false;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "锁定.png");
            this.imageList1.Images.SetKeyName(1, "解锁.png");
            this.imageList1.Images.SetKeyName(2, "最小化.png");
            this.imageList1.Images.SetKeyName(3, "最大化A.png");
            this.imageList1.Images.SetKeyName(4, "最大化B.png");
            this.imageList1.Images.SetKeyName(5, "关闭.png");
            // 
            // ControlBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.Controls.Add(this.ButtonLock);
            this.Controls.Add(this.ButtonMin);
            this.Controls.Add(this.ButtonMax);
            this.Controls.Add(this.ButtonClose);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ControlBox";
            this.Size = new System.Drawing.Size(122, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonLock;
        private System.Windows.Forms.Button ButtonMin;
        private System.Windows.Forms.Button ButtonMax;
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.ImageList imageList1;
    }
}
