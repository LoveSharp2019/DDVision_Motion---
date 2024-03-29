using System;
using Lxc.VisionPlus.ImageView;
using System.Collections;
using HalconDotNet;
using ViewROI.Config;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using LXCSystem.Msg.MsgBox2;
using LXCSystem.Msg;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Diagnostics;
using VSE.Core;

namespace Lxc.VisionPlus.ImageView.Model
{
	public delegate void IconicDelegate(int val);
	public delegate void FuncDelegate();
    public delegate void OnIconicObjectMovedHandler(Object sender, double moveX, double moveY);
    public delegate void OnIconicObjectZoomedHandler(Object sender, double zoomCenterX, double zoomCenterY, double scale);

    public class HWndCtrl
	{
		/// <summary>No action is performed on mouse events</summary>
		public const int MODE_VIEW_NONE       = 10;

		/// <summary>Zoom is performed on mouse events</summary>
		public const int MODE_VIEW_ZOOM       = 11;

		/// <summary>Move is performed on mouse events</summary>
		public const int MODE_VIEW_MOVE       = 12;

		/// <summary>Magnification is performed on mouse events</summary>
		public const int MODE_VIEW_ZOOMWINDOW	= 13;


		public const int MODE_INCLUDE_ROI     = 1;

		public const int MODE_EXCLUDE_ROI     = 2;


		/// <summary>
		/// Constant describes delegate message to signal new image
		/// </summary>
		public const int EVENT_UPDATE_IMAGE   = 31;
		/// <summary>
		/// Constant describes delegate message to signal error
		/// when reading an image from file
		/// </summary>
		public const int ERR_READING_IMG      = 32;
		/// <summary> 
		/// Constant describes delegate message to signal error
		/// when defining a graphical context
		/// </summary>
		public const int ERR_DEFINING_GC      = 33;

		/// <summary> 
		/// Maximum number of HALCON objects that can be put on the graphics 
		/// stack without loss. For each additional object, the first entry 
		/// is removed from the stack again.
		/// </summary>
		private const int MAXNUMOBJLIST       = 2;//原始值为50 实际上2都可以,因这里只是存储背景图片


		private int    stateView;
		public bool   mousePressed = false;
		private double startX,startY;

		/// <summary>HALCON window</summary>
		public System.Windows.Forms.PictureBox viewPort;

		/// <summary>
		/// Instance of ROIController, which manages ROI interaction
		/// </summary>
		public  ROIController roiManager;

		/* dispROI is a flag to know when to add the ROI models to the 
		   paint routine and whether or not to respond to mouse events for 
		   ROI objects */
		private int           dispROI;


      
        //开启编辑模式
        public bool EditModel = true;

		/* Basic parameters, like dimension of window and displayed image part */
		public int   windowWidth;
        public int   windowHeight;
        public int   imageWidth;
        public int imageHeight;

		private int[] CompRangeX;
		private int[] CompRangeY;


		private int    prevCompX, prevCompY;
		private double stepSizeX, stepSizeY;


		/* Image coordinates, which describe the image part that is displayed  
		   in the HALCON window */
		public   double ImgRow1, ImgCol1, ImgRow2, ImgCol2;

		/// <summary>Error message when an exception is thrown</summary>
		public string  exceptionText = "";


		/* Delegates to send notification messages to other classes */
		/// <summary>
		/// Delegate to add information to the HALCON window after 
		/// the paint routine has finished
		/// </summary>
		public FuncDelegate   addInfoDelegate;

		/// <summary>
		/// Delegate to notify about failed tasks of the HWndCtrl instance
		/// </summary>
		public IconicDelegate NotifyIconObserver;
        public delegate void LxcROIChangedEventHandler(object sender, EventArgs e);
        public event LxcROIChangedEventHandler ROIChangedEvent;

        private HWindow ZoomWindow;
		private  double zoomWndFactor;
		private  double zoomAddOn;
		private  int     zoomWndSize;


		/// <summary> 
		/// List of HALCON objects to be drawn into the HALCON window. 
		/// The list shouldn't contain more than MAXNUMOBJLIST objects, 
		/// otherwise the first entry is removed from the list.
		/// </summary>
        private readonly ArrayList HObjImageList;

		/// <summary>
		/// Instance that describes the graphical context for the
		/// HALCON window. According on the graphical settings
		/// attached to each HALCON object, this graphical context list 
		/// is updated constantly.
		/// </summary>
		private readonly GraphicsContext mGC;
		public HWindow hv_window = new HWindow();
		public bool isStaticWnd = true;

		/// <summary> 
		/// Initializes the image dimension, mouse delegation, and the 
		/// graphical context setup of the instance.
		/// </summary>
		/// <param name="view"> HALCON window </param>
		protected internal HWndCtrl(System.Windows.Forms.PictureBox view)
        {
            
			viewPort = view;
            
			stateView = MODE_VIEW_NONE;
			this.showZoomPercent = null;
			this.IsDispText = true;
			this.IsDispROI = true;
			this.IsDispRegion = true;
			zoomWndFactor = (double)imageWidth / viewPort.Width;
			zoomAddOn = Math.Pow(0.9, 5);
			zoomWndSize = 150;

			/*default*/
			CompRangeX = new int[] { 0, 100 };
			CompRangeY = new int[] { 0, 100 };

			prevCompX = prevCompY = 0;

			dispROI = MODE_INCLUDE_ROI;//1;

            viewPort.MouseUp += mouseUp;
			viewPort.MouseDown += mouseDown;
            viewPort.MouseWheel +=HMouseWheel;
			viewPort.MouseMove +=mouseMoved;

			addInfoDelegate = new FuncDelegate(dummyV);
			NotifyIconObserver = new IconicDelegate(dummy);

			// graphical stack 
			HObjImageList = new ArrayList(20);
            mGC = new GraphicsContext
            {
                gcNotification = new GCDelegate(exceptionGC)
            };
        }

       
        private void HMouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            viewPort.Focus();
            //关闭缩放事件
            if (isStaticWnd)
            {
                return;
            }


