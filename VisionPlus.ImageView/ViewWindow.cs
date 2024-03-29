using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System.Collections.Generic;

namespace Lxc.VisionPlus.ImageView
{
    public class ViewWindow : Model.IViewWindow
    {
        public   Model.HWndCtrl _hWndControl;

        private Model.ROIController _roiController;


        public ViewWindow(System.Windows.Forms.PictureBox window)
        {
            this._hWndControl = new Model.HWndCtrl(window);
            this._roiController = new Model.ROIController();
            this._hWndControl.setROIController(this._roiController);
            this._hWndControl.setViewState(Model.HWndCtrl.MODE_VIEW_NONE);
        }
        public Model.ROIController RoiController
        {
            get
            {
                return _roiController;
            }

            set
            {
                _roiController = value;
                this._hWndControl.setROIController(_roiController);
            }
        }
        //清空所有显示内容

        public void ClearWindow(bool bClearImage = true)
        {
            this._hWndControl.clearList(bClearImage);
            this._hWndControl.clearHRegionList();
            this._hWndControl.roiManager.reset();
            this._hWndControl.repaint();
        }
        public void displayImage(HObject img)
        {
            //添加背景图片
            this._hWndControl.addImageShow(img);
            //清空roi容器,不让roi显示
            this._roiController.reset();
            //显示图片
           this._roiController.resetWindowImage();


            //this._hWndControl.resetWindow();
           // this._hWndControl.resetAll();
            //this._hWndControl.repaint();
        }
        public void notDisplayRoi()
        {

            this._roiController.reset();
            //显示图片
            this._roiController.resetWindowImage();

        }
        //获取当前窗口显示的roi数量
        public int getRoiCount()
        {
            return _roiController.ROIList.Count;
        }
        //是否开启缩放事件
        public void setDrawModel(bool flag)
        {
            _hWndControl.isStaticWnd = flag;
        }
        //是否开启编辑事件
        public void setEditModel(bool flag)
        {
            _roiController.EditModel = flag;
          //  _hWndControl.drawModel = flag;
        }
        public void resetWindowImage(bool ReOpenWiindow = false)
        {
            this._hWndControl.resetWindow(ReOpenWiindow);
            this._roiController.resetWindowImage();
        }
        public void zoomImageByPercent(double percent)
        {
            _hWndControl.zoomImage(percent);
           
        }
        public void mouseleave()
        {
            _hWndControl.raiseMouseup();
        }

        public void zoomWindowImage()
        {
            this._roiController.zoomWindowImage();
        }

        public void moveWindowImage()
        {
            this._roiController.moveWindowImage();
        }

        public void noneWindowImage()
        {
            this._roiController.noneWindowImage();
        }

        public void genRect1(double row1, double col1, double row2, double col2, ref List<Model.ROI> rois)
        {
            this._roiController.genRect1(row1, col1, row2, col2, ref rois);
        }
        public void genNurbs(HTuple rows, HTuple cols, ref List<Model.ROI> rois)
        {
            this._roiController.genNurbs( rows,cols,ref rois);
        }
        public void genInitRect1( ref List<Model.ROI> rois)
        {
            this._roiController.genInitRect1(_roiController.viewController .imageHeight  , ref rois);
        }

        public void genRect2(double row, double col, double phi, double length1, double length2, ref List<Model.ROI> rois)
        {
            this._roiController.genRect2(row, col, phi, length1, length2, ref rois);
        }
        public void genInitRect2(ref List<Model.ROI> rois)
        {
            this._roiController.genInitRect2(_roiController.viewController.imageHeight, ref rois);
        }
        public void genCircle(double row, double col, double radius, ref List<Model.ROI> rois)
        {
            this._roiController.genCircle(row, col, radius, ref rois);
        }
        public void genCircularArc(double row, double col, double radius, double startPhi, double extentPhi, string direct,ref List<Model.ROI> rois)
        {
            this._roiController.genCircularArc(row, col, radius,  startPhi, extentPhi,direct,ref rois);
        }
        public void genLine(double beginRow, double beginCol, double endRow, double endCol, ref List<Model.ROI> rois)
        {
            this._roiController.genLine(beginRow, beginCol, endRow, endCol, ref rois);
        }

