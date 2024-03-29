using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VControls;
using VSE.Core;
using static VSE.BlobTool;

namespace VSE
{
    public partial class Win_BlobTool : ToolWinBase
    {
        public Win_BlobTool()
        {
            InitializeComponent();

            vLinkImage1.CallBackNodes += VLinkImage1_CallBackNodes;
            vLinkImage1.ImageChange += VLinkImage1_ImageChange;

            vLinkPose1.CallBackNodes += VLinkPose1_CallBackNodes;
            vLinkPose1.PoseChange += VLinkPose1_PoseChange;

            vroiSet1.H = PImageWin;
            vroiSet1.ROIChangedEvent += LxcROISet1_ROISetChangedEvent;
            
            PImageWin.viewWindow._hWndControl.ROIChangedEvent += _hWndControl_ROIChangedEvent;

            dgv_selectItem.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_selectItem_CellValueChanged);
            listBox1.Items.AddRange(BlobTool.ResultPar.featuresArrayT);
        }

        internal static BlobTool BlobTool = new BlobTool();
        private static Win_BlobTool _instance;
        internal static Win_BlobTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_BlobTool();
                return _instance;
            }
        }

        private void VLinkImage1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkImage1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkImage1.NoUseImage = toolName;
        }

        private void VLinkImage1_ImageChange(object sender, EventArgs e)
        {
            BlobTool.toolPar.InputPar.图像 = Job.FindJobByName(jobName).FindToolInfoByName(vLinkImage1.ImageText.Split('.')[0]).GetOutput(vLinkImage1.ImageText.Split('.')[1]).value as HImage;
            BlobTool.toolPar.RunPar.图像 = BlobTool.toolPar.InputPar.图像;
            PImageWin.Image = BlobTool.toolPar.InputPar.图像;
            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkImage1.ImageText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("图像").value.ToString());
        }

        private void VLinkPose1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkPose1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkPose1.NoUseROI = toolName;
        }

        private void VLinkPose1_PoseChange(object sender, EventArgs e)
        {
            if (vLinkPose1.ROIText != "无")
            {
                BlobTool.toolPar.InputPar.Pose = Job.FindJobByName(jobName).FindToolInfoByName(vLinkPose1.ROIText.Split('.')[0]).GetOutput(vLinkPose1.ROIText.Split('.')[1]).value as List<XYU>;
              
                if (BlobTool.toolPar.InputPar.Pose != null && BlobTool.toolPar.InputPar.Pose.Count > 0)
                {
                    BlobTool.toolPar.RunPar.StandardPose = BlobTool.toolPar.InputPar.Pose[0];
                    BlobTool.toolPar.RunPar.基准区域 = BlobTool.toolPar.ResultsPar.搜索区域;
                }
                else
                {
                    BlobTool.toolPar.RunPar.StandardPose = new XYU();
                    vroiSet1.ROIFitImage();
                }
                Job.FindJobByName(jobName).ConnectSource(toolName, vLinkPose1.ROIText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("跟随位置").value.ToString(), "跟随位置");
            }
            else
            {
                Job.FindJobByName(jobName).DisSource(toolName, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("跟随位置").value.ToString(), "跟随位置");
            }
        }

        private void LxcROISet1_ROISetChangedEvent(object sender, VControls.VROISet.LxcROISetChangedEventArgs e)
        {
            BlobTool.toolPar.RunPar.基准区域 = e.ROI;
        }

        private void _hWndControl_ROIChangedEvent(object sender, EventArgs e)
        {
            PImageWin.viewWindow.ClearWindow();
            PImageWin.Image = BlobTool.toolPar.InputPar.图像;
          
            if (BlobTool.toolPar.InputPar.Pose != null && BlobTool.toolPar.InputPar.Pose.Count > 0)
            {
                BlobTool.toolPar.RunPar.StandardPose = BlobTool.toolPar.InputPar.Pose[0];
            }
            else
            {
                BlobTool.toolPar.RunPar.StandardPose = new XYU();
            }

            if (BlobTool.toolPar.RunPar.基准区域 != null)
            {
                if (PImageWin.viewWindow.RoiController.ROIList.Count == 0)
                {
                    PImageWin.viewWindow.RoiController.ROIList.Add(BlobTool.toolPar.RunPar.基准区域);
                }
                else
                {
                    PImageWin.viewWindow.RoiController.ROIList[0] = BlobTool.toolPar.RunPar.基准区域;
                }
                PImageWin.viewWindow._hWndControl.repaint();
            }
        }

        private void Win_BlobTool_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                tabControl1.SelectedTab = tabPage1;
                lxcCheckBox_displayGoldenRegion.Checked = BlobTool.toolPar.RunPar.displayGoldenSearchRegion;
                lxcCheckBox_displaySearchRegion.Checked = BlobTool.toolPar.RunPar.displaySearchRegion;
                lxcCheckBox_displayCross.Checked = BlobTool.toolPar.RunPar.displayCross;
                lxcCheckBox_displayOutCircle.Checked = BlobTool.toolPar.RunPar.displayOutCircle;
                lxcCheckBox_regionDrawMode.Checked = BlobTool.toolPar.RunPar.regionDrawMode == VSE.Core.FillMode.Margin ? false : true;
                lxcCheckBox_outCircleDrawMode.Checked = BlobTool.toolPar.RunPar.outCircleDrawMode == VSE.Core.FillMode.Margin ? false : true;
                vroiSet1.mROI = BlobTool.toolPar.ResultsPar.搜索区域;
                numericUpDown_HighThresold.Value = BlobTool.toolPar.RunPar.MaxThreshold;
                numericUpDown_LowThresold.Value = BlobTool.toolPar.RunPar.MinThreshold;

                if (BlobTool.toolPar.InputPar.Pose != null && BlobTool.toolPar.InputPar.Pose.Count > 0)
                {
                    BlobTool.toolPar.RunPar.StandardPose = BlobTool.toolPar.InputPar.Pose[0];
                    BlobTool.toolPar.RunPar.基准区域 = BlobTool.toolPar.ResultsPar.搜索区域;
                }
                else
                {
                    BlobTool.toolPar.RunPar.StandardPose = new XYU();
                    vroiSet1.ROIFitImage();
                }
                _hWndControl_ROIChangedEvent(null, null);

                int count = BlobTool.L_item.Count;
                dgv_OpenCloseOperate.Rows.Clear();
                for (int i = 0; i < count; i++)
                {
                    int idx = dgv_OpenCloseOperate.Rows.Add();
                    ((DataGridViewCheckBoxCell)dgv_OpenCloseOperate.Rows[idx].Cells[0]).Value = BlobTool.L_item[i].enable;
                    dgv_OpenCloseOperate.Rows[idx].Cells[1].Value = BlobTool.L_item[i].type;
                    dgv_OpenCloseOperate.Rows[idx].Cells[2].Value = BlobTool.L_item[i].itemName;
                }

                dgv_selectItem.Rows.Clear();
                dgv_result.Columns.Clear();
                dgv_result.Columns.Add("N", "N");
                dgv_result.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
          
                for (int i = 0; i < BlobTool.IsUseBlobFilter.Count; i++)
                {
                    dgv_selectItem.Rows.Add();
                    dgv_selectItem.Rows[i].Cells[0].Value = BlobTool.ResultPar.Findfeature(BlobTool.IsUseBlobFilter[i].features); ;
                    dgv_selectItem.Rows[i].Cells[1].Value = BlobTool.IsUseBlobFilter[i].enable;
                    dgv_selectItem.Rows[i].Cells[2].Value = BlobTool.IsUseBlobFilter[i].operation;
                    dgv_selectItem.Rows[i].Cells[3].Value = BlobTool.IsUseBlobFilter[i].min;
                    dgv_selectItem.Rows[i].Cells[4].Value = BlobTool.IsUseBlobFilter[i].max;
                    dgv_result.Columns.Add(BlobTool.IsUseBlobFilter[i].features, dgv_selectItem.Rows[i].Cells[0].Value.ToString());
                    dgv_result.Columns[dgv_result.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                LoadResult();
            }
        }

        private void Pbtn_runTool_Click(object sender, EventArgs e)
        {
            Pbtn_runTool.Enabled = false;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            BlobTool.Run(true, true, toolName);
            long elapsedTime = sw.ElapsedMilliseconds;
            if (BlobTool.toolRunStatu != (ToolRunStatu.成功))
            {
                Win_BlobTool.Instance.Plbl_runTime.ForeColor = Color.Red;
                Plbl_runTime.Text = string.Format("耗时：0ms");
            }
            else
            {
                Win_BlobTool.Instance.Plbl_runTime.ForeColor = Color.FromArgb(244, 244, 244);
                Plbl_runTime.Text = string.Format("耗时：{0}ms", elapsedTime.ToString());
            }

            Plbl_toolTip.Text = "状态：" + BlobTool.toolRunStatu.ToString();
            Pbtn_runTool.Enabled = true;
        }

        private void Pbtn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Pbtn_help_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\HelpDoc\\斑点工具.pdf");
            }
            catch
            {
                Win_MessageBox.Instance.MessageBoxShow("\r\n工具暂无工具使用说明文档！", TipType.Warn);
            }
        }

        private void numericUpDown_LowThresold_OnValueChanged(object sender, EventArgs e)
        {
            BlobTool.toolPar.RunPar.MinThreshold = (int)numericUpDown_LowThresold.Value;
            if (BlobTool.toolPar.RunPar.MinThreshold > BlobTool.toolPar.RunPar.MaxThreshold)
            {
                numericUpDown_HighThresold.Value = numericUpDown_LowThresold.Value + 1;
            }
        }

        private void numericUpDown_HighThresold_OnValueChanged(object sender, EventArgs e)
        {
            BlobTool.toolPar.RunPar.MaxThreshold = (int)numericUpDown_HighThresold.Value;
            if (BlobTool.toolPar.RunPar.MaxThreshold < BlobTool.toolPar.RunPar.MinThreshold)
            {
                numericUpDown_LowThresold.Value = numericUpDown_HighThresold.Value - 1;
            }
        }

        private void dgv_OpenCloseOperate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 0)
                {
                    if (dgv_OpenCloseOperate.SelectedRows[0].Cells[1].Value.ToString() == "OpenCircle")
                    {

                        BlobTool.L_item[dgv_OpenCloseOperate.SelectedRows[0].Index].enable = !(bool)dgv_OpenCloseOperate.Rows[e.RowIndex].Cells[0].EditedFormattedValue;
                    }
                    if (dgv_OpenCloseOperate.SelectedRows[0].Cells[1].Value.ToString() == "CloseCircle")
                    {

                        BlobTool.L_item[dgv_OpenCloseOperate.SelectedRows[0].Index].enable = !(bool)dgv_OpenCloseOperate.Rows[e.RowIndex].Cells[0].EditedFormattedValue;
                    }
                    if (dgv_OpenCloseOperate.SelectedRows[0].Cells[1].Value.ToString() == "DilationCircle")
                    {

                        BlobTool.L_item[dgv_OpenCloseOperate.SelectedRows[0].Index].enable = !(bool)dgv_OpenCloseOperate.Rows[e.RowIndex].Cells[0].EditedFormattedValue;
                    }
                    if (dgv_OpenCloseOperate.SelectedRows[0].Cells[1].Value.ToString() == "ErosionCircle")
                    {

                        BlobTool.L_item[dgv_OpenCloseOperate.SelectedRows[0].Index].enable = !(bool)dgv_OpenCloseOperate.Rows[e.RowIndex].Cells[0].EditedFormattedValue;
                    }
                }
                nud_r.OnValueChanged -= nud_r_OnValueChanged;
                if (dgv_OpenCloseOperate.Rows.Count != 0 && dgv_OpenCloseOperate.SelectedRows.Count != 0)
                {
                    if (dgv_OpenCloseOperate.SelectedRows[0].Cells[1].Value.ToString() == "OpenCircle")
                    {

                        nud_r.Value = (decimal)((OpenCircle)BlobTool.L_item[dgv_OpenCloseOperate.SelectedRows[0].Index].item).r;
                    }
                    if (dgv_OpenCloseOperate.SelectedRows[0].Cells[1].Value.ToString() == "CloseCircle")
                    {
                        nud_r.Value = (decimal)((CloseCircle)BlobTool.L_item[dgv_OpenCloseOperate.SelectedRows[0].Index].item).r;

                    }
                    if (dgv_OpenCloseOperate.SelectedRows[0].Cells[1].Value.ToString() == "DilationCircle")
                    {
                        nud_r.Value = (decimal)((DilationCircle)BlobTool.L_item[dgv_OpenCloseOperate.SelectedRows[0].Index].item).r;

                    }
                    if (dgv_OpenCloseOperate.SelectedRows[0].Cells[1].Value.ToString() == "ErosionCircle")
                    {
                        nud_r.Value = (decimal)((ErosionCircle)BlobTool.L_item[dgv_OpenCloseOperate.SelectedRows[0].Index].item).r;
                    }
                }
                nud_r.OnValueChanged += nud_r_OnValueChanged;
            }
        }

        private void ToolStripMenuItem_开运算_Click(object sender, EventArgs e)
        {
            try
            {
                BlobTool.ItemType itemType = new BlobTool.ItemType("OpenCircle", "开运算", new BlobTool.OpenCircle());
                BlobTool.L_item.Add(itemType);

                int idx = dgv_OpenCloseOperate.Rows.Add();
                ((DataGridViewCheckBoxCell)dgv_OpenCloseOperate.Rows[idx].Cells[0]).Value = true;
                dgv_OpenCloseOperate.Rows[idx].Cells[1].Value = "OpenCircle";
                dgv_OpenCloseOperate.Rows[idx].Cells[2].Value = "开运算";
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        private void ToolStripMenuItem_闭运算_Click(object sender, EventArgs e)
        {
            try
            {
                BlobTool.ItemType itemType = new BlobTool.ItemType("CloseCircle", "闭运算", new BlobTool.CloseCircle());
                BlobTool.L_item.Add(itemType);

                int idx = dgv_OpenCloseOperate.Rows.Add();
                ((DataGridViewCheckBoxCell)dgv_OpenCloseOperate.Rows[idx].Cells[0]).Value = true;
                dgv_OpenCloseOperate.Rows[idx].Cells[1].Value = "CloseCircle";
                dgv_OpenCloseOperate.Rows[idx].Cells[2].Value = "闭运算";
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        private void ToolStripMenuItem_膨胀_Click(object sender, EventArgs e)
        {
            try
            {
                BlobTool.ItemType itemType = new BlobTool.ItemType("DilationCircle", "膨胀", new BlobTool.DilationCircle());
                BlobTool.L_item.Add(itemType);

                int idx = dgv_OpenCloseOperate.Rows.Add();
                ((DataGridViewCheckBoxCell)dgv_OpenCloseOperate.Rows[idx].Cells[0]).Value = true;
                dgv_OpenCloseOperate.Rows[idx].Cells[1].Value = "DilationCircle";
                dgv_OpenCloseOperate.Rows[idx].Cells[2].Value = "膨胀";
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        private void ToolStripMenuItem_腐蚀_Click(object sender, EventArgs e)
        {
            try
            {
                BlobTool.ItemType itemType = new BlobTool.ItemType("ErosionCircle", "腐蚀", new BlobTool.ErosionCircle());
                BlobTool.L_item.Add(itemType);

                int idx = dgv_OpenCloseOperate.Rows.Add();
                ((DataGridViewCheckBoxCell)dgv_OpenCloseOperate.Rows[idx].Cells[0]).Value = true;
                dgv_OpenCloseOperate.Rows[idx].Cells[1].Value = "ErosionCircle";
                dgv_OpenCloseOperate.Rows[idx].Cells[2].Value = "腐蚀";
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        private void toolStripButton_Add_Click(object sender, EventArgs e)
        {
            System.Drawing.Point p = new System.Drawing.Point();
            p.X = this.Location.X + PImageWin.Width + 35;
            p.Y = this.Location.Y + 75;
            contextMenuStrip1.Show(p);
        }

        private void toolStripButton_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_OpenCloseOperate.RowCount > 0)
                {
                    if (dgv_OpenCloseOperate.SelectedRows[0] != null)
                    {
                        int idx = dgv_OpenCloseOperate.SelectedRows[0].Index;
                        dgv_OpenCloseOperate.Rows.RemoveAt(idx);
                        BlobTool.L_item.RemoveAt(idx);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        private void toolStripButton_Up_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_OpenCloseOperate.RowCount > 0)
                {
                    int index = dgv_OpenCloseOperate.SelectedRows[0].Index;
                    if (index == 0)
                        return;

                    ItemType temp = BlobTool.L_item[index - 1];
                    BlobTool.L_item[index - 1] = BlobTool.L_item[index];
                    BlobTool.L_item[index] = temp;

                    DataGridViewRow row = dgv_OpenCloseOperate.Rows[index];
                    dgv_OpenCloseOperate.Rows.Remove(row);
                    dgv_OpenCloseOperate.Rows.Insert(index - 1, row);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        private void toolStripButton_Down_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_OpenCloseOperate.RowCount > 0)
                {
                    int index = dgv_OpenCloseOperate.SelectedRows[0].Index;
                    if (index == dgv_OpenCloseOperate.Rows.Count)
                        return;

                    ItemType temp = BlobTool.L_item[index + 1];
                    BlobTool.L_item[index + 1] = BlobTool.L_item[index];
                    BlobTool.L_item[index] = temp;

                    DataGridViewRow row = dgv_OpenCloseOperate.Rows[index];
                    dgv_OpenCloseOperate.Rows.Remove(row);
                    dgv_OpenCloseOperate.Rows.Insert(index + 1, row);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        private void nud_r_OnValueChanged(object sender, EventArgs e)
        {
            if (dgv_OpenCloseOperate.Rows.Count != 0 && dgv_OpenCloseOperate.SelectedRows.Count != 0)
            {
                if (dgv_OpenCloseOperate.SelectedRows[0].Cells[1].Value.ToString() == "OpenCircle")
                {

                    ((OpenCircle)BlobTool.L_item[dgv_OpenCloseOperate.SelectedRows[0].Index].item).r = (double)nud_r.Value;
                }
                if (dgv_OpenCloseOperate.SelectedRows[0].Cells[1].Value.ToString() == "CloseCircle")
                {

                    ((CloseCircle)BlobTool.L_item[dgv_OpenCloseOperate.SelectedRows[0].Index].item).r = (double)nud_r.Value;
                }
                if (dgv_OpenCloseOperate.SelectedRows[0].Cells[1].Value.ToString() == "DilationCircle")
                {

                    ((DilationCircle)BlobTool.L_item[dgv_OpenCloseOperate.SelectedRows[0].Index].item).r = (double)nud_r.Value;
                }
                if (dgv_OpenCloseOperate.SelectedRows[0].Cells[1].Value.ToString() == "ErosionCircle")
                {

                    ((ErosionCircle)BlobTool.L_item[dgv_OpenCloseOperate.SelectedRows[0].Index].item).r = (double)nud_r.Value;
                }
            }
        }

        private int rowSel;
        private int colSel;

        private void dgv_selectItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 1:
                    BlobTool.IsUseBlobFilter[e.RowIndex].enable = dgv_selectItem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "True";
                    break;
                case 2:
                    BlobTool.IsUseBlobFilter[e.RowIndex].operation = dgv_selectItem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    break;
                case 3:
                    BlobTool.IsUseBlobFilter[e.RowIndex].min = Convert.ToDouble(dgv_selectItem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    break;
                case 4:
                    BlobTool.IsUseBlobFilter[e.RowIndex].max = Convert.ToDouble(dgv_selectItem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    break;
                default:
                    break;
            }
        }

        private void dgv_selectItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowSel = e.RowIndex;
            colSel = e.ColumnIndex;
            comboBox_choseMethod.Items.Clear();
            comboBox_choseMethod.Visible = false;
            numericUpDown_choseMethodVal.Visible = false;
            listBox1.Visible = false;
            if (e.ColumnIndex != 0 && e.RowIndex != -1)
            {
                Rectangle rect = dgv_selectItem.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                comboBox_choseMethod.Left = rect.Left - 15;
                comboBox_choseMethod.Top = rect.Top + 28;
                comboBox_choseMethod.Width = rect.Width + 15;
                comboBox_choseMethod.Height = rect.Height + 5;
                numericUpDown_choseMethodVal.Left = rect.Left - 25;
                numericUpDown_choseMethodVal.Top = rect.Top + 28;
                numericUpDown_choseMethodVal.Width = rect.Width + 25;
                numericUpDown_choseMethodVal.Height = rect.Height + 5;

                if (e.ColumnIndex == 1)
                {
                    comboBox_choseMethod.Items.Add("True");
                    comboBox_choseMethod.Items.Add("False");
                    comboBox_choseMethod.Text = dgv_selectItem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    comboBox_choseMethod.Show();
                }
                else if (e.ColumnIndex == 2)
                {
                    comboBox_choseMethod.Items.Add("and");
                    comboBox_choseMethod.Items.Add("or");
                    comboBox_choseMethod.Text = dgv_selectItem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    comboBox_choseMethod.Show();
                }
                else if (e.ColumnIndex == 3)
                {
                    numericUpDown_choseMethodVal.Value = Convert.ToDecimal(dgv_selectItem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    numericUpDown_choseMethodVal.Show();
                }
                else if (e.ColumnIndex == 4)
                {
                    numericUpDown_choseMethodVal.Value = Convert.ToDecimal(dgv_selectItem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    numericUpDown_choseMethodVal.Show();
                }
            }
        }

        private void comboBox_choseMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv_selectItem.Rows[rowSel].Cells[colSel].Value = comboBox_choseMethod.Text;
            comboBox_choseMethod.Visible = false;
        }

        private void numericUpDown_choseMethodVal_ValueChanged(object sender, EventArgs e)
        {
            dgv_selectItem.Rows[rowSel].Cells[colSel].Value = numericUpDown_choseMethodVal.Value;
            numericUpDown_choseMethodVal.Visible = false;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem.ToString() == "添加所有")
            {
                dgv_selectItem.Rows.Clear();
                BlobTool.IsUseBlobFilter.Clear();
                for (int i = 0; i < listBox1.Items.Count - 1; i++)
                {
                    BlobTool.IsUseBlobFilter.Add(BlobTool.BlobFilter[i]);
                    dgv_selectItem.Rows.Add();
                    dgv_selectItem.Rows[dgv_selectItem.RowCount - 1].Cells[0].Value = listBox1.Items[i].ToString();
                    dgv_selectItem.Rows[dgv_selectItem.RowCount - 1].Cells[1].Value =  BlobTool.BlobFilter[i].enable;
                    dgv_selectItem.Rows[dgv_selectItem.RowCount - 1].Cells[2].Value =  BlobTool.BlobFilter[i].operation;
                    dgv_selectItem.Rows[dgv_selectItem.RowCount - 1].Cells[3].Value =  BlobTool.BlobFilter[i].min;
                    dgv_selectItem.Rows[dgv_selectItem.RowCount - 1].Cells[4].Value = BlobTool.BlobFilter[i].max;
                    dgv_result.Columns.Add(BlobTool.BlobFilter[i].features, listBox1.Items[i].ToString());
                    dgv_result.Columns[dgv_result.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                listBox1.Visible = false;
                return;
            }
            for (int i = 0; i < dgv_selectItem.Rows.Count; i++)
            {
                if (dgv_selectItem.Rows[i].Cells[0].Value.ToString() == listBox1.Items[listBox1.SelectedIndex].ToString())
                {
                    listBox1.Visible = false;
                    return;
                }

            }
            BlobTool.IsUseBlobFilter.Add(BlobTool.BlobFilter[listBox1.SelectedIndex]);
            dgv_selectItem.Rows.Add();
            dgv_selectItem.Rows[dgv_selectItem.RowCount - 1].Cells[0].Value = listBox1.Items[listBox1.SelectedIndex].ToString();
            dgv_selectItem.Rows[dgv_selectItem.RowCount - 1].Cells[1].Value =  BlobTool.BlobFilter[listBox1.SelectedIndex].enable;
            dgv_selectItem.Rows[dgv_selectItem.RowCount - 1].Cells[2].Value =  BlobTool.BlobFilter[listBox1.SelectedIndex].operation;
            dgv_selectItem.Rows[dgv_selectItem.RowCount - 1].Cells[3].Value =  BlobTool.BlobFilter[listBox1.SelectedIndex].min;
            dgv_selectItem.Rows[dgv_selectItem.RowCount - 1].Cells[4].Value = BlobTool.BlobFilter[listBox1.SelectedIndex].max;
            dgv_result.Columns.Add(BlobTool.BlobFilter[listBox1.SelectedIndex].features, listBox1.Items[listBox1.SelectedIndex].ToString());
            dgv_result.Columns[dgv_result.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            listBox1.Visible = false;
        }

        private void toolStripLabel_Add_Click(object sender, EventArgs e)
        {
            listBox1.Location = new Point(dgv_selectItem.Location.X, dgv_selectItem.Location.Y + 30);
            listBox1.Show();
        }

        private void toolStripLabel_Delete_Click(object sender, EventArgs e)
        {
            if (dgv_selectItem.SelectedRows.Count == 0)
            {
                return;
            }
            BlobTool.IsUseBlobFilter.RemoveAt(dgv_selectItem.SelectedRows[0].Index);
            dgv_selectItem.Rows.Remove(dgv_selectItem.SelectedRows[0]);
        }

        private void toolStripLabel_Up_Click(object sender, EventArgs e)
        {
            if (rowSel <= 0 || rowSel > dgv_selectItem.Rows.Count - 1)
            {
                return;
            }
            DataGridViewRow r = dgv_selectItem.Rows[rowSel];
            dgv_selectItem.Rows.Remove(r);
            dgv_selectItem.Rows.Insert(rowSel - 1, r);
            SelShapePar sp = BlobTool.IsUseBlobFilter[rowSel];
            BlobTool.IsUseBlobFilter.Remove(sp);
            BlobTool.IsUseBlobFilter.Insert(rowSel - 1, sp);
            dgv_selectItem.Rows[rowSel].Selected = false;
            dgv_selectItem.Rows[dgv_selectItem.RowCount - 1].Selected = false;
            dgv_selectItem.Rows[rowSel - 1].Selected = true;
            rowSel--;
        }

        private void toolStripLabel_Down_Click(object sender, EventArgs e)
        {
            if (rowSel >= dgv_selectItem.Rows.Count - 1 || rowSel < 0)
            {
                return;
            }
            DataGridViewRow r = dgv_selectItem.Rows[rowSel];
            dgv_selectItem.Rows.Remove(r);
            dgv_selectItem.Rows.Insert(rowSel + 1, r);
            SelShapePar sp = BlobTool.IsUseBlobFilter[rowSel];
            BlobTool.IsUseBlobFilter.Remove(sp);
            BlobTool.IsUseBlobFilter.Insert(rowSel + 1, sp);
            dgv_selectItem.Rows[rowSel].Selected = false;
            dgv_selectItem.Rows[dgv_selectItem.RowCount - 1].Selected = false;
            dgv_selectItem.Rows[rowSel + 1].Selected = true;
            rowSel++;
        }

        private void lxcCheckBox_displayGoldenRegion_Click(object sender, EventArgs e)
        {
            BlobTool.toolPar.RunPar.displayGoldenSearchRegion = lxcCheckBox_displayGoldenRegion.Checked;
        }

        private void lxcCheckBox_displaySearchRegion_Click(object sender, EventArgs e)
        {
            BlobTool.toolPar.RunPar.displaySearchRegion = lxcCheckBox_displaySearchRegion.Checked;
        }

        private void lxcCheckBox_displayResultRegion_Click(object sender, EventArgs e)
        {
            BlobTool.toolPar.RunPar.displayResultRegion = lxcCheckBox_displayResultRegion.Checked; 
        }

        private void lxcCheckBox_displayCross_Click(object sender, EventArgs e)
        {
            BlobTool.toolPar.RunPar.displayCross = lxcCheckBox_displayCross.Checked;
        }

        private void lxcCheckBox_displayOutCircle_Click(object sender, EventArgs e)
        {
            BlobTool.toolPar.RunPar.displayOutCircle = lxcCheckBox_displayOutCircle.Checked;
        }

        private void lxcCheckBox_regionDrawMode_Click(object sender, EventArgs e)
        {
            if (!lxcCheckBox_regionDrawMode.Checked)
            {
                BlobTool.toolPar.RunPar.regionDrawMode = VSE.Core.FillMode.Margin;
            }
            else
            {
                BlobTool.toolPar.RunPar.regionDrawMode = VSE.Core.FillMode.Fill;
            }
        }

        private void lxcCheckBox_outCircleDrawMode_Click(object sender, EventArgs e)
        {
            if (!lxcCheckBox_outCircleDrawMode.Checked)
            {
                BlobTool.toolPar.RunPar.outCircleDrawMode = VSE.Core.FillMode.Margin;
            }
            else
            {
                BlobTool.toolPar.RunPar.outCircleDrawMode = VSE.Core.FillMode.Fill;
            }
        }


        internal void LoadResult()
        {
            int c = BlobTool.toolPar.ResultsPar.L_resultBlob.Count;
            CCount.Text = c.ToString() + " 结果";
            DataTable dt = (DataTable)Win_BlobTool.Instance.dgv_result.DataSource;
            if (dt != null)
            {
                dt.Clear();
            }
            if (c <= 0)
            {
                return;
            }
            DataTable dddd = new DataTable();
            for (int j = 0; j < BlobTool.IsUseBlobFilter.Count + 1; j++)
            {
                dddd.Columns.Add();
                Win_BlobTool.Instance.dgv_result.Columns[j].DataPropertyName = "Column" + (j + 1);
            }
            for (int i = 0; i < c; i++)
            {
                string[] ss = new string[BlobTool.IsUseBlobFilter.Count + 1];
                ss[0] = (i + 1).ToString();
                for (int j = 1; j < BlobTool.IsUseBlobFilter.Count + 1; j++)
                {
                    ss[j] = BlobTool.toolPar.ResultsPar.L_resultBlob[i].FilteredResultOut[j - 1].ToString("f2");
                }
                dddd.Rows.Add(ss);
            }
            dgv_result.DataSource = dddd;
        }

        private void dgv_result_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Click_Result_List(e);
        }

        internal void Click_Result_List(DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;
                for (int i = 0; i < BlobTool.toolPar.ResultsPar.L_resultBlob.Count; i++)
                {
                    if (BlobTool.toolPar.ResultsPar.L_resultBlob[i].ID.ToString() == dgv_result.Rows[e.RowIndex].Cells[0].Value.ToString())
                    {

                        Win_BlobTool.Instance.PImageWin.HobjectToHimage(BlobTool.toolPar.InputPar.图像);
                      
                        if (BlobTool.toolPar.RunPar.displayGoldenSearchRegion)
                        {
                            Win_BlobTool.Instance.PImageWin.displayHRegion(BlobTool.toolPar.RunPar.基准区域.getRegion(), "blue", "margin");
                        }
                        if (BlobTool.toolPar.RunPar.displaySearchRegion)
                        {
                            Win_BlobTool.Instance.PImageWin.displayHRegion(BlobTool.toolPar.ResultsPar.搜索区域.getRegion(), "blue", "margin");
                        }
                        if (BlobTool.toolPar.RunPar.displayResultRegion)
                        {
                            Win_BlobTool.Instance.PImageWin.displayHRegion(BlobTool.toolPar.ResultsPar.L_resultBlob[i].MRegion, "red", BlobTool.toolPar.RunPar.regionDrawMode == VSE.Core.FillMode.Margin ? "fill" : "margin");
                        }
                        if (BlobTool.toolPar.RunPar.displayOutCircle)
                        {
                            HRegion G = new HRegion();
                            double r, c, d;
                            BlobTool.toolPar.ResultsPar.L_resultBlob[i].MRegion.SmallestCircle(out r, out c, out d);
                            G.GenCircle(r, c, d);
                            Win_BlobTool.Instance.PImageWin.displayHRegion(G, "green", BlobTool.toolPar.RunPar.outCircleDrawMode == VSE.Core.FillMode.Margin ? "fill" : "margin");
                        }
                        if (BlobTool.toolPar.RunPar.displayCross)
                        {
                            HObject cross;
                            HOperatorSet.GenCrossContourXld(out cross, BlobTool.toolPar.ResultsPar.L_resultBlob[i].CenterOfMassX, BlobTool.toolPar.ResultsPar.L_resultBlob[i].CenterOfMassY, 20, 0);
                            Win_BlobTool.Instance.PImageWin.displayHRegion(cross, "blue");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

    }
}
