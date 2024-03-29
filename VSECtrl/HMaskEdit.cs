using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Windows.Forms;

namespace VControls
{
    public partial class HMaskEdit : Form
    {
        private HImage image = new HImage();//图片
        public HRegion OutRegion = new HRegion();
        private HRegion brush_region = new HRegion();//笔刷
        private HRegion final_region = new HRegion();//需要获得的区域
        int hv_Button = 0;
        double hv_Row = 0, hv_Column = 0;
        HImage ho_Image;
        HRegion brush_region_affine;
        double areaBrush, rowBrush, columnBrush;
        HHomMat2D homMat2D=new HHomMat2D();
        string actionType = "draw";
        HRegion ExpTmpOutVar_0;
        HRegion tempRegion=new HRegion();
        HTuple ih;
        HTuple iw;
        HRegion WorkRegion = new HRegion();
        bool IsSafe = false;
        public HMaskEdit(HImage image,HRegion MaskRegion,HRegion WorkRegion=null)
        {
            this.image = image;
            image.GetImageSize(out iw,out ih);
           

            InitializeComponent();
            this.WorkRegion = WorkRegion;
           
            t = new System.Threading.Thread(GetPos);
            t.Start();
            this.TopMost = true;
            this.WindowState= FormWindowState.Maximized;
            if (MaskRegion!=null&&MaskRegion.IsInitialized())
            {
                final_region = MaskRegion;
                tempRegion = new HRegion(MaskRegion);
              //  hWindow_Final1.displayHRegion(final_region, "firebrick");
            }
            try
            {
                hWindow_Final1.Image = image;
                if (WorkRegion != null)
                {
                    HRegion g = new HRegion();
                    g.GenRectangle1(0, 0, ih, iw);
                    IsSafe = true;
                    hWindow_Final1.displayHRegion(g.Difference(WorkRegion), "orange");
                }
                笔刷_Click(null, null);
               

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //hWindow_Final1.DispObj(ho_Image);
            toolStripButton5.Checked = true;
            toolStripButton6.Checked = false;
            actionType = "";

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            toolStripButton5.Checked = false;
            toolStripButton6.Checked = true;
            actionType = "draw";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ToolStripButton tb = sender as ToolStripButton;
            toolStripButton1.Checked = false;
            toolStripButton2.Checked = false;
            toolStripButton3.Checked = false;
            toolStripButton4.Checked = false;
            switch (tb.ToolTipText)
            {
                case "最小笔划":
                    CreateBrush(2);
                    toolStripButton1.Checked = true;
                    break;
                case "小笔划":
                    CreateBrush(5);
                    toolStripButton2.Checked = true;
                    break;
                case "大笔划":
                    CreateBrush(10);
                    toolStripButton3.Checked = true;
                    break;
                case "最大笔划":
                    CreateBrush(20);
                    toolStripButton4.Checked = true;
                    break;
                default:
                    break;
            }
        }

        private void toolStripDropDownButton1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "矩形":
                    toolStripDropDownButton1.Image = 矩形.Image;
                    break;
                case "仿射矩形":
                    toolStripDropDownButton1.Image = 仿射矩形.Image;
                    break;
                case "椭圆":
                    toolStripDropDownButton1.Image = 椭圆.Image;
                    break;
                case "笔刷":
                    toolStripDropDownButton1.Image = 笔刷.Image;
                    break;
            }
        }

        

