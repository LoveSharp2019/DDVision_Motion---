using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HalconDotNet;

namespace Lxc.VisionPlus.ImageView
{
	public class ThresholdPlot
	{
		public string XName
		{
			get
			{
				return this.string_0;
			}
			private set
			{
				this.string_0 = value;
			}
		}
        
		public string YName
		{
			get
			{
				return this.string_1;
			}
			set
			{
				this.string_1 = value;
			}
		}
        
		public ThresholdPlot(Control panel, bool useMouseHandle)
		{

            GrawAxis(panel);
            panel.Paint -= this.method_6;
            panel.Paint += this.method_6;
			if (useMouseHandle)
			{
                panel.MouseMove -= this.method_5;
                panel.MouseMove += this.method_5;
			}
		}
        public void GrawAxis(Control panel)
        {
            this.graphics_0 = panel.CreateGraphics();
            this.YAxisToPanelLeftDis = 55f;
            this.float_0 = (float)(panel.Size.Width - YAxisToPanelLeftDis);
            this.float_1 = (float)(panel.Size.Height - 22);
          
            this.float_4 = (float)(panel.Size.Height - 22);
            this.float_2 = 5f;
            this.int_2 = (int)(this.float_0 + this.YAxisToPanelLeftDis - this.float_2);
            this.int_3 = (int)this.float_1;
            this.int_1 = 0;
            float num = 0f;
            float num2 = 0f;
            this.float_8 = num;
            this.float_7 = num2;
            this.int_0 = 5;
            this.float_5 =255f;
            this.float_6 = 255f;
            this.pen_0 = new Pen(Color.DarkGray, 1f);
            this.pen_1 = new Pen(Color.FromArgb(192, 64, 0), 1f);//直方图颜色
            this.pen_2 = new Pen(Color.LightSteelBlue, 1f);//鼠标跟随十字线颜色
            this.pen_2.DashStyle = DashStyle.Dash;
            this.solidBrush_0 = new SolidBrush(Color.DarkGray);
            this.solidBrush_1 = new SolidBrush(Color.FromArgb(64,64,64));//背景颜色
            this.AychPliFv = new Font("Arial",8f, FontStyle.Bold);
       
            this.stringFormat_0 = new StringFormat();
            this.stringFormat_0.Alignment = StringAlignment.Far;
            this.bitmap_0 = new Bitmap(panel.Size.Width, panel.Size.Height);
            this.graphics_1 = Graphics.FromImage(this.bitmap_0);
            this.XName = "X";
            this.YName = "Y";
            this.resetPlot();
        }
		public void setLabel(string x, string y)
		{
			this.XName = x;
			this.YName = y;
		}
        
		public ThresholdPlot(Control panel)
		{

		}
        
		public void setOrigin(int x, int y)
		{
			if (x >= 1 && y >= 1)
			{
				float num = this.YAxisToPanelLeftDis;
				this.YAxisToPanelLeftDis = (float)x;
				this.float_4 = (float)y;
				this.float_0 = this.float_0 + num - this.YAxisToPanelLeftDis;
				this.float_1 = this.float_4;
				this.int_2 = (int)(this.float_0 + this.YAxisToPanelLeftDis - this.float_2);
				this.int_3 = (int)this.float_1;
			}
		}
        
		public void setAxisAdaption(int mode, float val)
		{
			if (mode != 3)
			{
				this.int_0 = mode;
			}
			else
			{
				this.int_0 = mode;
				this.float_6 = ((val > 0f) ? val : 255f);
			}
		}
        
		public void setAxisAdaption(int mode)
		{
			this.setAxisAdaption(mode, -1f);
		}
        
