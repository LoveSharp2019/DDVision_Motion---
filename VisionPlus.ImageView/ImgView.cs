using HalconDotNet;
using Lxc.VisionPlus.Core.LxcAttribute;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VSE.Core;

namespace Lxc.VisionPlus.ImageView
{
    [Serializable]
    public struct RecordImage
    {
        public string color;
        [PropertyAttribute(true, false)]
        public HObject region;
        [PropertyAttribute(true, false)]
        public List<Shape> rois;
    }
    [Serializable]
    public partial class ImgView : UserControl
    {
        #region 私有变量定义.
        public delegate void RecordHandle(object sender, LxcRecordChangeEventArgs e);
        public class LxcRecordChangeEventArgs
        {
            private List<RecordImage> _Record;
            public List<RecordImage> Record
            {
                get
                {
                    return this._Record;
                }
                set
                {
                    this._Record = value;
                }
            }
            public LxcRecordChangeEventArgs(List<RecordImage> re)
            {
                _Record = re;
            }
        }
        public virtual void OnRecordChange(LxcRecordChangeEventArgs e)
        {
            if (RecordChangeEvent != null)
            {
                RecordChangeEvent(this, e);
            }
        }
        private HWindow /**/                 LocalWindow;
        private HWindow /**/                 FullWindow; 
        private HImage  /**/                 hv_image;                                        //缩放时操作的图片  此处千万不要使用hv_image = new HImage(),不然在生成控件dll的时候,会导致无法序列化,去你妈隔壁,还好老子有版本控制,不然都找不到这种恶心问题
        private int /**/                     hv_imageWidth, hv_imageHeight;                   //图片宽,高
        private string /**/                  str_imgSize;                                     //图片尺寸大小 5120X3840
        private bool    /**/                 drawModel = false;                                //绘制模式下,不允许缩放和鼠标右键菜单

        public ViewWindow viewWindow;    /**/                                      //ViewWindow
        public System.Windows.Forms.PictureBox hWindowControl;   /**/                                           // 当前halcon窗口
        public List<RecordImage> ImageRecords = new List<RecordImage>();
        public bool IsRecordVis;
        public event RecordHandle RecordChangeEvent;
        private FullScreen FullScreemWin = new FullScreen();
        bool isFullScreen = false;
        bool IsFullScreen
        {
            get { return isFullScreen; } 
            set
            {
                if (isFullScreen!=value)
                {
                    if (hv_image!=null&&hv_image.IsInitialized())
                    {
                        hv_window.CloseWindow();
                        hv_window.Dispose();
                        isFullScreen = value;
                      
                        if (isFullScreen)
                        {
                            
                            FullScreemWin.pictureBox1.MouseMove += FullScreen_HMouseMove;
                            FullScreemWin.VisibleChanged += FullWindow_SizeChanged;


                        }
                        else
                        {
                            this.viewWindow._hWndControl.viewPort = hWindowControl;
                            this.viewWindow._hWndControl.hv_window = hv_window;
                            this.Image = hv_image.CopyImage();
                            this.viewWindow.resetWindowImage();
                            FullScreemWin.VisibleChanged -= FullWindow_SizeChanged;
                            FullScreemWin.pictureBox1.MouseMove -= FullScreen_HMouseMove;
                        }
                    }
                   
                }
            } 
        }
        private bool EnMouPos = false;//是否可以获取鼠标位置；
        #endregion
        public override Color BackColor 
        { 
            get {return base.BackColor;}
            set 
            { 
                base.BackColor = value;
                imgviewCaptionLabel.BackColor = value;
                
            }
        }
        public override Color ForeColor
        {
            get {return base.ForeColor;}
            set
            {
                base.ForeColor = value;
                imgviewCaptionLabel.ForeColor = value;
            }
        }
        public bool CaptionVisible
        {
            get{return imgviewCaptionLabel.Visible;}
            set
            {
                imgviewCaptionLabel.Visible = value;
            }
        }
        String cap = "";
        public string Caption
        {
            get {return imgviewCaptionLabel.Text;}
            set
            {
                cap = value;
                imgviewCaptionLabel.Text = value;
            }
        }
        public void SetResult(bool IsOK)
        {
            if (IsOK)
            {
                imgviewCaptionLabel.ForeColor = Color.LightGreen;
                imgviewCaptionLabel.Text = cap + "___OK";
                imgviewCaptionLabel.BackColor = BackColor;
            }
            else
            {
                imgviewCaptionLabel.ForeColor = Color.White;
                imgviewCaptionLabel.Text = cap + "___NG";
                imgviewCaptionLabel.BackColor = Color.DarkRed;
            }
        }