            double scale;

            if (e.Delta > 0)
                scale = 0.9;
            else
                scale = 1.1111111111111112;
             double positionY;
                    double positionX;
                        int button_state;
            hv_window.GetMpositionSubPix(out positionY, out positionX, out button_state);

			zoomImage(positionX, positionY, scale);
	
 
        }
		/// <summary>
		/// Read dimensions of the image to adjust own window settings
		/// </summary>
		/// <param name="image">HALCON image</param>
		private void setImagePart(HImage image)
		{
             string type;
             int w;
             int h;
             image.GetImagePointer1(out type, out w, out h);
            setImagePart(0, 0, h, w);
		}


		/// <summary>
		/// Adjust window settings by the values supplied for the left 
		/// upper corner and the right lower corner
		/// </summary>
		/// <param name="r1">y coordinate of left upper corner</param>
		/// <param name="c1">x coordinate of left upper corner</param>
		/// <param name="r2">y coordinate of right lower corner</param>
		/// <param name="c2">x coordinate of right lower corner</param>
		private void setImagePart(int r1, int c1, int r2, int c2)
		{
			this.ImgRow1 = (double)r1;
			this.ImgCol1 = (double)c1;
			this.imageHeight = r2;
			this.ImgRow2 = (double)r2;
			this.imageWidth = c2;
			this.ImgCol2 = (double)c2;

			double zoomWidth = (double)this.imageWidth / (double)this.viewPort.Width;
			double zoomHeight = (double)this.imageHeight / (double)this.viewPort.Height;
			zoomWidth = (zoomHeight = Math.Max(zoomWidth, zoomHeight));
			if (this.showZoomPercent != null)
			{
				this.showZoomPercent((zoomWidth == 0.0) ? 1.0 : (1.0 / zoomWidth));
			}
			this.ImgCol1 = -((double)this.viewPort.Width * zoomWidth - (double)this.imageWidth) / 2.0;
			this.ImgCol2 = ((double)this.viewPort.Width * zoomWidth - (double)this.imageWidth) / 2.0 + (double)this.imageWidth;
			this.ImgRow1 = -((double)this.viewPort.Height * zoomHeight - (double)this.imageHeight) / 2.0;
			this.ImgRow2 = ((double)this.viewPort.Height * zoomHeight - (double)this.imageHeight) / 2.0 + (double)this.imageHeight;
			hv_window.SetPart(ImgRow1, ImgCol1, ImgRow2, ImgCol2);
			//this.viewPort.ImagePart = imagePart;
		}


		/// <summary>
		/// Sets the view mode for mouse events in the HALCON window
		/// (zoom, move, magnify or none).
		/// </summary>
		/// <param name="mode">One of the MODE_VIEW_* constants</param>
        protected internal void setViewState(int mode)
		{
			stateView = mode;

			if (roiManager != null)
				roiManager.resetROI();
		}

		/********************************************************************/
		private void dummy(int val)
		{
		}

		private void dummyV()
		{
		}

		/*******************************************************************/
		private void exceptionGC(string message)
		{
			exceptionText = message;
			NotifyIconObserver(ERR_DEFINING_GC);
		}

		/// <summary>
		/// Paint or don't paint the ROIs into the HALCON window by 
		/// defining the parameter to be equal to 1 or not equal to 1.
		/// </summary>
		public void setDispLevel(int mode)
		{
			dispROI = mode;
		}

		/****************************************************************************/
		/*                          graphical element                               */
		/****************************************************************************/
		private void zoomImage(double x, double y, double scale)
		{
            //关闭缩放事件
            if (isStaticWnd)
            {
                return;
            }

			double lengthC, lengthR;
			double percentC, percentR;
			int    lenC, lenR;

			percentC = (x - ImgCol1) / (ImgCol2 - ImgCol1);
			percentR = (y - ImgRow1) / (ImgRow2 - ImgRow1);

			lengthC = (ImgCol2 - ImgCol1) * scale;
			lengthR = (ImgRow2 - ImgRow1) * scale;

			ImgCol1 = x - lengthC * percentC;
			ImgCol2 = x + lengthC * (1 - percentC);

			ImgRow1 = y - lengthR * percentR;
			ImgRow2 = y + lengthR * (1 - percentR);

			lenC = (int)Math.Round(lengthC);
			lenR = (int)Math.Round(lengthR);
            hv_window.SetPart(ImgRow1, ImgCol1, ImgRow2, ImgCol2);
            double _zoomWndFactor = 1;
            _zoomWndFactor = scale * zoomWndFactor;
            if (this.showZoomPercent != null)
            {
                this.showZoomPercent((_zoomWndFactor == 0.0) ? 1.0 : (1.0 / _zoomWndFactor));
            }
            if (zoomWndFactor < 0.01 && _zoomWndFactor < zoomWndFactor)
            {
                //超过一定缩放比例就不在缩放
                resetWindow();
                return;
            }
            if (zoomWndFactor > 100 && _zoomWndFactor > zoomWndFactor)
            {
                //超过一定缩放比例就不在缩放
                resetWindow();
                return;
            }
            zoomWndFactor = _zoomWndFactor;



			repaint();
		}
		private bool IsDispText;

		private bool IsDispCenterLine;