		public void drawFunction(HTuple tuple)
		{
			if (tuple.Length == 0)
			{
				this.resetPlot();
			}
			else
			{
				HTuple htuple = tuple.TupleSortIndex();
				int num = tuple.Length - 1;
				int num2 = (int)tuple[htuple[htuple.Length - 1].I].D;
				this.float_5 = (float)num;
				int num3 = this.int_0;
				if (num3 != 4)
				{
					if (num3 == 5)
					{
						this.float_6 = (float)num2;
					}
				}
				else
				{
					this.float_6 = (((float)num2 > this.float_6) ? ((float)num2) : this.float_6);
				}
				this.graphics_1.Clear(Color.FromArgb(64, 64, 64));
				this.graphics_1.FillRectangle(this.solidBrush_1, this.YAxisToPanelLeftDis, 0f, this.float_0, this.float_1);
				float float_ = this.method_4();
				this.method_1(tuple, float_);
                this.graphics_1.RotateTransform(-45);
                Brush sd = new SolidBrush(Color.FromArgb(80,80,80));
                this.graphics_1.DrawString("LiXiangChun", new Font("微软雅黑", 14), sd, new Point(10, 140));
                this.graphics_1.DrawString("LiXiangChun", new Font("微软雅黑", 14), sd, new Point(20, 180));
                this.graphics_1.Flush();
				this.graphics_0.DrawImageUnscaled(this.bitmap_0, 0, 0);

                this.graphics_0.Flush();
			}
		}
        
		public void resetPlot()
		{
			this.graphics_1.Clear(Color.FromArgb(64, 64, 64));
			this.graphics_1.FillRectangle(this.solidBrush_1, this.YAxisToPanelLeftDis, 0f, this.float_0, this.float_1);
			this.hfunction1D_0 = null;
			this.method_4();
			this.graphics_1.Flush();
			this.method_0();
		}
        
		private void method_0()
		{
			this.graphics_0.DrawImageUnscaled(this.bitmap_0, 0, 0);
			this.graphics_0.Flush();
		}
        
		private void method_1(HTuple htuple_0, float float_9)
		{
			if (float_9 > 1f)
			{
				this.pointF_0 = this.method_2(htuple_0);
			}
			else
			{
				this.pointF_0 = this.method_3(htuple_0);
			}
			int num = this.pointF_0.Length;
			this.hfunction1D_0 = new HFunction1D(htuple_0);
			//for (int i = 0; i < num - 1; i++)
			//{
			//	this.graphics_1.DrawLine(this.pen_1, this.pointF_0[i], this.pointF_0[i + 1]);
			//}
            GraphicsPath s = new GraphicsPath();
            s.AddLines(pointF_0);
           // s.FillMode = FillMode.Winding;
            this.graphics_1.DrawPath(this.pen_1,s);
         this.graphics_1.FillPath(this.pen_1.Brush, s);

        }
        
		private PointF[] method_2(HTuple htuple_0)
		{
			float num = this.float_5;
			float num2 = this.float_6;
			this.float_7 = ((num != 0f) ? ((this.float_0 - this.float_2) / num) : 0f);
			this.float_8 = ((num2 != 0f) ? ((this.float_1 - this.float_2) / num2) : 0f);
			int length = htuple_0.Length;
			PointF[] array = new PointF[length+2];
            array[0] = new PointF(this.YAxisToPanelLeftDis, this.float_1);

            for (int i = 1; i < length+1; i++)
			{
				float num3 = (float)htuple_0[i-1].D;
				float x = this.YAxisToPanelLeftDis + (float)(i-1) * this.float_7;
				float y = this.float_1 - num3 * this.float_8;
				array[i] = new PointF(x, y);
			}
            array[length+1] = new PointF(this.YAxisToPanelLeftDis + (float)(length-1) * this.float_7, this.float_1);
            return array;
		}
        
		private PointF[] method_3(HTuple htuple_0)
		{
			float num = this.float_5;
			float num2 = this.float_6;
			this.float_7 = ((num != 0f) ? ((this.float_0 - this.float_2) / num) : 0f);
			this.float_8 = ((num2 != 0f) ? ((this.float_1 - this.float_2) / num2) : 0f);
			int length = htuple_0.Length;
			PointF[] array = new PointF[length * 2+2];
			float y = 0f;
			int num3 = 1;
			float num4;
			float x;
			for (int i = 0; i < length; i++)
			{
				num4 = (float)htuple_0[i].D;
				x = this.YAxisToPanelLeftDis + (float)i * this.float_7 - this.float_7 / 2f;
				y = this.float_1 - num4 * this.float_8;
				array[num3] = new PointF(x, y);
				num3++;
				x = this.YAxisToPanelLeftDis + (float)i * this.float_7 + this.float_7 / 2f;
				array[num3] = new PointF(x, y);
				num3++;
			}
			num3--;
			x = this.YAxisToPanelLeftDis + (float)(length - 1) * this.float_7;
			array[num3] = new PointF(x, y);
			num4 = (float)htuple_0[0].D;
			x = this.YAxisToPanelLeftDis;
			y = this.float_1 - num4 * this.float_8;
			array[0] = new PointF(this.YAxisToPanelLeftDis - this.float_7 / 2f, this.float_1);
            array[array.Length-1] = new PointF(array[array.Length - 2].X, this.float_1);
            return array;
		}
        