        public void SetImageViewColor(Color color)
        {
            imgviewCaptionLabel.BackColor = color;
        }

        public HWindow hv_window
        {

            get {
                if (IsFullScreen)
                {
                    return FullWindow;
                }
                else
                {
                    if (LocalWindow==null)
                    {
                        if (mCtrl_HWindow.Width * mCtrl_HWindow.Height == 0)
                        {
                            mCtrl_HWindow.Width = 800;
                            mCtrl_HWindow.Height = 600;
                        }
                        LocalWindow=  new HWindow(0, 0, mCtrl_HWindow.Width, mCtrl_HWindow.Height, mCtrl_HWindow.Handle, "visible", "");
                    }
                    return LocalWindow;
                }
               
            }
        }
        public enum CoordSystem
        {
            image,
            window
        }
        
        /// <summary>
        /// 初始化控件
        /// </summary>
        public ImgView()
        {
           
            
            InitializeComponent();
           

            toolTip1.SetToolTip(m_CtrlHStatusLabelCtrl,"1");
            //

            // hv_window = this.mCtrl_HWindow.HalconWindow;

            //              设定鼠标按下时图标的形状
            //              'arrow'  'default' 'crosshair' 'text I-beam' 'Slashed circle' 'Size All'
            //              'Size NESW' 'Size S' 'Size NWSE' 'Size WE' 'Vertical Arrow' 'Hourglass'
            //
            // hv_window.SetMshape("Hourglass");
            viewWindow = new ViewWindow(mCtrl_HWindow);
            hWindowControl = this.mCtrl_HWindow;
            splitContainer1.Visible = false;
           
            mCtrl_HWindow.Height = this.Height;

            viewWindow._hWndControl.showZoomPercent = new HWndCtrl.ShowZoomPercent(this.ShowZoomPercent);

        }


        /// <summary>
        /// 绘制模式下,不允许缩放和鼠标右键菜单
        /// </summary>
        public bool DrawModel
        {
            get { return drawModel; }
            set
            {
                //缩放控制
   
                viewWindow.setDrawModel(value);
                drawModel = value;
            }
        }
        private winMode _EditModel = winMode.Menu;//绘制的图形是否可以编辑
        
        public winMode EditModel
        {
            get
            {
                return _EditModel;
            }
            set
            {
                viewWindow.setEditModel(value != winMode.None);
                    this.useContextMenuStrip(value != winMode.None);
             
             
                SwitchWinMode(value);

                _EditModel = value;
            }
        }
        
