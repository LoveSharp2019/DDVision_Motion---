using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VControls;
using VSE.Core;
using static VSE.MorphologyTool;

namespace VSE
{
    public partial class Win_MorphologyTool : ToolWinBase
    {
        public Win_MorphologyTool()
        {
            InitializeComponent();
            btn_OrgImg.FlatAppearance.BorderColor = VUI.ButtonBorderColor;
            vLinkROI1.CallBackNodes += VLinkROI1_CallBackNodes;
            vLinkROI1.ROIChange += VLinkROI1_ROIChange;
            vLinkImage1.CallBackNodes += VLinkImage1_CallBackNodes;
            vLinkImage1.ImageChange += VLinkImage1_ImageChange;
        }

        private void VLinkROI1_ROIChange(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        private void VLinkROI1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkROI1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkROI1.NoUseROI = toolName;
        }
        private void VLinkImage1_ImageChange(object sender, EventArgs e)
        {
            MorphologyTool.toolPar.InputPar.图像 = Job.FindJobByName(jobName).FindToolInfoByName(vLinkImage1.ImageText.Split('.')[0]).GetOutput(vLinkImage1.ImageText.Split('.')[1]).value as HImage;
            PImageWin.Image = MorphologyTool.toolPar.InputPar.图像;
            Job.FindJobByName(jobName).ConnectSource(toolName, vLinkImage1.ImageText, Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetInput("图像").value.ToString());
        }

        private void VLinkImage1_CallBackNodes(object sender, EventArgs e)
        {
            vLinkImage1.Nodes = Job.GetJobTreeStatic().Nodes;
            vLinkImage1.NoUseImage = toolName;

        }
        private static Win_MorphologyTool _instance;
        public static Win_MorphologyTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_MorphologyTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static MorphologyTool MorphologyTool = new MorphologyTool();
        internal List<ROI> regions = new List<ROI>();

        private void Pbtn_close_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void Pbtn_runTool_Click(object sender, System.EventArgs e)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
          

                MorphologyTool.Run(true, true, toolName);
                long time = sw.ElapsedMilliseconds;

