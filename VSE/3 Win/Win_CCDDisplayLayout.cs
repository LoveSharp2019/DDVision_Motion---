using LXCSystem.Control.CommonCtrl;
using LXCSystem.Files;
using System;
using System.Windows.Forms;

namespace VSE
{
    public partial class CCDDisplayLayout : Form
    {
        public CCDDisplayLayout()
        {
            InitializeComponent();
            this.TopLevel = false;
        }
        private static CCDDisplayLayout _instance;
       
        public static CCDDisplayLayout Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CCDDisplayLayout();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        private void lxcComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            layout1.SetMode((VControls.Layout.CCDWindDispalyMode)lxcComboBox1.SelectedIndex);
            CreateNameTextBox(lxcComboBox1.SelectedIndex + 1);
            for (int i = 0; i < lxcGroupBox1.Controls.Count; i++)
            {
                if (i< Project.Instance.configuration.imageWindowName.Count)
                {
                    ((LxcTextEdit)lxcGroupBox1.Controls[i]).TextValue = Project.Instance.configuration.imageWindowName[i];
                }
                
            }
        }
        private void CreateNameTextBox(int count)
        {
            lxcGroupBox1.Controls.Clear();
            for (int i = 0; i < count; i++)
            {
                LXCSystem.Control.CommonCtrl.LxcTextEdit lxcTextEdit1=new LXCSystem.Control.CommonCtrl.LxcTextEdit();
               lxcTextEdit1.BackColor = System.Drawing.Color.FromArgb(30,30,30);
               lxcTextEdit1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(168)))), ((int)(((byte)(192)))));
               lxcTextEdit1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lxcTextEdit1.ForeColor = System.Drawing.Color.FromArgb(224, 224, 224);
               lxcTextEdit1.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(67)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
               lxcTextEdit1.Location = new System.Drawing.Point(0 + i * 170, 22);
               lxcTextEdit1.LxcName = (i+1).ToString();
               lxcTextEdit1.Name = "lxcTextEdit1";
               lxcTextEdit1.Padding = new System.Windows.Forms.Padding(2);
               lxcTextEdit1.PasswordChar = '\0';
               lxcTextEdit1.Size = new System.Drawing.Size(150, 28);
               lxcTextEdit1.TabIndex = 8;
               lxcTextEdit1.TextBoxSize = new System.Drawing.Size(120, 22);
               lxcTextEdit1.TextFont = new System.Drawing.Font("微软雅黑", 12F);
               lxcTextEdit1.TextForeColor = System.Drawing.Color.FromArgb(224, 224, 224);
                lxcTextEdit1.TextValue = "";
               lxcTextEdit1.TitleBackColor = System.Drawing.Color.Transparent;
               lxcTextEdit1.TitleFont = new System.Drawing.Font("微软雅黑", 12F);
               lxcTextEdit1.TitleForeColor = System.Drawing.Color.FromArgb(224, 224, 224);
                lxcGroupBox1.Controls.Add(lxcTextEdit1);
            }
        }

        private void lxcButton1_Click(object sender, EventArgs e)
        {
            Project.Instance.configuration.ImgWinCount = (byte)(lxcComboBox1.SelectedIndex + 1);
            int sss=(byte)layout1.GetSelectIndex();
            Project.Instance.configuration.ImgWinLayout = sss;
            Project.Instance.configuration.imageWindowName.Clear();
           // string name = "";
            foreach (LxcTextEdit item in lxcGroupBox1.Controls)
            {
                Project.Instance.configuration.imageWindowName.Add(item.TextValue);
             
            }
            Win_ImageWindow.Instance.SetLayout();
        }

        private void CCDDisplayLayout_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                lxcComboBox1.Text = Project.Instance.configuration.ImgWinCount.ToString();
                lxcComboBox1.SelectedIndex = Project.Instance.configuration.ImgWinCount - 1;//必须放到设置文本的后面
                
                layout1.SetSelectIndex(Project.Instance.configuration.ImgWinLayout);
            }
        }
    }
}