		private float method_4()
		{
			float num = 0f;
			float num2 = 5f;
			float num3 = this.YAxisToPanelLeftDis;
			float num4 = this.float_4;
			float num5 = this.float_5;
			if ((double)num5 != 0.0)
			{
				num = (this.float_0 - this.float_2) / num5;
			}
			float num6;
			if ((double)num > 10.0)
			{
				num6 = 1f;
			}
			else if (num > 2f)
			{
				num6 = 10f;
			}
			else if ((double)num > 0.2)
			{
				num6 = 100f;
			}
			else
			{
				num6 = 1000f;
			}
			float num7 = 0f;
			float num8 = num4;
			float num9 = num * num5;
			this.graphics_1.DrawLine(this.pen_0, num3, num4, num3 + this.float_0 - this.float_2, num4);
			this.graphics_1.DrawLine(this.pen_0, num3 + num7, num8, num3 + num7, num8 + 6f);
			this.graphics_1.DrawString(string.Concat(0), this.AychPliFv, this.solidBrush_0, num3 + num7 + 4f, num8 + 8f, this.stringFormat_0);
			this.graphics_1.DrawLine(this.pen_0, num3 + num9, num8, num3 + num9, num8 + 6f);
			this.graphics_1.DrawString(string.Concat((int)num5), this.AychPliFv, this.solidBrush_0, num3 + num9 + 4f, num8 + 8f, this.stringFormat_0);
			float num10 = (float)((int)(num5 / num6));
			num10 = ((num6 == 10f) ? (num10 - 1f) : num10);
			int num11 = 1;
			while ((float)num11 <= num10)
			{
				num7 = num6 * (float)num11 * num;
				if (num9 - num7 >= 20f)
				{
					this.graphics_1.DrawLine(this.pen_0, num3 + num7, num8, num3 + num7, num8 + 6f);
					this.graphics_1.DrawString(string.Concat((int)((float)num11 * num6)), this.AychPliFv, this.solidBrush_0, num3 + num7 + 5f, num8 + 8f, this.stringFormat_0);
				}
				num11++;
			}
			float result = num6;
			float num12 = this.float_6;
			if ((double)num12 != 0.0)
			{
				num = (this.float_1 - this.float_2) / num12;
			}
			if ((double)num > 10.0)
			{
				num6 = 1f;
			}
			else if (num > 2f)
			{
				num6 = 10f;
			}
			else if ((double)num > 0.8)
			{
				num6 = 50f;
			}
			else if ((double)num > 0.12)
			{
				num6 = 100f;
			}
			else
			{
				num6 = 1000f;
			}
			num7 = num3;
			float num13 = num4 - num * num12;
			this.graphics_1.DrawLine(this.pen_0, num3, num4, num3, num4 - (this.float_1 - this.float_2));
			this.graphics_1.DrawLine(this.pen_0, num7, num4, num7 - 10f, num4);
			this.graphics_1.DrawString(string.Concat(0), this.AychPliFv, this.solidBrush_0, num7 - 12f, num4 - num2, this.stringFormat_0);
			this.graphics_1.DrawLine(this.pen_0, num7, num13, num7 - 10f, num13);
			this.graphics_1.DrawString(string.Concat(num12), this.AychPliFv, this.solidBrush_0, num7 - 12f, num13 - num2, this.stringFormat_0);
			num10 = (float)((int)(num12 / num6));
			num10 = ((num6 == 10f) ? (num10 - 1f) : num10);
			int num14 = 1;
			while ((float)num14 <= 20)
			{
				num8 = num4 - num12 / 20 * (float)num14 * num;
				//if (num8 - num13 >= 10f && (num14 % 5 == 0))
				//{
					this.graphics_1.DrawLine(this.pen_0, num7, num8, num7 - 10f, num8);
					this.graphics_1.DrawString(string.Concat((int)((float)num14 * num12/20)), this.AychPliFv, this.solidBrush_0, num7 - 12f, num8 - num2, this.stringFormat_0);
				//}
				num14++;
			}
			return result;
		}
        
