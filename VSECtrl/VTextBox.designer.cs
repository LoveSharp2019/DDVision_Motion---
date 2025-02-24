﻿namespace VControls
{
    partial class VTextBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VTextBox));
            this.lbl_line = new System.Windows.Forms.Label();
            this.tbx_text = new System.Windows.Forms.TextBox();
            this.btn_eye = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_line
            // 
            this.lbl_line.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_line.BackColor = System.Drawing.Color.Gray;
            this.lbl_line.Location = new System.Drawing.Point(2, 22);
            this.lbl_line.Name = "lbl_line";
            this.lbl_line.Size = new System.Drawing.Size(152, 1);
            this.lbl_line.TabIndex = 10;
            // 
            // tbx_text
            // 
            this.tbx_text.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbx_text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbx_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbx_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbx_text.Location = new System.Drawing.Point(7, 1);
            this.tbx_text.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.tbx_text.Name = "tbx_text";
            this.tbx_text.Size = new System.Drawing.Size(143, 18);
            this.tbx_text.TabIndex = 9;
            this.tbx_text.TextChanged += new System.EventHandler(this.tbx_text_TextChanged);
            this.tbx_text.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbx_text_KeyDown);
            this.tbx_text.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbx_text_MouseUp);
            // 
            // btn_eye
            // 
            this.btn_eye.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_eye.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_eye.BackgroundImage")));
            this.btn_eye.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_eye.FlatAppearance.BorderSize = 0;
            this.btn_eye.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_eye.Location = new System.Drawing.Point(126, 1);
            this.btn_eye.Name = "btn_eye";
            this.btn_eye.Size = new System.Drawing.Size(24, 20);
            this.btn_eye.TabIndex = 11;
            this.btn_eye.TabStop = false;
            this.btn_eye.UseVisualStyleBackColor = true;
            this.btn_eye.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_eye_MouseDown);
            this.btn_eye.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_eye_MouseUp);
            // 
            // VTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.btn_eye);
            this.Controls.Add(this.lbl_line);
            this.Controls.Add(this.tbx_text);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(457, 25);
            this.MinimumSize = new System.Drawing.Size(23, 25);
            this.Name = "VTextBox";
            this.Size = new System.Drawing.Size(157, 25);
            this.Load += new System.EventHandler(this.TextBox_Load);
            this.Enter += new System.EventHandler(this.TextBox_Enter);
            this.Leave += new System.EventHandler(this.TextBox_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_line;
        private System.Windows.Forms.TextBox tbx_text;
        private System.Windows.Forms.Button btn_eye;
    }
}
