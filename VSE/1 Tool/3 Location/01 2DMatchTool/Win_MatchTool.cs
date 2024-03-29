using HalconDotNet;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    public partial class Win_MatchTool : ToolWinBase
    {
        public Win_MatchTool()
        {
            InitializeComponent();
            
            vroiSet2.H = PImageWin;

            vroiSet2.ROIChangedEvent += LxcROISet2_ROISetChangedEvent;
            vroiSet1.ModeRoi = true;
            vroiSet1.H = PImageWin;

            vroiSet1.ROIChangedEvent += LxcROISet1_ROISetChangedEvent;

        }
        
        private void LxcROISet1_ROISetChangedEvent(object sender, VControls.VROISet.LxcROISetChangedEventArgs e)
        {
            MatchTool.toolPar.RunPar.TemplateRegion = e.ROI;
   
        }
        private void LxcROISet2_ROISetChangedEvent(object sender, VControls.VROISet.LxcROISetChangedEventArgs e)
        {
            MatchTool.toolPar.InputPar.ROIRegion = e.ROI.getRegion();
        }
        internal static MatchTool MatchTool = new MatchTool();
        private static Win_MatchTool _instance;
        public static Win_MatchTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_MatchTool();
                return _instance;
            }
        }

        private void Pbtn_runTool_Click(object sender, EventArgs e)
        {
            Pbtn_runTool.Enabled = false;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            MatchTool.Run(true, true, toolName);
            long elapsedTime = sw.ElapsedMilliseconds;
            if (MatchTool.toolRunStatu != (  ToolRunStatu.成功))
            {
                Win_MatchTool.Instance.Plbl_runTime.ForeColor = Color.Red;
                Plbl_runTime.Text = string.Format("耗时：0ms");
            }
            else
            {
                //显示结果
                if (Win_MatchTool.Instance.Visible)
                {
                    Win_MatchTool.Instance.dgv_matchResult.Rows.Clear();
                    for (int i = 0; i < MatchTool.toolPar.Results.FindCount; i++)
                    {
                    int index = Win_MatchTool.Instance.dgv_matchResult.Rows.Add();
                    dgv_matchResult.Rows[index].Cells[0].Value = i + 1;
                    dgv_matchResult.Rows[index].Cells[1].Value = Math.Round((double)MatchTool.toolPar.Results.ResultsList[i].Socre, 3);
                    dgv_matchResult.Rows[index].Cells[2].Value = Math.Round((double)MatchTool.toolPar.Results.ResultsList[i].Col, 3);
                    dgv_matchResult.Rows[index].Cells[3].Value = Math.Round((double)MatchTool.toolPar.Results.ResultsList[i].Row, 3);
                    dgv_matchResult.Rows[index].Cells[4].Value = Math.Round((double)MatchTool.toolPar.Results.ResultsList[i].Angle*180/Math.PI, 3);
                    }
                    Application.DoEvents();
                }
                Win_MatchTool.Instance.Plbl_runTime.ForeColor = Color.FromArgb(244,244,244);
                Plbl_runTime.Text = string.Format("耗时：{0}ms", elapsedTime.ToString());
            }
            Plbl_toolTip.Text = "状态：" + MatchTool.toolRunStatu.ToString();
            Pbtn_runTool.Enabled = true;
        }

        private void Pbtn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MatchTool.toolPar.RunPar.matchMode = (MatchMode)(comboBox2.SelectedIndex);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MatchTool.toolPar.RunPar.TemplateRegion == null)
            {
                label4.ForeColor = Color.Red;
                label4.Text = "状态：未创建模板";
                return;
            }
            Win_MatchTool.Instance.button4.Text = "学习中";
            Thread.Sleep(20);
            Application.DoEvents();
            MatchTool.ShowStandardImage();
            MatchTool.CreateAndShowTemplate();
            //shapeMatchTool.ShowTemplate();
            Win_MatchTool.Instance.button4.Text = "重新学习";
        }

        private void numericUpDown3_OnValueChanged(object sender, EventArgs e)
        {
            MatchTool.toolPar.RunPar.minScore = (double)numericUpDown3.Value;
        }

        private void numericUpDown6_OnValueChanged(object sender, EventArgs e)
        {
            MatchTool.toolPar.RunPar.matchNum =(int)numericUpDown6.Value;
        }

        private void lxcNumEdit1_OnValueChanged(object sender, EventArgs e)
        {
            MatchTool.toolPar.RunPar.angleStep = (int)lxcNumEdit1.Value;
        }

       

        private void tkb_contrast_OnValueChanged(object sender, EventArgs e)
        {
            MatchTool.toolPar.RunPar.contrast = (int)tkb_contrast.Value;
        }

        private void numericUpDown2_OnValueChanged(object sender, EventArgs e)
        {
            MatchTool.toolPar.RunPar.startAngle = (int)numericUpDown2.Value;
        }

        private void numericUpDown1_OnValueChanged(object sender, EventArgs e)
        {
            MatchTool.toolPar.RunPar.angleRange = (int)numericUpDown1.Value;
        }

        private void numericUpDown16_OnValueChanged(object sender, EventArgs e)
        {
            MatchTool.toolPar.RunPar.minScale = (double)numericUpDown16.Value;
        }

        private void numericUpDown18_OnValueChanged(object sender, EventArgs e)
        {
            MatchTool.toolPar.RunPar.maxScale = (double)numericUpDown18.Value;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            MatchTool.toolPar.RunPar.polarity = comboBox5.Text;
        }

        private void lxcButton1_Click(object sender, EventArgs e)
        {
            MatchTool.toolPar.RunPar.StandardImage = MatchTool.toolPar.InputPar.InPutImage;
            MatchTool.ShowStandardImage();
            vroiSet1.mROI = MatchTool.toolPar.RunPar.TemplateRegion;
            button4.Enabled = MatchTool.toolPar.RunPar.StandardImage != null && MatchTool.toolPar.RunPar.StandardImage.IsInitialized() && MatchTool.toolPar.RunPar.TemplateRegion != null;
            lxcButton3.Enabled = button4.Enabled;
            lxcButton2.Enabled = button4.Enabled;
            if (MatchTool.toolPar.RunPar.matchMode == MatchMode.BasedShape)
            {

                MatchTool.toolPar.RunPar.ShapeModel = null;

            }
            else if (MatchTool.toolPar.RunPar.matchMode == MatchMode.BasedGray)
            {
                MatchTool.toolPar.RunPar.NccModel = null;
            }
        }

        private void Win_MatchTool_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                
                if (MatchTool.toolPar.RunPar.StandardImage != null && MatchTool.toolPar.RunPar.StandardImage.IsInitialized())
                {
                    MatchTool.ShowStandardImage();
                    lxcButton2.Enabled = true;
                    lxcButton3.Enabled = true;
                    if (MatchTool.toolPar.RunPar.isTrained)
                    {
                        Win_MatchTool.Instance.button4.Text = "重新学习";
                        MatchTool.ShowTemplate();
                    }
                    else
                    {
                        Win_MatchTool.Instance.button4.Text = "训练";
                        imgView1.hv_window.ClearWindow();
                    }
                }
                else
                {
                    lxcButton2.Enabled=false;
                    lxcButton3.Enabled = false;
                    Win_MatchTool.Instance.button4.Text = "训练";
                    PImageWin.Image = MatchTool.toolPar.InputPar.InPutImage;
                    imgView1.hv_window.ClearWindow();
                }
                tabControl1.SelectedTab = tabPage1;
                comboBox2.SelectedIndex = (int)MatchTool.toolPar.RunPar.matchMode;//匹配模式
                vroiSet1.mROI = MatchTool.toolPar.RunPar.TemplateRegion;
                numericUpDown3.Value = (decimal)MatchTool.toolPar.RunPar.minScore;
                numericUpDown6.Value = (decimal)MatchTool.toolPar.RunPar.matchNum;
                lxcNumEdit1.Value = (decimal)MatchTool.toolPar.RunPar.angleStep;
                tkb_contrast.Value = (decimal)MatchTool.toolPar.RunPar.contrast;
                ckb_autoContrast.Checked= MatchTool.toolPar.RunPar.autoContrast;
                numericUpDown2.Value = (decimal)MatchTool.toolPar.RunPar.startAngle;
                numericUpDown1.Value = (decimal)MatchTool.toolPar.RunPar.angleRange;
                numericUpDown16.Value = (decimal)MatchTool.toolPar.RunPar.minScale;
                numericUpDown18.Value = (decimal)MatchTool.toolPar.RunPar.maxScale;
                comboBox5.Text = MatchTool.toolPar.RunPar.polarity;

                checkBox1.Checked = !MatchTool.toolPar.RunPar.showTemplate;
            
                lxcCheckBox3.Checked= !MatchTool.toolPar.RunPar.showIndex;
           
                lxcCheckBox4.Checked= !MatchTool.toolPar.RunPar.showCross;
           
                lxcCheckBox1.Checked= !MatchTool.toolPar.RunPar.showFeature;
            
                lxcCheckBox2.Checked= !MatchTool.toolPar.RunPar.showSearchRegion;
                Win_MatchTool.Instance.dgv_matchResult.Rows.Clear();
                for (int i = 0; i < MatchTool.toolPar.Results.FindCount; i++)
                {
                    int index = Win_MatchTool.Instance.dgv_matchResult.Rows.Add();
                    dgv_matchResult.Rows[index].Cells[0].Value = i + 1;
                    dgv_matchResult.Rows[index].Cells[1].Value = Math.Round((double)MatchTool.toolPar.Results.ResultsList[i].Socre, 3);
                    dgv_matchResult.Rows[index].Cells[2].Value = Math.Round((double)MatchTool.toolPar.Results.ResultsList[i].Col, 3);
                    dgv_matchResult.Rows[index].Cells[3].Value = Math.Round((double)MatchTool.toolPar.Results.ResultsList[i].Row, 3);
                    dgv_matchResult.Rows[index].Cells[4].Value = Math.Round((double)MatchTool.toolPar.Results.ResultsList[i].Angle * 180 / Math.PI, 3);
                }
                Application.DoEvents();

                //if (shapeMatchTool.isTrained)
                //{
                //    if (shapeMatchTool.standardImage != null && shapeMatchTool.standardImage.IsInitialized())
                //    {
                //        shapeMatchTool.ShowTemplate();
                //    }
                //    else
                //    {
                //        PImageWin.Image = shapeMatchTool.toolPar.InputPar.图像;
                //        shapeMatchTool.CreateAndShowTemplate();
                //    }



                //}
                //else
                //{

                //}
                //如果有基准图像，显示基准图，没有基准图显示输入图像

                //加载模板图像

                //加载模板参数


                button4.Enabled = MatchTool.toolPar.RunPar.StandardImage != null && MatchTool.toolPar.RunPar.StandardImage.IsInitialized() && MatchTool.toolPar.RunPar.TemplateRegion !=null;
                
               
            }
        }

        private void lxcButton2_Click(object sender, EventArgs e)
        {
            if (MatchTool.toolPar.RunPar.StandardImage != null && MatchTool.toolPar.RunPar.StandardImage.IsInitialized())
            {
                MatchTool.ShowStandardImage();
                vroiSet1.mROI = MatchTool.toolPar.RunPar.TemplateRegion;
                button4.Enabled = MatchTool.toolPar.RunPar.TemplateRegion != null;
                lxcButton3.Enabled = button4.Enabled;
            }
        }

        private void ckb_autoContrast_Click(object sender, EventArgs e)
        {

            MatchTool.toolPar.RunPar.autoContrast = ckb_autoContrast.Checked;
            
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            MatchTool.toolPar.RunPar.showTemplate = !checkBox1.Checked;
        }

        private void lxcCheckBox3_Click(object sender, EventArgs e)
        {
            MatchTool.toolPar.RunPar.showIndex = !lxcCheckBox3.Checked;
        }

        private void lxcCheckBox4_Click(object sender, EventArgs e)
        {
            MatchTool.toolPar.RunPar.showCross = !lxcCheckBox4.Checked;
        }

        private void lxcCheckBox1_Click(object sender, EventArgs e)
        {
            MatchTool.toolPar.RunPar.showFeature = !lxcCheckBox1.Checked;
        }

        private void lxcCheckBox2_Click(object sender, EventArgs e)
        {
            MatchTool.toolPar.RunPar.showSearchRegion = !lxcCheckBox2.Checked;
        }

        private void lxcButton3_Click(object sender, EventArgs e)
        {
            HMaskEdit HE = new HMaskEdit(MatchTool.toolPar.RunPar.StandardImage.CopyImage(), MatchTool.MaskModel_region,
                MatchTool.toolPar.RunPar.TemplateRegion.getRegion());
            HE.ShowDialog();
            //PImageWin.displayHRegion(HE.OutRegion, "firebrick");
            MatchTool.MaskModel_region=HE.OutRegion;
        }

        private void lxcButton4_Click(object sender, EventArgs e)
        {
            HMaskEdit HE = new HMaskEdit(MatchTool.toolPar.RunPar.StandardImage.CopyImage(), MatchTool.MaskFind_region);
            HE.ShowDialog();
            //PImageWin.displayHRegion(HE.OutRegion, "firebrick");
            MatchTool.MaskFind_region = HE.OutRegion;
        }
        /// <summary>
        /// 单击结果dgv控件，查看每一个匹配结果
        /// </summary>
        internal void ClickResultDgv(DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                PImageWin.Image = (MatchTool.toolPar.InputPar.InPutImage);
                int rowIndex = e.RowIndex;
                string index = dgv_matchResult.Rows[rowIndex].Cells[0].Value.ToString();
                double row = Convert.ToDouble(dgv_matchResult.Rows[rowIndex].Cells[3].Value);
                double col = Convert.ToDouble(dgv_matchResult.Rows[rowIndex].Cells[2].Value);
                double angle = Convert.ToDouble(dgv_matchResult.Rows[rowIndex].Cells[4].Value);


                //显示区域
                HHomMat2D homMat2D=new HHomMat2D();
                homMat2D.HomMat2dIdentity();
                HTuple area1, row1, column1;
                area1=MatchTool.toolPar.RunPar.TemplateRegion.getRegion().AreaCenter( out row1, out column1);
                homMat2D= homMat2D.HomMat2dTranslate((HTuple)(-row1), (HTuple)(-column1));
                homMat2D= homMat2D.HomMat2dRotate(angle, 0, 0);
                HRegion rectangle1AfterTrans;
                homMat2D= homMat2D.HomMat2dTranslate(row, col);
                rectangle1AfterTrans=MatchTool.toolPar.RunPar.TemplateRegion.getRegion().AffineTransRegion( homMat2D, "nearest_neighbor");
                PImageWin.displayHRegion(rectangle1AfterTrans, "green","margin");

                //显示中心十字架
                HObject cross;
                HOperatorSet.GenCrossContourXld(out cross, row, col, 20, angle);
                PImageWin.displayHRegion(cross, "blue");
                HXLDCont contour = new HXLDCont();
                //显示特征
                if (MatchTool.toolPar.RunPar.matchMode == MatchMode.BasedShape)
                {
                    contour = MatchTool.toolPar.RunPar.ShapeModel.GetShapeModelContours(1);

                    homMat2D = new HHomMat2D();
                    homMat2D.HomMat2dIdentity();
                    homMat2D.VectorAngleToRigid(0, 0, 0, row, col, angle);
                    contour = contour.AffineTransContourXld(homMat2D);
                    PImageWin.displayHRegion(contour, "orange");
                }
        
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void dgv_matchResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClickResultDgv(e);
        }

        private void Pbtn_help_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\HelpDoc\\模板匹配工具.pdf");
            }
            catch
            {
                Win_MessageBox.Instance.MessageBoxShow("\r\n工具暂无工具使用说明文档！", TipType.Warn);
            }
        }
    }
}
