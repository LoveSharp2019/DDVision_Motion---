namespace VControls
{
    partial class VROISet
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
            this.lxcGroupBox1 = new LXCSystem.Control.CommonCtrl.LxcGroupBox();
            this.vroiSel1 = new VControls.VROISel();
            this.lxcButton1 = new LXCSystem.Control.CommonCtrl.LxcButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_Ellipse = new System.Windows.Forms.Panel();
            this.Ellipse_CenterX = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.Ellipse_phi = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.Ellipse_R2 = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.Ellipse_R1 = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.Ellipse_CenterY = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.panel_Circle = new System.Windows.Forms.Panel();
            this.Circle_CenterX = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.Circle_R = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.Circle_CenterY = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.panel_Rectangle1 = new System.Windows.Forms.Panel();
            this.Rectangle1_LeftTopX = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.Rectangle1_RightBottomY = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.Rectangle1_RightBottomX = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.Rectangle1_LeftTopY = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.panel_Rectangle2 = new System.Windows.Forms.Panel();
            this.Rectangle2_CenterX = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.Rectangle2_phi = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.Rectangle2_LengthY = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.Rectangle2_LengthX = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.Rectangle2_CenterY = new LXCSystem.Control.CommonCtrl.LxcNumEdit();
            this.lxcGroupBox1.SuspendLayout();
            this.panel_Ellipse.SuspendLayout();
            this.panel_Circle.SuspendLayout();
            this.panel_Rectangle1.SuspendLayout();
            this.panel_Rectangle2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lxcGroupBox1
            // 
            this.lxcGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.lxcGroupBox1.BorderColor = System.Drawing.Color.Gray;
            this.lxcGroupBox1.BorderStyle = LXCSystem.Control.Base.Common.EnumBorderStyle.XStyle;
            this.lxcGroupBox1.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lxcGroupBox1.CaptionFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lxcGroupBox1.Controls.Add(this.vroiSel1);
            this.lxcGroupBox1.Controls.Add(this.lxcButton1);
            this.lxcGroupBox1.Controls.Add(this.label1);
            this.lxcGroupBox1.Controls.Add(this.panel_Ellipse);
            this.lxcGroupBox1.Controls.Add(this.panel_Circle);
            this.lxcGroupBox1.Controls.Add(this.panel_Rectangle1);
            this.lxcGroupBox1.Controls.Add(this.panel_Rectangle2);
            this.lxcGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lxcGroupBox1.ForeColor = System.Drawing.Color.White;
            this.lxcGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.lxcGroupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lxcGroupBox1.Name = "lxcGroupBox1";
            this.lxcGroupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lxcGroupBox1.Size = new System.Drawing.Size(326, 300);
            this.lxcGroupBox1.TabIndex = 1;
            this.lxcGroupBox1.TabStop = false;
            this.lxcGroupBox1.Text = "ROI区域";
            this.lxcGroupBox1.TextMargin = 6;
            // 
            // vroiSel1
            // 
            this.vroiSel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.vroiSel1.Location = new System.Drawing.Point(73, 20);
            this.vroiSel1.Margin = new System.Windows.Forms.Padding(4);
            this.vroiSel1.Name = "vroiSel1";
            this.vroiSel1.SelectedIndex = 0;
            this.vroiSel1.Size = new System.Drawing.Size(147, 28);
            this.vroiSel1.TabIndex = 6;
            // 
            // lxcButton1
            // 
            this.lxcButton1.AutoSize = false;
            this.lxcButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lxcButton1.ForePathGetter = null;
            this.lxcButton1.Image = null;
            this.lxcButton1.Location = new System.Drawing.Point(227, 18);
            this.lxcButton1.LxcButtonAppearance.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lxcButton1.LxcButtonAppearance.BackColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.lxcButton1.LxcButtonAppearance.BackColorPressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lxcButton1.LxcButtonAppearance.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.lxcButton1.LxcButtonAppearance.BorderColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.lxcButton1.LxcButtonAppearance.BorderColorPressed = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.lxcButton1.LxcButtonAppearance.ForeColorHover = System.Drawing.Color.White;
            this.lxcButton1.LxcButtonAppearance.ForeColorNormal = System.Drawing.Color.White;
            this.lxcButton1.LxcButtonAppearance.ForeColorPressed = System.Drawing.Color.White;
            this.lxcButton1.LxcButtonAppearance.ImageHover = null;
            this.lxcButton1.LxcButtonAppearance.ImageNormal = null;
            this.lxcButton1.LxcButtonAppearance.ImagePressed = null;
            this.lxcButton1.LxcForeImage = null;
            this.lxcButton1.LxcForeImageSize = new System.Drawing.Size(0, 0);
            this.lxcButton1.LxcForePathAlign = LXCSystem.Control.Base.Button.LxcButtonThemeBase.ButtonImageAlignment.Left;
            this.lxcButton1.LxcForePathSize = new System.Drawing.Size(0, 0);
            this.lxcButton1.LxcSpaceBetweenPathAndText = 0;
            this.lxcButton1.Name = "lxcButton1";
            this.lxcButton1.Size = new System.Drawing.Size(84, 32);
            this.lxcButton1.TabIndex = 5;
            this.lxcButton1.Text = "适应图像";
            this.lxcButton1.UseVisualStyleBackColor = true;
            this.lxcButton1.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "区域形状";
            // 
            // panel_Ellipse
            // 
            this.panel_Ellipse.BackColor = System.Drawing.Color.Transparent;
            this.panel_Ellipse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Ellipse.Controls.Add(this.Ellipse_CenterX);
            this.panel_Ellipse.Controls.Add(this.Ellipse_phi);
            this.panel_Ellipse.Controls.Add(this.Ellipse_R2);
            this.panel_Ellipse.Controls.Add(this.Ellipse_R1);
            this.panel_Ellipse.Controls.Add(this.Ellipse_CenterY);
            this.panel_Ellipse.Location = new System.Drawing.Point(13, 54);
            this.panel_Ellipse.Name = "panel_Ellipse";
            this.panel_Ellipse.Size = new System.Drawing.Size(298, 230);
            this.panel_Ellipse.TabIndex = 3;
            this.panel_Ellipse.Visible = false;
            // 
            // Ellipse_CenterX
            // 
            this.Ellipse_CenterX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Ellipse_CenterX.BorderColor = System.Drawing.Color.Gray;
            this.Ellipse_CenterX.DecimalPlaces = 4;
            this.Ellipse_CenterX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Ellipse_CenterX.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Ellipse_CenterX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Ellipse_CenterX.Location = new System.Drawing.Point(5, 10);
            this.Ellipse_CenterX.LxcName = "中心X:";
            this.Ellipse_CenterX.MaxNum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Ellipse_CenterX.MinNum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Ellipse_CenterX.Name = "Ellipse_CenterX";
            this.Ellipse_CenterX.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Ellipse_CenterX.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Ellipse_CenterX.NumSize = new System.Drawing.Size(200, 30);
            this.Ellipse_CenterX.Size = new System.Drawing.Size(288, 36);
            this.Ellipse_CenterX.TabIndex = 2;
            this.Ellipse_CenterX.TitleBackColor = System.Drawing.Color.Transparent;
            this.Ellipse_CenterX.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Ellipse_CenterX.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Ellipse_CenterX.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Ellipse_CenterX.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Ellipse_CenterX.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Ellipse_CenterX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Ellipse_CenterX.OnValueChanged += new System.EventHandler(this.Ellipse_CenterX_ValueChanged);
            // 
            // Ellipse_phi
            // 
            this.Ellipse_phi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Ellipse_phi.BorderColor = System.Drawing.Color.Gray;
            this.Ellipse_phi.DecimalPlaces = 4;
            this.Ellipse_phi.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Ellipse_phi.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Ellipse_phi.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Ellipse_phi.Location = new System.Drawing.Point(5, 178);
            this.Ellipse_phi.LxcName = "角度：";
            this.Ellipse_phi.MaxNum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.Ellipse_phi.MinNum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.Ellipse_phi.Name = "Ellipse_phi";
            this.Ellipse_phi.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Ellipse_phi.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Ellipse_phi.NumSize = new System.Drawing.Size(200, 30);
            this.Ellipse_phi.Size = new System.Drawing.Size(288, 36);
            this.Ellipse_phi.TabIndex = 2;
            this.Ellipse_phi.TitleBackColor = System.Drawing.Color.Transparent;
            this.Ellipse_phi.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Ellipse_phi.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Ellipse_phi.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Ellipse_phi.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Ellipse_phi.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Ellipse_phi.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Ellipse_phi.OnValueChanged += new System.EventHandler(this.Ellipse_CenterX_ValueChanged);
            // 
            // Ellipse_R2
            // 
            this.Ellipse_R2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Ellipse_R2.BorderColor = System.Drawing.Color.Gray;
            this.Ellipse_R2.DecimalPlaces = 4;
            this.Ellipse_R2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Ellipse_R2.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Ellipse_R2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Ellipse_R2.Location = new System.Drawing.Point(5, 136);
            this.Ellipse_R2.LxcName = "短半径：";
            this.Ellipse_R2.MaxNum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Ellipse_R2.MinNum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Ellipse_R2.Name = "Ellipse_R2";
            this.Ellipse_R2.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Ellipse_R2.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Ellipse_R2.NumSize = new System.Drawing.Size(200, 30);
            this.Ellipse_R2.Size = new System.Drawing.Size(288, 36);
            this.Ellipse_R2.TabIndex = 2;
            this.Ellipse_R2.TitleBackColor = System.Drawing.Color.Transparent;
            this.Ellipse_R2.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Ellipse_R2.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Ellipse_R2.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Ellipse_R2.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Ellipse_R2.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Ellipse_R2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Ellipse_R2.OnValueChanged += new System.EventHandler(this.Ellipse_CenterX_ValueChanged);
            // 
            // Ellipse_R1
            // 
            this.Ellipse_R1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Ellipse_R1.BorderColor = System.Drawing.Color.Gray;
            this.Ellipse_R1.DecimalPlaces = 4;
            this.Ellipse_R1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Ellipse_R1.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Ellipse_R1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Ellipse_R1.Location = new System.Drawing.Point(5, 94);
            this.Ellipse_R1.LxcName = "长半径：";
            this.Ellipse_R1.MaxNum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Ellipse_R1.MinNum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Ellipse_R1.Name = "Ellipse_R1";
            this.Ellipse_R1.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Ellipse_R1.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Ellipse_R1.NumSize = new System.Drawing.Size(200, 30);
            this.Ellipse_R1.Size = new System.Drawing.Size(288, 36);
            this.Ellipse_R1.TabIndex = 2;
            this.Ellipse_R1.TitleBackColor = System.Drawing.Color.Transparent;
            this.Ellipse_R1.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Ellipse_R1.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Ellipse_R1.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Ellipse_R1.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Ellipse_R1.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Ellipse_R1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Ellipse_R1.OnValueChanged += new System.EventHandler(this.Ellipse_CenterX_ValueChanged);
            // 
            // Ellipse_CenterY
            // 
            this.Ellipse_CenterY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Ellipse_CenterY.BorderColor = System.Drawing.Color.Gray;
            this.Ellipse_CenterY.DecimalPlaces = 4;
            this.Ellipse_CenterY.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Ellipse_CenterY.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Ellipse_CenterY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Ellipse_CenterY.Location = new System.Drawing.Point(5, 52);
            this.Ellipse_CenterY.LxcName = "中心Y：";
            this.Ellipse_CenterY.MaxNum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Ellipse_CenterY.MinNum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Ellipse_CenterY.Name = "Ellipse_CenterY";
            this.Ellipse_CenterY.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Ellipse_CenterY.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Ellipse_CenterY.NumSize = new System.Drawing.Size(200, 30);
            this.Ellipse_CenterY.Size = new System.Drawing.Size(288, 36);
            this.Ellipse_CenterY.TabIndex = 2;
            this.Ellipse_CenterY.TitleBackColor = System.Drawing.Color.Transparent;
            this.Ellipse_CenterY.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Ellipse_CenterY.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Ellipse_CenterY.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Ellipse_CenterY.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Ellipse_CenterY.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Ellipse_CenterY.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Ellipse_CenterY.OnValueChanged += new System.EventHandler(this.Ellipse_CenterX_ValueChanged);
            // 
            // panel_Circle
            // 
            this.panel_Circle.BackColor = System.Drawing.Color.Transparent;
            this.panel_Circle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Circle.Controls.Add(this.Circle_CenterX);
            this.panel_Circle.Controls.Add(this.Circle_R);
            this.panel_Circle.Controls.Add(this.Circle_CenterY);
            this.panel_Circle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_Circle.Location = new System.Drawing.Point(13, 54);
            this.panel_Circle.Name = "panel_Circle";
            this.panel_Circle.Size = new System.Drawing.Size(298, 141);
            this.panel_Circle.TabIndex = 3;
            this.panel_Circle.Visible = false;
            // 
            // Circle_CenterX
            // 
            this.Circle_CenterX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Circle_CenterX.BorderColor = System.Drawing.Color.Gray;
            this.Circle_CenterX.DecimalPlaces = 4;
            this.Circle_CenterX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Circle_CenterX.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Circle_CenterX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Circle_CenterX.Location = new System.Drawing.Point(5, 10);
            this.Circle_CenterX.LxcName = "中心X:";
            this.Circle_CenterX.MaxNum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Circle_CenterX.MinNum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Circle_CenterX.Name = "Circle_CenterX";
            this.Circle_CenterX.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Circle_CenterX.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Circle_CenterX.NumSize = new System.Drawing.Size(200, 30);
            this.Circle_CenterX.Size = new System.Drawing.Size(288, 36);
            this.Circle_CenterX.TabIndex = 2;
            this.Circle_CenterX.TitleBackColor = System.Drawing.Color.Transparent;
            this.Circle_CenterX.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Circle_CenterX.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Circle_CenterX.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Circle_CenterX.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Circle_CenterX.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Circle_CenterX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Circle_CenterX.OnValueChanged += new System.EventHandler(this.Circle_ValueChanged);
            // 
            // Circle_R
            // 
            this.Circle_R.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Circle_R.BorderColor = System.Drawing.Color.Gray;
            this.Circle_R.DecimalPlaces = 4;
            this.Circle_R.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Circle_R.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Circle_R.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Circle_R.Location = new System.Drawing.Point(5, 94);
            this.Circle_R.LxcName = "半径：";
            this.Circle_R.MaxNum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Circle_R.MinNum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Circle_R.Name = "Circle_R";
            this.Circle_R.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Circle_R.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Circle_R.NumSize = new System.Drawing.Size(200, 30);
            this.Circle_R.Size = new System.Drawing.Size(288, 36);
            this.Circle_R.TabIndex = 2;
            this.Circle_R.TitleBackColor = System.Drawing.Color.Transparent;
            this.Circle_R.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Circle_R.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Circle_R.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Circle_R.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Circle_R.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Circle_R.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Circle_R.OnValueChanged += new System.EventHandler(this.Circle_ValueChanged);
            // 
            // Circle_CenterY
            // 
            this.Circle_CenterY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Circle_CenterY.BorderColor = System.Drawing.Color.Gray;
            this.Circle_CenterY.DecimalPlaces = 4;
            this.Circle_CenterY.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Circle_CenterY.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Circle_CenterY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Circle_CenterY.Location = new System.Drawing.Point(5, 52);
            this.Circle_CenterY.LxcName = "中心Y：";
            this.Circle_CenterY.MaxNum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Circle_CenterY.MinNum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Circle_CenterY.Name = "Circle_CenterY";
            this.Circle_CenterY.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Circle_CenterY.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Circle_CenterY.NumSize = new System.Drawing.Size(200, 30);
            this.Circle_CenterY.Size = new System.Drawing.Size(288, 36);
            this.Circle_CenterY.TabIndex = 2;
            this.Circle_CenterY.TitleBackColor = System.Drawing.Color.Transparent;
            this.Circle_CenterY.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Circle_CenterY.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Circle_CenterY.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Circle_CenterY.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Circle_CenterY.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Circle_CenterY.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Circle_CenterY.OnValueChanged += new System.EventHandler(this.Circle_ValueChanged);
            // 
            // panel_Rectangle1
            // 
            this.panel_Rectangle1.BackColor = System.Drawing.Color.Transparent;
            this.panel_Rectangle1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Rectangle1.Controls.Add(this.Rectangle1_LeftTopX);
            this.panel_Rectangle1.Controls.Add(this.Rectangle1_RightBottomY);
            this.panel_Rectangle1.Controls.Add(this.Rectangle1_RightBottomX);
            this.panel_Rectangle1.Controls.Add(this.Rectangle1_LeftTopY);
            this.panel_Rectangle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_Rectangle1.Location = new System.Drawing.Point(13, 54);
            this.panel_Rectangle1.Name = "panel_Rectangle1";
            this.panel_Rectangle1.Size = new System.Drawing.Size(298, 186);
            this.panel_Rectangle1.TabIndex = 3;
            this.panel_Rectangle1.Visible = false;
            // 
            // Rectangle1_LeftTopX
            // 
            this.Rectangle1_LeftTopX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle1_LeftTopX.BorderColor = System.Drawing.Color.Gray;
            this.Rectangle1_LeftTopX.DecimalPlaces = 4;
            this.Rectangle1_LeftTopX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle1_LeftTopX.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Rectangle1_LeftTopX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Rectangle1_LeftTopX.Location = new System.Drawing.Point(5, 10);
            this.Rectangle1_LeftTopX.LxcName = "左上角顶点X:";
            this.Rectangle1_LeftTopX.MaxNum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Rectangle1_LeftTopX.MinNum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Rectangle1_LeftTopX.Name = "Rectangle1_LeftTopX";
            this.Rectangle1_LeftTopX.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Rectangle1_LeftTopX.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Rectangle1_LeftTopX.NumSize = new System.Drawing.Size(200, 30);
            this.Rectangle1_LeftTopX.Size = new System.Drawing.Size(288, 36);
            this.Rectangle1_LeftTopX.TabIndex = 2;
            this.Rectangle1_LeftTopX.TitleBackColor = System.Drawing.Color.Transparent;
            this.Rectangle1_LeftTopX.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle1_LeftTopX.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Rectangle1_LeftTopX.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle1_LeftTopX.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Rectangle1_LeftTopX.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Rectangle1_LeftTopX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Rectangle1_LeftTopX.OnValueChanged += new System.EventHandler(this.Rectangle1_LeftTopX_ValueChanged);
            // 
            // Rectangle1_RightBottomY
            // 
            this.Rectangle1_RightBottomY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle1_RightBottomY.BorderColor = System.Drawing.Color.Gray;
            this.Rectangle1_RightBottomY.DecimalPlaces = 4;
            this.Rectangle1_RightBottomY.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle1_RightBottomY.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Rectangle1_RightBottomY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Rectangle1_RightBottomY.Location = new System.Drawing.Point(5, 136);
            this.Rectangle1_RightBottomY.LxcName = "右下角顶点Y:";
            this.Rectangle1_RightBottomY.MaxNum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Rectangle1_RightBottomY.MinNum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Rectangle1_RightBottomY.Name = "Rectangle1_RightBottomY";
            this.Rectangle1_RightBottomY.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Rectangle1_RightBottomY.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Rectangle1_RightBottomY.NumSize = new System.Drawing.Size(200, 30);
            this.Rectangle1_RightBottomY.Size = new System.Drawing.Size(288, 36);
            this.Rectangle1_RightBottomY.TabIndex = 2;
            this.Rectangle1_RightBottomY.TitleBackColor = System.Drawing.Color.Transparent;
            this.Rectangle1_RightBottomY.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle1_RightBottomY.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Rectangle1_RightBottomY.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle1_RightBottomY.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Rectangle1_RightBottomY.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Rectangle1_RightBottomY.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Rectangle1_RightBottomY.OnValueChanged += new System.EventHandler(this.Rectangle1_LeftTopX_ValueChanged);
            // 
            // Rectangle1_RightBottomX
            // 
            this.Rectangle1_RightBottomX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle1_RightBottomX.BorderColor = System.Drawing.Color.Gray;
            this.Rectangle1_RightBottomX.DecimalPlaces = 4;
            this.Rectangle1_RightBottomX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle1_RightBottomX.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Rectangle1_RightBottomX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Rectangle1_RightBottomX.Location = new System.Drawing.Point(5, 94);
            this.Rectangle1_RightBottomX.LxcName = "右下角顶点X:";
            this.Rectangle1_RightBottomX.MaxNum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Rectangle1_RightBottomX.MinNum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Rectangle1_RightBottomX.Name = "Rectangle1_RightBottomX";
            this.Rectangle1_RightBottomX.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Rectangle1_RightBottomX.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Rectangle1_RightBottomX.NumSize = new System.Drawing.Size(200, 30);
            this.Rectangle1_RightBottomX.Size = new System.Drawing.Size(288, 36);
            this.Rectangle1_RightBottomX.TabIndex = 2;
            this.Rectangle1_RightBottomX.TitleBackColor = System.Drawing.Color.Transparent;
            this.Rectangle1_RightBottomX.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle1_RightBottomX.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Rectangle1_RightBottomX.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle1_RightBottomX.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Rectangle1_RightBottomX.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Rectangle1_RightBottomX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Rectangle1_RightBottomX.OnValueChanged += new System.EventHandler(this.Rectangle1_LeftTopX_ValueChanged);
            // 
            // Rectangle1_LeftTopY
            // 
            this.Rectangle1_LeftTopY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle1_LeftTopY.BorderColor = System.Drawing.Color.Gray;
            this.Rectangle1_LeftTopY.DecimalPlaces = 4;
            this.Rectangle1_LeftTopY.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle1_LeftTopY.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Rectangle1_LeftTopY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Rectangle1_LeftTopY.Location = new System.Drawing.Point(5, 52);
            this.Rectangle1_LeftTopY.LxcName = "左上角顶点Y:";
            this.Rectangle1_LeftTopY.MaxNum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Rectangle1_LeftTopY.MinNum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Rectangle1_LeftTopY.Name = "Rectangle1_LeftTopY";
            this.Rectangle1_LeftTopY.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Rectangle1_LeftTopY.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Rectangle1_LeftTopY.NumSize = new System.Drawing.Size(200, 30);
            this.Rectangle1_LeftTopY.Size = new System.Drawing.Size(288, 36);
            this.Rectangle1_LeftTopY.TabIndex = 2;
            this.Rectangle1_LeftTopY.TitleBackColor = System.Drawing.Color.Transparent;
            this.Rectangle1_LeftTopY.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle1_LeftTopY.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Rectangle1_LeftTopY.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle1_LeftTopY.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Rectangle1_LeftTopY.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Rectangle1_LeftTopY.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Rectangle1_LeftTopY.OnValueChanged += new System.EventHandler(this.Rectangle1_LeftTopX_ValueChanged);
            // 
            // panel_Rectangle2
            // 
            this.panel_Rectangle2.BackColor = System.Drawing.Color.Transparent;
            this.panel_Rectangle2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Rectangle2.Controls.Add(this.Rectangle2_CenterX);
            this.panel_Rectangle2.Controls.Add(this.Rectangle2_phi);
            this.panel_Rectangle2.Controls.Add(this.Rectangle2_LengthY);
            this.panel_Rectangle2.Controls.Add(this.Rectangle2_LengthX);
            this.panel_Rectangle2.Controls.Add(this.Rectangle2_CenterY);
            this.panel_Rectangle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_Rectangle2.Location = new System.Drawing.Point(13, 54);
            this.panel_Rectangle2.Name = "panel_Rectangle2";
            this.panel_Rectangle2.Size = new System.Drawing.Size(298, 230);
            this.panel_Rectangle2.TabIndex = 3;
            this.panel_Rectangle2.Visible = false;
            // 
            // Rectangle2_CenterX
            // 
            this.Rectangle2_CenterX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle2_CenterX.BorderColor = System.Drawing.Color.Gray;
            this.Rectangle2_CenterX.DecimalPlaces = 4;
            this.Rectangle2_CenterX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle2_CenterX.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Rectangle2_CenterX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Rectangle2_CenterX.Location = new System.Drawing.Point(5, 10);
            this.Rectangle2_CenterX.LxcName = "中心点X:";
            this.Rectangle2_CenterX.MaxNum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Rectangle2_CenterX.MinNum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Rectangle2_CenterX.Name = "Rectangle2_CenterX";
            this.Rectangle2_CenterX.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Rectangle2_CenterX.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Rectangle2_CenterX.NumSize = new System.Drawing.Size(200, 30);
            this.Rectangle2_CenterX.Size = new System.Drawing.Size(288, 36);
            this.Rectangle2_CenterX.TabIndex = 2;
            this.Rectangle2_CenterX.TitleBackColor = System.Drawing.Color.Transparent;
            this.Rectangle2_CenterX.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle2_CenterX.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Rectangle2_CenterX.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle2_CenterX.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Rectangle2_CenterX.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Rectangle2_CenterX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Rectangle2_CenterX.OnValueChanged += new System.EventHandler(this.Rectangle2_CenterX_ValueChanged);
            // 
            // Rectangle2_phi
            // 
            this.Rectangle2_phi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle2_phi.BorderColor = System.Drawing.Color.Gray;
            this.Rectangle2_phi.DecimalPlaces = 4;
            this.Rectangle2_phi.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle2_phi.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Rectangle2_phi.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Rectangle2_phi.Location = new System.Drawing.Point(5, 178);
            this.Rectangle2_phi.LxcName = "角度：";
            this.Rectangle2_phi.MaxNum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.Rectangle2_phi.MinNum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.Rectangle2_phi.Name = "Rectangle2_phi";
            this.Rectangle2_phi.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Rectangle2_phi.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Rectangle2_phi.NumSize = new System.Drawing.Size(200, 30);
            this.Rectangle2_phi.Size = new System.Drawing.Size(288, 36);
            this.Rectangle2_phi.TabIndex = 2;
            this.Rectangle2_phi.TitleBackColor = System.Drawing.Color.Transparent;
            this.Rectangle2_phi.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle2_phi.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Rectangle2_phi.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle2_phi.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Rectangle2_phi.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Rectangle2_phi.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Rectangle2_phi.OnValueChanged += new System.EventHandler(this.Rectangle2_CenterX_ValueChanged);
            // 
            // Rectangle2_LengthY
            // 
            this.Rectangle2_LengthY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle2_LengthY.BorderColor = System.Drawing.Color.Gray;
            this.Rectangle2_LengthY.DecimalPlaces = 4;
            this.Rectangle2_LengthY.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle2_LengthY.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Rectangle2_LengthY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Rectangle2_LengthY.Location = new System.Drawing.Point(5, 136);
            this.Rectangle2_LengthY.LxcName = "边长Y:";
            this.Rectangle2_LengthY.MaxNum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Rectangle2_LengthY.MinNum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Rectangle2_LengthY.Name = "Rectangle2_LengthY";
            this.Rectangle2_LengthY.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Rectangle2_LengthY.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Rectangle2_LengthY.NumSize = new System.Drawing.Size(200, 30);
            this.Rectangle2_LengthY.Size = new System.Drawing.Size(288, 36);
            this.Rectangle2_LengthY.TabIndex = 2;
            this.Rectangle2_LengthY.TitleBackColor = System.Drawing.Color.Transparent;
            this.Rectangle2_LengthY.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle2_LengthY.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Rectangle2_LengthY.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle2_LengthY.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Rectangle2_LengthY.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Rectangle2_LengthY.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Rectangle2_LengthY.OnValueChanged += new System.EventHandler(this.Rectangle2_CenterX_ValueChanged);
            // 
            // Rectangle2_LengthX
            // 
            this.Rectangle2_LengthX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle2_LengthX.BorderColor = System.Drawing.Color.Gray;
            this.Rectangle2_LengthX.DecimalPlaces = 4;
            this.Rectangle2_LengthX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle2_LengthX.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Rectangle2_LengthX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Rectangle2_LengthX.Location = new System.Drawing.Point(5, 94);
            this.Rectangle2_LengthX.LxcName = "边长X:";
            this.Rectangle2_LengthX.MaxNum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Rectangle2_LengthX.MinNum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Rectangle2_LengthX.Name = "Rectangle2_LengthX";
            this.Rectangle2_LengthX.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Rectangle2_LengthX.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Rectangle2_LengthX.NumSize = new System.Drawing.Size(200, 30);
            this.Rectangle2_LengthX.Size = new System.Drawing.Size(288, 36);
            this.Rectangle2_LengthX.TabIndex = 2;
            this.Rectangle2_LengthX.TitleBackColor = System.Drawing.Color.Transparent;
            this.Rectangle2_LengthX.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle2_LengthX.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Rectangle2_LengthX.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle2_LengthX.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Rectangle2_LengthX.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Rectangle2_LengthX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Rectangle2_LengthX.OnValueChanged += new System.EventHandler(this.Rectangle2_CenterX_ValueChanged);
            // 
            // Rectangle2_CenterY
            // 
            this.Rectangle2_CenterY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle2_CenterY.BorderColor = System.Drawing.Color.Gray;
            this.Rectangle2_CenterY.DecimalPlaces = 4;
            this.Rectangle2_CenterY.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle2_CenterY.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Rectangle2_CenterY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Rectangle2_CenterY.Location = new System.Drawing.Point(5, 52);
            this.Rectangle2_CenterY.LxcName = "中心点Y:";
            this.Rectangle2_CenterY.MaxNum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.Rectangle2_CenterY.MinNum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.Rectangle2_CenterY.Name = "Rectangle2_CenterY";
            this.Rectangle2_CenterY.NumFont = new System.Drawing.Font("微软雅黑", 15F);
            this.Rectangle2_CenterY.NumForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Rectangle2_CenterY.NumSize = new System.Drawing.Size(200, 30);
            this.Rectangle2_CenterY.Size = new System.Drawing.Size(288, 36);
            this.Rectangle2_CenterY.TabIndex = 2;
            this.Rectangle2_CenterY.TitleBackColor = System.Drawing.Color.Transparent;
            this.Rectangle2_CenterY.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rectangle2_CenterY.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Rectangle2_CenterY.UpDownButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Rectangle2_CenterY.UpDownButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.Rectangle2_CenterY.UpDownButtonForeColor = System.Drawing.Color.Gray;
            this.Rectangle2_CenterY.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Rectangle2_CenterY.OnValueChanged += new System.EventHandler(this.Rectangle2_CenterX_ValueChanged);
            // 
            // VROISet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lxcGroupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "VROISet";
            this.Size = new System.Drawing.Size(326, 300);
            this.lxcGroupBox1.ResumeLayout(false);
            this.lxcGroupBox1.PerformLayout();
            this.panel_Ellipse.ResumeLayout(false);
            this.panel_Circle.ResumeLayout(false);
            this.panel_Rectangle1.ResumeLayout(false);
            this.panel_Rectangle2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LXCSystem.Control.CommonCtrl.LxcGroupBox lxcGroupBox1;
        private LXCSystem.Control.CommonCtrl.LxcButton lxcButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_Circle;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Circle_CenterX;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Circle_R;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Circle_CenterY;
        private System.Windows.Forms.Panel panel_Rectangle1;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Rectangle1_LeftTopX;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Rectangle1_RightBottomY;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Rectangle1_RightBottomX;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Rectangle1_LeftTopY;
        private System.Windows.Forms.Panel panel_Rectangle2;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Rectangle2_CenterX;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Rectangle2_phi;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Rectangle2_LengthY;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Rectangle2_LengthX;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Rectangle2_CenterY;
        private System.Windows.Forms.Panel panel_Ellipse;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Ellipse_CenterX;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Ellipse_phi;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Ellipse_R2;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Ellipse_R1;
        private LXCSystem.Control.CommonCtrl.LxcNumEdit Ellipse_CenterY;
        private VROISel vroiSel1;
    }
}
