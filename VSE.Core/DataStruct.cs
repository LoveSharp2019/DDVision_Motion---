using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSE.Core
{
    internal class DataStruct
    {
    }
    /// <summary>
    /// 工具运行状态
    /// </summary>
    public enum ToolRunStatu
    {
        未运行,
        未启用,
        缺少输入搜索区域,
        未指定图像路径,
        无输入图像,
        未创建模板,
        未训练字符,
        无输入字符串,
        未指定输入图像,
        未指定ROI,
        未指定图像模板,
        缺少输入图像,
        未指定输入坐标点,
        输入项未链接源,
        输入图像不能被转化,
        文件夹内无图像,
        图像文件异常或路径不合法,
        未指定采集设备,
        成功,
        未知原因,
        定位结果U值超限,
        定位结果X值超限,
        定位结果Y值超限,
        未匹配到模板,
        未找到边,
        运行成功但是所有的特征都被设置不显示,
        未找到电池,
        电池形状异常或视野中有干扰物,
        相机实时状态下不可采集图像,
        未找到线,
        未扫描到条码,
        匹配数量不足,
        未指定以太网通讯端,
        未指定以太网触发命令,
        未建立通讯连接,
        相机未连接,
        采集图像时出错,
        未指定被引用标定工具,
        抓边工具特征点数量不足,
        抓圆工具特征点数量不足,
        图像队列数目超过设定上限,
    }
    /// <summary>
    /// 工具类型
    /// </summary>
    public enum ToolType
    {

        None,
        SDK_Halcon = 4,
        ImageAcq = 4,
        SaveImage,
        Binarization,
        Morphology,
        ImageFiltering,
        ImageDecomposition,
        FindROI,
        FindRegion,
        DefectDetection,
        CalibCheckerboard,
        CalibNPointTool,
        Transform,
        Match,
        BlobAnalyse,
        FindLine,
        FindCircle,
        FindCorner,
        P2PDistance,
        P2CDistance,
        P2LDistance,
        LLAngle,
        LLIntersect,
        LCIntersect,
        CCIntersect,
        RRegionGraySubtract,
        OCR,
        ReadCode,
        VerdictMeasure,
        Output

        //EyeHandCalib,
        //OneKeyEyeHandCalib,
        //OneDimensionalCalib,
        //CircleCalibration,
        //SubImage,


        //CreateROI,
        //ArrayRegion,
        //Mark,
        //CreatePosition,
        //Barcode,
        //RegionFeature,
        //RegionOperation,
        //QRCode,
        //Scaner_Kenyence,
        //KeyenceScanner1,
        //KeyenceScanner2,
        //Light_OPT,
        //OPTLightControl,
        //UpCamAlign,
        //DownCamAlign,
        //RotatePlatform,

        //DistancePL,
        //DistanceSS,

        //CodeEdit,
        //Label,
        //Output,
        //CreateLine,
        //CenterOfPP,
        //BuChang,
        //batteryFirstAlign,
        //XYPlatform,

        //ToStr,
        //PointAlign,
        //PointOffset,
        //AlignFit,
        //QuoteTrans,
        //ImagePreprocessing,
        //AlignWithoutCalibRotateCenter,
        //DisplayEdit,
        //DistancePP,
        //EthernetReceive,
        //EthernetSend,
        //DataAnalyse,
        //Measurement,
        //AngleLL,
    }
    /// <summary>
    /// 语言 
    /// </summary>
    public enum Language
    {
        Chinese,
        English,
    }
    /// <summary>
    /// 主题
    /// </summary>
    public enum Theme
    {
        Dark,
        Light,
        Blue
    }
    [Serializable]
    public class ToolParBase
    {

    }
    public enum MatchMode
    {
        BasedShape,
        BasedGray,
    }
    [Serializable]
    public class Variable
    {
        public Variable(int index, string type, string name, IsRW IsRW = IsRW.R)
        {
            this.index = index;
            this.type = type;
            this.name = name;
            this.IsRW = IsRW;
        }
        public IsRW IsRW { get; set; }
        public int index = 0;
        public string type = "Int";
        public string name = "Value";
        public object value = 0;
        public string info = string.Empty;
        public int variableType = 1;        //0表示系统变量     1表示自定义变量
    }
    [Serializable]
    public enum IsRW
    {
        R,
        W,
        RW
    }
    /// <summary>
    /// 工具的输入输出类
    /// </summary>
    [Serializable]
    public class ToolIO
    {
        public ToolIO() { }
        public ToolIO(string IOName1, object value1, DataType ioType1)
        {
            this.IOName = IOName1;
            this.value = value1;
            this.ioType = ioType1;
        }

        public string IOName;
        public object value;
        public DataType ioType;
    }

    /// <summary>
    /// 字符类型   白纸黑字|黑纸白字
    /// </summary>
    internal enum CharType
    {
        BlackChar,
        WhiteChar,
    }

    [Serializable]
    public struct Label
    {
        public string ValueType;
        public string ExpectValue;
        public string OutputItem;
        public string PreAddStr;
        public string Row;
        public string Col;
        public string Incolor;
        public string Size;
        public string DownLimit;
        public string UpLimit;
        public string OutColor;
    }
    public enum JobRunMode
    {
        RunAfterCall,
        LoopRunAfterStart,
    }
    public enum JobRunStatu
    {
        Succeed,
        Fail,
    }

    public enum SortMode
    {
        从上至下且从左至右,
        从左至右且从上至下,
        从上至下且从右至左,
        从左至右且从下至上,
        None
    }

    public enum DataType
    {
        String,
        Int,
        Double,
        Bool,
        Line,
        Circle,
        Ellipese,
        Segment,
        CircleArc,
        EllipseArc,
        Pose,
        XY,
        Region,
        Image,
    }
    //public enum DeviceType
    //{
    //    LightController_OPT,
    //    TCPIPSever,
    //    TCPIPClient,
    //}
    public enum TipType
    {
        Tip,
        Warn,
        Error,
    }
    public enum PLCBrand
    {
        Omron,
        Panasonic,
        Mitsubishi,
        Siemens,
        AB,
    }

    /// <summary>
    /// 运动控制卡类型 固高GTS系列|雷赛IOC0640系列
    /// </summary>
    public enum CardType
    {
        无,
        固高_GTS,
        雷赛_IOC0640,
        雷塞_DMC2210,
        雷塞_DMC2410,
        凌华_AMP204C,
        联赢_WMX,
        安川_MP3100,
    }

    /// <summary>
    /// 填充模式 填充|轮廓
    /// </summary>
    public enum FillMode
    {
        Fill,
        Margin,
    }

    /// <summary>
    /// 区域类型
    /// </summary>
    public enum RegionType
    {
        AllImage,
        Rectangle1,
        Rectangle2,
        Circle,
        Ellipse,
        MultPoint,
        Ring,
        Any,
        整幅图像,
        矩形,
        仿射矩形,
        圆,
        多点,
        椭圆,
        圆环,
        任意,
        InputRegion,        //此区域类型指来自于输入的区域
    }

    /// <summary>
    /// 标定类型 四点|九点
    /// </summary>
    public enum CalibType
    {
        Four_Point,
        Nine_Point,
    }

    /// <summary>
    /// 相机安装类型 眼在手外|眼在手上
    /// </summary>
    public enum FixedType
    {
        OutsideHand,
        OnHand,
    }

    public enum CommunicationType
    {
        None,
        Internet_Client,
        Internet_Sever,
        SerialPort,
        IO,
    }

    /// <summary>
    /// 视觉处理模式
    /// </summary>
    //public enum VisionManageModel
    //{
    //    AcqProSeparate,//取像，图片处理分离
    //    AcqProIntegration,//取像，图片处理一体
    //}

    [Serializable]
    public class Line
    {
        public Line()
        {
            _起点 = new XY();
            _终点 = new XY();
        }
        public Line(XY StartP,XY EndP)
        {
            _起点 = StartP;
            _终点 = EndP;
        }
        public Line(double StartPX, double StartPY, double EndPX, double EndPY)
        {
            _起点 = new XY(StartPX, StartPY);
            _终点 = new XY(EndPX, EndPY);
        }
        private XY _起点;

        public XY 起点
        {
            get { return _起点; }
            set { _起点 = value; }
        }
        private XY _终点;

        public XY 终点
        {
            get { return _终点; }
            set { _终点 = value; }
        }

        internal string ToShowTip()
        {
            return 起点.X.ToString() + " | " + 起点.Y.ToString() + " | " + 终点.X.ToString() + " | " + 终点.Y.ToString();
        }
        private HTuple _方向;
        public double 方向
        {
            get
            {
                HOperatorSet.AngleLx(起点.X, 起点.Y, 终点.X, 终点.Y, out _方向);
                return Math.Round(_方向.D, 3);
            }
        }
        public double GetAngle()
        {
            HTuple angle;
            HOperatorSet.AngleLx(起点.X, 起点.Y, 终点.X, 终点.Y, out angle);
            return angle;
        }
    }
    [Serializable]
    public class Circle
    {
        public Circle()
        {
            _圆心 = new XY();
            _半径 = 1;


        }
        public Circle(XY CenterP, double R)
        {
            _圆心 = CenterP;
            _半径 = R;
        }
        public Circle(double CenterPX, double CenterPY, double R)
        {
            _圆心 = new XY(CenterPX, CenterPY);
            _半径 = R;
        }
        private XY _圆心;

        public XY 圆心
        {
            get { return _圆心; }
            set { _圆心 = value; }
        }
       
        internal string ToShowTip()
        {
            return _圆心.X.ToString() + " | " + _圆心.Y.ToString() + " | " + _半径.ToString();
        }

        private HTuple _半径;
        public double 半径
        {
            get { return _半径; }
            set { _半径 = value; }
        }
        private double startRad; //
        private double endRad;//
        

        public double StartRad
        {
            get { return startRad; }
            set { startRad = value; }
        }

        public double EndRad
        {
            get { return endRad; }
            set { endRad = value; }
        }

    }

    /// <summary>
    /// 采集设备
    /// </summary>
    [Serializable]
    public class AcquistionDevice
    {
        public HTuple Handle;
        public string DeviceStr;
        public string InterfaceType;
        public string DeviceDescriptionStr;
        public double Exposure;
        public int MinExposure;
        public int MaxExposure;
    }

    /// <summary>
    /// 条码识别工具结果结构
    /// </summary>
    [Serializable]
    public struct RunResult
    {
        public string ResultString;
        public string BarcodeType;
        public HObject Region;
        public double Row;
        public double Col;
        public double Angle;
    }

    /// <summary>
    /// 通讯配置项
    /// </summary>
    [Serializable]
    public struct CommConfigItem
    {
        public string ReceivedCommand;
        public string JobName;
        public string OutputItem;
        public string NGRespond;
        public string PrefixStr;
        public string suffixStr;
    }

    /// <summary>
    /// 模板匹配结果
    /// </summary>
    [Serializable]
    public struct MatchResult
    {
        public double Row;
        public double Col;
        public double Angle;
        public double Socre;
    }

    /// <summary>
    /// 预处理项
    /// </summary>
    [Serializable]
    public class PreProcessing
    {
        public string PreProcessingType;
        public string ElementType;
        public Int32 ElementSize;
        public Int32 MinArea;
        public Int32 MaxArea;
        public bool Enable;
    }

    /// <summary>
    /// XYU结果
    /// </summary>
    [Serializable]
    public class XYU
    {
        public XYU()
        {
            _point = new XY();
        }
        public XYU(double x, double y, double u)
        {
            _point = new XY();
            _point.X = x;
            _point.Y = y;
            _u = u;
        }
        private XY _point;

        public XY Point
        {
            get { return _point; }
            set { _point = value; }
        }
        private double _u;
        public double U
        {
            get
            {
                return Math.Round(_u, 3);
            }
            set { _u = value; }
        }
        /// <summary>
        /// 将XYU类型转化成格式化字符串
        /// </summary>
        /// <returns></returns>
        internal string ToFormatStr()
        {
            return (Point.X >= 0 ? "+" + Point.X.ToString("000.000") : Point.X.ToString("000.000")) + (Point.Y >= 0 ? "+" + Point.Y.ToString("000.000") : Point.Y.ToString("000.000")) + (U >= 0 ? "+" + U.ToString("000.000") : U.ToString("000.000"));

            //  return (X >= 0 ? "+" + X.ToString("000.000") : X.ToString("000.000")) + ";" + (Y >= 0 ? "+" + Y.ToString("000.000") : Y.ToString("000.000")) + ";" + (U >= 0 ? "+" + U.ToString("000.000") : U.ToString("000.000"));
        }
        /// <summary>
        /// 将XYU类型转化成格式化字符串
        /// </summary>
        /// <returns></returns>
        internal string ToFormatStrTwoSpace()
        {
            return (Point.X >= 0 ? "+" + Point.X.ToString("000.000") : Point.X.ToString("000.000")) + (Point.Y >= 0 ? "  +" + Point.Y.ToString("000.000") : Point.Y.ToString("000.000")) + (U >= 0 ? "  +" + U.ToString("000.000") : U.ToString("000.000"));

            //  return (X >= 0 ? "+" + X.ToString("000.000") : X.ToString("000.000")) + ";" + (Y >= 0 ? "+" + Y.ToString("000.000") : Y.ToString("000.000")) + ";" + (U >= 0 ? "+" + U.ToString("000.000") : U.ToString("000.000"));
        }
        internal string ToShowTip()
        {
            return Point.X.ToString() + " | " + Point.Y.ToString() + " | " + U.ToString();
        }
        /// <summary>
        /// 重写 -
        /// </summary>
        /// <param name="p1">点1</param>
        /// <param name="p2">点2</param>
        /// <returns></returns>
        public static XYU operator -(XYU p1, XYU p2)
        {
            return new XYU(p1.Point.X - p2.Point.X, p1.Point.Y - p2.Point.Y, p1.U - p2.U);
        }
        /// <summary>
        /// 重写 +
        /// </summary>
        /// <param name="p1">点1</param>
        /// <param name="p2">点2</param>
        /// <returns></returns>
        public static XYU operator +(XYU p1, XYU p2)
        {
            return new XYU(p1.Point.X + p2.Point.X, p1.Point.Y + p2.Point.Y, p1.U + p2.U);
        }
    }

    /// <summary>
    /// 阈值分割后的筛选项
    /// </summary>
    [Serializable]
    public class SelShapePar
    {
        public string features;
        public string operation;
        public double min;
        public double max;
        public bool enable;
    }

    [Serializable]
    public class XY
    {
        public XY() { }
        public XY(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        private double _x;

        public double X
        {
            get
            {
                return Math.Round(_x, 3);
            }
            set { _x = value; }
        }
        private double _y;

        public double Y
        {
            get
            {
                return Math.Round(_y, 3);
            }
            set { _y = value; }
        }
        /// <summary>
        /// 重写 -
        /// </summary>
        /// <param name="p1">点1</param>
        /// <param name="p2">点2</param>
        /// <returns></returns>
        public static XY operator -(XY p1, XY p2)
        {
            return new XY(p1.X - p2.X, p1.Y - p2.Y);
        }
        /// <summary>
        /// 重写 +
        /// </summary>
        /// <param name="p1">点1</param>
        /// <param name="p2">点2</param>
        /// <returns></returns>
        public static XY operator +(XY p1, XY p2)
        {
            return new XY(p1.X + p2.X, p1.Y + p2.Y);
        }
        /// <summary>
        /// 获得点矢量长度
        /// </summary>
        internal double GetDistance
        {
            get
            {
                return Math.Sqrt(X * X + Y * Y);
            }
        }
        internal string ToShowTip()
        {
            return X.ToString() + " | " + Y.ToString();
        }
    }
}
