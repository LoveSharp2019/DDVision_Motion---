﻿namespace VControls
{
    partial class VButton
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_button
            // 
            this.btn_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btn_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.btn_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_button.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_button.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_button.Location = new System.Drawing.Point(0, 0);
            this.btn_button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_button.Name = "btn_button";
            this.btn_button.Size = new System.Drawing.Size(107, 38);
            this.btn_button.TabIndex = 0;
            this.btn_button.Text = "Button";
            this.btn_button.UseVisualStyleBackColor = false;
            this.btn_button.Click += new System.EventHandler(this.btn_button_Click);
            // 
            // CButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btn_button);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CButton";
            this.Size = new System.Drawing.Size(107, 38);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_button;
    }
}
