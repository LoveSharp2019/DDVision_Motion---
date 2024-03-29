using System;
using HalconDotNet;
using System.Collections.Generic;

namespace Lxc.VisionPlus.ImageView.Model
{

	/// <summary>
	/// This class is a base class containing virtual methods for handling
	/// ROIs. Therefore, an inheriting class needs to define/override these
	/// methods to provide the ROIController with the necessary information on
	/// its (= the ROIs) shape and position. The example project provides 
	/// derived ROI shapes for rectangles, lines, circles, and circular arcs.
	/// To use other shapes you must derive a new class from the base class 
	/// ROI and implement its methods.
	/// </summary>    
    [Serializable] 
	public class ROI
    {
       public enum ROIType
        {
			ROIRectangle1,
			ROIRectangle2,
			ROICircle,
			ROILine,
			ROIEllipse,
			ROICalipersLine,
			ROICalipersCircle,
			ROINurbs,
			ROICircularArc
        }
        private string color = "yellow";
        public double Row, Column;  // first handle
        public string Color
        {
            get { return this.color; }
            set { this.color = value; }
        }
        public bool IsModeROI = false;
        public string ModeROIColor = "yellow";
        private ROIType _type;
        public ROIType Type 
        { 
            get 
            {
                return this._type ;
            } 
            set 
            { 
                this._type = value;
            } 
        }

		// class members of inheriting ROI classes
		protected int   NumHandles;
		protected int	activeHandleIdx;

		/// <summary>
		/// Flag to define the ROI to be 'positive' or 'negative'.
		/// </summary>
		protected int     OperatorFlag;

		/// <summary>Parameter to define the line style of the ROI.</summary>
		public HTuple     flagLineStyle;

		/// <summary>Constant for a positive ROI flag.</summary>
		public const int  POSITIVE_FLAG	= ROIController.MODE_ROI_POS;

		/// <summary>Constant for a negative ROI flag.</summary>
		public const int  NEGATIVE_FLAG	= ROIController.MODE_ROI_NEG;

		public const int  ROI_TYPE_LINE       = 10;
		public const int  ROI_TYPE_CIRCLE     = 11;
		public const int  ROI_TYPE_CIRCLEARC  = 12;
		public const int  ROI_TYPE_RECTANCLE1 = 13;
		public const int  ROI_TYPE_RECTANGLE2 = 14;


		protected HTuple  posOperation = new HTuple();
		protected HTuple  negOperation = new HTuple(new int[] { 2, 2 });

		/// <summary>Constructor of abstract ROI class.</summary>
		public ROI() { }
        public virtual void createROINurbs(HTuple  rows, HTuple  cols) { }
        public virtual void createROINurbs(double imageHeight) { }
        public virtual void createRectangle1(double row1, double col1, double row2, double col2) { }
        public virtual void createInitRectangle1(double imageHeight) { }
        public virtual void createRectangle2(double row, double col, double phi, double length1, double length2) { }
        public virtual void createInitRectangle2(double imageHeight) { }
        public virtual void createCircle(double row,double col,double radius) { }
        public virtual void createCircularArc(double row, double col, double radius, double startPhi, double extentPhi, string direct) { }
        public virtual void createLine(double beginRow, double beginCol, double endRow, double endCol) { }

		/// <summary>Creates a new ROI instance at the mouse position.</summary>
		/// <param name="midX">
		/// x (=column) coordinate for ROI
		/// </param>
		/// <param name="midY">
		/// y (=row) coordinate for ROI
		/// </param>
		public virtual void createROI(double midX, double midY) { }

		/// <summary>Paints the ROI into the supplied window.</summary>
		/// <param name="window">HALCON window</param>
		public virtual void draw(HalconDotNet.HWindow window,int imageWidth,int imageHeight) { }

		/// <summary> 
		/// Returns the distance of the ROI handle being
		/// closest to the image point(x,y)
		/// </summary>
		/// <param name="x">x (=column) coordinate</param>
		/// <param name="y">y (=row) coordinate</param>
		/// <returns> 
		/// Distance of the closest ROI handle.
		/// </returns>
		public virtual double distToClosestHandle(double x, double y)
		{
			return -1;
		}

		/// <summary> 
		/// Paints the active handle of the ROI object into the supplied window. 
		/// </summary>
		/// <param name="window">HALCON window</param>
        public virtual void displayActive(HalconDotNet.HWindow window, int imageWidth, int imageHeight) { }