        public void useContextMenuStrip(bool value)
        {
            this.mCtrl_HWindow.ContextMenuStrip = (value ? this.hv_MenuStrip : null);
        }
        /// <summary>
        /// 设置image,初始化控件参数
        /// </summary>
        //  [DesignerSerializationVisibility.Hidden]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public HImage Image
        {
            get {
                return this.hv_image;
            }
            set
            {
                if (value != null&& value.IsInitialized())
                {

                    if (!HWndCtrl.IsDesignMode())
                    {
                       
                    }

                    if (mCtrl_HWindow.Image != null)
                    {
                        mCtrl_HWindow.Image = null;
                        mCtrl_HWindow.Refresh();
                    }
                    ;
                   

                    this.hv_image = value.CopyImage();
                    hv_image.GetImageSize(out hv_imageWidth, out hv_imageHeight);
                    str_imgSize = String.Format("图像大小【{0}*{1}】", hv_imageWidth, hv_imageHeight);
                    if (IsFullScreen)
                    {
                        if (FullWindow == null || !FullWindow.IsInitialized())
                        {
                            //FullScreen temp = new FullScreen();
                            //temp.WindowState = FormWindowState.Maximized;
                            //temp.Show();
                            //Size s = temp.pictureBox1.Size;
                            //temp.Close();
                            FullWindow = new HWindow(0, 0, FullScreemWin.pictureBox1.Width, FullScreemWin.pictureBox1.Height, FullScreemWin.pictureBox1.Handle, "visible", "");
                        }
                    }
                    else
                    {
                        if (LocalWindow == null||!LocalWindow.IsInitialized())
                        {

                            LocalWindow = new HWindow(0, 0, mCtrl_HWindow.Width, (mCtrl_HWindow.Height == 0)?100: mCtrl_HWindow.Height, mCtrl_HWindow.Handle, "visible", "");

                        }
                    }
                    
                   
                    viewWindow._hWndControl.hv_window = hv_window;
                    //DispImageFit(mCtrl_HWindow);

                    viewWindow._hWndControl.windowWidth = mCtrl_HWindow.Width;
                    viewWindow._hWndControl.windowHeight = mCtrl_HWindow.Height;
                 
                    viewWindow.displayImage(hv_image);
                }
            }
        }

       

        public System.Windows.Forms.PictureBox getHWindowControl()
        {
            return this.mCtrl_HWindow;
        }

        
        /// <summary>
        /// 鼠标在空间窗体里滑动,显示鼠标所在位置的灰度值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HWindowControl_HMouseMove(object sender, MouseEventArgs e)
        {
            if (hv_image != null)
            {
                try
                {
                    string str_value;
                    string str_position;
                    bool _isXOut = true, _isYOut = true;
                    HTuple channel_count;
                    double positionY;
                    double positionX;
                        int button_state;
                    HOperatorSet.CountChannels(hv_image, out channel_count);

                    hv_window.GetMpositionSubPix(out positionY, out positionX, out button_state);
                    str_position = String.Format("| 坐标【{0:0000.0},{1:0000.0}】", positionX, positionY);

                    _isXOut = (positionX < 0 || positionX >= hv_imageWidth);
                    _isYOut = (positionY < 0 || positionY >= hv_imageHeight);

                    if (!_isXOut && !_isYOut)
                    {
                        if ((int)channel_count == 1)
                        {
                            double grayVal;
                            grayVal = hv_image.GetGrayval((int)positionY, (int)positionX);
                            str_value = "| 灰度值:" + grayVal.ToString("f0");
                        }
                        else if ((int)channel_count == 3)
                        {
                            double grayValRed, grayValGreen, grayValBlue;

                            HImage _RedChannel, _GreenChannel, _BlueChannel;

                            _RedChannel = hv_image.AccessChannel(1);
                            _GreenChannel = hv_image.AccessChannel(2);
                            _BlueChannel = hv_image.AccessChannel(3);

                            grayValRed = _RedChannel.GetGrayval((int)positionY, (int)positionX);
                            grayValGreen = _GreenChannel.GetGrayval((int)positionY, (int)positionX);
                            grayValBlue = _BlueChannel.GetGrayval((int)positionY, (int)positionX);

                            _RedChannel.Dispose();
                            _GreenChannel.Dispose();
                            _BlueChannel.Dispose();

                            str_value = String.Format("R:{0:000.0}, G:{1:000.0},B: {2:000.0}", grayValRed, grayValGreen, grayValBlue);
                        }
                        else
                        {
                            str_value = "";
                        }
                       m_CtrlHStatusLabelCtrl.Text = str_imgSize + " " + str_position + " " + str_value;;
                       
                    }
                    else
                    {
                        
                    }
                }
                catch (Exception )
                {
                    
                    //不处理
                }
            }
        }
        private void FullScreen_HMouseMove(object sender, MouseEventArgs e)
        {
            if (hv_image != null)
            {
                try
                {
                    string str_value;
                    string str_position;
                    bool _isXOut = true, _isYOut = true;
                    HTuple channel_count;
                    HOperatorSet.CountChannels(hv_image, out channel_count);
                     double positionY;
                    double positionX;
                        int button_state;
                    hv_window.GetMpositionSubPix(out positionY, out positionX, out button_state);
                    str_position = String.Format("| 坐标【{0:0000.0},{1:0000.0}】", positionX, positionY);

                    _isXOut = (positionX < 0 || positionX >= hv_imageWidth);
                    _isYOut = (positionY < 0 || positionY >= hv_imageHeight);

                    if (!_isXOut && !_isYOut)
                    {
                        if ((int)channel_count == 1)
                        {
                            double grayVal;
                            grayVal = hv_image.GetGrayval((int)positionY, (int)positionX);
                            str_value = "| 灰度值:" + grayVal.ToString("f0");
                        }
                        else if ((int)channel_count == 3)
                        {
                            double grayValRed, grayValGreen, grayValBlue;

                            HImage _RedChannel, _GreenChannel, _BlueChannel;

                            _RedChannel = hv_image.AccessChannel(1);
                            _GreenChannel = hv_image.AccessChannel(2);
                            _BlueChannel = hv_image.AccessChannel(3);

                            grayValRed = _RedChannel.GetGrayval((int)positionY, (int)positionX);
                            grayValGreen = _GreenChannel.GetGrayval((int)positionY, (int)positionX);
                            grayValBlue = _BlueChannel.GetGrayval((int)positionY, (int)positionX);

                            _RedChannel.Dispose();
                            _GreenChannel.Dispose();
                            _BlueChannel.Dispose();

                            str_value = String.Format("R:{0:000.0}, G:{1:000.0},B: {2:000.0}", grayValRed, grayValGreen, grayValBlue);
                        }
                        else
                        {
                            str_value = "";
                        }
                        FullScreemWin.Text = str_imgSize + " " + str_position + " " + str_value; ;

                    }
                    else
                    {

                    }
                }
                catch (Exception)
                {

                    //不处理
                }
            }
        }
        public void displayMessage(string message, int row, int colunm)
        {
            viewWindow._hWndControl.addText(message, row, colunm);
        }

