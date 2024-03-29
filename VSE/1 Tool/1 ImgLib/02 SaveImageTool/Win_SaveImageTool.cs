using VSE.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    internal partial class Win_SaveImageTool : FormBase
    {      
        /// <summary>
             /// 当前工具所属的流程
             /// </summary>
        public string jobName = string.Empty;
        /// <summary>
        /// 当前工具名
        /// </summary>
        public string toolName = string.Empty;
        internal Win_SaveImageTool()
        {
            InitializeComponent();
            this.BackColor = VControls.VUI.WinTitleBarBackColor;
            pnl_formBox.BackColor = VControls.VUI.WinBackColor;
            btn_runTool.FlatAppearance.BorderColor = VUI.ButtonBorderColor;
            btn_cancel.FlatAppearance.BorderColor = VUI.ButtonBorderColor;
            comboBox1.BackColorNormal = VControls.VUI.WinBackColor;
            textBox1.ValueChanged += textBox1_valueChanged;
          
        }

        void textBox1_valueChanged(double value)
        {
            saveImageTool.saveDays = (int)textBox1.Value;
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_SaveImageTool _instance;
        public static Win_SaveImageTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_SaveImageTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static SaveImageTool saveImageTool = new SaveImageTool();
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_runTool_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            saveImageTool.Run(true, true, toolName);
            long elapsedTime = sw.ElapsedMilliseconds;
            if (saveImageTool.toolRunStatu != (  ToolRunStatu.成功))
            {
                label1.ForeColor = Color.Red;
                label2.Text = string.Format("耗时：0ms");
            }
            else
            {
                label1.ForeColor = Color.Black;
                label2.Text = string.Format("耗时：{0}ms", elapsedTime.ToString());
            }
            label1.Text = "状态：" + saveImageTool.toolRunStatu.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            saveImageTool.expandTime = checkBox1.Checked;
            pictureBox6.Image = saveImageTool.expandTime ? Resources.复选框 : Resources.去复选框;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            saveImageTool.autoClear = checkBox2.Checked;
            pictureBox4.Image = saveImageTool.autoClear ? Resources.复选框 : Resources.去复选框;
        }

       

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            saveImageTool.autoCreateDirectory = checkBox3.Checked;
            pictureBox5.Image = saveImageTool.autoCreateDirectory ? Resources.复选框 : Resources.去复选框;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (System.IO.Directory.Exists(saveImageTool.imageSavePath))
                {
                    Win_SaveImageTool.Instance.TopMost = false;
                    Process.Start(saveImageTool.imageSavePath);
                }
                
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox1.Checked;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = !checkBox2.Checked;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            checkBox3.Checked = !checkBox3.Checked;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = !radioButton1.Checked;

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                saveImageTool.imageSource = (radioButton1.Checked ? ImageSource.InputImage : ImageSource.WindowImage);
                pictureBox8.Image = radioButton1.Checked ? Resources.勾选 : Resources.去勾选;
                pictureBox7.Image = radioButton2.Checked ? Resources.勾选 : Resources.去勾选;
                if (saveImageTool.imageSavePath == "D:\\DDVision")
                {
                    saveImageTool.imageSavePath += "\\原始图像";
                    tbx_imageSavePath.TextStr = saveImageTool.imageSavePath;
                }
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = !radioButton2.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                saveImageTool.imageSource = (radioButton2.Checked ? ImageSource.WindowImage : ImageSource.InputImage);
                pictureBox7.Image = radioButton2.Checked ? Resources.勾选 : Resources.去勾选;
                pictureBox8.Image = radioButton1.Checked ? Resources.勾选 : Resources.去勾选;
                if (saveImageTool.imageSavePath ==string .Format ( "D:\\DDVision\\Image\\{0}\\原始图像",jobName ))
                {
                    saveImageTool.imageSavePath = string.Format("D:\\DDVision\\Image\\{0}\\结果图像", jobName);
                    tbx_imageSavePath.TextStr = saveImageTool.imageSavePath;
                }
            }
        }

        private void btn_drawTemplateRegionRectangle1_Click(object sender, EventArgs e)
        {
            try
            {
                VControls.FolderBrowserDialog _sampleVistaFolderBrowserDialog = new VControls.FolderBrowserDialog();
                if (Directory.Exists(saveImageTool.imageSavePath))
                    _sampleVistaFolderBrowserDialog.SelectedPath = saveImageTool.imageSavePath;
                _sampleVistaFolderBrowserDialog.Description ="请选择图像文件夹路径";
                if (_sampleVistaFolderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    saveImageTool.imageSavePath = _sampleVistaFolderBrowserDialog.SelectedPath;
                    tbx_imageSavePath.TextStr = _sampleVistaFolderBrowserDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void tbx_imageSavePath_Leave(object sender, EventArgs e)
        {
            saveImageTool.imageSavePath = tbx_imageSavePath.Text;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            saveImageTool.imageName = textBox2.TextStr;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(saveImageTool.imageSavePath))
            {
                Directory.Delete(saveImageTool.imageSavePath, true);
            }
        }

        private void switchButton1_Click(object sender, EventArgs e)
        {
            if (Job.loadForm)
                return;
            bool enable = Job.FindJobByName(jobName).FindToolInfoByName(toolName).enable;
            Job.FindJobByName(jobName).FindToolInfoByName(toolName).enable = !enable;
        }

        private void textBox1_ValueChanged(double value)
        {
            try
            {
                saveImageTool.saveDays = Convert.ToInt16(textBox1.Text.Trim());
            }
            catch { }
        }

        private void textBox2_TextStrChanged(string textStr)
        {

            saveImageTool.imageName = textBox2.TextStr.Trim();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            this.Visible = false;
        }
    }
}