                if (MorphologyTool.toolRunStatu != (  ToolRunStatu.成功))
                {
                    Plbl_toolTip.ForeColor = Color.Red;
                    Plbl_runTime.Text = string.Format("耗时：0ms");
                    Plbl_toolTip.Text = "状态：" + MorphologyTool.toolRunStatu.ToString();
                }
                else
                {
                    Plbl_toolTip.ForeColor = Color.White;
                    Plbl_runTime.Text = string.Format("耗时：{0}ms", time.ToString());
                    PImageWin.viewWindow._hWndControl.clearHRegionList();
                    PImageWin.viewWindow._hWndControl.repaint();
                    PImageWin.displayHRegion(MorphologyTool.toolPar.ResultPar.region,"green");
                    Plbl_toolTip.Text = "状态：" + MorphologyTool.toolRunStatu.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            System.Drawing.Point p = new System.Drawing.Point();
            p.X = this.Location.X + PImageWin.Width + 35;
            p.Y = this.Location.Y + 75;
            contextMenuStrip1.Show(p);
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MorphologyTool.ItemType itemType = new MorphologyTool.ItemType("OpenCircle", "开运算", new MorphologyTool.OpenCircle());
                MorphologyTool.L_item.Add(itemType);

                int idx = dataGridView1.Rows.Add();
                ((DataGridViewCheckBoxCell)dataGridView1.Rows[idx].Cells[0]).Value = true;
                dataGridView1.Rows[idx].Cells[1].Value = "OpenCircle";
                dataGridView1.Rows[idx].Cells[2].Value = "开运算";
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void ClosetoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                MorphologyTool.ItemType itemType = new MorphologyTool.ItemType("CloseCircle", "闭运算", new MorphologyTool.CloseCircle());
                MorphologyTool.L_item.Add(itemType);

                int idx = dataGridView1.Rows.Add();
                ((DataGridViewCheckBoxCell)dataGridView1.Rows[idx].Cells[0]).Value = true;
                dataGridView1.Rows[idx].Cells[1].Value = "CloseCircle";
                dataGridView1.Rows[idx].Cells[2].Value = "闭运算";
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.RowCount>0)
                {
                    if (dataGridView1.SelectedRows[0] != null)
                    {
                        int idx = dataGridView1.SelectedRows[0].Index;
                        dataGridView1.Rows.RemoveAt(idx);
                        MorphologyTool.L_item.RemoveAt(idx);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.RowCount > 0)
                {

               
                int index = dataGridView1.SelectedRows[0].Index;
                if (index == 0)
                    return;

                ItemType temp = MorphologyTool.L_item[index - 1];
                MorphologyTool.L_item[index - 1] = MorphologyTool.L_item[index];
                MorphologyTool.L_item[index] = temp;

                DataGridViewRow row = dataGridView1.Rows[index];
                dataGridView1.Rows.Remove(row);
                dataGridView1.Rows.Insert(index - 1, row);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.RowCount > 0)
                {

                    int index = dataGridView1.SelectedRows[0].Index;
                    if (index == dataGridView1.Rows.Count)
                        return;

                    ItemType temp = MorphologyTool.L_item[index + 1];
                    MorphologyTool.L_item[index + 1] = MorphologyTool.L_item[index];
                    MorphologyTool.L_item[index] = temp;

                    DataGridViewRow row = dataGridView1.Rows[index];
                    dataGridView1.Rows.Remove(row);
                    dataGridView1.Rows.Insert(index + 1, row);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void btn_OrgImg_MouseDown(object sender, MouseEventArgs e)
        {
            PImageWin.ClearWindow();
            PImageWin.Image = MorphologyTool.toolPar.InputPar.图像;
        }

        private void btn_OrgImg_MouseUp(object sender, MouseEventArgs e)
        {
            PImageWin.displayHRegion(MorphologyTool.toolPar.ResultPar.region, "green");
        }

        private void 膨胀ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MorphologyTool.ItemType itemType = new MorphologyTool.ItemType("DilationCircle", "膨胀", new MorphologyTool.DilationCircle());
                MorphologyTool.L_item.Add(itemType);

                int idx = dataGridView1.Rows.Add();
                ((DataGridViewCheckBoxCell)dataGridView1.Rows[idx].Cells[0]).Value = true;
                dataGridView1.Rows[idx].Cells[1].Value = "DilationCircle";
                dataGridView1.Rows[idx].Cells[2].Value = "膨胀";
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void 腐蚀ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MorphologyTool.ItemType itemType = new MorphologyTool.ItemType("ErosionCircle", "腐蚀", new MorphologyTool.ErosionCircle());
                MorphologyTool.L_item.Add(itemType);

                int idx = dataGridView1.Rows.Add();
                ((DataGridViewCheckBoxCell)dataGridView1.Rows[idx].Cells[0]).Value = true;
                dataGridView1.Rows[idx].Cells[1].Value = "ErosionCircle";
                dataGridView1.Rows[idx].Cells[2].Value = "腐蚀";
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void Win_MorphologyTool_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
              int count=  MorphologyTool.L_item.Count;
                dataGridView1.Rows.Clear();
                for (int i = 0; i < count; i++)
                {
                    int idx = dataGridView1.Rows.Add();
                    ((DataGridViewCheckBoxCell)dataGridView1.Rows[idx].Cells[0]).Value = MorphologyTool.L_item[i].enable;
                    dataGridView1.Rows[idx].Cells[1].Value = MorphologyTool.L_item[i].type;
                    dataGridView1.Rows[idx].Cells[2].Value = MorphologyTool.L_item[i].itemName;
                }
              
            }
        }