        public List<double> smallestActiveROI(out string name, out int index)
        {
            List<double> resual = this._roiController.smallestActiveROI(out name,out index);
            return resual;
        }
        
        public Model.ROI smallestActiveROI(out List<double> data, out int index)
        {
            Model.ROI roi = this._roiController.smallestActiveROI(out data, out index);
            return roi;
        }

        public void selectROI(int index)
        {
            this._roiController.selectROI(index);
        }

        public void selectROI( List<Model.ROI> rois, int index)
        {
            //this._roiController.selectROI(index);
            if ((rois.Count > index)&&(index>=0))
            {
                this._hWndControl.resetAll();
                this._hWndControl.repaint();

                HTuple m_roiData = null;
                m_roiData = rois[index].getModelData();

                switch (rois[index].Type)
                {
                    case ROI.ROIType.ROIRectangle1:

                        if (m_roiData != null)
                        {
                            this._roiController.displayRect1(rois[index].Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D);
                        }
                        break;
                    case ROI.ROIType.ROIRectangle2:

                        if (m_roiData != null)
                        {
                            this._roiController.displayRect2(rois[index].Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D, m_roiData[4].D);
                        }
                        break;
                    case ROI.ROIType.ROICircle:

                        if (m_roiData != null)
                        {
                            this._roiController.displayCircle(rois[index].Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D);
                        }
                        break;
                    case ROI.ROIType.ROICircularArc:

                        if (m_roiData != null)
                        {
                            this._roiController.displayCircularArc(rois[index].Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D, m_roiData[4].D);
                        }
                        break;
                    case ROI.ROIType.ROILine:

                        if (m_roiData != null)
                        {
                            this._roiController.displayLine(rois[index].Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void displayROI(List<Model.ROI> rois)
        {
            if (rois == null)
            {
                return;
            }
            //this._hWndControl.resetAll();
            //this._hWndControl.repaint();

            foreach (var roi in rois)
            {
                

                switch (roi.Type)
                {
                    case ROI.ROIType.ROIRectangle1:
                               HTuple m_roiData = null;
                m_roiData = roi.getModelData();
                        if (m_roiData != null)
                        {
                            this._roiController.displayRect1(roi.Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D);
                        }
                        break;
                    case ROI.ROIType.ROIRectangle2:
                        m_roiData = null;  
                m_roiData = roi.getModelData();
                        if (m_roiData != null)
                        {
                            this._roiController.displayRect2(roi.Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D, m_roiData[4].D);
                     
                        }
                        break;
                    case ROI.ROIType.ROICircle:
                        m_roiData = roi.getModelData();
                        if (m_roiData != null)
                        {
                            this._roiController.displayCircle(roi.Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D);
                        }
                        break;
                    case ROI.ROIType.ROILine:
                        m_roiData = roi.getModelData();
                        if (m_roiData != null)
                        {
                            this._roiController.displayLine(roi.Color, m_roiData[0].D, m_roiData[1].D, m_roiData[2].D, m_roiData[3].D);
                        }
                        break;
                    case ROI.ROIType.ROINurbs:
                        HTuple rows, cols;
                        roi.getModelData(out rows ,out cols );
                        if (rows  != null)
                        {
                            this._roiController.displayNurbs(roi.Color, rows, cols);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void removeActiveROI(ref List<Model.ROI> rois)
        {
            this._roiController.removeActiveROI(ref rois);
        }

        public void setActiveRoi(int index)
        {
            this._roiController.activeROIidx = index;
        }
        public static void SetDisplayFont(HWindow hv_WindowHandle, HTuple hv_Size,HTuple hv_Bold, HTuple hv_Slant)
        {
            HTuple hv_OS = null, hv_BufferWindowHandle = new HTuple();
            HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_Scale = new HTuple(), hv_Exception = new HTuple();
            HTuple hv_SubFamily = new HTuple(), hv_Fonts = new HTuple();
            HTuple hv_SystemFonts = new HTuple(), hv_Guess = new HTuple();
            HTuple hv_I = new HTuple(), hv_Index = new HTuple(), hv_AllowedFontSizes = new HTuple();
            HTuple hv_Distances = new HTuple(), hv_Indices = new HTuple();
            HTuple hv_FontSelRegexp = new HTuple(), hv_FontsCourier = new HTuple();
            HTuple hv_Bold_COPY_INP_TMP = hv_Bold.Clone();
            HTuple hv_Size_COPY_INP_TMP = hv_Size.Clone();
            HTuple hv_Slant_COPY_INP_TMP = hv_Slant.Clone();


            HOperatorSet.GetSystem("operating_system", out hv_OS);

            if ((int)((new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(-1)))) != 0)
            {
                hv_Size_COPY_INP_TMP = 16;
            }
            if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
            {
              
              
                if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
                {
                    hv_Bold_COPY_INP_TMP = "Bold";
                }
                else if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("false"))) != 0)
                {
                    hv_Bold_COPY_INP_TMP = "Normal";
                }
                else
                {
                    hv_Exception = "Wrong value of control parameter Bold";
                    throw new HalconException(hv_Exception);
                }
               
                try
                {
                    //HTuple FONTS = new HTuple();
                    //HOperatorSet.QueryFont(hv_BufferWindowHandle,out FONTS);
                    hv_WindowHandle.SetFont(("Arial" + "-" + hv_Bold_COPY_INP_TMP+"-" + hv_Size_COPY_INP_TMP));
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    //throw (Exception)
                }
            }
            return;

        }
        public static void DisMsg(HWindow hv_WindowHandle, HTuple hv_String, ImgView.CoordSystem CoordSystem, HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {

            HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
            HTuple hv_Row1Part = null, hv_Column1Part = null, hv_Row2Part = null;
            HTuple hv_Column2Part = null,  hv_MaxAscent = null;
            int hv_WidthWin = 0, hv_HeightWin = 0, hv_RowWin = 0, hv_ColumnWin = 0;
            HTuple hv_MaxDescent = null, hv_MaxWidth = null, hv_MaxHeight = null;
            HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRow = new HTuple();
            HTuple hv_FactorColumn = new HTuple(), hv_UseShadow = null;
            HTuple hv_ShadowColor = null, hv_Exception = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
            HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = new HTuple();
            HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
            HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
            HTuple hv_CurrentColor = new HTuple();
            HTuple hv_Box_COPY_INP_TMP = hv_Box.Clone();
            HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
            HTuple hv_String_COPY_INP_TMP = hv_String.Clone();
            HTuple hv_CoordSystem = (CoordSystem == ImgView.CoordSystem.window) ? "window" : "image";


            hv_WindowHandle.GetRgb(out hv_Red, out hv_Green, out hv_Blue);
            hv_WindowHandle.GetPart(out hv_Row1Part, out hv_Column1Part, out hv_Row2Part,
                out hv_Column2Part);
            hv_WindowHandle.GetWindowExtents(out hv_RowWin, out hv_ColumnWin,out hv_WidthWin, out hv_HeightWin);
            hv_WindowHandle.SetPart( 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            //
            //default settings
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
            {
                hv_Color_COPY_INP_TMP = "";
            }
            //
            hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
            //
            //Estimate extentions of text depending on font size.
            hv_MaxAscent= hv_WindowHandle.GetFontExtents(out hv_MaxDescent,
                out hv_MaxWidth, out hv_MaxHeight);
            if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
            {
                hv_R1 = hv_Row_COPY_INP_TMP.Clone();
                hv_C1 = hv_Column_COPY_INP_TMP.Clone();
            }
            else
            {
                //Transform image to window coordinates
                hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
                hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
                hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
                hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
            }
            //
            //Display text box depending on text size
            hv_UseShadow = 1;
            hv_ShadowColor = "gray";
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleEqual("true"))) != 0)
            {
                if (hv_Box_COPY_INP_TMP == null)
                    hv_Box_COPY_INP_TMP = new HTuple();
                hv_Box_COPY_INP_TMP[0] = "#fce9d4";
                hv_ShadowColor = "#f28d26";
            }
            if ((int)(new HTuple((new HTuple(hv_Box_COPY_INP_TMP.TupleLength())).TupleGreater(
                1))) != 0)
            {
                if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual("true"))) != 0)
                {
                    //Use default ShadowColor set above
                }
                else if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual(
                    "false"))) != 0)
                {
                    hv_UseShadow = 0;
                }
                else
                {
                    hv_ShadowColor = hv_Box_COPY_INP_TMP[1];
                    //Valid color?
                    try
                    {
                        hv_WindowHandle.SetColor(hv_Box_COPY_INP_TMP.TupleSelect(
                            1));
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        hv_Exception = "Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)";
                        throw new HalconException(hv_Exception);
                    }
                }
            }
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleNotEqual("false"))) != 0)
            {
                //Valid color?
                try
                {
                    hv_WindowHandle.SetColor(hv_Box_COPY_INP_TMP.TupleSelect(0));
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    hv_Exception = "Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)";
                    throw new HalconException(hv_Exception);
                }
                //Calculate box extents
                hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
                hv_Width = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    HOperatorSet.GetStringExtents(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                        hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                    hv_Width = hv_Width.TupleConcat(hv_W);
                }
                hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    ));
                hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                hv_R2 = hv_R1 + hv_FrameHeight;
                hv_C2 = hv_C1 + hv_FrameWidth;
                //Display rectangles
                hv_DrawMode=hv_WindowHandle.GetDraw();
                hv_WindowHandle.SetDraw( "fill");
                //Set shadow color
                hv_WindowHandle.SetColor(hv_ShadowColor);
                if ((int)(hv_UseShadow) != 0)
                {
                    hv_WindowHandle.DispRectangle1(hv_R1 + 1, hv_C1 + 1, hv_R2 + 1, hv_C2 + 1);
                }
                //Set box color
                hv_WindowHandle.SetColor( hv_Box_COPY_INP_TMP.TupleSelect(0));
                hv_WindowHandle.DispRectangle1( hv_R1, hv_C1, hv_R2, hv_C2);
                hv_WindowHandle.SetDraw( hv_DrawMode);
            }
            //Write text.
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                    )));
                if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                    "auto")))) != 0)
                {
                    hv_WindowHandle.SetColor( hv_CurrentColor);
                }
                else
                {
                    hv_WindowHandle.SetRgb( hv_Red, hv_Green, hv_Blue);
                }
                hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
                hv_WindowHandle.SetTposition( (int)(hv_Row_COPY_INP_TMP[0].D), (int)(hv_C1[0].D));
                hv_WindowHandle.WriteString( hv_String_COPY_INP_TMP.TupleSelect(
                    hv_Index));
            }
            //Reset changed window settings
            hv_WindowHandle.SetRgb( hv_Red, hv_Green, hv_Blue);
            hv_WindowHandle.SetPart( hv_Row1Part, hv_Column1Part, hv_Row2Part,
                hv_Column2Part);


        }
        public static void DisMsg(HWindow hv_WindowHandle, string Msg)
        {
            DisMsg(hv_WindowHandle, Msg, ImgView.CoordSystem.window);
        }
        public static void DisMsg(HWindow hv_WindowHandle, string Msg, ImgView.CoordSystem hv_CoordSystem)
        {
            DisMsg(hv_WindowHandle, Msg, hv_CoordSystem, 2, 2);
        }

        public static void DisMsg(HWindow hv_WindowHandle, HTuple Msg, ImgView.CoordSystem hv_CoordSystem, HTuple hv_Row, HTuple hv_Column)
        {
            DisMsg(hv_WindowHandle, Msg, hv_CoordSystem, hv_Row, hv_Column, "black");
        }
        public static void DisMsg(HWindow hv_WindowHandle, HTuple Msg, ImgView.CoordSystem hv_CoordSystem, HTuple hv_Row, HTuple hv_Column, HTuple Color)
        {
            DisMsg(hv_WindowHandle, Msg, hv_CoordSystem, hv_Row, hv_Column, Color, "false");
        }
    }
}
