using VControls;
namespace VSE
{
    partial class Win_JobInfo
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
            this.cComboBox1.SelectedIndexChanged -= new System.EventHandler(this.cComboBox1_SelectedIndexChanged);
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win_JobInfo));
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.taskList3 = new System.Windows.Forms.TaskList();
            this.taskList2 = new System.Windows.Forms.TaskList();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.taskList1 = new System.Windows.Forms.TaskList();
            this.button3 = new System.Windows.Forms.Button();
            this.cComboBox1 = new LXCSystem.Control.CommonCtrl.LxcComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbx_imageWindowList = new LXCSystem.Control.CommonCtrl.LxcComboBox();
            this.comboBox1 = new LXCSystem.Control.CommonCtrl.LxcComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xxxxxxxxxxxxxxxxxxxxxxx)).BeginInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.SuspendLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.SuspendLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // XXXXXXXXXXXXXXXXXXXXXXpictureBox1
            // 
            this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("XXXXXXXXXXXXXXXXXXXXXXpictureBox1.Image")));
            // 
            // XXXXXXXXXXXXXlbl_title
            // 
            this.XXXXXXXXXXXXXlbl_title.Size = new System.Drawing.Size(961, 25);
            this.XXXXXXXXXXXXXlbl_title.Text = "流程属性";
            this.XXXXXXXXXXXXXlbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx
            // 
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx.Panel1
            // 
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.Controls.Add(this.label2);
            // 
            // xxxxxxxxxxxxxxxxxxxxxxx.Panel2
            // 
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.Controls.Add(this.panel2);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Size = new System.Drawing.Size(993, 625);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(17, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 67;
            this.label1.Text = "调试窗口：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(293, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 21);
            this.label4.TabIndex = 70;
            this.label4.Text = "生产窗口：";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.taskList3);
            this.panel2.Controls.Add(this.taskList2);
            this.panel2.Controls.Add(this.taskList1);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.cComboBox1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cbx_imageWindowList);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(993, 589);
            this.panel2.TabIndex = 72;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label9.ForeColor = System.Drawing.Color.Gray;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(11, 38);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(971, 24);
            this.label9.TabIndex = 115;
            this.label9.Text = "输出配置";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label7.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.label7.Location = new System.Drawing.Point(664, 62);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(318, 24);
            this.label7.TabIndex = 115;
            this.label7.Text = "流程输出";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label6.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(254, 62);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(240, 24);
            this.label6.TabIndex = 115;
            this.label6.Text = "当前工具输出";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label5.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(10, 62);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(241, 24);
            this.label5.TabIndex = 115;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button4.Enabled = false;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(498, 213);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(160, 56);
            this.button4.TabIndex = 114;
            this.button4.Text = "清空输出列表";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Enabled = false;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(499, 151);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(160, 56);
            this.button2.TabIndex = 114;
            this.button2.Text = "从输出列表移除";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Enabled = false;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(499, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 56);
            this.button1.TabIndex = 114;
            this.button1.Text = "填加到输出列表";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // taskList3
            // 
            this.taskList3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.taskList3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.taskList3.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.taskList3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.taskList3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.taskList3.HideSelection = false;
            this.taskList3.HotTracking = true;
            this.taskList3.Indent = 30;
            this.taskList3.ItemHeight = 30;
            this.taskList3.Location = new System.Drawing.Point(664, 88);
            this.taskList3.Name = "taskList3";
            this.taskList3.ShowLines = false;
            this.taskList3.Size = new System.Drawing.Size(318, 491);
            this.taskList3.TabIndex = 112;
            this.taskList3.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.taskList3_NodeMouseClick);
            // 
            // taskList2
            // 
            this.taskList2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.taskList2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.taskList2.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.taskList2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.taskList2.ForeColor = System.Drawing.Color.Silver;
            this.taskList2.HideSelection = false;
            this.taskList2.HotTracking = true;
            this.taskList2.ImageList = this.imageList1;
            this.taskList2.Indent = 30;
            this.taskList2.ItemHeight = 30;
            this.taskList2.Location = new System.Drawing.Point(254, 88);
            this.taskList2.Name = "taskList2";
            this.taskList2.ShowLines = false;
            this.taskList2.Size = new System.Drawing.Size(240, 491);
            this.taskList2.TabIndex = 112;
            this.taskList2.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.taskList2_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "输出.png");
            // 
            // taskList1
            // 
            this.taskList1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.taskList1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.taskList1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.taskList1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.taskList1.ForeColor = System.Drawing.Color.Silver;
            this.taskList1.HideSelection = false;
            this.taskList1.HotTracking = true;
            this.taskList1.Indent = 30;
            this.taskList1.ItemHeight = 30;
            this.taskList1.Location = new System.Drawing.Point(11, 88);
            this.taskList1.Name = "taskList1";
            this.taskList1.ShowLines = false;
            this.taskList1.Size = new System.Drawing.Size(240, 491);
            this.taskList1.TabIndex = 112;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.SeaGreen;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button3.Location = new System.Drawing.Point(891, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 30);
            this.button3.TabIndex = 111;
            this.button3.TabStop = false;
            this.button3.Text = "保 存";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // cComboBox1
            // 
            this.cComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cComboBox1.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.cComboBox1.BackColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.cComboBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cComboBox1.ComboxFont = new System.Drawing.Font("微软雅黑", 12F);
            this.cComboBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cComboBox1.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cComboBox1.ItemDrawMode = LXCSystem.Control.Base.Common.ListDrawMode.AutoDraw;
            this.cComboBox1.Items.AddRange(new object[] {
            "调用时执行",
            "启动后连续执行"});
            this.cComboBox1.ListScrollAppearance.BackColorHover = System.Drawing.Color.Empty;
            this.cComboBox1.ListScrollAppearance.BackColorNormal = System.Drawing.Color.Empty;
            this.cComboBox1.ListScrollAppearance.BackColorPressed = System.Drawing.Color.Empty;
            this.cComboBox1.ListScrollAppearance.ButtonColorHover = System.Drawing.Color.Empty;
            this.cComboBox1.ListScrollAppearance.ButtonColorNormal = System.Drawing.Color.Empty;
            this.cComboBox1.ListScrollAppearance.ButtonColorPressed = System.Drawing.Color.Empty;
            this.cComboBox1.Location = new System.Drawing.Point(664, 8);
            this.cComboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cComboBox1.Name = "cComboBox1";
            this.cComboBox1.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.cComboBox1.SelectedItemFontColor = System.Drawing.Color.White;
            this.cComboBox1.Size = new System.Drawing.Size(198, 25);
            this.cComboBox1.TabIndex = 77;
            this.cComboBox1.Text = null;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(585, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 21);
            this.label3.TabIndex = 76;
            this.label3.Text = "执行方式：";
            // 
            // cbx_imageWindowList
            // 
            this.cbx_imageWindowList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbx_imageWindowList.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.cbx_imageWindowList.BackColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.cbx_imageWindowList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbx_imageWindowList.ComboxFont = new System.Drawing.Font("微软雅黑", 12F);
            this.cbx_imageWindowList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_imageWindowList.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbx_imageWindowList.ItemDrawMode = LXCSystem.Control.Base.Common.ListDrawMode.AutoDraw;
            this.cbx_imageWindowList.ListScrollAppearance.BackColorHover = System.Drawing.Color.Empty;
            this.cbx_imageWindowList.ListScrollAppearance.BackColorNormal = System.Drawing.Color.Empty;
            this.cbx_imageWindowList.ListScrollAppearance.BackColorPressed = System.Drawing.Color.Empty;
            this.cbx_imageWindowList.ListScrollAppearance.ButtonColorHover = System.Drawing.Color.Empty;
            this.cbx_imageWindowList.ListScrollAppearance.ButtonColorNormal = System.Drawing.Color.Empty;
            this.cbx_imageWindowList.ListScrollAppearance.ButtonColorPressed = System.Drawing.Color.Empty;
            this.cbx_imageWindowList.Location = new System.Drawing.Point(371, 8);
            this.cbx_imageWindowList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbx_imageWindowList.Name = "cbx_imageWindowList";
            this.cbx_imageWindowList.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.cbx_imageWindowList.SelectedItemFontColor = System.Drawing.Color.White;
            this.cbx_imageWindowList.Size = new System.Drawing.Size(198, 25);
            this.cbx_imageWindowList.TabIndex = 74;
            this.cbx_imageWindowList.Text = null;
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboBox1.BackColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.comboBox1.BackColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.comboBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboBox1.ComboxFont = new System.Drawing.Font("微软雅黑", 12F);
            this.comboBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.comboBox1.ItemDrawMode = LXCSystem.Control.Base.Common.ListDrawMode.AutoDraw;
            this.comboBox1.ListScrollAppearance.BackColorHover = System.Drawing.Color.Empty;
            this.comboBox1.ListScrollAppearance.BackColorNormal = System.Drawing.Color.Empty;
            this.comboBox1.ListScrollAppearance.BackColorPressed = System.Drawing.Color.Empty;
            this.comboBox1.ListScrollAppearance.ButtonColorHover = System.Drawing.Color.Empty;
            this.comboBox1.ListScrollAppearance.ButtonColorNormal = System.Drawing.Color.Empty;
            this.comboBox1.ListScrollAppearance.ButtonColorPressed = System.Drawing.Color.Empty;
            this.comboBox1.Location = new System.Drawing.Point(93, 8);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.comboBox1.SelectedItemFontColor = System.Drawing.Color.White;
            this.comboBox1.Size = new System.Drawing.Size(178, 25);
            this.comboBox1.TabIndex = 73;
            this.comboBox1.Text = null;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.label10.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label10.ForeColor = System.Drawing.Color.LightGreen;
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Location = new System.Drawing.Point(497, 88);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(164, 491);
            this.label10.TabIndex = 115;
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label8.Location = new System.Drawing.Point(3, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(987, 84);
            this.label8.TabIndex = 3;
            this.label8.Text = "输出配置";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(49, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "流程设定";
            // 
            // Win_JobInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ClientSize = new System.Drawing.Size(997, 633);
            this.LockBox = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Win_JobInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "当前流程属性";
            this.VisibleChanged += new System.EventHandler(this.Win_JobInfo_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.XXXXXXXXXXXXXXXXXXXXXXpictureBox1)).EndInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.ResumeLayout(false);
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel1.PerformLayout();
            this.xxxxxxxxxxxxxxxxxxxxxxx.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xxxxxxxxxxxxxxxxxxxxxxx)).EndInit();
            this.xxxxxxxxxxxxxxxxxxxxxxx.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        internal LXCSystem.Control.CommonCtrl.LxcComboBox cbx_imageWindowList;
        internal LXCSystem.Control.CommonCtrl.LxcComboBox comboBox1;
        internal LXCSystem.Control.CommonCtrl.LxcComboBox cComboBox1;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button button3;
        private System.Windows.Forms.TaskList taskList3;
        private System.Windows.Forms.TaskList taskList2;
        private System.Windows.Forms.TaskList taskList1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button4;
    }
}