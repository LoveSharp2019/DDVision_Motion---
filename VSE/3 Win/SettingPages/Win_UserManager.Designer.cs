using VControls;
namespace VSE
{
    partial class Win_UserManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_UserManager));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lxcRadioButton1 = new LXCSystem.Control.CommonCtrl.LxcRadioButton();
            this.lxcRadioButton4 = new LXCSystem.Control.CommonCtrl.LxcRadioButton();
            this.OrgPassword = new System.Windows.Forms.TextBox();
            this.NewPassword = new System.Windows.Forms.TextBox();
            this.AgainPassword = new System.Windows.Forms.TextBox();
            this.OrgPasswordTip = new System.Windows.Forms.Label();
            this.NewPasswordTip = new System.Windows.Forms.Label();
            this.AgainPasswordTip = new System.Windows.Forms.Label();
            this.lxcButton2 = new LXCSystem.Control.CommonCtrl.LxcButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lxcButton1 = new LXCSystem.Control.CommonCtrl.LxcButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.Location = new System.Drawing.Point(37, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "原始密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(37, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "修改密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label3.Location = new System.Drawing.Point(37, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "密码确认：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label5.Location = new System.Drawing.Point(37, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 21);
            this.label5.TabIndex = 13;
            this.label5.Text = "账      户：";
            // 
            // lxcRadioButton1
            // 
            this.lxcRadioButton1.AutoSize = true;
            this.lxcRadioButton1.BorderColor = System.Drawing.Color.White;
            this.lxcRadioButton1.CheckBoxBackColor = System.Drawing.Color.Black;
            this.lxcRadioButton1.CheckColor = System.Drawing.Color.Lime;
            this.lxcRadioButton1.InnerCircleRadius = 4;
            this.lxcRadioButton1.Location = new System.Drawing.Point(208, 16);
            this.lxcRadioButton1.MinimumSize = new System.Drawing.Size(22, 22);
            this.lxcRadioButton1.Name = "lxcRadioButton1";
            this.lxcRadioButton1.OuterCircleRadius = 8;
            this.lxcRadioButton1.Size = new System.Drawing.Size(62, 22);
            this.lxcRadioButton1.TabIndex = 14;
            this.lxcRadioButton1.Text = "操作员";
            this.lxcRadioButton1.UseVisualStyleBackColor = true;
            // 
            // lxcRadioButton4
            // 
            this.lxcRadioButton4.AutoSize = true;
            this.lxcRadioButton4.BorderColor = System.Drawing.Color.White;
            this.lxcRadioButton4.CheckBoxBackColor = System.Drawing.Color.Black;
            this.lxcRadioButton4.CheckColor = System.Drawing.Color.Lime;
            this.lxcRadioButton4.Checked = true;
            this.lxcRadioButton4.InnerCircleRadius = 4;
            this.lxcRadioButton4.Location = new System.Drawing.Point(131, 16);
            this.lxcRadioButton4.MinimumSize = new System.Drawing.Size(22, 22);
            this.lxcRadioButton4.Name = "lxcRadioButton4";
            this.lxcRadioButton4.OuterCircleRadius = 8;
            this.lxcRadioButton4.Size = new System.Drawing.Size(62, 22);
            this.lxcRadioButton4.TabIndex = 15;
            this.lxcRadioButton4.TabStop = true;
            this.lxcRadioButton4.Text = "管理员";
            this.lxcRadioButton4.UseVisualStyleBackColor = true;
            // 
            // OrgPassword
            // 
            this.OrgPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OrgPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OrgPassword.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.OrgPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.OrgPassword.Location = new System.Drawing.Point(131, 61);
            this.OrgPassword.Name = "OrgPassword";
            this.OrgPassword.PasswordChar = '※';
            this.OrgPassword.Size = new System.Drawing.Size(139, 23);
            this.OrgPassword.TabIndex = 16;
            // 
            // NewPassword
            // 
            this.NewPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.NewPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.NewPassword.Location = new System.Drawing.Point(131, 104);
            this.NewPassword.Name = "NewPassword";
            this.NewPassword.PasswordChar = '※';
            this.NewPassword.Size = new System.Drawing.Size(139, 23);
            this.NewPassword.TabIndex = 16;
            // 
            // AgainPassword
            // 
            this.AgainPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.AgainPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AgainPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AgainPassword.Location = new System.Drawing.Point(131, 150);
            this.AgainPassword.Name = "AgainPassword";
            this.AgainPassword.PasswordChar = '※';
            this.AgainPassword.Size = new System.Drawing.Size(139, 23);
            this.AgainPassword.TabIndex = 16;
            // 
            // OrgPasswordTip
            // 
            this.OrgPasswordTip.AutoSize = true;
            this.OrgPasswordTip.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.OrgPasswordTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.OrgPasswordTip.Location = new System.Drawing.Point(307, 61);
            this.OrgPasswordTip.Name = "OrgPasswordTip";
            this.OrgPasswordTip.Size = new System.Drawing.Size(0, 21);
            this.OrgPasswordTip.TabIndex = 0;
            // 
            // NewPasswordTip
            // 
            this.NewPasswordTip.AutoSize = true;
            this.NewPasswordTip.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.NewPasswordTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.NewPasswordTip.Location = new System.Drawing.Point(307, 106);
            this.NewPasswordTip.Name = "NewPasswordTip";
            this.NewPasswordTip.Size = new System.Drawing.Size(0, 21);
            this.NewPasswordTip.TabIndex = 0;
            // 
            // AgainPasswordTip
            // 
            this.AgainPasswordTip.AutoSize = true;
            this.AgainPasswordTip.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.AgainPasswordTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.AgainPasswordTip.Location = new System.Drawing.Point(307, 152);
            this.AgainPasswordTip.Name = "AgainPasswordTip";
            this.AgainPasswordTip.Size = new System.Drawing.Size(0, 21);
            this.AgainPasswordTip.TabIndex = 0;
            // 
            // lxcButton2
            // 
            this.lxcButton2.AutoSize = false;
            this.lxcButton2.DrawFocusRect = true;
            this.lxcButton2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lxcButton2.ForePathGetter = null;
            this.lxcButton2.Image = null;
            this.lxcButton2.Location = new System.Drawing.Point(41, 204);
            this.lxcButton2.LxcButtonAppearance.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
            this.lxcButton2.LxcButtonAppearance.BackColorNormal = System.Drawing.Color.Silver;
            this.lxcButton2.LxcButtonAppearance.BackColorPressed = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(213)))), ((int)(((byte)(218)))));
            this.lxcButton2.LxcButtonAppearance.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(189)))));
            this.lxcButton2.LxcButtonAppearance.BorderColorNormal = System.Drawing.Color.Silver;
            this.lxcButton2.LxcButtonAppearance.BorderColorPressed = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(189)))));
            this.lxcButton2.LxcButtonAppearance.ForeColorHover = System.Drawing.Color.Black;
            this.lxcButton2.LxcButtonAppearance.ForeColorNormal = System.Drawing.Color.Black;
            this.lxcButton2.LxcButtonAppearance.ForeColorPressed = System.Drawing.Color.Black;
            this.lxcButton2.LxcButtonAppearance.ImageHover = null;
            this.lxcButton2.LxcButtonAppearance.ImageNormal = null;
            this.lxcButton2.LxcButtonAppearance.ImagePressed = null;
            this.lxcButton2.LxcForeImage = null;
            this.lxcButton2.LxcForeImageSize = new System.Drawing.Size(0, 0);
            this.lxcButton2.LxcForePathAlign = LXCSystem.Control.Base.Button.LxcButtonThemeBase.ButtonImageAlignment.Left;
            this.lxcButton2.LxcForePathSize = new System.Drawing.Size(0, 0);
            this.lxcButton2.LxcSpaceBetweenPathAndText = 0;
            this.lxcButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lxcButton2.Name = "lxcButton2";
            this.lxcButton2.Size = new System.Drawing.Size(139, 38);
            this.lxcButton2.TabIndex = 28;
            this.lxcButton2.Text = "确认";
            this.lxcButton2.UseVisualStyleBackColor = true;
            this.lxcButton2.Click += new System.EventHandler(this.lxcButton2_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
            this.label4.Location = new System.Drawing.Point(272, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 21);
            this.label4.TabIndex = 0;
            this.label4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label4_MouseDown);
            this.label4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label4_MouseUp);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.Location = new System.Drawing.Point(272, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 21);
            this.label6.TabIndex = 0;
            this.label6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label6_MouseDown);
            this.label6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label6_MouseUp);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
            this.label7.Location = new System.Drawing.Point(272, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 21);
            this.label7.TabIndex = 0;
            this.label7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label7_MouseDown);
            this.label7.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label7_MouseUp);
            // 
            // lxcButton1
            // 
            this.lxcButton1.AutoSize = false;
            this.lxcButton1.DrawFocusRect = true;
            this.lxcButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lxcButton1.ForePathGetter = null;
            this.lxcButton1.Image = null;
            this.lxcButton1.Location = new System.Drawing.Point(186, 204);
            this.lxcButton1.LxcButtonAppearance.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
            this.lxcButton1.LxcButtonAppearance.BackColorNormal = System.Drawing.Color.Silver;
            this.lxcButton1.LxcButtonAppearance.BackColorPressed = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(213)))), ((int)(((byte)(218)))));
            this.lxcButton1.LxcButtonAppearance.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(189)))));
            this.lxcButton1.LxcButtonAppearance.BorderColorNormal = System.Drawing.Color.Silver;
            this.lxcButton1.LxcButtonAppearance.BorderColorPressed = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(189)))));
            this.lxcButton1.LxcButtonAppearance.ForeColorHover = System.Drawing.Color.Black;
            this.lxcButton1.LxcButtonAppearance.ForeColorNormal = System.Drawing.Color.Black;
            this.lxcButton1.LxcButtonAppearance.ForeColorPressed = System.Drawing.Color.Black;
            this.lxcButton1.LxcButtonAppearance.ImageHover = null;
            this.lxcButton1.LxcButtonAppearance.ImageNormal = null;
            this.lxcButton1.LxcButtonAppearance.ImagePressed = null;
            this.lxcButton1.LxcForeImage = null;
            this.lxcButton1.LxcForeImageSize = new System.Drawing.Size(0, 0);
            this.lxcButton1.LxcForePathAlign = LXCSystem.Control.Base.Button.LxcButtonThemeBase.ButtonImageAlignment.Left;
            this.lxcButton1.LxcForePathSize = new System.Drawing.Size(0, 0);
            this.lxcButton1.LxcSpaceBetweenPathAndText = 0;
            this.lxcButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lxcButton1.Name = "lxcButton1";
            this.lxcButton1.Size = new System.Drawing.Size(139, 38);
            this.lxcButton1.TabIndex = 28;
            this.lxcButton1.Text = "重置为默认密码";
            this.lxcButton1.UseVisualStyleBackColor = true;
            this.lxcButton1.Visible = false;
            this.lxcButton1.Click += new System.EventHandler(this.lxcButton1_Click);
            // 
            // Win_UserManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(883, 566);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lxcButton1);
            this.Controls.Add(this.lxcButton2);
            this.Controls.Add(this.AgainPassword);
            this.Controls.Add(this.NewPassword);
            this.Controls.Add(this.OrgPassword);
            this.Controls.Add(this.lxcRadioButton1);
            this.Controls.Add(this.lxcRadioButton4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AgainPasswordTip);
            this.Controls.Add(this.NewPasswordTip);
            this.Controls.Add(this.OrgPasswordTip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Win_UserManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "管理员密码修改";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label5;
        private LXCSystem.Control.CommonCtrl.LxcRadioButton lxcRadioButton1;
        private LXCSystem.Control.CommonCtrl.LxcRadioButton lxcRadioButton4;
        private System.Windows.Forms.TextBox OrgPassword;
        private System.Windows.Forms.TextBox NewPassword;
        private System.Windows.Forms.TextBox AgainPassword;
        internal System.Windows.Forms.Label OrgPasswordTip;
        internal System.Windows.Forms.Label NewPasswordTip;
        internal System.Windows.Forms.Label AgainPasswordTip;
        private LXCSystem.Control.CommonCtrl.LxcButton lxcButton2;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label7;
        private LXCSystem.Control.CommonCtrl.LxcButton lxcButton1;
    }
}