		private bool IsDispRegion;
		private bool IsDispROI;
		public bool IsDispState;
		private bool IsDispSplit;
		private Color[] cc = new Color[] { Color.Green, Color.Orange };
		public int[] V = { 50,100,100};
		public void funcSet()
		{
			this.viewPort.Focus();
			HImage himage = null;
			if (this.HObjImageList.Count > 0)
			{
				himage = new HImage(((HObjectEntry)this.HObjImageList[0]).HObj);
			}
			if (himage == null)
			{
				LxcMsg2.Show(null, "请先加载图片再执行此操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{

                Form form = new Form
                {
                    Icon = VisionPlus.ImageView.Properties.Resources.ICO,
                    Font = new Font("微软雅黑", 12F),
                    Text = "设定",
                    TopMost = true,
                    StartPosition = FormStartPosition.Manual,
                    Location = viewPort.PointToScreen(viewPort.Location)
                };
                SetWin SetWin = new SetWin(new bool[] { IsDispState, IsDispSplit, IsDispCenterLine, IsDispText, IsDispROI, IsDispRegion, CenterLineBaseWin, !CenterLineBaseWin, CenterLineInMid }, cc,V);
				Size size = SetWin.Size;
				size.Height += 50;
				size.Width += 50;
				form.Size = size;
				form.MaximizeBox = false;
				form.MinimizeBox = false;
				form.FormBorderStyle = FormBorderStyle.FixedDialog;
				form.Controls.Add(SetWin);
				SetWin.Dock = DockStyle.Fill;
				form.ShowDialog();
				cc = SetWin.c;
				IsDispState = SetWin.check[0];
				IsDispSplit = SetWin.check[1];
				IsDispCenterLine = SetWin.check[2];
				IsDispText = SetWin.check[3];
				IsDispROI = SetWin.check[4];
				IsDispRegion = SetWin.check[5];
				CenterLineBaseWin = SetWin.check[6];
				CenterLineInMid = SetWin.check[8];
				this.repaint();
				//form.Dispose();
				//SetWin.Dispose();
			}
		}
		/// <summary>
		/// Scales the image in the HALCON window according to the 
		/// value scaleFactor
		/// </summary>
		public void zoomImage(double scaleFactor)
        {  
			double MoveX = (ImgCol2 +ImgCol1) / 2.0;
            double MoveY = (ImgRow2 + ImgRow1) / 2.0;
			double ZOOM = (double)imageWidth / viewPort.Width;
			if (scaleFactor == 1 && zoomWndFactor == 1)
            {
                return;
            }
            else
			{
                if (MoveX > 0 && MoveX < imageWidth && MoveY > 0 && MoveY < imageHeight)
				{
					ImgRow1 = -viewPort.Height / 2 + MoveY;
					ImgCol1 = -viewPort.Width / 2 + MoveX;
					ImgRow2 = viewPort.Height + ImgRow1;
					ImgCol2 = viewPort.Width + ImgCol1;
					hv_window.SetPart(ImgRow1, ImgCol1, ImgRow2, ImgCol2);
					
                }
                else
                {
					ImgRow1 = -viewPort.Height / 2 + imageHeight/2;
					ImgCol1 = -viewPort.Width / 2 + imageWidth / 2;
					ImgRow2 = viewPort.Height + ImgRow1;
					ImgCol2 = viewPort.Width + ImgCol1;
					
					hv_window.SetPart(ImgRow1, ImgCol1, ImgRow2, ImgCol2);
				}
				if (this.showZoomPercent != null)
				{
					this.showZoomPercent(1);
				}
				zoomWndFactor = 1;
				repaint();

			}

        }


		/// <summary>
		/// Scales the HALCON window according to the value scale
		/// </summary>
		public void scaleWindow(double scale)
		{
			ImgRow1 = 0;
			ImgCol1 = 0;

			ImgRow2 = imageHeight;
			ImgCol2 = imageWidth;

			viewPort.Width = (int)(ImgCol2 * scale);
			viewPort.Height = (int)(ImgRow2 * scale);

			zoomWndFactor = ((double)imageWidth / viewPort.Width);
		}

		/// <summary>
		/// Recalculates the image-window-factor, which needs to be added to 
		/// the scale factor for zooming an image. This way the zoom gets 
		/// adjusted to the window-image relation, expressed by the equation 
		/// imageWidth/viewPort.Width.
		/// </summary>
		public void setZoomWndFactor()
		{
			zoomWndFactor = ((double)imageWidth / viewPort.Width);
		}

		/// <summary>
		/// Sets the image-window-factor to the value zoomF
		/// </summary>
		public void setZoomWndFactor(double zoomF)
		{
			zoomWndFactor = zoomF;
		}

		/*******************************************************************/
		private void moveImage(double motionX, double motionY)
		{
			ImgRow1 += -motionY;
			ImgRow2 += -motionY;

			ImgCol1 += -motionX;
			ImgCol2 += -motionX;

            //System.Drawing.Rectangle rect = viewPort.ImagePart;
            //rect.X = (int)Math.Round(ImgCol1);
            //rect.Y = (int)Math.Round(ImgRow1);
            //viewPort.ImagePart = rect;
            hv_window.SetPart(ImgRow1,ImgCol1, ImgRow2, ImgCol2);
            repaint();
		}


		/// <summary>
		/// Resets all parameters that concern the HALCON window display 
		/// setup to their initial values and clears the ROI list.
		/// </summary>
        protected internal void resetAll()
		{
			ImgRow1 = 0;
			ImgCol1 = 0;
			ImgRow2 = imageHeight;
			ImgCol2 = imageWidth;

			zoomWndFactor = (double)imageWidth / viewPort.Width;

			//System.Drawing.Rectangle rect = viewPort.ImagePart;
			//rect.X = (int)ImgCol1;
			//rect.Y = (int)ImgRow1;
			//rect.Width = (int)imageWidth;
			//rect.Height = (int)imageHeight;
			//viewPort.ImagePart = rect;


			if (roiManager != null)
				roiManager.reset();
		}

        protected internal void resetWindow(bool ReOpenWiindow=false)
		{
            if (ReOpenWiindow)
            {
				hv_window.CloseWindow();
				hv_window.OpenWindow(0,0,viewPort.Width, viewPort.Height,viewPort.Handle, "visible", "");
			}
			ImgRow1 = 0;
			ImgCol1 = 0;
			ImgRow2 = imageHeight;
			ImgCol2 = imageWidth;
			setImagePart(0, 0, imageHeight, imageWidth);
          
				zoomWndFactor = (double)imageWidth / viewPort.Width;
           
			
           
        }


		/*************************************************************************/
		/*      			 Event handling for mouse	   	                     */
		/*************************************************************************/
		private void mouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            viewPort.Focus();
            //关闭缩放事件
            if (isStaticWnd)
            {
                return;
            }

            stateView = MODE_VIEW_MOVE;
			mousePressed = true;
			int activeROIidx = -1;
             double positionY;
                    double positionX;
                        int button_state;
            hv_window.GetMpositionSubPix(out positionY, out positionX, out button_state);
            if (roiManager != null && (dispROI == MODE_INCLUDE_ROI))
			{
				activeROIidx = roiManager.mouseDownAction(positionX, positionY);
			}

			if (activeROIidx == -1)
			{
				switch (stateView)
				{
					case MODE_VIEW_MOVE:
						startX = positionX;
						startY = positionY;
						break;

					case MODE_VIEW_NONE:
						break;
					case MODE_VIEW_ZOOMWINDOW:
						activateZoomWindow((int)e.X, (int)e.Y);
						break;
					default:
						break;
				}
			}
			//end of if
		}

