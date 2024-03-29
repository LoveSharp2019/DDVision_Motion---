using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSE.Core
{
    public class Method
    {
       
       /// <summary>
       /// 获取当前日期 -中文
       /// </summary>
       /// <returns></returns>
        public static string GetDay()
        {
          
            return DateTime.Now.ToString("dddd");
        }
        /// <summary>
        /// 截屏整个屏幕
        /// </summary>
        public static void Screenshot()
        { //屏幕宽
            int iWidth = Screen.PrimaryScreen.Bounds.Width;
            //屏幕高
            int iHeight = Screen.PrimaryScreen.Bounds.Height;
            //按照屏幕宽高创建位图
            Image img = new Bitmap(iWidth, iHeight);
            //从一个继承自Image类的对象中创建Graphics对象
            Graphics gc = Graphics.FromImage(img);
            //抓屏并拷贝到myimage里
            gc.CopyFromScreen(new System.Drawing.Point(0, 0), new System.Drawing.Point(0, 0), new Size(iWidth, iHeight));
            //this.BackgroundImage = img;

            //保存
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            System.Windows.Forms.SaveFileDialog dig_saveImage = new System.Windows.Forms.SaveFileDialog();
            dig_saveImage.Title = ("请选择图像保存路径");
            dig_saveImage.Filter = "图像文件(*.jpg)|*.jpg|Image File|*.tif|Image File(*.png)|*.png|Image File(*.bmp)|*.bmp";
            dig_saveImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            dig_saveImage.FileName = DateTime.Now.ToString("yyyy_MM_dd");
            if (dig_saveImage.ShowDialog() == DialogResult.OK)
                img.Save(dig_saveImage.FileName);       //保存位图
           }
            /// <summary>
            /// 生成Point集合的提示信息
            /// </summary>
            /// <param name="list"></param>
            /// <returns></returns>
            public static string FormatShowTip(object value)
        {
            try
            {
                if (value == null)
                    return "空";

                string temp = value.ToString();
                string result = string.Empty;
                switch (temp)
                {
                    case "VisionAndMotionPro.XYU":
                        return string.Empty;

                    case "HObject":
                        return string.Empty;
                    case "VisionAndMotionPro.Point":
                        XY point = value as XY;

                        result = string.Format("{0}  {1}", point.X.ToString("0000.000"), point.Y.ToString("0000.000"));
                        return result;
                    case "Double":
                        break;
                    case "HalconDotNet.HObject":
                        result = "图形变量暂不支持显示";
                        return result;
                    case "System.Collections.Generic.List`1[VisionAndMotionPro.Point]":
                        List<XY> L_point = value as List<XY>;

                        for (int i = 0; i < L_point.Count; i++)
                        {
                            result += string.Format("{0} |  {1}  {2}\r\n", (i + 1), L_point[i].X.ToString("0000.000"), L_point[i].Y.ToString("0000.000"));
                        }
                        return result;
                    case "System.Collections.Generic.List`1[VisionAndMotionPro.XYU]":
                        List<XYU> L_xyu = value as List<XYU>;

                        for (int i = 0; i < L_xyu.Count; i++)
                        {
                            result += string.Format("{0} |  {1}  {2}  {3}\r\n", (i + 1), L_xyu[i].Point.X.ToString("0000.000"), L_xyu[i].Point.Y.ToString("0000.000"), L_xyu[i].U.ToString("0000.000"));
                        }
                        return result;
                    default:
                        result = value.ToString();
                        return result;
           
                }
                return string.Empty;
            }
            catch (Exception )
            {
                //Log.SaveError(ex);
                return string.Empty;
            }
        }
    }
}
