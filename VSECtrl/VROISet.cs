using HalconDotNet;
using Lxc.VisionPlus.ImageView;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace VControls
{
    public partial class VROISet : UserControl
    {
        public VROISet()
        {
            InitializeComponent();
            vroiSel1.ROIChangedEvent += LxcROISel1_ROIChangedEvent;
        }
        public delegate void LxcROISetChangedEventHandler(object sender, LxcROISetChangedEventArgs e);
        public class LxcROISetChangedEventArgs : EventArgs
        {
            private ROI mROI;

            public ROI ROI
            {
                get
                {
                    return this.mROI;
                }
            }

            public LxcROISetChangedEventArgs(ROI roi)
            {
                mROI = roi;
            }

        }
        public event LxcROISetChangedEventHandler ROIChangedEvent;
        private ROI mRegion;
        public ROI mROI
        {
            get
            {
                return this.mRegion;
            }
            set
            {
                this.mRegion = value;
                LxcROILoadFromSubject(mRegion);
            }
        }
        public string Title
        {
            get { return lxcGroupBox1.Text; }
            set
            {
                lxcGroupBox1.Text = value;
                base.Invalidate();
            }
        }
        public bool ModeRoi = false;
        public void ROIFitImage()
        {
            ROISetImageHalf();
        }
        
        HWndCtrl _hWndControl;
        private ImgView h;
        public ImgView H
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                if (_hWndControl != null)
                {
                    _hWndControl.ROIChangedEvent -= RoiController_ROIChangedEvent;
                }
                h = value;

                h.viewWindow.RoiController.ROIList.Add(new ROIRectangle1());
                if (ModeRoi)
                {
                    h.viewWindow.RoiController.ROIList.Add(new ROIRectangle1());
                }
                _hWndControl = h.viewWindow._hWndControl;
                _hWndControl.ROIChangedEvent += RoiController_ROIChangedEvent;
            }
            get
            {
                return h;
            }
        }
        private void RoiController_ROIChangedEvent(object sender, EventArgs e)
        {
            OnROIChangedEvent();
        }
        private void LxcROISel1_ROIChangedEvent(object sender, VROISel.LxcROIChangedEventArgs e)
        {
            if (_hWndControl==null)
            {
                return;
            }
            double ih = _hWndControl.imageHeight;
            double iw = _hWndControl.imageWidth;
            double d = (ih > iw ? iw / 4 : ih / 4);
            panel_Rectangle1.Visible = false;
            panel_Rectangle2.Visible = false;
            panel_Circle.Visible = false;
            panel_Ellipse.Visible = false;
            UnLoadEvent();
            lxcButton1.Enabled = true;
            switch (e.ROI)
            {
                case "圆形":
                    panel_Circle.Visible = true;

                    //h.genCircle("blue", 70, 70, 50, ref a);
                    mRegion = new ROICircle();
                    ((ROICircle)mRegion).UpdateROI(iw / 2.0, ih / 2.0, ih > iw ? iw / 4.0 : ih / 4.0);
                    Circle_CenterX.Value = (Decimal)(iw / 2.0);
                    Circle_CenterY.Value = (Decimal)(ih / 2.0);
                    Circle_R.Value = (Decimal)(ih > iw ? iw / 4.0 : ih / 4.0);

                    break;
                case "椭圆":
                    panel_Ellipse.Visible = true;
                    mRegion = new ROIEllipse();
                    ((ROIEllipse)mRegion).UpdateROI(iw / 2.0, ih / 2.0, ih > iw ? iw / 4.0 : ih / 4.0, ih > iw ? iw / 4.0 : ih / 4.0, 0);
                    Ellipse_CenterX.Value = (Decimal)(iw / 2.0);
                    Ellipse_CenterY.Value = (Decimal)(ih / 2.0);
                    Ellipse_R1.Value = (Decimal)(ih > iw ? iw / 4.0 : ih / 4.0);
                    Ellipse_R2.Value = (Decimal)(ih > iw ? iw / 4.0 : ih / 4.0);
                    Ellipse_phi.Value = 0;

                    break;
                case "矩形":
                    panel_Rectangle1.Visible = true;
                    mRegion = new ROIRectangle1();
                    double x1 = iw / 2 - d;
                    double y1 = ih / 2 - d;
                    double x2 = iw / 2 + d;
                    double y2 = ih / 2 + d;
                    ((ROIRectangle1)mRegion).UpdateROI(y1, x1, y2, x2);
                    Rectangle1_LeftTopX.Value = (Decimal)x1;
                    Rectangle1_LeftTopY.Value = (Decimal)y1;
                    Rectangle1_RightBottomX.Value = (Decimal)x2;
                    Rectangle1_RightBottomY.Value = (Decimal)y2;

                    break;
                case "带角度矩形":
                    panel_Rectangle2.Visible = true;
                    mRegion = new ROIRectangle2();
                    ((ROIRectangle2)mRegion).UpdateROI(iw / 2.0, ih / 2.0, 0, iw / 2.0, ih / 2.0);
                    Rectangle2_CenterX.Value = (Decimal)(iw / 2.0);
                    Rectangle2_CenterY.Value = (Decimal)(ih / 2.0);
                    Rectangle2_LengthX.Value = (Decimal)(iw / 2.0);
                    Rectangle2_LengthY.Value = (Decimal)(ih / 2.0);
                    Rectangle2_phi.Value = 0;

                    break;
                case "整幅图像":
                    mRegion = new ROIImage();
    
                    ((ROIImage)mRegion).UpdateROI(0, 0, ih, iw);
                    lxcButton1.Enabled = false;
                    break;
                default:
                    mRegion = new ROI();
                    break;
            }
            if (h.viewWindow.RoiController.ROIList.Count == 0)
            {
                h.viewWindow.RoiController.ROIList.Add(new ROI());
                h.viewWindow.RoiController.ROIList.Add(new ROI());
                h.viewWindow.RoiController.ROIList.Add(new ROICoordinate());
            }
            
                if (ModeRoi)
                {
                    mRegion.IsModeROI = true;
                    h.viewWindow.RoiController.ROIList[1] = mRegion;
                if (((ROICoordinate)h.viewWindow.RoiController.ROIList[2]).Row==0)
                {
                    ((ROICoordinate)h.viewWindow.RoiController.ROIList[2]).Row = 70;
                    ((ROICoordinate)h.viewWindow.RoiController.ROIList[2]).Column = 70;
                }
                ;
                ((ROICoordinate)h.viewWindow.RoiController.ROIList[2]).UpdateROI(mRegion.Column, mRegion.Row);
            }
                else
                {
                    h.viewWindow.RoiController.ROIList[0] = mRegion;
                }

            

            // h.viewWindow._hWndControl.repaint();
            _hWndControl.repaint();
            OnROIChangedEvent();
        }
        private void LxcROILoadFromSubject(ROI mroi)
        {
            if (mroi == null)
            {
                return;
            }

            panel_Rectangle1.Visible = false;
            panel_Rectangle2.Visible = false;
            panel_Circle.Visible = false;
            panel_Ellipse.Visible = false;

            UnLoadEvent();
            vroiSel1.ROIChangedEvent -= LxcROISel1_ROIChangedEvent;
            lxcButton1.Enabled = true;
            switch (mroi.GetType().Name)
            {
                case "ROICircle":
                    ROICircle rc = mroi as ROICircle;
                    panel_Circle.Visible = true;
                    //  mRegion = new ROICircle(rc.Row, rc.Column, rc.Radius);
                    vroiSel1.SelectedIndex = 1;
                    break;
                case "ROIEllipse":
                    ROIEllipse re = mroi as ROIEllipse;
                    panel_Ellipse.Visible = true;
                    //  mRegion = new ROIEllipse(re.Row, re.Column, re.Phi, re.Radius1, re.Radius2);
                    vroiSel1.SelectedIndex = 2;

                    break;
                case "ROIRectangle1":
                    ROIRectangle1 rr = mroi as ROIRectangle1;
                    panel_Rectangle1.Visible = true;
                    //  mRegion = new ROIRectangle1(rr.Row1, rr.Column1, rr.Row2, rr.Column2);
                    vroiSel1.SelectedIndex = 3;

                    break;
                case "ROIRectangle2":
                    ROIRectangle2 rr2 = mroi as ROIRectangle2;
                    panel_Rectangle2.Visible = true;
                    // mRegion = new ROIRectangle2(rr2.Row, rr2.Column, rr2.Phi, rr2.Lenth1, rr2.Lenth2);
                    vroiSel1.SelectedIndex = 4;

                    break;
                default:
                    lxcButton1.Enabled = false;
                    vroiSel1.SelectedIndex = 0;
                    break;
            }
            vroiSel1.ROIChangedEvent += LxcROISel1_ROIChangedEvent;
            OnROIChangedEvent();
        }
        private void OnROIChangedEvent()
        {
            if (ROIChangedEvent != null && mRegion != null)
            {
                UnLoadEvent();
                if (h.viewWindow.RoiController.ROIList.Count == 0)
                {
                    h.viewWindow.RoiController.ROIList.Add(new ROI());
                    h.viewWindow.RoiController.ROIList.Add(new ROI());
                    h.viewWindow.RoiController.ROIList.Add(new ROICoordinate());
                }

                if (ModeRoi)
                {
                    mRegion.IsModeROI = true;
                    h.viewWindow.RoiController.ROIList[1] = mRegion;
                    if (((ROICoordinate)h.viewWindow.RoiController.ROIList[2]).Row == 0)
                    {
                        ((ROICoordinate)h.viewWindow.RoiController.ROIList[2]).Row = 70;
                        ((ROICoordinate)h.viewWindow.RoiController.ROIList[2]).Column = 70;
                    }
                ;
                    ((ROICoordinate)h.viewWindow.RoiController.ROIList[2]).UpdateROI(mRegion.Column, mRegion.Row);
                }
                else
                {
                    h.viewWindow.RoiController.ROIList[0] = mRegion;
                }

                switch (vroiSel1.SelectedValue)
                {
                    case "圆形":

                        Circle_CenterX.Value = (Decimal)((ROICircle)mRegion).Column;
                        Circle_CenterY.Value = (Decimal)((ROICircle)mRegion).Row;
                        Circle_R.Value = (Decimal)((ROICircle)mRegion).Radius;
                       
                       // ((ROICircle)mRegion).UpdateROI(((ROICircle)mRegion).Column, ((ROICircle)mRegion).Row,((ROICircle)mRegion).Radius);
                        break;
                    case "椭圆":
                        Ellipse_CenterX.Value = (Decimal)((ROIEllipse)mRegion).Column;
                        Ellipse_CenterY.Value = (Decimal)((ROIEllipse)mRegion).Row;
                        Ellipse_R1.Value = (Decimal)((ROIEllipse)mRegion).Radius1;
                        Ellipse_R2.Value = (Decimal)((ROIEllipse)mRegion).Radius2;
                        Ellipse_phi.Value = (Decimal)(((ROIEllipse)mRegion).Phi * 180.0 / Math.PI);
                        break;
                    case "矩形":
                        Rectangle1_LeftTopX.Value = (Decimal)((ROIRectangle1)mRegion).Column1;
                        Rectangle1_LeftTopY.Value = (Decimal)((ROIRectangle1)mRegion).Row1;
                        Rectangle1_RightBottomX.Value = (Decimal)((ROIRectangle1)mRegion).Column2;
                        Rectangle1_RightBottomY.Value = (Decimal)((ROIRectangle1)mRegion).Row2;

                        break;
                    case "带角度矩形":
                        Rectangle2_CenterX.Value = (Decimal)((ROIRectangle2)mRegion).Column;
                        Rectangle2_CenterY.Value = (Decimal)((ROIRectangle2)mRegion).Row;
                        Rectangle2_LengthX.Value = (Decimal)((ROIRectangle2)mRegion).Lenth1 * 2;
                        Rectangle2_LengthY.Value = (Decimal)((ROIRectangle2)mRegion).Lenth2 * 2;
                        Rectangle2_phi.Value = (Decimal)(((ROIRectangle2)mRegion).Phi * 180.0 / Math.PI); ;

                        break;
                    default:

                        break;
                }
                LoadEvent();
                ROIChangedEvent(this, new LxcROISetChangedEventArgs(mRegion));

                _hWndControl.repaint();
            }
        }
        private void ROISetImageHalf()
        {
            if (ROIChangedEvent != null)
            {
                UnLoadEvent();
                double ih = _hWndControl.imageHeight;
                double iw = _hWndControl.imageWidth;
                double d = (ih > iw ? iw / 4 : ih / 4);

                switch (vroiSel1.SelectedValue)
                {
                    case "圆形":
                        ((ROICircle)mRegion).UpdateROI(iw / 2.0, ih / 2.0, ih > iw ? iw / 4.0 : ih / 4.0);
                        Circle_CenterX.Value = (Decimal)(iw / 2.0);
                        Circle_CenterY.Value = (Decimal)(ih / 2.0);
                        Circle_R.Value = (Decimal)(ih > iw ? iw / 4.0 : ih / 4.0);

                        break;
                    case "椭圆":
                        ((ROIEllipse)mRegion).UpdateROI(iw / 2.0, ih / 2.0, ih > iw ? iw / 4.0 : ih / 4.0, ih > iw ? iw / 4.0 : ih / 4.0, 0);
                        Ellipse_CenterX.Value = (Decimal)(iw / 2.0);
                        Ellipse_CenterY.Value = (Decimal)(ih / 2.0);
                        Ellipse_R1.Value = (Decimal)(ih > iw ? iw / 4.0 : ih / 4.0);
                        Ellipse_R2.Value = (Decimal)(ih > iw ? iw / 4.0 : ih / 4.0);
                        Ellipse_phi.Value = 0;
                        break;
                    case "矩形":
                        double x1 = iw / 2 - d;
                        double y1 = ih / 2 - d;
                        double x2 = iw / 2 + d;
                        double y2 = ih / 2 + d;
                        ((ROIRectangle1)mRegion).UpdateROI(y1, x1, y2, x2);
                        Rectangle1_LeftTopX.Value = (Decimal)x1;
                        Rectangle1_LeftTopY.Value = (Decimal)y1;
                        Rectangle1_RightBottomX.Value = (Decimal)x2;
                        Rectangle1_RightBottomY.Value = (Decimal)y2;

                        break;
                    case "带角度矩形":
                        ((ROIRectangle2)mRegion).UpdateROI(iw / 2.0, ih / 2.0, 0, iw / 2.0, ih / 2.0);
                        Rectangle2_CenterX.Value = (Decimal)(iw / 2.0);
                        Rectangle2_CenterY.Value = (Decimal)(ih / 2.0);
                        Rectangle2_LengthX.Value = (Decimal)(iw / 2.0);
                        Rectangle2_LengthY.Value = (Decimal)(ih / 2.0);
      
                        Rectangle2_phi.Value = 0;

                        break;
                    default:

                        break;
                }
                _hWndControl.repaint();
                LoadEvent();
                ROIChangedEvent(this, new LxcROISetChangedEventArgs(mRegion));
            }
        }
        private void UnLoadEvent()
        { //卸载控件值改变事件
            this.Circle_CenterX.OnValueChanged -= new System.EventHandler(this.Circle_ValueChanged);
            this.Circle_CenterY.OnValueChanged -= new System.EventHandler(this.Circle_ValueChanged);
            this.Circle_R.OnValueChanged -= new System.EventHandler(this.Circle_ValueChanged);
            this.Rectangle1_LeftTopX.OnValueChanged -= new System.EventHandler(this.Rectangle1_LeftTopX_ValueChanged);
            this.Rectangle1_LeftTopY.OnValueChanged -= new System.EventHandler(this.Rectangle1_LeftTopX_ValueChanged);
            this.Rectangle1_RightBottomX.OnValueChanged -= new System.EventHandler(this.Rectangle1_LeftTopX_ValueChanged);
            this.Rectangle1_RightBottomY.OnValueChanged -= new System.EventHandler(this.Rectangle1_LeftTopX_ValueChanged);
            this.Rectangle2_CenterX.OnValueChanged -= new System.EventHandler(this.Rectangle2_CenterX_ValueChanged);
            this.Rectangle2_CenterY.OnValueChanged -= new System.EventHandler(this.Rectangle2_CenterX_ValueChanged);
            this.Rectangle2_phi.OnValueChanged -= new System.EventHandler(this.Rectangle2_CenterX_ValueChanged);
            this.Rectangle2_LengthX.OnValueChanged -= new System.EventHandler(this.Rectangle2_CenterX_ValueChanged);
            this.Rectangle2_LengthY.OnValueChanged -= new System.EventHandler(this.Rectangle2_CenterX_ValueChanged);
            this.Ellipse_CenterX.OnValueChanged -= new System.EventHandler(this.Ellipse_CenterX_ValueChanged);
            this.Ellipse_CenterY.OnValueChanged -= new System.EventHandler(this.Ellipse_CenterX_ValueChanged);
            this.Ellipse_phi.OnValueChanged -= new System.EventHandler(this.Ellipse_CenterX_ValueChanged);
            this.Ellipse_R1.OnValueChanged -= new System.EventHandler(this.Ellipse_CenterX_ValueChanged);
            this.Ellipse_R2.OnValueChanged -= new System.EventHandler(this.Ellipse_CenterX_ValueChanged);
        }
        private void LoadEvent()
        {
            this.Circle_CenterX.OnValueChanged += new System.EventHandler(this.Circle_ValueChanged);
            this.Circle_CenterY.OnValueChanged += new System.EventHandler(this.Circle_ValueChanged);
            this.Circle_R.OnValueChanged += new System.EventHandler(this.Circle_ValueChanged);
            this.Rectangle1_LeftTopX.OnValueChanged += new System.EventHandler(this.Rectangle1_LeftTopX_ValueChanged);
            this.Rectangle1_LeftTopY.OnValueChanged += new System.EventHandler(this.Rectangle1_LeftTopX_ValueChanged);
            this.Rectangle1_RightBottomX.OnValueChanged += new System.EventHandler(this.Rectangle1_LeftTopX_ValueChanged);
            this.Rectangle1_RightBottomY.OnValueChanged += new System.EventHandler(this.Rectangle1_LeftTopX_ValueChanged);
            this.Rectangle2_CenterX.OnValueChanged += new System.EventHandler(this.Rectangle2_CenterX_ValueChanged);
            this.Rectangle2_CenterY.OnValueChanged += new System.EventHandler(this.Rectangle2_CenterX_ValueChanged);
            this.Rectangle2_phi.OnValueChanged += new System.EventHandler(this.Rectangle2_CenterX_ValueChanged);
            this.Rectangle2_LengthX.OnValueChanged += new System.EventHandler(this.Rectangle2_CenterX_ValueChanged);
            this.Rectangle2_LengthY.OnValueChanged += new System.EventHandler(this.Rectangle2_CenterX_ValueChanged);
            this.Ellipse_CenterX.OnValueChanged += new System.EventHandler(this.Ellipse_CenterX_ValueChanged);
            this.Ellipse_CenterY.OnValueChanged += new System.EventHandler(this.Ellipse_CenterX_ValueChanged);
            this.Ellipse_phi.OnValueChanged += new System.EventHandler(this.Ellipse_CenterX_ValueChanged);
            this.Ellipse_R1.OnValueChanged += new System.EventHandler(this.Ellipse_CenterX_ValueChanged);
            this.Ellipse_R2.OnValueChanged += new System.EventHandler(this.Ellipse_CenterX_ValueChanged);
        }
        private void Circle_ValueChanged(object sender, EventArgs e)
        {
            ((ROICircle)mRegion).UpdateROI((double)Circle_CenterX.Value, (double)Circle_CenterY.Value, (double)Circle_R.Value);
            _hWndControl.repaint();
        }
        private void Rectangle1_LeftTopX_ValueChanged(object sender, EventArgs e)
        {
            ((ROIRectangle1)mRegion).UpdateROI((double)Rectangle1_LeftTopY.Value, (double)Rectangle1_LeftTopX.Value, (double)Rectangle1_RightBottomY.Value, (double)Rectangle1_RightBottomX.Value);

            _hWndControl.repaint();
        }
        private void Rectangle2_CenterX_ValueChanged(object sender, EventArgs e)
        {
            ((ROIRectangle2)mRegion).UpdateROI((double)Rectangle2_CenterX.Value, (double)Rectangle2_CenterY.Value, (double)Rectangle2_phi.Value, (double)Rectangle2_LengthX.Value, (double)Rectangle2_LengthY.Value);

            _hWndControl.repaint();
        }
        private void Ellipse_CenterX_ValueChanged(object sender, EventArgs e)
        {
            ((ROIEllipse)mRegion).UpdateROI((double)Ellipse_CenterX.Value, (double)Ellipse_CenterY.Value, (double)Ellipse_R1.Value, (double)Ellipse_R2.Value, (double)Ellipse_phi.Value);

            _hWndControl.repaint();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ROISetImageHalf();
        }
    }
}