		/*******************************************************************/
		private void activateZoomWindow(int X, int Y)
		{
			double posX, posY;
			int zoomZone;

			if (ZoomWindow != null)
				ZoomWindow.Dispose();

			HOperatorSet.SetSystem("border_width", 10);
			ZoomWindow = new HWindow();

			posX = ((X - ImgCol1) / (ImgCol2 - ImgCol1)) * viewPort.Width;
			posY = ((Y - ImgRow1) / (ImgRow2 - ImgRow1)) * viewPort.Height;

			zoomZone = (int)((zoomWndSize / 2) * zoomWndFactor * zoomAddOn);
			ZoomWindow.OpenWindow((int)posY - (zoomWndSize / 2), (int)posX - (zoomWndSize / 2),
								   zoomWndSize, zoomWndSize,
								   viewPort.Handle, "visible", "");
			ZoomWindow.SetPart(Y - zoomZone, X - zoomZone, Y + zoomZone, X + zoomZone);
			repaint(ZoomWindow);
			ZoomWindow.SetColor("black");
		}

        public void raiseMouseup()
        {
            mousePressed = false;
            if (roiManager != null
                && (roiManager.activeROIidx != -1)
                && (dispROI == MODE_INCLUDE_ROI))
            {
                roiManager.NotifyRCObserver(ROIController.EVENT_UPDATE_ROI);
            }
            else if (stateView == MODE_VIEW_ZOOMWINDOW)
            {
                ZoomWindow.Dispose();
            }
         
        }
		/*******************************************************************/
		private void mouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            //关闭缩放事件
            if (isStaticWnd)
            {
                return;
            }

