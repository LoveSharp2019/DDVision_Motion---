using System;
using System.IO;
using VControls;
using VSE.Core;
using WinFormsUI.Docking;

namespace VSE
{
    internal partial class Win_ProjetSettings : DockContent
    {
        internal Win_ProjetSettings()
        {
            InitializeComponent();
            //cbx_cardType.SelectedIndexChanged += cbx_cardType_SelectedIndexChanged;
            this.BackColor = VUI.WinBackColor;
        }

        void cbx_cardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbx_cardType.SelectedIndex == 0)
            //    ckb_vitualCard.Visible = false;
            //else
            //    ckb_vitualCard.Visible = true;
        }



        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Win_ProjetSettings _instance;
        public static Win_ProjetSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_ProjetSettings();
                return _instance;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ckb_vitualCard_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btn_drawTemplateRegionRectangle2_Click(object sender, EventArgs e)
        {
            Project.InportProject();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Project.ExportProject ();
        }

        private void Win_ProjetSettings_Load(object sender, EventArgs e)
        {
            CCDDisplayLayout.Instance.Dock= System.Windows.Forms.DockStyle.Fill;
            lxcGroupBox4.Controls.Add(CCDDisplayLayout.Instance);
            CCDDisplayLayout.Instance.Show();
        }
        private void btn_selectPath_Click(object sender, EventArgs e)
        {
            try
            {



                VControls.FolderBrowserDialog folderBrowseDialog = new VControls.FolderBrowserDialog();
                if (Directory.Exists(Project.Instance.configuration.dataPath))
                    folderBrowseDialog.SelectedPath = Project.Instance.configuration.dataPath;

                folderBrowseDialog.Description = "请选择数据存储路径";
                if (folderBrowseDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Project.Instance.configuration.dataPath = folderBrowseDialog.SelectedPath;
                    tbx_dataPath.Text = folderBrowseDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        private void Win_ProjetSettings_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.ckb_dataSaveDays.ValueChanged -= new VControls.DValueChanged(this.ckb_dataSaveDays_ValueChanged);
                this.tbx_jobsRunPouseTime.ValueChanged -= new VControls.DValueChanged(this.tbx_jobsRunPouseTime_ValueChanged);
                tbx_programTitle.TextStr = Project.Instance.configuration.ProgramTitle;
                if (Project.Instance.configuration.UserWinModeNoDefault)
                {
                    lxcRadioButton1.Checked=true;
                }
                else
                {
                    lxcRadioButton4.Checked = true;
                }
                tbx_dataPath.TextStr= Project.Instance.configuration.dataPath;

                ckb_dataSaveDays.Value=Project.Instance.configuration.dataSaveDays;
                tbx_jobsRunPouseTime.Value = Project.Instance.configuration.timeBetweenJobRun;
                lxcCheckBox3.Checked = Project.Instance.configuration.failStop;
                lxcCheckBox1.Checked = Project.Instance.configuration.endStop;
                this.ckb_dataSaveDays.ValueChanged += new VControls.DValueChanged(this.ckb_dataSaveDays_ValueChanged);
                this.tbx_jobsRunPouseTime.ValueChanged += new VControls.DValueChanged(this.tbx_jobsRunPouseTime_ValueChanged);
            }
        }

        private void tbx_programTitle_TextStrChanged(string textStr)
        {
            Project.Instance.configuration.ProgramTitle = tbx_programTitle.TextStr;
        }

        private void lxcRadioButton4_Click(object sender, EventArgs e)
        {
            Project.Instance.configuration.UserWinModeNoDefault = lxcRadioButton4.Checked;
        }

        private void ckb_dataSaveDays_ValueChanged(double value)
        {
            Project.Instance.configuration.dataSaveDays =(int)ckb_dataSaveDays.Value;


        }

        private void tbx_jobsRunPouseTime_ValueChanged(double value)
        {
            Project.Instance.configuration.timeBetweenJobRun = (int)tbx_jobsRunPouseTime.Value;
        }

        private void lxcCheckBox3_Click(object sender, EventArgs e)
        {
            Project.Instance.configuration.failStop= lxcCheckBox3.Checked;
        }

        private void lxcCheckBox1_Click(object sender, EventArgs e)
        {
            Project.Instance.configuration.endStop= lxcCheckBox1.Checked;
        }
    }
}
