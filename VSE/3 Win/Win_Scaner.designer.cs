namespace VSE
{
    partial class Win_Scaner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_Scaner));
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbx_output = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lnk_clear = new System.Windows.Forms.LinkLabel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_endCharEnter = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_endCharNone = new System.Windows.Forms.Button();
            this.btn_send = new VControls.VButton();
            this.cButton2 = new VControls.VButton();
            this.tbx_scanNum = new VControls.VTextBox();
            this.tbx_trigCmd = new VControls.VTextBox();
            this.btn_close = new VControls.VButton();
            this.btn_openPort = new VControls.VButton();
            this.tbx_sendMsg = new VControls.VTextBox();
            this.cbx_parityBit = new LXCSystem.Control.CommonCtrl.LxcComboBox();
            this.cbx_portName = new LXCSystem.Control.CommonCtrl.LxcComboBox();
            this.cbx_stopBit = new LXCSystem.Control.CommonCtrl.LxcComboBox();
            this.tbx_dataBit = new LXCSystem.Control.CommonCtrl.LxcComboBox();
            this.cbx_baudRate = new LXCSystem.Control.CommonCtrl.LxcComboBox();
            this.tbx_clientName = new VControls.VTextBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "停止位：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "效验位：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据位：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "波特率：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "端   口：";
            // 
            // tbx_output
            // 
            this.tbx_output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbx_output.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbx_output.Location = new System.Drawing.Point(231, 42);
            this.tbx_output.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_output.Multiline = true;
            this.tbx_output.Name = "tbx_output";
            this.tbx_output.Size = new System.Drawing.Size(295, 237);
            this.tbx_output.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(228, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 17);
            this.label9.TabIndex = 16;
            this.label9.Text = "通讯记录";
            // 
            // lnk_clear
            // 
            this.lnk_clear.AutoSize = true;
            this.lnk_clear.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lnk_clear.Location = new System.Drawing.Point(494, 23);
            this.lnk_clear.Name = "lnk_clear";
            this.lnk_clear.Size = new System.Drawing.Size(32, 17);
            this.lnk_clear.TabIndex = 17;
            this.lnk_clear.TabStop = true;
            this.lnk_clear.Text = "清空";
            this.lnk_clear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk_clear_LinkClicked);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(106)))), ((int)(((byte)(175)))));
            this.panel8.Location = new System.Drawing.Point(217, 7);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1, 390);
            this.panel8.TabIndex = 151;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(3, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 154;
            this.label6.Text = "名   称：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 269);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 166;
            this.label7.Text = "触发命令：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 296);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 17);
            this.label8.TabIndex = 168;
            this.label8.Text = "触发次数：";
            // 
            // btn_endCharEnter
            // 
            this.btn_endCharEnter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_endCharEnter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_endCharEnter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_endCharEnter.FlatAppearance.BorderSize = 0;
            this.btn_endCharEnter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.btn_endCharEnter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btn_endCharEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_endCharEnter.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_endCharEnter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_endCharEnter.Location = new System.Drawing.Point(107, 319);
            this.btn_endCharEnter.Name = "btn_endCharEnter";
            this.btn_endCharEnter.Size = new System.Drawing.Size(42, 25);
            this.btn_endCharEnter.TabIndex = 174;
            this.btn_endCharEnter.TabStop = false;
            this.btn_endCharEnter.Text = "\\r\\n";
            this.btn_endCharEnter.UseVisualStyleBackColor = false;
            this.btn_endCharEnter.Click += new System.EventHandler(this.btn_endCharEnter_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(3, 323);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 17);
            this.label10.TabIndex = 173;
            this.label10.Text = "结束符：";
            // 
            // btn_endCharNone
            // 
            this.btn_endCharNone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_endCharNone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_endCharNone.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_endCharNone.FlatAppearance.BorderSize = 0;
            this.btn_endCharNone.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.btn_endCharNone.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btn_endCharNone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_endCharNone.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_endCharNone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_endCharNone.Location = new System.Drawing.Point(65, 319);
            this.btn_endCharNone.Name = "btn_endCharNone";
            this.btn_endCharNone.Size = new System.Drawing.Size(42, 25);
            this.btn_endCharNone.TabIndex = 172;
            this.btn_endCharNone.TabStop = false;
            this.btn_endCharNone.Text = "无";
            this.btn_endCharNone.UseVisualStyleBackColor = false;
            this.btn_endCharNone.Click += new System.EventHandler(this.btn_endCharNone_Click);
            // 
            // btn_send
            // 
            this.btn_send.BackColor = System.Drawing.Color.White;
            this.btn_send.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_send.Location = new System.Drawing.Point(471, 298);
            this.btn_send.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(55, 24);
            this.btn_send.TabIndex = 171;
            this.btn_send.TextStr = "Button";
            this.btn_send.Clicked += new VControls.DClicked(this.btn_send_Clicked);
            // 
            // cButton2
            // 
            this.cButton2.BackColor = System.Drawing.Color.White;
            this.cButton2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cButton2.Location = new System.Drawing.Point(231, 331);
            this.cButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cButton2.Name = "cButton2";
            this.cButton2.Size = new System.Drawing.Size(67, 28);
            this.cButton2.TabIndex = 170;
            this.cButton2.TextStr = "Button";
            this.cButton2.Clicked += new VControls.DClicked(this.cButton2_Clicked);
            // 
            // tbx_scanNum
            // 
            this.tbx_scanNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbx_scanNum.DefaultText = "请输入失败重扫次数";
            this.tbx_scanNum.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_scanNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbx_scanNum.Location = new System.Drawing.Point(63, 294);
            this.tbx_scanNum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_scanNum.MaximumSize = new System.Drawing.Size(400, 22);
            this.tbx_scanNum.MinimumSize = new System.Drawing.Size(20, 22);
            this.tbx_scanNum.Name = "tbx_scanNum";
            this.tbx_scanNum.PasswordChar = false;
            this.tbx_scanNum.ReadOnly = false;
            this.tbx_scanNum.Size = new System.Drawing.Size(142, 22);
            this.tbx_scanNum.TabIndex = 169;
            this.tbx_scanNum.TextStr = "";
            this.tbx_scanNum.TextStrChanged += new VControls.DTextStrChanged(this.tbx_scanNum_TextStrChanged);
            // 
            // tbx_trigCmd
            // 
            this.tbx_trigCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbx_trigCmd.DefaultText = "请输入触发扫码枪的命令";
            this.tbx_trigCmd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_trigCmd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbx_trigCmd.Location = new System.Drawing.Point(63, 267);
            this.tbx_trigCmd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_trigCmd.MaximumSize = new System.Drawing.Size(400, 22);
            this.tbx_trigCmd.MinimumSize = new System.Drawing.Size(20, 22);
            this.tbx_trigCmd.Name = "tbx_trigCmd";
            this.tbx_trigCmd.PasswordChar = false;
            this.tbx_trigCmd.ReadOnly = false;
            this.tbx_trigCmd.Size = new System.Drawing.Size(142, 22);
            this.tbx_trigCmd.TabIndex = 167;
            this.tbx_trigCmd.TextStr = "";
            this.tbx_trigCmd.TextStrChanged += new VControls.DTextStrChanged(this.tbx_trigCmd_TextStrChanged);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.White;
            this.btn_close.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_close.Location = new System.Drawing.Point(136, 224);
            this.btn_close.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(67, 28);
            this.btn_close.TabIndex = 163;
            this.btn_close.TextStr = "Button";
            this.btn_close.Clicked += new VControls.DClicked(this.btn_close_Clicked);
            // 
            // btn_openPort
            // 
            this.btn_openPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_openPort.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_openPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_openPort.Location = new System.Drawing.Point(63, 224);
            this.btn_openPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_openPort.Name = "btn_openPort";
            this.btn_openPort.Size = new System.Drawing.Size(67, 28);
            this.btn_openPort.TabIndex = 162;
            this.btn_openPort.TextStr = "Button";
            this.btn_openPort.Clicked += new VControls.DClicked(this.btn_connect_Clicked);
            // 
            // tbx_sendMsg
            // 
            this.tbx_sendMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbx_sendMsg.DefaultText = "请输入需要发送的信息";
            this.tbx_sendMsg.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_sendMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbx_sendMsg.Location = new System.Drawing.Point(231, 300);
            this.tbx_sendMsg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_sendMsg.MaximumSize = new System.Drawing.Size(400, 22);
            this.tbx_sendMsg.MinimumSize = new System.Drawing.Size(20, 22);
            this.tbx_sendMsg.Name = "tbx_sendMsg";
            this.tbx_sendMsg.PasswordChar = false;
            this.tbx_sendMsg.ReadOnly = false;
            this.tbx_sendMsg.Size = new System.Drawing.Size(234, 22);
            this.tbx_sendMsg.TabIndex = 161;
            this.tbx_sendMsg.TextStr = "";
            // 
            // cbx_parityBit
            // 
            this.cbx_parityBit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbx_parityBit.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cbx_parityBit.BackColorNormal = System.Drawing.Color.White;
            this.cbx_parityBit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(174)))), ((int)(((byte)(174)))));
            this.cbx_parityBit.ComboxFont = new System.Drawing.Font("微软雅黑", 12F);
            this.cbx_parityBit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_parityBit.FontColor = System.Drawing.Color.Black;
            this.cbx_parityBit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbx_parityBit.ItemDrawMode = LXCSystem.Control.Base.Common.ListDrawMode.AutoDraw;
            this.cbx_parityBit.ListScrollAppearance.BackColorHover = System.Drawing.Color.Empty;
            this.cbx_parityBit.ListScrollAppearance.BackColorNormal = System.Drawing.Color.Empty;
            this.cbx_parityBit.ListScrollAppearance.BackColorPressed = System.Drawing.Color.Empty;
            this.cbx_parityBit.ListScrollAppearance.ButtonColorHover = System.Drawing.Color.Empty;
            this.cbx_parityBit.ListScrollAppearance.ButtonColorNormal = System.Drawing.Color.Empty;
            this.cbx_parityBit.ListScrollAppearance.ButtonColorPressed = System.Drawing.Color.Empty;
            this.cbx_parityBit.Location = new System.Drawing.Point(53, 184);
            this.cbx_parityBit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_parityBit.Name = "cbx_parityBit";
            this.cbx_parityBit.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.cbx_parityBit.SelectedItemFontColor = System.Drawing.Color.White;
            this.cbx_parityBit.Size = new System.Drawing.Size(130, 25);
            this.cbx_parityBit.TabIndex = 160;
            this.cbx_parityBit.SelectedIndexChanged += new System.EventHandler(this.cbx_parityBit_SelectedIndexChanged);
            // 
            // cbx_portName
            // 
            this.cbx_portName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbx_portName.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cbx_portName.BackColorNormal = System.Drawing.Color.White;
            this.cbx_portName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(174)))), ((int)(((byte)(174)))));
            this.cbx_portName.ComboxFont = new System.Drawing.Font("微软雅黑", 12F);
            this.cbx_portName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_portName.FontColor = System.Drawing.Color.Black;
            this.cbx_portName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbx_portName.ItemDrawMode = LXCSystem.Control.Base.Common.ListDrawMode.AutoDraw;
            this.cbx_portName.ListScrollAppearance.BackColorHover = System.Drawing.Color.Empty;
            this.cbx_portName.ListScrollAppearance.BackColorNormal = System.Drawing.Color.Empty;
            this.cbx_portName.ListScrollAppearance.BackColorPressed = System.Drawing.Color.Empty;
            this.cbx_portName.ListScrollAppearance.ButtonColorHover = System.Drawing.Color.Empty;
            this.cbx_portName.ListScrollAppearance.ButtonColorNormal = System.Drawing.Color.Empty;
            this.cbx_portName.ListScrollAppearance.ButtonColorPressed = System.Drawing.Color.Empty;
            this.cbx_portName.Location = new System.Drawing.Point(53, 56);
            this.cbx_portName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_portName.Name = "cbx_portName";
            this.cbx_portName.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.cbx_portName.SelectedItemFontColor = System.Drawing.Color.White;
            this.cbx_portName.Size = new System.Drawing.Size(130, 25);
            this.cbx_portName.TabIndex = 159;
            this.cbx_portName.SelectedIndexChanged += new System.EventHandler(this.cbx_portName_SelectedIndexChanged);
            // 
            // cbx_stopBit
            // 
            this.cbx_stopBit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbx_stopBit.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cbx_stopBit.BackColorNormal = System.Drawing.Color.White;
            this.cbx_stopBit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(174)))), ((int)(((byte)(174)))));
            this.cbx_stopBit.ComboxFont = new System.Drawing.Font("微软雅黑", 12F);
            this.cbx_stopBit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_stopBit.FontColor = System.Drawing.Color.Black;
            this.cbx_stopBit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbx_stopBit.ItemDrawMode = LXCSystem.Control.Base.Common.ListDrawMode.AutoDraw;
            this.cbx_stopBit.ListScrollAppearance.BackColorHover = System.Drawing.Color.Empty;
            this.cbx_stopBit.ListScrollAppearance.BackColorNormal = System.Drawing.Color.Empty;
            this.cbx_stopBit.ListScrollAppearance.BackColorPressed = System.Drawing.Color.Empty;
            this.cbx_stopBit.ListScrollAppearance.ButtonColorHover = System.Drawing.Color.Empty;
            this.cbx_stopBit.ListScrollAppearance.ButtonColorNormal = System.Drawing.Color.Empty;
            this.cbx_stopBit.ListScrollAppearance.ButtonColorPressed = System.Drawing.Color.Empty;
            this.cbx_stopBit.Location = new System.Drawing.Point(53, 152);
            this.cbx_stopBit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_stopBit.Name = "cbx_stopBit";
            this.cbx_stopBit.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.cbx_stopBit.SelectedItemFontColor = System.Drawing.Color.White;
            this.cbx_stopBit.Size = new System.Drawing.Size(130, 25);
            this.cbx_stopBit.TabIndex = 158;
            this.cbx_stopBit.SelectedIndexChanged += new System.EventHandler(this.cbx_stopBit_SelectedIndexChanged);
            // 
            // tbx_dataBit
            // 
            this.tbx_dataBit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbx_dataBit.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.tbx_dataBit.BackColorNormal = System.Drawing.Color.White;
            this.tbx_dataBit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(174)))), ((int)(((byte)(174)))));
            this.tbx_dataBit.ComboxFont = new System.Drawing.Font("微软雅黑", 12F);
            this.tbx_dataBit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_dataBit.FontColor = System.Drawing.Color.Black;
            this.tbx_dataBit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbx_dataBit.ItemDrawMode = LXCSystem.Control.Base.Common.ListDrawMode.AutoDraw;
            this.tbx_dataBit.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.tbx_dataBit.ListScrollAppearance.BackColorHover = System.Drawing.Color.Empty;
            this.tbx_dataBit.ListScrollAppearance.BackColorNormal = System.Drawing.Color.Empty;
            this.tbx_dataBit.ListScrollAppearance.BackColorPressed = System.Drawing.Color.Empty;
            this.tbx_dataBit.ListScrollAppearance.ButtonColorHover = System.Drawing.Color.Empty;
            this.tbx_dataBit.ListScrollAppearance.ButtonColorNormal = System.Drawing.Color.Empty;
            this.tbx_dataBit.ListScrollAppearance.ButtonColorPressed = System.Drawing.Color.Empty;
            this.tbx_dataBit.Location = new System.Drawing.Point(53, 120);
            this.tbx_dataBit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_dataBit.Name = "tbx_dataBit";
            this.tbx_dataBit.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.tbx_dataBit.SelectedItemFontColor = System.Drawing.Color.White;
            this.tbx_dataBit.Size = new System.Drawing.Size(130, 25);
            this.tbx_dataBit.TabIndex = 157;
            this.tbx_dataBit.SelectedIndexChanged += new System.EventHandler(this.tbx_dataBit_SelectedIndexChanged);
            // 
            // cbx_baudRate
            // 
            this.cbx_baudRate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbx_baudRate.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cbx_baudRate.BackColorNormal = System.Drawing.Color.White;
            this.cbx_baudRate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(174)))), ((int)(((byte)(174)))));
            this.cbx_baudRate.ComboxFont = new System.Drawing.Font("微软雅黑", 12F);
            this.cbx_baudRate.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_baudRate.FontColor = System.Drawing.Color.Black;
            this.cbx_baudRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbx_baudRate.ItemDrawMode = LXCSystem.Control.Base.Common.ListDrawMode.AutoDraw;
            this.cbx_baudRate.Items.AddRange(new object[] {
            "4800",
            "9600",
            "10004",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cbx_baudRate.ListScrollAppearance.BackColorHover = System.Drawing.Color.Empty;
            this.cbx_baudRate.ListScrollAppearance.BackColorNormal = System.Drawing.Color.Empty;
            this.cbx_baudRate.ListScrollAppearance.BackColorPressed = System.Drawing.Color.Empty;
            this.cbx_baudRate.ListScrollAppearance.ButtonColorHover = System.Drawing.Color.Empty;
            this.cbx_baudRate.ListScrollAppearance.ButtonColorNormal = System.Drawing.Color.Empty;
            this.cbx_baudRate.ListScrollAppearance.ButtonColorPressed = System.Drawing.Color.Empty;
            this.cbx_baudRate.Location = new System.Drawing.Point(53, 88);
            this.cbx_baudRate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_baudRate.Name = "cbx_baudRate";
            this.cbx_baudRate.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.cbx_baudRate.SelectedItemFontColor = System.Drawing.Color.White;
            this.cbx_baudRate.Size = new System.Drawing.Size(130, 25);
            this.cbx_baudRate.TabIndex = 156;
            this.cbx_baudRate.SelectedIndexChanged += new System.EventHandler(this.cbx_baudRate_SelectedIndexChanged);
            // 
            // tbx_clientName
            // 
            this.tbx_clientName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbx_clientName.DefaultText = "请输入扫码枪名称";
            this.tbx_clientName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_clientName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbx_clientName.Location = new System.Drawing.Point(52, 18);
            this.tbx_clientName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbx_clientName.MaximumSize = new System.Drawing.Size(400, 22);
            this.tbx_clientName.MinimumSize = new System.Drawing.Size(20, 22);
            this.tbx_clientName.Name = "tbx_clientName";
            this.tbx_clientName.PasswordChar = false;
            this.tbx_clientName.ReadOnly = false;
            this.tbx_clientName.Size = new System.Drawing.Size(151, 22);
            this.tbx_clientName.TabIndex = 155;
            this.tbx_clientName.TextStr = "";
            // 
            // Win_Scaner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(542, 415);
            this.Controls.Add(this.btn_endCharEnter);
            this.Controls.Add(this.btn_endCharNone);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.cButton2);
            this.Controls.Add(this.tbx_scanNum);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbx_trigCmd);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_openPort);
            this.Controls.Add(this.tbx_sendMsg);
            this.Controls.Add(this.cbx_parityBit);
            this.Controls.Add(this.cbx_portName);
            this.Controls.Add(this.cbx_stopBit);
            this.Controls.Add(this.tbx_dataBit);
            this.Controls.Add(this.cbx_baudRate);
            this.Controls.Add(this.tbx_clientName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.lnk_clear);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbx_output);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Win_Scaner";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "串口";
            this.Load += new System.EventHandler(this.Win_Serial_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.LinkLabel lnk_clear;
        private System.Windows.Forms.Panel panel8;
        private VControls.VTextBox tbx_clientName;
        internal System.Windows.Forms.Label label6;
        private LXCSystem.Control.CommonCtrl.LxcComboBox cbx_baudRate;
        private LXCSystem.Control.CommonCtrl.LxcComboBox tbx_dataBit;
        private LXCSystem.Control.CommonCtrl.LxcComboBox cbx_stopBit;
        private LXCSystem.Control.CommonCtrl.LxcComboBox cbx_portName;
        private LXCSystem.Control.CommonCtrl.LxcComboBox cbx_parityBit;
        private VControls.VTextBox tbx_sendMsg;
        internal VControls.VButton btn_openPort;
        internal VControls.VButton btn_close;
        private System.Windows.Forms.Label label7;
        private VControls.VTextBox tbx_trigCmd;
        private VControls.VTextBox tbx_scanNum;
        private System.Windows.Forms.Label label8;
        internal VControls.VButton cButton2;
        private VControls.VButton btn_send;
        internal System.Windows.Forms.TextBox tbx_output;
        internal System.Windows.Forms.Button btn_endCharEnter;
        public System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Button btn_endCharNone;

    }
}