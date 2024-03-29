namespace VControls
{
    partial class DisplayTableRow
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
            this.RowText = new System.Windows.Forms.Label();
            this.lineBottom = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RowText
            // 
            this.RowText.BackColor = System.Drawing.Color.Transparent;
            this.RowText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RowText.Location = new System.Drawing.Point(0, 0);
            this.RowText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.RowText.Name = "RowText";
            this.RowText.Size = new System.Drawing.Size(923, 37);
            this.RowText.TabIndex = 0;
            this.RowText.Text = "内容";
            this.RowText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lineBottom
            // 
            this.lineBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lineBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lineBottom.Location = new System.Drawing.Point(0, 36);
            this.lineBottom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lineBottom.Name = "lineBottom";
            this.lineBottom.Size = new System.Drawing.Size(923, 1);
            this.lineBottom.TabIndex = 0;
            // 
            // DisplayTableRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lineBottom);
            this.Controls.Add(this.RowText);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "DisplayTableRow";
            this.Size = new System.Drawing.Size(923, 37);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label RowText;
        private System.Windows.Forms.Label lineBottom;
    }
}
