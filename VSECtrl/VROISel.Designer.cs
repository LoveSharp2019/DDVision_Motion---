namespace VControls
{
    partial class VROISel
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
            this.CB = new LXCSystem.Control.CommonCtrl.LxcComboBox();
            this.SuspendLayout();
            // 
            // CB
            // 
            this.CB.BackColor = System.Drawing.Color.White;
            this.CB.BackColorHover = System.Drawing.Color.Navy;
            this.CB.BackColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CB.BorderColor = System.Drawing.Color.Gray;
            this.CB.ComboxFont = new System.Drawing.Font("微软雅黑", 10F);
            this.CB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CB.Editable = false;
            this.CB.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.CB.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CB.ItemDrawMode = LXCSystem.Control.Base.Common.ListDrawMode.AutoDraw;
            this.CB.ListScrollAppearance.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.CB.ListScrollAppearance.BackColorNormal = System.Drawing.Color.Gray;
            this.CB.ListScrollAppearance.BackColorPressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.CB.ListScrollAppearance.ButtonColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.CB.ListScrollAppearance.ButtonColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CB.ListScrollAppearance.ButtonColorPressed = System.Drawing.Color.Gray;
            this.CB.Location = new System.Drawing.Point(0, 0);
            this.CB.Margin = new System.Windows.Forms.Padding(4);
            this.CB.Name = "CB";
            this.CB.SelectedItemBackColor = System.Drawing.Color.Black;
            this.CB.SelectedItemFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.CB.Size = new System.Drawing.Size(188, 28);
            this.CB.TabIndex = 1;
            this.CB.Text = null;
            this.CB.SelectedIndexChanged += new System.EventHandler(this.CB_SelectedIndexChanged);
            // 
            // VROISel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CB);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "VROISel";
            this.Size = new System.Drawing.Size(188, 28);
            this.ResumeLayout(false);

        }

        #endregion

        private LXCSystem.Control.CommonCtrl.LxcComboBox CB;
    }
}