			mousePressed = false;
            if (ROIChangedEvent != null)
            {
                ROIChangedEvent(this, null);
            }
            if (roiManager != null
				&& (roiManager.activeROIidx != -1)
				&& (dispROI == MODE_INCLUDE_ROI))
			{
				roiManager.NotifyRCObserver(ROIController.EVENT_UPDATE_ROI);
			}
			else if (stateView == MODE_VIEW_ZOOMWINDOW)
			{
				ZoomWindow.Dispose();
			}
		}

		/*******************************************************************/
		private double keepPositionY = 0;
		private double keepPositionX = 0;
		private void mouseMoved(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            viewPort.Focus();
            //关闭缩放事件
            if (isStaticWnd)
            {
                return;
            }

			double motionX, motionY;
			double posX, posY;
			double zoomZone;


			if (!mousePressed)
				return;
            double positionY;
            double positionX;
            int button_state;
			try
			{
				hv_window.GetMpositionSubPix(out positionY, out positionX, out button_state);
				keepPositionY = positionY;
				keepPositionX = positionX;
			}
			catch
			{
				//positionY = 0;
				//positionX = 0;
				positionY = keepPositionY;
				positionX = keepPositionX;
			}
			if (roiManager != null && (roiManager.activeROIidx != -1) && (dispROI == MODE_INCLUDE_ROI))
			{ 
				roiManager.mouseMoveAction(positionX, positionY);
			}
			else if (stateView == MODE_VIEW_MOVE)
			{
                motionX = positionX - this.startX;
                motionY = positionY - this.startY;

                if (((int)motionX != 0) || ((int)motionY != 0))
				{
					moveImage(motionX, motionY);
                    //startX = e.X  - motionX*zoomWndFactor;
                    //startY = e.Y - motionY * zoomWndFactor;
                }
			}
			else if (stateView == MODE_VIEW_ZOOMWINDOW)
			{
				HSystem.SetSystem("flush_graphic", "false");
				ZoomWindow.ClearWindow();


				posX = ((e.X - ImgCol1) / (ImgCol2 - ImgCol1)) * viewPort.Width;
				posY = ((e.Y - ImgRow1) / (ImgRow2 - ImgRow1)) * viewPort.Height;
				zoomZone = (zoomWndSize / 2) * zoomWndFactor * zoomAddOn;

				ZoomWindow.SetWindowExtents((int)posY - (zoomWndSize / 2),
											(int)posX - (zoomWndSize / 2),
											zoomWndSize, zoomWndSize);
				ZoomWindow.SetPart((int)(e.Y - zoomZone), (int)(e.X - zoomZone),
								   (int)(e.Y + zoomZone), (int)(e.X + zoomZone));
				repaint(ZoomWindow);

				HSystem.SetSystem("flush_graphic", "true");
				ZoomWindow.DispLine(-100.0, -100.0, -100.0, -100.0);
			}
		}

		/// <summary>
		/// To initialize the move function using a GUI component, the HWndCtrl
		/// first needs to know the range supplied by the GUI component. 
		/// For the x direction it is specified by xRange, which is 
		/// calculated as follows: GuiComponentX.Max()-GuiComponentX.Min().
		/// The starting value of the GUI component has to be supplied 
		/// by the parameter Init
		/// </summary>
		public void setGUICompRangeX(int[] xRange, int Init)
		{
			int cRangeX;

			CompRangeX = xRange;
			cRangeX = xRange[1] - xRange[0];
			prevCompX = Init;
			stepSizeX = ((double)imageWidth / cRangeX) * (imageWidth / windowWidth);

		}

		/// <summary>
		/// To initialize the move function using a GUI component, the HWndCtrl
		/// first needs to know the range supplied by the GUI component. 
		/// For the y direction it is specified by yRange, which is 
		/// calculated as follows: GuiComponentY.Max()-GuiComponentY.Min().
		/// The starting value of the GUI component has to be supplied 
		/// by the parameter Init
		/// </summary>
		public void setGUICompRangeY(int[] yRange, int Init)
		{
			int cRangeY;

			CompRangeY = yRange;
			cRangeY = yRange[1] - yRange[0];
			prevCompY = Init;
			stepSizeY = ((double)imageHeight / cRangeY) * (imageHeight / windowHeight);
		}


		/// <summary>
		/// Resets to the starting value of the GUI component.
		/// </summary>
		public void resetGUIInitValues(int xVal, int yVal)
		{
			prevCompX = xVal;
			prevCompY = yVal;
		}

		/// <summary>
		/// Moves the image by the value valX supplied by the GUI component
		/// </summary>
		public void moveXByGUIHandle(int valX)
		{
			double motionX;

			motionX = (valX - prevCompX) * stepSizeX;

			if (motionX == 0)
				return;

			moveImage(motionX, 0.0);
			prevCompX = valX;
		}


		/// <summary>
		/// Moves the image by the value valY supplied by the GUI component
		/// </summary>
		public void moveYByGUIHandle(int valY)
		{
			double motionY;

			motionY = (valY - prevCompY) * stepSizeY;

			if (motionY == 0)
				return;

			moveImage(0.0, motionY);
			prevCompY = valY;
		}

		/// <summary>
		/// Zooms the image by the value valF supplied by the GUI component
		/// </summary>
		public void zoomByGUIHandle(double valF)
		{
			double x, y, scale;
			double prevScaleC;



			x = (ImgCol1 + (ImgCol2 - ImgCol1) / 2);
			y = (ImgRow1 + (ImgRow2 - ImgRow1) / 2);

			prevScaleC = (double)((ImgCol2 - ImgCol1) / imageWidth);
			scale = ((double)1.0 / prevScaleC * (100.0 / valF));

			zoomImage(x, y, scale);
		}

		/// <summary>
		/// Triggers a repaint of the HALCON window
		/// </summary>
		public void repaint()
		{
			if (HObjImageList.Count != 0)
			{
				repaint(hv_window);
			}
		}
		double FontsizeOld = 0;
		private void DispImgText()
		{
			try
			{
				for (int i = 0; i < this.HObjImageList.Count; i++)
				{
					HObjectEntry hobjectEntry = (HObjectEntry)this.HObjImageList[i];
					if (i == 0)
					{
						if (hobjectEntry.HObj != null && hobjectEntry.HObj.IsInitialized())
						{
							this.mGC.applyContext(hv_window, hobjectEntry.gContext);
							hv_window.DispObj(hobjectEntry.HObj);
						}
					}
                    else if (hobjectEntry.Message != null && this.IsDispText)
                    {
						double zoomWidth = (double)this.imageWidth / (double)this.viewPort.Width;
						double zoomHeight = (double)this.imageHeight / (double)this.viewPort.Height;
						zoomWidth = (zoomHeight = Math.Max(zoomWidth, zoomHeight));

						double zoom = (hobjectEntry.Message.coordSystem == ImgView.CoordSystem.image) ? (1.0 / this.zoomWndFactor) : (1.0 / zoomWidth);
                        double num = hobjectEntry.Message.changeDisplayFontSize(hv_window, zoom, this.FontsizeOld);
                        this.FontsizeOld = num;
                        hobjectEntry.Message.DispMessage(hv_window, hobjectEntry.Message.coordSystem);
                    }
                }
			}
			catch (Exception)
			{
			}
		}
		private void DispRegion()
		{
			if (this.IsDispRegion)
			{

				try
				{
                    if (this.hRegionList.Count > 1000)
                    {
						System.Windows.Forms.MessageBox.Show("设定区域超过上限，界面显示卡顿");
						return;
                    }
			
					foreach (HRegionEntry hregionEntry in this.hRegionList)
					{
						hv_window.SetColor(hregionEntry.Color);
						hv_window.SetDraw(hregionEntry.DrawMode);
						hv_window.SetLineWidth(hregionEntry.LineWidth);
						if (hregionEntry != null && hregionEntry.HObject.IsInitialized())
						{
							hv_window.DispObj(hregionEntry.HObject);
						}
					}
				}
				catch (Exception)
				{
				}
			}

		}
		private void DispROI()
		{
			if (this.roiManager != null && this.IsDispROI)
			{
				this.roiManager.paintData(hv_window);
			}
		}
		private string Color2Str(Color c)
		{
			if (c == Color.Green)
			{
				return "green";
			}
			if (c == Color.Orange)
			{
				return "magenta";
			}

			return "blue";


		}
		public void MeasureP2PDistance()
		{
            //this.viewPort.Focus();

            ViewWindow.SetDisplayFont(hv_window, 20, "true", "false");

            ViewWindow.DisMsg(hv_window, "鼠标点击两个位置后,单击鼠标右键完成", ImgView.CoordSystem.window, 20, 20, "lime green", "false");
            HTuple red,green,blue;
            hv_window.GetRgb(out red, out green, out blue);
            int num = (int)hv_window.GetLineWidth();
            string draw = hv_window.GetDraw();
            hv_window.SetLineWidth(2);
            hv_window.SetLineStyle(new HTuple());
            hv_window.SetColor("magenta");
            double row1,column1,row2,column2;
            hv_window.DrawLine(out row1, out column1, out row2, out column2);
            hv_window.DispLine(row1, column1, row2, column2);
            hv_window.SetRgb(red, green, blue);
            hv_window.SetLineWidth(num);
            hv_window.SetDraw(draw);
            HTuple htuple;
            HOperatorSet.DistancePp(row1, column1, row2, column2, out htuple);
            double DisR = Math.Abs(row2 - row1);
            double DisC = Math.Abs(column2 - column1);
            Form form = new Form
            {
                Icon = VisionPlus.ImageView.Properties.Resources.ICO,
                Font = new Font("微软雅黑", 12F),
                Text = "量测信息",
                StartPosition = FormStartPosition.CenterParent
            };
            DisInfoWin DisInfoWin = new DisInfoWin();
            Size size = DisInfoWin.Size;
            size.Height += 50;
            size.Width += 50;
            form.Size = size;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.Controls.Add(DisInfoWin);
            DisInfoWin.Dock = DockStyle.Fill;
            DisInfoWin.label1.Text = string.Format("直线距离{0:f2}px\r\rx轴距离{1:f2}px\r\ry轴距离{2:f2}px", htuple.D, DisC, DisR);
            form.ShowDialog();
            this.repaint(hv_window);
		}
		public void funcShowGrayHisto()
		{
			//this.viewPort.Focus();
			HImage himage = null;
			if (this.HObjImageList.Count > 0)
			{
				himage = new HImage(((HObjectEntry)this.HObjImageList[0]).HObj);
			}
			if (himage == null)
			{
				LxcMsg2.Show(null, "请先加载图片再执行此操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				HWindow hvppleWindow = hv_window;
				ViewWindow.SetDisplayFont(hvppleWindow, 20, "true", "false");
				ViewWindow.DisMsg(hvppleWindow, "鼠标左键点击并拉取矩形区域，鼠标右键完成", ImgView.CoordSystem.window, 20, 20, "green","false");
			
				HTuple red;
				HTuple green ;
				HTuple blue ;
				hvppleWindow.GetRgb(out red, out green, out blue);
				int num = (int)hvppleWindow.GetLineWidth();
				string draw = hvppleWindow.GetDraw();
				hvppleWindow.SetLineWidth(1);
				hvppleWindow.SetLineStyle(new HTuple());
				hvppleWindow.SetColor("green");
				double num2;
				double num3;
				double num4;
				double num5;
				hvppleWindow.DrawRectangle1(out num2, out num3, out num4, out num5);
                Form form = new Form
                {
                    Icon = VisionPlus.ImageView.Properties.Resources.ICO,
                    Font = new Font("微软雅黑", 12F),
                    Text = "灰度直方图",
                    StartPosition = FormStartPosition.CenterParent
                };
                ThresholdUnit thresholdUnit = new ThresholdUnit();
				Size size = thresholdUnit.Size;
				size.Height += 50;
				size.Width += 50;
				form.Size = size;
				form.Controls.Add(thresholdUnit);
				thresholdUnit.Dock = DockStyle.Fill;
				HRegion hregion = new HRegion();
				hregion.GenRectangle1(num2,
					num3,
					num4,
					num5);
                HTuple ss;
				HTuple grayHisto = himage.GrayHisto(hregion, out ss);
				thresholdUnit.setAxisAdaption(4);
				thresholdUnit.setLabel("灰度值", "频率");
				thresholdUnit.setFunctionPlotValue(grayHisto);
				thresholdUnit.computeStatistics(grayHisto);

				form.ShowDialog();
				hvppleWindow.SetRgb(red, green, blue);
				hvppleWindow.SetLineWidth(num);
				hvppleWindow.SetDraw(draw);

				this.repaint();
			}
		}
		public bool CenterLineBaseWin = true;
		public bool CenterLineInMid = true;
		private void DispCenterLine()
		{
			if (this.IsDispCenterLine)
			{
				hv_window.SetLineWidth(1);
				hv_window.SetLineStyle(new HTuple());
				hv_window.SetColor(Color2Str(cc[1]));
				double CX, CY;
				if (CenterLineBaseWin)
                {
                    if (CenterLineInMid)
                    {
						CX = (double)(this.ImgCol2 + ImgCol1) / 2.0;
						CY = (double)(this.ImgRow2 + ImgRow1) / 2.0;
                    }
                    else
                    {
						CX = ImgCol1 + V[1] * zoomWndFactor;
						CY = ImgRow1+V[2]*zoomWndFactor;
					}
					
					hv_window.DispLine(CY, this.ImgCol1, CY, this.ImgCol2);
					hv_window.DispLine(ImgRow1, CX, ImgRow2, CX);
					
				}
				else
				{
					if (CenterLineInMid)
					{
						CX = imageWidth / 2.0;
						CY = imageHeight / 2.0;
                    }
                    else
                    {
						CX = V[1];
						CY = V[2];
					}
					
					hv_window.DispLine(CY, 0, CY, imageWidth);
					hv_window.DispLine(0, CX, imageHeight, CX);
					
				}

				double R = 10 * zoomWndFactor;
				hv_window.SetLineWidth(3);
				hv_window.DispLine(CY, CX - R * 6, CY, CX + R * 6);
				hv_window.DispLine(CY - R * 6, CX, CY + R * 6, CX);
				HXLDCont circle = new HXLDCont();
				hv_window.SetLineWidth(2);
				circle.GenCircleContourXld(CY, CX, R, 0, 6.28318, "positive", 0.1);
				hv_window.DispXld(circle);
			}
			
		}
		private void DispImgRegion()
		{

            if (imageHeight != 0)
            {
                HTuple red,green,blue;
                hv_window.GetRgb(out red, out green, out blue);
                int num = (int)hv_window.GetLineWidth();
                string draw = hv_window.GetDraw();

                hv_window.SetDraw("margin");
                hv_window.SetColor("green");
                hv_window.SetLineWidth(1);
                //hv_window.SetLineStyle(new HTuple());
                hv_window.DispRectangle1(0, 0, (double)imageHeight, (double)imageWidth);
                hv_window.SetRgb(red, green, blue);
                hv_window.SetLineWidth(num);
                hv_window.SetDraw(draw);
            }


        }
		private void DispSplit()
		{
			if (this.IsDispSplit)
			{
                 HTuple red,green,blue;
                hv_window.GetRgb(out red, out green, out blue);
                int num = (int)hv_window.GetLineWidth();
				string draw = hv_window.GetDraw();

				hv_window.SetDraw("margin");
				hv_window.SetColor(Color2Str(cc[0]));
				hv_window.SetLineWidth(1);
				int dis = (int)(V[0] / (1 / zoomWndFactor));
				///画横线
                for (int i = 0; (this.ImgRow1 + dis * i) < ImgRow2; i++)
                {
					hv_window.DispLine(this.ImgRow1 + dis * i, this.ImgCol1, this.ImgRow1 + dis * i, this.ImgCol2);
				}
				///画竖线
				for (int i = 0; (this.ImgCol1 + dis * i) < ImgCol2; i++)
				{
					hv_window.DispLine(this.ImgRow1, this.ImgCol1 + dis * i, this.ImgRow2, this.ImgCol1 + dis * i);
				}
				hv_window.SetRgb(red, green, blue);
				hv_window.SetLineWidth(num);
				hv_window.SetDraw(draw);

			}
		}
		/// <summary>
		/// Repaints the HALCON window 'window'
		/// </summary>
		public void repaint(HalconDotNet.HWindow window)
		{
            try
            {
				try
				{
					if (!HWndCtrl.IsDesignMode())
					{
                        if (!Dog.GetStates())
                        {
                            return;
                        }
                    }
					int count = this.HObjImageList.Count;
					HSystem.SetSystem("flush_graphic", "false");
					window.ClearWindow();
					mGC.stateOfSettings.Clear();
					this.DispImgText();
					this.DispRegion();
					this.DispROI();
					this.DispCenterLine();
					this.DispImgRegion();
			
					this.DispSplit();
		
					HSystem.SetSystem("flush_graphic", "true");
					window.SetColor("dim gray");
					window.DispLine(-100.0, -100.0, -101.0, -101.0);
				}
				catch (Exception ex)
				{
                    LXCSystem.Msg.MsgBox2.MessageBox.Show(null, ex.Message);
				}
				

            }
            catch (Exception)
            {

            }
		}



		/********************************************************************/
		/*                      GRAPHICSSTACK                               */
		/********************************************************************/

		/// <summary>
		/// Adds an iconic object to the graphics stack similar to the way
		/// it is defined for the HDevelop graphics stack.
		/// </summary>
		/// <param name="obj">Iconic object</param>
		public void addIconicVar(HObject img)
		{
            //先把HObjImageList给全部释放了,源代码 会出现内存泄漏问题
            for (int i = 0; i < HObjImageList.Count; i++)
            {
                 ((HObjectEntry)HObjImageList[i]).clear();
            }


			HObjectEntry entry;

			if (img == null)
				return;
            
            HTuple classValue;
            HOperatorSet.GetObjClass(img, out classValue);
            if (!classValue.S.Equals("image"))
            {
                return;
            }
            
            HImage obj = new HImage(img);

			if (obj.IsInitialized())
            {
                string type; int w; int h;
                obj.GetImagePointer1(out type, out  w, out h);


                clearList();

					if ((h != imageHeight) || (w != imageWidth))
					{
						imageHeight = h;
						imageWidth = w;
						zoomWndFactor = (double)imageWidth / viewPort.Width;
						setImagePart(0, 0, h, w);
					}
				
			}//if

			entry = new HObjectEntry(obj, mGC.copyContextList());

			HObjImageList.Add(entry);

			//每当传入背景图的时候 都清空HObjectList
			clearHRegionList();

            if (HObjImageList.Count > MAXNUMOBJLIST)
            {
                //需要自己手动释放
                ((HObjectEntry)HObjImageList[0]).clear();
                HObjImageList.RemoveAt(1);
            }
				
		}
		public void addText(string message, int row, int colunm, int size, string color, ImgView.CoordSystem coordSystem = ImgView.CoordSystem.image)
		{
			this.addIconicVar(new HWndMessage(message, row, colunm, size, color, coordSystem));
		}

		public void addText(string message, int row, int colunm)
		{
			this.addIconicVar(new HWndMessage(message, row, colunm));
		}

		public void addIconicVar(HWndMessage message)
		{
			if (message != null)
			{
				HObjectEntry hobjectEntry = new HObjectEntry(message);
				if (this.HObjImageList.Count >= 1 && hobjectEntry.Message != null)
				{
					double zoomWidth = (double)this.imageWidth / (double)this.viewPort.Width;
					double zoomHeight = (double)this.imageHeight / (double)this.viewPort.Height;
					zoomWidth = (zoomHeight = Math.Max(zoomWidth, zoomHeight));
					double zoom = (hobjectEntry.Message.coordSystem == ImgView.CoordSystem.image) ? (1.0 / this.zoomWndFactor) : (1.0 / zoomWidth);
					double num = hobjectEntry.Message.changeDisplayFontSize(hv_window, zoom, this.FontsizeOld);
					this.FontsizeOld = num;
					if (this.IsDispText)
					{
						hobjectEntry.Message.DispMessage(hv_window, hobjectEntry.Message.coordSystem);
					}
					this.HObjImageList.Add(hobjectEntry);
					if (this.HObjImageList.Count > 50)
					{
						((HObjectEntry)this.HObjImageList[1]).clear();
						this.HObjImageList.RemoveAt(1);
					}
				}
			}
		}

		/// <summary>
		/// Clears all entries from the graphics stack 
		/// </summary>
		public void clearList()
		{
			HObjImageList.Clear();
		}

		/// <summary>
		/// Returns the number of items on the graphics stack
		/// </summary>
		public int getListCount()
		{
			return HObjImageList.Count;
		}

		/// <summary>
		/// Changes the current graphical context by setting the specified mode
		/// (constant starting by GC_*) to the specified value.
		/// </summary>
		/// <param name="mode">
		/// Constant that is provided by the class GraphicsContext
		/// and describes the mode that has to be changed, 
		/// e.g., GraphicsContext.GC_COLOR
		/// </param>
		/// <param name="val">
		/// Value, provided as a string, 
		/// the mode is to be changed to, e.g., "blue" 
		/// </param>
		public void changeGraphicSettings(string mode, string val)
		{
			switch (mode)
			{
				case GraphicsContext.GC_COLOR:
					mGC.setColorAttribute(val);
					break;
				case GraphicsContext.GC_DRAWMODE:
					mGC.setDrawModeAttribute(val);
					break;
				case GraphicsContext.GC_LUT:
					mGC.setLutAttribute(val);
					break;
				case GraphicsContext.GC_PAINT:
					mGC.setPaintAttribute(val);
					break;
				case GraphicsContext.GC_SHAPE:
					mGC.setShapeAttribute(val);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Changes the current graphical context by setting the specified mode
		/// (constant starting by GC_*) to the specified value.
		/// </summary>
		/// <param name="mode">
		/// Constant that is provided by the class GraphicsContext
		/// and describes the mode that has to be changed, 
		/// e.g., GraphicsContext.GC_LINEWIDTH
		/// </param>
		/// <param name="val">
		/// Value, provided as an integer, the mode is to be changed to, 
		/// e.g., 5 
		/// </param>
		public void changeGraphicSettings(string mode, int val)
		{
			switch (mode)
			{
				case GraphicsContext.GC_COLORED:
					mGC.setColoredAttribute(val);
					break;
				case GraphicsContext.GC_LINEWIDTH:
					mGC.setLineWidthAttribute(val);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Changes the current graphical context by setting the specified mode
		/// (constant starting by GC_*) to the specified value.
		/// </summary>
		/// <param name="mode">
		/// Constant that is provided by the class GraphicsContext
		/// and describes the mode that has to be changed, 
		/// e.g.,  GraphicsContext.GC_LINESTYLE
		/// </param>
		/// <param name="val">
		/// Value, provided as an HTuple instance, the mode is 
		/// to be changed to, e.g., new HTuple(new int[]{2,2})
		/// </param>
		public void changeGraphicSettings(string mode, HTuple val)
		{
			switch (mode)
			{
				case GraphicsContext.GC_LINESTYLE:
					mGC.setLineStyleAttribute(val);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Clears all entries from the graphical context list
		/// </summary>
		public void clearGraphicContext()
		{
			mGC.clear();
		}

		/// <summary>
		/// Returns a clone of the graphical context list (hashtable)
		/// </summary>
		public Hashtable getGraphicContext()
		{
			return mGC.copyContextList();
		}

        /// <summary>
        /// Registers an instance of an ROIController with this window 
        /// controller (and vice versa).
        /// </summary>
        /// <param name="rC"> 
        /// Controller that manages interactive ROIs for the HALCON window 
        /// </param>
        protected internal void setROIController(ROIController rC)
        {
            roiManager = rC;
            rC.setViewController(this);
            this.setViewState(HWndCtrl.MODE_VIEW_NONE);
        }
        /// <summary>
        /// 添加设定显示的图像
        /// </summary>
        /// <param name="image"></param>
        protected internal void addImageShow(HObject image)
        {
            addIconicVar(image);
        }


		#region 再次显示region和 xld
		public void addRegion(HObject hObj)
		{
			this.addRegion(hObj, "green", "fill", 1);
		}

		public void addRegion(HObject hObj, string color, string drawmode)
		{
			this.addRegion(hObj, color, drawmode, 1);
		}

		public void addRegion(HObject hObj, string color, string drawmode, int lineWidth)
		{
			lock (this)
			{
				try
				{
					if (color == null)
					{
						color = "red";
					}
					if (drawmode == null)
					{
						drawmode = "margin";
					}
					//hzy20220602,hv_window为空防呆
					if (hv_window == null)
					{
						return;
					}
					///////////////////////////////
					hv_window.SetColor(color);
					hv_window.SetDraw(drawmode);
					hv_window.SetLineWidth(lineWidth);
					if (hObj != null && hObj.IsInitialized())
					{
						HObject hobject = new HObject(hObj);
						this.hRegionList.Add(new HRegionEntry(hobject, color, drawmode, lineWidth, ""));
						hv_window.DispObj(hobject);
					}
				}
				catch { }
			}
		}
        /// <summary>
        /// hObjectList用来存储存入的HObject
        /// </summary>
        private List<HRegionEntry> hRegionList = new List<HRegionEntry>();

		public void clearList(bool bClearImage = true)
		{
			if (this.HObjImageList != null && this.HObjImageList.Count > 0)
			{
				for (int i = this.HObjImageList.Count - 1; i >= 0; i--)
				{
					if (this.HObjImageList[i] != null && (i != 0 || bClearImage))
					{
						((HObjectEntry)this.HObjImageList[i]).clear();
						this.HObjImageList.RemoveAt(i);
					}
				}

			}
		}
		public static bool IsDesignMode()
		{
			bool returnFlag = false;

#if DEBUG
			if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
			{
				returnFlag = true;
			}
			else if (Process.GetCurrentProcess().ProcessName == "devenv")
			{
				returnFlag = true;
			}
#endif

			return returnFlag;
		}

      
        /// <summary>
        /// 每次传入新的背景Image时,清空hObjectList,避免内存没有被释放
        /// </summary>
        public void clearHRegionList()
		{
			try
			{
				foreach (HRegionEntry hregionEntry in this.hRegionList)
				{
					hregionEntry.clear();
				}
				this.hRegionList.Clear();
			}
			catch { }
		}

		/// <summary>
		/// 将hObjectList中的HObject,按照先后顺序显示出来
		/// </summary>


		#endregion
		public HWndCtrl.ShowZoomPercent showZoomPercent;
		public delegate void ShowZoomPercent(double per);

	}//end of class
}//end of namespace