		/// <summary> 
		/// Recalculates the shape of the ROI. Translation is 
		/// performed at the active handle of the ROI object 
		/// for the image coordinate (x,y).
		/// </summary>
		/// <param name="x">x (=column) coordinate</param>
		/// <param name="y">y (=row) coordinate</param>
		public virtual void moveByHandle(double x, double y) { }

		/// <summary>Gets the HALCON region described by the ROI.</summary>
		public virtual HRegion getRegion()
		{
			return null;
		}

		public virtual double getDistanceFromStartPoint(double row, double col)
		{
			return 0.0;
		}
		/// <summary>
		/// Gets the model information described by 
		/// the ROI.
		/// </summary> 
		public virtual HTuple getModelData()
		{
			return null;
		}
        public virtual HTuple getRowsData()
        {
            return null;
        }
        public virtual HTuple getColsData()
        {
            return null;
        }
        public virtual void  getModelData(out HTuple  t1,out HTuple  t2)
        {
            t1 = new HTuple();
            t2 = new HTuple();
        }
		/// <summary>Number of handles defined for the ROI.</summary>
		/// <returns>Number of handles</returns>
		public int getNumHandles()
		{
			return NumHandles;
		}

		/// <summary>Gets the active handle of the ROI.</summary>
		/// <returns>Index of the active handle (from the handle list)</returns>
		public int getActHandleIdx()
		{
			return activeHandleIdx;
		}

		/// <summary>
		/// Gets the sign of the ROI object, being either 
		/// 'positive' or 'negative'. This sign is used when creating a model
		/// region for matching applications from a list of ROIs.
		/// </summary>
		public int getOperatorFlag()
		{
			return OperatorFlag;
		}

        public HXLDCont GenArrowContourXld( double hv_Row1, double hv_Column1,
     double hv_Row2, double hv_Column2, double hv_HeadLength, double hv_HeadWidth)
        {
            HXLDCont Arrowxld=new HXLDCont();
            HXLDCont TempArrowxld = new HXLDCont();

            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            HTuple hv_Length = new HTuple(), hv_ZeroLengthIndices = new HTuple();
            HTuple hv_DR = new HTuple(), hv_DC = new HTuple(), hv_HalfHeadWidth = new HTuple();
            double hv_RowP1, hv_ColP1;
            double hv_RowP2 , hv_ColP2 ;
            //计算两点长度
            HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Length);
            hv_ZeroLengthIndices.Dispose();
            if (hv_Length.D==0)
            {
               
            }

          
           
            hv_DR.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_DR = (1.0 * (hv_Row2 - hv_Row1)) / hv_Length;
            }
            hv_DC.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_DC = (1.0 * (hv_Column2 - hv_Column1)) / hv_Length;
            }
            hv_HalfHeadWidth.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_HalfHeadWidth = hv_HeadWidth / 2.0;
            }

            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_RowP1 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) + (hv_HalfHeadWidth * hv_DC);
            }

            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_ColP1 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) - (hv_HalfHeadWidth * hv_DR);
            }

            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_RowP2 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) - (hv_HalfHeadWidth * hv_DC);
            }

            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_ColP2 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) + (hv_HalfHeadWidth * hv_DR);
            }
            //
            //Finally create output XLD contour for each input point pair
           
                Arrowxld.GenContourPolygonXld(new HTuple(hv_Row1, hv_Row2, hv_RowP1, hv_Row2, hv_RowP2, hv_Row2),new HTuple(hv_Column1, hv_Column2, hv_ColP1, hv_Column2, hv_ColP2, hv_Column2));
      
            hv_Length.Dispose();
            hv_ZeroLengthIndices.Dispose();
            hv_DR.Dispose();
            hv_DC.Dispose();
            hv_HalfHeadWidth.Dispose();


            return Arrowxld;
        }
        /// <summary>
        /// Sets the sign of a ROI object to be positive or negative. 
        /// The sign is used when creating a model region for matching
        /// applications by summing up all positive and negative ROI models
        /// created so far.
        /// </summary>
        /// <param name="flag">Sign of ROI object</param>
        public void setOperatorFlag(int flag)
		{
			OperatorFlag = flag;

			switch (OperatorFlag)
			{
				case ROI.POSITIVE_FLAG:
					flagLineStyle = posOperation;
					break;
				case ROI.NEGATIVE_FLAG:
					flagLineStyle = negOperation;
					break;
				default:
					flagLineStyle = posOperation;
					break;
			}
		}
	}//end of class
}//end of namespace