        private void HMaskEdit_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void HMaskEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            flag=false;
            if (!CloseOK)
            {
                OutRegion = tempRegion;
            }
        }

       

       

        private void CreateBrush(double BrushSize)
        {
            HRegion ho_temp_brush = new HRegion();
            try
            {
                ho_temp_brush.GenCircle(10, 10, BrushSize);
                brush_region.Dispose();
                brush_region = ho_temp_brush;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void 笔刷_Click(object sender, EventArgs e)
        {
            toolStripButton8.Visible = false;
            toolStripSeparator3.Visible = false;
            toolStripSeparator2.Visible = true;
            toolStripButton1.Visible = true;
            toolStripButton2.Visible = true;
            toolStripButton3.Visible = true;
            toolStripButton4.Visible = true;
            toolStripLabel1.Visible = true;
            StartTimer = true;
            toolStripDropDownButton1.Text = "笔刷";
            hWindow_Final1.viewWindow.RoiController.ROIList.Clear();
            hWindow_Final1.viewWindow._hWndControl.repaint();
            CreateBrush(10);
        }
        private void 矩形_Click(object sender, EventArgs e)
        {
            hWindow_Final1.EditModel = Lxc.VisionPlus.ImageView.ImgView.winMode.Menu;
            double d = (ih > iw ? iw / 4 : ih / 4);
            double x1 = iw / 2 - d;
            double y1 = ih / 2 - d;
            double x2 = iw / 2 + d;
            double y2 = ih / 2 + d;
            toolStripButton8.Visible = true;
            toolStripSeparator3.Visible = true;
            toolStripSeparator2.Visible = false;
            toolStripButton1.Visible = false;
            toolStripButton2.Visible = false;
            toolStripButton3.Visible = false;
            toolStripButton4.Visible = false;
            toolStripLabel1.Visible = false;
            StartTimer = false;
            toolStripDropDownButton1.Text = "矩形";
            ROIRectangle1 roi = new ROIRectangle1();
            hWindow_Final1.viewWindow.RoiController.ROIList.Clear();
            hWindow_Final1.viewWindow.RoiController.ROIList.Add(roi);
            roi.UpdateROI(y1, x1, y2, x2);
            hWindow_Final1.viewWindow._hWndControl.repaint();
        }

        private void 仿射矩形_Click(object sender, EventArgs e)
        {
            double d = (ih > iw ? iw / 4 : ih / 4);
            double x1 = iw / 2 ;
            double y1 = ih / 2 ;
            double x2 = iw / 2 ;
            double y2 = ih / 2 ;
            toolStripButton8.Visible = true;
            toolStripSeparator3.Visible = true;
            toolStripSeparator2.Visible = false;
            toolStripButton1.Visible = false;
            toolStripButton2.Visible = false;
            toolStripButton3.Visible = false;
            toolStripButton4.Visible = false;
            toolStripLabel1.Visible = false;
            StartTimer = false;
            toolStripDropDownButton1.Text = "仿射矩形";
            hWindow_Final1.viewWindow.RoiController.ROIList.Clear();
            ROIRectangle2 roi = new ROIRectangle2();
            hWindow_Final1.viewWindow.RoiController.ROIList.Add(roi);
            roi.UpdateROI(x1, y1, 0, x2, y2);
            hWindow_Final1.viewWindow._hWndControl.repaint();
        }

        private void 椭圆_Click(object sender, EventArgs e)
        {
            toolStripButton8.Visible = true;
            toolStripSeparator3.Visible = true;
            toolStripSeparator2.Visible = false;
            toolStripButton1.Visible = false;
            toolStripButton2.Visible = false;
            toolStripButton3.Visible = false;
            toolStripButton4.Visible = false;
            toolStripLabel1.Visible = false;
            StartTimer = false;
            toolStripDropDownButton1.Text = "椭圆";
            ROIEllipse roi = new ROIEllipse();
            hWindow_Final1.viewWindow.RoiController.ROIList.Clear();
            hWindow_Final1.viewWindow.RoiController.ROIList.Add(roi);
            roi.UpdateROI(iw / 2.0, ih / 2.0, ih > iw ? iw / 4.0 : ih / 4.0, ih > iw ? iw / 4.0 : ih / 4.0, 0);
            hWindow_Final1.viewWindow._hWndControl.repaint();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (actionType== "draw")
            {
                final_region = final_region.Union2(((ROI)hWindow_Final1.viewWindow.RoiController.ROIList[0]).getRegion());
            }
            else
            {
                final_region = final_region.Difference(((ROI)hWindow_Final1.viewWindow.RoiController.ROIList[0]).getRegion());
            }
            if (IsSafe)
            {
                final_region = final_region.Intersection(WorkRegion);
            }

            hWindow_Final1.viewWindow._hWndControl.clearHRegionList();
            if (WorkRegion != null)
            {
                HRegion g = new HRegion();
                g.GenRectangle1(0, 0, ih, iw);
                IsSafe = true;
                hWindow_Final1.displayHRegion(g.Difference(WorkRegion), "orange");
            }
            hWindow_Final1.displayHRegion(final_region, "firebrick");
            hWindow_Final1.viewWindow._hWndControl.repaint();
        }

        private void hWindow_Final1_MouseDown(object sender, MouseEventArgs e)
        {
            if (StartTimer)
            {
                IsStart = true;
                hWindow_Final1.EditModel = Lxc.VisionPlus.ImageView.ImgView.winMode.None;
            }
            
        }

        System.Threading.Thread t;
        bool flag = true;
        bool IsStart=false;
        bool StartTimer=false;
        bool CloseOK = false;
  
        private void vButton1_Clicked()
        {
            flag = false;
            OutRegion = final_region;
            CloseOK = true;
            this.Close();
        }

        private void vButton2_Clicked()
        {
         
            CloseOK = false;
            this.Close();
        }



        private void hWindow_Final1_MouseUp(object sender, MouseEventArgs e)
        {
            IsStart=false;
            hWindow_Final1.EditModel = Lxc.VisionPlus.ImageView.ImgView.winMode.MoveZoom;
        }

        public void GetPos()
        {
            while (flag)
            {
                System.Threading.Thread.Sleep(2);
                Application.DoEvents();
                if (IsStart)
                {
                try
                {
                        
                        if (!hWindow_Final1.GetMP(out hv_Row, out hv_Column, out hv_Button))
                    {
                        //hWindow_Final1.EditModel = Lxc.VisionPlus.ImageView.ImgView.winMode.MoveZoom;
                        continue;
                    }
                      //  System.Diagnostics.Debug.WriteLine(hv_Button);
                        //Application.DoEvents();
                    }
#pragma warning disable CS0168 // 声明了变量“eX”，但从未使用过
                catch (HalconException eX)
#pragma warning restore CS0168 // 声明了变量“eX”，但从未使用过
                {
                    hv_Button = 0;
                        continue;
                    }
                    if (final_region.IsInitialized())
                    {
                        hWindow_Final1.viewWindow._hWndControl.clearHRegionList();
                        if (WorkRegion != null)
                        {
                            HRegion g = new HRegion();
                            g.GenRectangle1(0, 0, ih, iw);
                            IsSafe = true;
                            hWindow_Final1.displayHRegion(g.Difference(WorkRegion), "orange");
                        }
                        hWindow_Final1.displayHRegion(final_region, "firebrick");
                    }

                    if (hv_Row >= 0 && hv_Column >= 0)
                    {
                        homMat2D.VectorAngleToRigid(rowBrush, columnBrush, 0, hv_Row, hv_Column, 0);
                        brush_region_affine.Dispose();
                        brush_region_affine = brush_region.AffineTransRegion(homMat2D, "nearest_neighbor");

                        //1为鼠标左键
                        if (hv_Button == 1)
                        {
                            switch (actionType)
                            {
                                case "draw":
                                    {
                                        if (final_region.IsInitialized())
                                        {

                                            ExpTmpOutVar_0 = final_region.Union2(brush_region_affine);
                                            final_region.Dispose();
                                            if (IsSafe)
                                            {
                                                final_region = ExpTmpOutVar_0.Intersection(WorkRegion);
                                            }
                                            else
                                            {
                                                final_region = ExpTmpOutVar_0;
                                            }
                                            

                                        }
                                        else
                                        {
                                            final_region = new HRegion(brush_region_affine);
                                        }

                                    }
                                    break;
                                default:
                                    if (!final_region.IsInitialized())
                                    {
                                        break;
                                    }
                                    ExpTmpOutVar_0 = final_region.Difference(brush_region_affine);
                                    final_region.Dispose();
                                    if (IsSafe)
                                    {
                                        final_region = ExpTmpOutVar_0.Intersection(WorkRegion);
                                    }
                                    else
                                    {
                                        final_region = ExpTmpOutVar_0;
                                    }
                                    break;


                            }

                        }
                        
                    }
                    

                }
            }
        }
      
        private void Form1_Load(object sender, EventArgs e)
        {
            hWindow_Final1.hv_window.SetColor("firebrick");

            brush_region_affine = new HRegion();
            ho_Image = new HImage(image);
            areaBrush = brush_region.AreaCenter(out rowBrush, out columnBrush);
            //显示

            if (final_region != null && final_region.IsInitialized())
            {
                if (WorkRegion != null)
                {
                    HRegion g = new HRegion();
                    g.GenRectangle1(0, 0, ih, iw);
                    IsSafe = true;
                    hWindow_Final1.displayHRegion(g.Difference(WorkRegion), "orange");
                }
                hWindow_Final1.displayHRegion(final_region, "firebrick");
            }

            //hWindow_Final1.MaskMode = true;
        }
    }
}
