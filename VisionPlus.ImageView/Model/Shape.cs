using System;
using HalconDotNet;

namespace Lxc.VisionPlus.ImageView.Model
{
    [Serializable]
    public class Shape
    {
        private string color = "yellow";

        public string Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        private string _type;
        public string Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        // class members of inheriting ROI classes
        protected int NumHandles;
        protected int activeHandleIdx;

        /// <summary>
        /// Flag to define the ROI to be 'positive' or 'negative'.
        /// </summary>
        protected int OperatorFlag;

        /// <summary>Parameter to define the line style of the ROI.</summary>
        public HTuple flagLineStyle;

        /// <summary>Constant for a positive ROI flag.</summary>
        public const int POSITIVE_FLAG = ROIController.MODE_ROI_POS;

        /// <summary>Constant for a negative ROI flag.</summary>
        public const int NEGATIVE_FLAG = ROIController.MODE_ROI_NEG;

        public const int ROI_TYPE_LINE = 10;
        public const int ROI_TYPE_CIRCLE = 11;
        public const int ROI_TYPE_CIRCLEARC = 12;
        public const int ROI_TYPE_RECTANCLE1 = 13;
        public const int ROI_TYPE_RECTANGLE2 = 14;


        protected HTuple posOperation = new HTuple();
        protected HTuple negOperation = new HTuple(new int[] { 2, 2 });

        /// <summary>Constructor of abstract ROI class.</summary>
        public Shape() { }

        public virtual void createRectangle1(double row1, double col1, double row2, double col2) { }
        public virtual void createRectangle2(double row, double col, double phi, double length1, double length2) { }
        public virtual void createCircle(double row, double col, double radius) { }
        public virtual void createObject(HObject obj) { }
        public virtual void createEllipse(double row, double col, double phi, double radius1, double radius2) { }
        public virtual void createLine(double beginRow, double beginCol, double endRow, double endCol) { }

        /// <summary>Creates a new ROI instance at the mouse position.</summary>
        /// <param name="midX">
        /// x (=column) coordinate for ROI
        /// </param>
        /// <param name="midY">
        /// y (=row) coordinate for ROI
        /// </param>
        public virtual void createShape(double midX, double midY) { }

        /// <summary>Paints the ROI into the supplied window.</summary>
        /// <param name="window">HALCON window</param>
        public virtual void draw(HalconDotNet.HWindow window, double scale) { }



        /// <summary> 
        /// Paints the active handle of the ROI object into the supplied window. 
        /// </summary>
        /// <param name="window">HALCON window</param>
        public virtual void displayActive(HalconDotNet.HWindow window) { }

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
}