        public void displayMessage(string message, int row, int colunm, int size, string color)
        {
            viewWindow._hWndControl.addText(message, row, colunm, size, color, CoordSystem.image);
        }

        public void displayMessage(string message, int row, int colunm, int size, string color, CoordSystem coordSystem)
        {
            viewWindow._hWndControl.addText(message, row, colunm, size, color, coordSystem);
        }
        private void ShowZoomPercent(double scale)
        {
            int num = (int)Math.Round(scale * 100.0);
            string txt = "|   缩放比例" + "【" + num.ToString() + "%】";
            this.label1.Text = txt;
        }

        public void ClearWindow()
        {
            try
            {
                this.Invoke(new Action(
                        () =>
                        {
                            //this.hv_image = null;
                            
                           splitContainer1.Visible = false;
                            hv_window.ClearWindow();
                         
                            viewWindow.ClearWindow();

                        }
                    ));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        /// <summary>
        /// Hobject转换为的临时Himage,显示背景图
        /// </summary>
        /// <param name="hobject">传递Hobject,必须为图像</param>
        public void HobjectToHimage(HObject hobject)
        {
            if (hobject == null || !hobject.IsInitialized())
            {
                ClearWindow();
                return;
            }

            this.Image = new HImage(hobject);

        }

        #region 缩放后,再次显示传入的HObject
        public void displayHRegion(HObject obj, string color, string drawmode="fill")
        {
            this.viewWindow._hWndControl.addRegion(obj, color, drawmode);
        }

        public void displayHRegion(HObject obj, string color, string drawmode, int lineWidth)
        {
            this.viewWindow._hWndControl.addRegion(obj, color, drawmode, lineWidth);
        }

        public void displayHRegion(HObject obj)
        {
            this.viewWindow._hWndControl.addRegion(obj);
        }
        #endregion

        /// <summary>
        /// 鼠标离开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mCtrl_HWindow_MouseLeave(object sender, EventArgs e)
        {
            EnMouPos = false;
            //避免鼠标离开窗口,再返回的时候,图表随着鼠标移动
            if (hv_image != null && hv_image.IsInitialized())
            {
                viewWindow.mouseleave();
            }
        }
        private void recordChangeEvent(object sender, LxcRecordChangeEventArgs e)
        {
                //for (int i = 0; i < e.Record.Count; i++)
                //{
                //    viewWindow._hWndControl.addRegion(e.Record[i].region, e.Record[i].color,"fill");
                //}
                //viewWindow._hWndControl.repaint();

        }
        private void HWindow_Final_Load(object sender, EventArgs e)
        {
          
            RecordChangeEvent += new RecordHandle(recordChangeEvent);
        }

        private void tsbtn_OriginSize_Click(object sender, EventArgs e)
        {
            if (hv_image != null && hv_image.IsInitialized())
            {
                this.viewWindow.zoomImageByPercent(1.0);
            }
        }

        private void tsbtn_showInfo_Click(object sender, EventArgs e)
        {
            this.viewWindow._hWndControl.funcSet();
            showStatusBar();
        }
        public void showStatusBar()
        {
            base.SuspendLayout();
            if (this.viewWindow._hWndControl.IsDispState)
            {
                splitContainer1.Visible = true;
                m_CtrlHStatusLabelCtrl.Text = str_imgSize + " | 坐标【0000.0,0000.0】| 灰度值:0";
                mCtrl_HWindow.MouseMove -= HWindowControl_HMouseMove;
                mCtrl_HWindow.MouseMove += HWindowControl_HMouseMove;
            }
            else
            {
                splitContainer1.Visible = false;
                mCtrl_HWindow.MouseMove -= HWindowControl_HMouseMove;
            }
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        public bool GetMP(out double hv_Row, out double hv_Column, out int hv_Button)
        {
            try
            {
                if (EnMouPos)
                {
                    this.hv_window.GetMpositionSubPix(out hv_Row, out hv_Column, out hv_Button);
                    return true;
                }
                else
                {
                    hv_Row = 0;
                    hv_Column = 0;
                    hv_Button = 0;
                    return false;
                }
            }
            catch (Exception)
            {
                hv_Row = 0;
                hv_Column = 0;
                hv_Button = 0;
                return false;
            }

        }
        private void saveDumpWindow()
        {
            if (hv_image != null && hv_image.IsInitialized())
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png|All Files|*.*"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(saveFileDialog.FileName))
                {
                    try
                    {
                        hv_window.DumpWindow("png best", saveFileDialog.FileName);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        /// <summary>
        /// 保存原始图片到本地
        /// </summary>
        private void saveOriginImage()
        {
            if (hv_image != null && hv_image.IsInitialized())
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "BMP Image|*.bmp|All Files|*.*"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(saveFileDialog.FileName))
                {
                    lock (this)
                    {
                        try
                        {
                            HOperatorSet.WriteImage(this.hv_image, "bmp", 0, saveFileDialog.FileName);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
        }  
     

        private void tsbtn_saveOriginImage_Click(object sender, EventArgs e)
        {
            this.saveOriginImage();
        }

        private void tsbtn_saveDumpWindow_Click(object sender, EventArgs e)
        {
            saveDumpWindow();
        }

        private void mCtrl_HWindow_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (hv_image != null)
                {
                    if (splitContainer1.Width>670)
                    {
                        splitContainer1.Panel2Collapsed=false;
                    }
                    else
                    {
                        splitContainer1.Panel2Collapsed = true;
                    }
                    this.viewWindow.resetWindowImage(true);
                }

            }
            catch (Exception)
            {
            }

        }
        private void FullWindow_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (FullScreemWin.Visible)
                {
                    if (hv_image != null)
                    {
                        int sss = this.viewWindow._hWndControl.imageHeight;
                        this.viewWindow._hWndControl.viewPort = FullScreemWin.pictureBox1;
                        this.viewWindow._hWndControl.hv_window = hv_window;
                        int ssss = this.viewWindow._hWndControl.imageHeight;
                        this.Image = hv_image.CopyImage();
                      
                        this.viewWindow.resetWindowImage(true);
                    }
                }
              

            }
            catch (Exception)
            {
            }

        }
        public bool StaticWnd
        {
            get
            {
                return this.viewWindow._hWndControl.isStaticWnd;
            }
            set
            {
                this.setStaticWnd(value);

            }
        }
        public void setStaticWnd(bool IsStatic)
        {
            this.viewWindow._hWndControl.isStaticWnd = IsStatic;

        }
        private void 区域灰度直方图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hv_image != null && hv_image.IsInitialized())
            {
                winMode m = EditModel;
                SwitchWinMode(winMode.Draw);
                this.viewWindow._hWndControl.funcShowGrayHisto();
                SwitchWinMode(m);
            }
        }

        private void 测量距离ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hv_image != null && hv_image.IsInitialized())
            {
                winMode m = EditModel;
                SwitchWinMode(winMode.Draw);
                this.viewWindow._hWndControl.MeasureP2PDistance();
                SwitchWinMode(m);
            }
        }