        private void nud_r_OnValueChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count!=0&&dataGridView1.SelectedRows.Count!=0)
            {
                if (dataGridView1.SelectedRows[0].Cells[1].Value.ToString() == "OpenCircle")
                {

                    ((OpenCircle)MorphologyTool.L_item[dataGridView1.SelectedRows[0].Index].item).r = (double)nud_r.Value;
                }
                if (dataGridView1.SelectedRows[0].Cells[1].Value.ToString() == "CloseCircle")
                {

                    ((CloseCircle)MorphologyTool.L_item[dataGridView1.SelectedRows[0].Index].item).r = (double)nud_r.Value;
                }
                if (dataGridView1.SelectedRows[0].Cells[1].Value.ToString() == "DilationCircle")
                {

                    ((DilationCircle)MorphologyTool.L_item[dataGridView1.SelectedRows[0].Index].item).r = (double)nud_r.Value;
                }
                if (dataGridView1.SelectedRows[0].Cells[1].Value.ToString() == "ErosionCircle")
                {

                    ((ErosionCircle)MorphologyTool.L_item[dataGridView1.SelectedRows[0].Index].item).r = (double)nud_r.Value;
                }
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>-1)
            {
                if (e.ColumnIndex == 0)
                {
                    if(dataGridView1.SelectedRows[0].Cells[1].Value.ToString() == "OpenCircle")
                    {

                        MorphologyTool.L_item[dataGridView1.SelectedRows[0].Index].enable = !(bool)dataGridView1.Rows[e.RowIndex].Cells[0].EditedFormattedValue;
                    }
                    if (dataGridView1.SelectedRows[0].Cells[1].Value.ToString() == "CloseCircle")
                    {

                        MorphologyTool.L_item[dataGridView1.SelectedRows[0].Index].enable = !(bool)dataGridView1.Rows[e.RowIndex].Cells[0].EditedFormattedValue;
                    }
                    if (dataGridView1.SelectedRows[0].Cells[1].Value.ToString() == "DilationCircle")
                    {

                       MorphologyTool.L_item[dataGridView1.SelectedRows[0].Index].enable = !(bool)dataGridView1.Rows[e.RowIndex].Cells[0].EditedFormattedValue;
                    }
                    if (dataGridView1.SelectedRows[0].Cells[1].Value.ToString() == "ErosionCircle")
                    {

                       MorphologyTool.L_item[dataGridView1.SelectedRows[0].Index].enable = !(bool)dataGridView1.Rows[e.RowIndex].Cells[0].EditedFormattedValue;
                     }
                   
                }
                nud_r.OnValueChanged -= nud_r_OnValueChanged;
                if (dataGridView1.Rows.Count != 0 && dataGridView1.SelectedRows.Count != 0)
                {
                    if (dataGridView1.SelectedRows[0].Cells[1].Value.ToString() == "OpenCircle")
                    {

                        nud_r.Value = (decimal)((OpenCircle)MorphologyTool.L_item[dataGridView1.SelectedRows[0].Index].item).r;
                    }
                    if (dataGridView1.SelectedRows[0].Cells[1].Value.ToString() == "CloseCircle")
                    {
                        nud_r.Value = (decimal)((CloseCircle)MorphologyTool.L_item[dataGridView1.SelectedRows[0].Index].item).r;

                    }
                    if (dataGridView1.SelectedRows[0].Cells[1].Value.ToString() == "DilationCircle")
                    {
                        nud_r.Value = (decimal)((DilationCircle)MorphologyTool.L_item[dataGridView1.SelectedRows[0].Index].item).r;

                    }
                    if (dataGridView1.SelectedRows[0].Cells[1].Value.ToString() == "ErosionCircle")
                    {
                        nud_r.Value = (decimal)((ErosionCircle)MorphologyTool.L_item[dataGridView1.SelectedRows[0].Index].item).r;
                    }
                }
                nud_r.OnValueChanged += nud_r_OnValueChanged;

            }

            
        }
    }
}
