using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSE
{
    [Serializable]
    public class Point2D
    {
        double x;
        double y;

        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        public Point2D(double x, double y)
        {
            this.x = x;
            this.y = y; 
        }
        public Point2D()
        {
            this.x =0;
            this.y =0;
        }

        public static Point2D operator +(Point2D a, Point2D b)
        {
            Point2D res = new Point2D();
            res.X =a.x + b.x;   
            res.Y =a.y + b.y;   
            return res;
        }
        public static Point2D operator -(Point2D a, Point2D b)
        {
            Point2D res = new Point2D();
            res.X = a.x - b.x;
            res.Y = a.y - b.y;
            return res;
        }
    }
    [Serializable]
    public class Line2D
    {
        Point2D startP=new Point2D(0,0);

        Double radian = 0;
      /// <summary>
      /// 线结构起始点
      /// </summary>
        public Point2D StartP
        {
            get { return startP; }
            set { startP = value; }
        }
        /// <summary>
        /// 线结构 弧度值
        /// </summary>
        public double Radian
        {
            get { return radian; }
            set { radian = value; }
        }

      /// <summary>
      /// 构造线结构
      /// </summary>
      /// <param name="startP">起始点</param>
      /// <param name="Angle">角度/弧度</param>
      /// <param name="isRad">为True Angle 传入为弧度 为False传入为角度</param>
        public Line2D(Point2D startP, double Angle,bool isRad)
        {
            if (isRad)
            {
                
                this.radian = Angle;
            }
            else
            {
                SetAngle(Angle);
            }
            this.startP = startP;
        }
        public Line2D()
        {
            
        }
        /// <summary>
        /// 通过传入角度 设定弧度值
        /// </summary>
        /// <param name="Angle">角度</param>
        public void SetAngle(double Angle)
        {
            Radian = Angle * Math.PI/180.0; 
        }
        /// <summary>
        /// 获取角度
        /// </summary>
        /// <returns>返回角度值</returns>
        public Double GetAngle()
        {
            return (Radian*180.0 / Math.PI);
        }
       
    }

    [Serializable]
    public class Segment2D
    {
        Point2D startP = new Point2D(0, 0);
        Point2D endP = new Point2D(1, 1);
        /// <summary>
        /// 线段结构起始点
        /// </summary>
        public Point2D StartP
        {
            get { return startP; }
            set { startP = value; }
        }
        /// <summary>
        /// 线段结构终点
        /// </summary>
        public Point2D EndP
        {
            get { return endP; }
            set { endP = value; }
        }

        /// <summary>
        /// 构造线段结构
        /// </summary>
        /// <param name="startP">起始点</param>
        /// <param name="endP">终点</param>

        public Segment2D(Point2D startP, Point2D endP)
        {
            
            this.startP = startP;
            this.endP = endP;
        }
        public Segment2D()
        {

        }
        
        /// <summary>
        /// 获取角度 -180--+180
        /// </summary>
        /// <returns>返回角度值</returns>
        public Double GetAngle()
        {
            double r = 0;

                r= Math.Atan2(EndP.Y - StartP.Y, EndP.X - StartP.X)*180/Math.PI; 
            
            
            return r;
        }
        /// <summary>
        /// 获取弧度 -π--+π
        /// </summary>
        /// <returns>返回弧度值</returns>
        public Double GetRadian()
        {
            double r = 0;

            r = Math.Atan2(EndP.Y - StartP.Y, EndP.X - StartP.X);

            return r;
        }
    }

    [Serializable]
    public class Circle2D
    {
        Point2D centerP = new Point2D(0, 0);
        double r = 1;
        /// <summary>
        /// 圆结构 圆心
        /// </summary>
        public Point2D CenterP
        {
            get { return centerP; }
            set { centerP = value; }
        }
        /// <summary>
        /// 圆结构 半径
        /// </summary>
        public double R
        {
            get { return r; }
            set { r = value; }
        }

        /// <summary>
        /// 构造圆结构
        /// </summary>
        /// <param name="centerP">圆心</param>
        /// <param name="r">半径</param>

        public Circle2D(Point2D centerP, double r)
        {

            this.centerP = centerP;
            this.r = r;
        }
        public Circle2D()
        {

        }

        
    }
    [Serializable]
    public class Arc2D: Circle2D
    {
        double startAngle =0;
        double endAngle = Math.PI;

        /// <summary>
        /// 圆弧结构 起始角度
        /// </summary>
        public double StartAngle
        {
            get { return startAngle; }
            set { startAngle = value; }
        }
        /// <summary>
        /// 圆弧结构 结束角度
        /// </summary>
        public double EndAngle
        {
            get { return endAngle; }
            set { endAngle = value; }
        }
        /// <summary>
        /// 构造圆弧结构
        /// </summary>
        /// <param name="centerP">圆心</param>
        /// <param name="r">半径</param>
        /// <param name="r">起始角度</param>
        /// <param name="r">结束角度</param>
        public Arc2D(Point2D centerP, double r, double startAngle, double endAngle)
        {

            this.CenterP = centerP;
            this.R = r;
            this.startAngle = startAngle;   
            this.endAngle = endAngle;   
        }
        public Arc2D()
        {

        }


    }
}