        private void 平移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hv_image != null && hv_image.IsInitialized())
            {
                SwitchWinMode(winMode.MoveZoom);
                指针ToolStripMenuItem.Checked = false;
                平移ToolStripMenuItem.Checked = true;
            }
        }

        private void 指针ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchWinMode(winMode.Menu);
            指针ToolStripMenuItem.Checked = true;
            平移ToolStripMenuItem.Checked = false;
        }

       

        private void tsbtn_fitWindow_Click(object sender, EventArgs e)
        {
            if (hv_image != null && hv_image.IsInitialized())
            {
                this.viewWindow.resetWindowImage();
            }
           
        }
       public enum winMode
        {
            Menu,//不能绘制 不能缩放 带菜单
            Draw,//不带菜单不能缩放 只能绘制
            MoveZoom,//缩放移动带菜单
            None,//不带菜单 不能绘制 不能缩放
        }
        private void 打开图像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog
                {
                    Filter = "Image|*.bmp;*.jpg;*.png;*.tif|BMP|*.bmp|JPEG|*.jpg|PNG|*.png|TIFF|*.tif"
                };
                op.ShowDialog();
                HImage img = new HImage();
                if (op.FileName != "")
                {
                    img.ReadImage(op.FileName);

                    Image = img;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("此图像损坏！");
            }
            
           
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            IsFullScreen = true;
            if (isFullScreen)
            {
                FullScreemWin.ShowDialog();
            }
            IsFullScreen = false;
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            toolTip1.SetToolTip(m_CtrlHStatusLabelCtrl, m_CtrlHStatusLabelCtrl.Text);
           
        }

        private void mCtrl_HWindow_MouseEnter(object sender, EventArgs e)
        {
            EnMouPos = true;
        }

        private void mCtrl_HWindow_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown( e);
        }

        private void mCtrl_HWindow_MouseUp(object sender, MouseEventArgs e)
        {
            this.OnMouseUp(e);
        }

        private void SwitchWinMode(winMode Mode)
        {
          
            StaticWnd= true;
            指针ToolStripMenuItem.Checked = false;
            平移ToolStripMenuItem.Checked = false;
            switch (Mode)
            {
                case winMode.Menu:
                    指针ToolStripMenuItem.Checked = true;
                    break;
                case winMode.Draw:
                    break;
                case winMode.MoveZoom:
                    StaticWnd = false;
                    平移ToolStripMenuItem.Checked = true;
                    break;
                case winMode.None:
                    
                    break;
                default:
                    break;
            }
        }

    }


}
