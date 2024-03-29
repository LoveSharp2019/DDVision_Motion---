using HalconDotNet;
using Lxc.VisionPlus.ImageView;
using Lxc.VisionPlus.ImageView.Model;
using LXCSystem.Control.CommonCtrl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinFormsUI.Docking;

namespace VSE
{
    public partial class Win_ImageWindow : DockContent
    {
        System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

        public Win_ImageWindow()
        {
            InitializeComponent();
            tableLayoutPanel1 = new TableLayoutPanel();
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1151, 631);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.TabIndex = 2;
            this.Controls.Add(tableLayoutPanel1);
        }


        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_ImageWindow _instance;
        public static Win_ImageWindow Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_ImageWindow();
                _instance.CloseButtonVisible = false;
                return _instance;
            }
        }
        internal DockState dockState = new DockState();

        /// <summary>
        /// 图像窗体集合
        /// </summary>
        public static Dictionary<string, ImgView> HDisplayCtrs = new Dictionary<string, ImgView>();

     


        /// <summary>
        /// 显示图像
        /// </summary>
        /// <param name="image"></param>
   
        private void Win_ImageWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Project.Instance.configuration.imageWindowName.Remove(this.Text);
            Win_ImageWindow.HDisplayCtrs.Remove(this.Text);
            _instance = null;
        }

        internal List<ROI> regions = new List<ROI>();

        private void CreateHDisplayCtrs(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ROIController roiController1 = new ROIController();
                ImgView hDisplayCtr1 = new ImgView();
                hDisplayCtr1.BackColor = Color.FromArgb(45,45,48);
                hDisplayCtr1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                hDisplayCtr1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                hDisplayCtr1.Location = new System.Drawing.Point(270, 38);
                hDisplayCtr1.Margin = new System.Windows.Forms.Padding(0);
                hDisplayCtr1.Dock = DockStyle.Fill;
                hDisplayCtr1.Name = "hDisplayCtr1";
                hDisplayCtr1.viewWindow.RoiController = roiController1;
                hDisplayCtr1.CaptionVisible = true;
                

                HDisplayCtrs.Add(i.ToString(), hDisplayCtr1);

            }
        }
        public void SetCaption()
        {
           


            int count = HDisplayCtrs.Count;
            for (int i = 0; i < count; i++)
            {
                if (Project.Instance.configuration.imageWindowName.Count == 0 || Project.Instance.configuration.imageWindowName[i] == "")
                {
                    HDisplayCtrs[i.ToString()].Caption = (i + 1).ToString();
                }
                else
                {
                    if (CCDDisplayLayout.Instance.lxcGroupBox1.Controls.Count!=0)
                    {
                        ((LxcTextEdit)CCDDisplayLayout.Instance.lxcGroupBox1.Controls[i]).TextValue = Project.Instance.configuration.imageWindowName[i];
                    }
                    
                   
                    HDisplayCtrs[i.ToString()].Caption = Project.Instance.configuration.imageWindowName[i];
                }
            }
        }
        private void CreatLayoutTable(int rowNum, int colNum)
        {
            this.tableLayoutPanel1.ColumnStyles.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();
            this.tableLayoutPanel1.ColumnCount = colNum;
            for (int i = 0; i < colNum; i++)
            {
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 / colNum));
            }

            this.tableLayoutPanel1.RowCount = rowNum;
            for (int i = 0; i < rowNum; i++)
            {
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / rowNum));
            }

        }
        public void SetLayout()
        {
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            HDisplayCtrs.Clear();
            this.tableLayoutPanel1.Controls.Clear();
            CreateHDisplayCtrs(Project.Instance.configuration.ImgWinCount);
            SetCaption();
            switch (Project.Instance.configuration.ImgWinCount)
            {
                case 1:
                    CreatLayoutTable(1, 1);
                    this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["0"], 0, 0);
                    break;
                case 2:

                    if (Project.Instance.configuration.ImgWinLayout == 1)
                    {
                        CreatLayoutTable(2, 1);
                        this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["0"], 0, 0);
                        this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["1"], 0, 1);
                    }
                    else
                    {
                        CreatLayoutTable(1, 2);
                        this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["0"], 0, 0);
                        this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["1"], 1, 0);
                    }
                    break;
                case 3:
                    #region 三画布模式生成
                    switch (Project.Instance.configuration.ImgWinLayout)
                    {
                        case 0:
                            CreatLayoutTable(2, 2);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["0"], 0, 0);
                            this.tableLayoutPanel1.SetRowSpan(HDisplayCtrs["0"], 2);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["1"], 1, 0);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["2"], 1, 1);
                            break;
                        case 1:
                            CreatLayoutTable(2, 2);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["0"], 0, 0);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["1"], 0, 1);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["2"], 1, 0);
                            this.tableLayoutPanel1.SetRowSpan(HDisplayCtrs["2"], 2);
                            break;
                        case 2:
                            CreatLayoutTable(2, 2);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["0"], 0, 0);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["1"], 1, 0);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["2"], 0, 1);
                            this.tableLayoutPanel1.SetColumnSpan(HDisplayCtrs["2"], 2);
                            break;
                        case 3:
                            CreatLayoutTable(2, 2);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["0"], 0, 0);
                            this.tableLayoutPanel1.SetColumnSpan(HDisplayCtrs["0"], 2);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["1"], 0, 1);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["2"], 1, 1);

                            break;
                    }
                    #endregion
                    break;
                case 4:
                    #region 四画布模式生成
                    switch (Project.Instance.configuration.ImgWinLayout)
                    {
                       
                        case 0:
                            CreatLayoutTable(2, 2);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["0"], 0, 0);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["1"], 1, 0);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["2"], 0, 1);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["3"], 1, 1);
                            break;
                        case 1:
                            CreatLayoutTable(3, 2);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["0"], 0, 0);
                            this.tableLayoutPanel1.SetRowSpan(HDisplayCtrs["0"], 3);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["1"], 1, 0);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["2"], 1, 1);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["3"], 1, 2);
                            break;
                        case 2:
                            CreatLayoutTable(3, 2);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["0"], 0, 0);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["1"], 0, 1);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["2"], 0, 2);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["3"], 1, 0);
                            this.tableLayoutPanel1.SetRowSpan(HDisplayCtrs["3"], 3);
                            break;
                        case 3:
                            CreatLayoutTable(2, 3);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["0"], 0, 0);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["1"], 1, 0);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["2"], 2, 0);
                            this.tableLayoutPanel1.Controls.Add(HDisplayCtrs["3"], 0, 1);
                            this.tableLayoutPanel1.SetColumnSpan(HDisplayCtrs["0"], 3);

                            break;
                    }
                    #endregion
                    break;

            }
            this.tableLayoutPanel1.ResumeLayout();
            // this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout();
        }

        private void Win_ImageWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