		private void method_5(object sender, MouseEventArgs e)
		{
			int x = e.X;
			if (this.int_1 != x && (float)x >= this.YAxisToPanelLeftDis && x <= this.int_2 && this.hfunction1D_0 != null)
			{
				this.int_1 = x;
				int num = (int)Math.Round((double)(((float)x - this.YAxisToPanelLeftDis) / this.float_7));
				HTuple yvalueFunct1d = this.hfunction1D_0.GetYValueFunct1d(new HTuple(num), "zero");
				float num2 = (float)yvalueFunct1d[0].D;
				float num3 = this.float_1 - num2 * this.float_8;
				this.graphics_0.DrawImageUnscaled(this.bitmap_0, 0, 0);
				this.graphics_0.DrawLine(this.pen_2, x, 0, x, this.int_3);
				this.graphics_0.DrawLine(this.pen_2, this.YAxisToPanelLeftDis, num3, (float)this.int_2 + this.float_2, num3);
                PointF pp = GetIntersection(new PointF(x,0), new PointF(x, this.int_3), new PointF(this.YAxisToPanelLeftDis, num3), new PointF((float)this.int_2 + this.float_2, num3));

                string s = string.Format("{0}={1}", this.XName, num);
				string s2 = string.Format("{0}={1}", this.YName, (int)num2);
                if (num > 200)
                {
                    if (pp.Y<int_3/3)
                    {
                        this.graphics_0.DrawString(s, this.AychPliFv, this.solidBrush_0, pp.X - 100, pp.Y+2);
                        this.graphics_0.DrawString(s2, this.AychPliFv, this.solidBrush_0, pp.X - 100, pp.Y + 17);
                    }
                    else
                    {
                        this.graphics_0.DrawString(s, this.AychPliFv, this.solidBrush_0, pp.X - 100, pp.Y-40);
                        this.graphics_0.DrawString(s2, this.AychPliFv, this.solidBrush_0, pp.X - 100, pp.Y -25);
                    }
                 
                }
                else
                {
                    if (pp.Y < int_3 / 3)
                    {
                        this.graphics_0.DrawString(s, this.AychPliFv, this.solidBrush_0, pp.X +5, pp.Y + 2);
                        this.graphics_0.DrawString(s2, this.AychPliFv, this.solidBrush_0, pp.X + 5, pp.Y + 17);
                    }
                    else
                    {
                        this.graphics_0.DrawString(s, this.AychPliFv, this.solidBrush_0, pp.X + 5, pp.Y - 40);
                        this.graphics_0.DrawString(s2, this.AychPliFv, this.solidBrush_0, pp.X + 5, pp.Y - 25);
                    }
                }
			
				this.graphics_0.Flush();
			}
		}
        public static PointF GetIntersection(PointF lineFirstStar, PointF lineFirstEnd, PointF lineSecondStar, PointF lineSecondEnd)
        {
            /*
             * L1，L2都存在斜率的情况：
             * 直线方程L1: ( y - y1 ) / ( y2 - y1 ) = ( x - x1 ) / ( x2 - x1 ) 
             * => y = [ ( y2 - y1 ) / ( x2 - x1 ) ]( x - x1 ) + y1
             * 令 a = ( y2 - y1 ) / ( x2 - x1 )
             * 有 y = a * x - a * x1 + y1   .........1
             * 直线方程L2: ( y - y3 ) / ( y4 - y3 ) = ( x - x3 ) / ( x4 - x3 )
             * 令 b = ( y4 - y3 ) / ( x4 - x3 )
             * 有 y = b * x - b * x3 + y3 ..........2
             * 
             * 如果 a = b，则两直线平等，否则， 联解方程 1,2，得:
             * x = ( a * x1 - b * x3 - y1 + y3 ) / ( a - b )
             * y = a * x - a * x1 + y1
             * 
             * L1存在斜率, L2平行Y轴的情况：
             * x = x3
             * y = a * x3 - a * x1 + y1
             * 
             * L1 平行Y轴，L2存在斜率的情况：
             * x = x1
             * y = b * x - b * x3 + y3
             * 
             * L1与L2都平行Y轴的情况：
             * 如果 x1 = x3，那么L1与L2重合，否则平等
             * 
            */
            float a = 0, b = 0;
            int state = 0;
            if (lineFirstStar.X != lineFirstEnd.X)
            {
                a = (lineFirstEnd.Y - lineFirstStar.Y) / (lineFirstEnd.X - lineFirstStar.X);
                state |= 1;
            }
            if (lineSecondStar.X != lineSecondEnd.X)
            {
                b = (lineSecondEnd.Y - lineSecondStar.Y) / (lineSecondEnd.X - lineSecondStar.X);
                state |= 2;
            }
            switch (state)
            {
                case 0: //L1与L2都平行Y轴
                    {
                        if (lineFirstStar.X == lineSecondStar.X)
                        {
                            //throw new Exception("两条直线互相重合，且平行于Y轴，无法计算交点。");
                            return new PointF(0, 0);
                        }
                        else
                        {
                            //throw new Exception("两条直线互相平行，且平行于Y轴，无法计算交点。");
                            return new PointF(0, 0);
                        }
                    }
                case 1: //L1存在斜率, L2平行Y轴
                    {
                        float x = lineSecondStar.X;
                        float y = (lineFirstStar.X - x) * (-a) + lineFirstStar.Y;
                        return new PointF(x, y);
                    }
                case 2: //L1 平行Y轴，L2存在斜率
                    {
                        float x = lineFirstStar.X;
                        //网上有相似代码的，这一处是错误的。你可以对比case 1 的逻辑 进行分析
                        //源code:lineSecondStar * x + lineSecondStar * lineSecondStar.X + p3.Y;
                        float y = (lineSecondStar.X - x) * (-b) + lineSecondStar.Y;
                        return new PointF(x, y);
                    }
                case 3: //L1，L2都存在斜率
                    {
                        if (a == b)
                        {
                            // throw new Exception("两条直线平行或重合，无法计算交点。");
                            return new PointF(0, 0);
                        }
                        float x = (a * lineFirstStar.X - b * lineSecondStar.X - lineFirstStar.Y + lineSecondStar.Y) / (a - b);
                        float y = a * x - a * lineFirstStar.X + lineFirstStar.Y;
                        return new PointF(x, y);
                    }
            }
            // throw new Exception("不可能发生的情况");
            return new PointF(0, 0);
        }
        private void method_6(object sender, PaintEventArgs e)
		{
			this.method_0();
		}
        
		private Graphics graphics_0;
        
		private Graphics graphics_1;
        
		private Pen pen_0;
        
		private Pen pen_1;
        
		private Pen pen_2;
        
		private SolidBrush solidBrush_0;
        
		private SolidBrush solidBrush_1;
        
		private Font AychPliFv;
        
		private StringFormat stringFormat_0;
        
		private Bitmap bitmap_0;
        
		private float float_0;
        
		private float float_1;
        
		private float float_2;
        
		private float YAxisToPanelLeftDis;
        
		private float float_4;
        
		private PointF[] pointF_0;
        
		private HFunction1D hfunction1D_0;
        
		private int int_0;
        
		private float float_5;
        
		private float float_6;
        
		private float float_7;
        
		private float float_8;
        
		public const int AXIS_RANGE_FIXED = 3;
        
		public const int AXIS_RANGE_INCREASING = 4;
        
		public const int AXIS_RANGE_ADAPTING = 5;
        
		private int int_1;
        
		private int int_2;
        
		private int int_3;
        
		private string string_0;
        
		private string string_1;
	}
}
