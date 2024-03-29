using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSE.Core
{
    [Serializable]
    public class OutT
    {
        DataType mDataType;
        object mValue;
        string mPath;
        int mIconIndex;
        string toolName;
        string outItem;
        ToolType mToolType;

        public OutT(object mValue, DataType mDataType, string mPath, int mIconIndex, ToolType toolType)
        {
            this.mDataType = mDataType;
            this.mValue = mValue;
            this.mPath = mPath;
            this.mIconIndex = mIconIndex;
            this.toolName=mPath.Split('.')[0];
            this.outItem = mPath.Split('.')[1];
            this.mToolType = toolType;
        }

        public DataType MDataType
        {
            get { return mDataType; }
            set { mDataType = value; }
        }

        public object MValue
        {
            get { return mValue; }
            set { mValue = value; }
        }

        public string MPath
        {
            get { return mPath; }
            set
            {
                mPath = value;
                this.toolName = mPath.Split('.')[0];
                this.outItem = mPath.Split('.')[1];
            }
        }

        public int MIconIndex
        {
            get { return mIconIndex; }
            set { mIconIndex = value; }
        }

        public string ToolName
        {
            get { return toolName; }
        }

        public string OutItem
        {
            get { return outItem; }
        }

        public ToolType MToolType
        {
            get { return mToolType; }
            set { mToolType = value; }
        }
        /// <summary>
        /// 获取bool类型的值
        /// </summary>
        /// <returns>如果不是bool类型返回Null</returns>
        public bool? GetValueB()
        {
            if (MDataType== DataType.Bool)
            {
                return (bool)MValue;
            }
            return null;
        
        }
        /// <summary>
        /// 获取string类型的值
        /// </summary>
        /// <returns>如果不是string类型返回Null</returns>
        public string GetValueS()
        {
            if(MDataType == DataType.String)
            {
                return (string)MValue;
            }
            return null;

        }
        /// <summary>
        /// 获取double类型的值
        /// </summary>
        /// <returns>如果不是double类型返回Null</returns>
        public double? GetValueD()
        {
            if (MDataType == DataType.Double)
            {
                return (double)MValue;
            }
            return null;
        }
        /// <summary>
        /// 获取double类型的值
        /// </summary>
        /// <returns>如果不是double类型返回Null</returns>
        public int? GetValueI()
        {
            if (MDataType == DataType.Int)
            {
                return (int)MValue;
            }
            return null;

        }
        /// <summary>
        /// 获取Pose类型的值
        /// </summary>
        /// <returns>如果不是Pose类型返回Null</returns>
        public XYU GetValuePose()
        {
            if (MDataType == DataType.Pose)
            {
                List<XYU> L = (List<XYU>)MValue;
                if (L.Count>0)
                {
                    return L[0];
                }
                return null;
            }
            return null;

        }
        /// <summary>
        /// 获取Pose类型的个数
        /// </summary>
        /// <returns></returns>
        public int GetValuePoseCount()
        {
            if (MDataType == DataType.Pose)
            {
                List<XYU> L = (List<XYU>)MValue;
                return L.Count;
            }
            return 0;
        }
        /// <summary>
        /// 获取Line类型的值
        /// </summary>
        /// <returns>如果不是Line类型返回Null</returns>
        public Line GetValueLine()
        {
            if (MDataType == DataType.Line)
            {
                return (Line)MValue;
            }
            return null;

        }
        /// <summary>
        /// 获取Circle类型的值
        /// </summary>
        /// <returns>如果不是Circle类型返回Null</returns>
        public Circle GetValueCircle()
        {
            if (MDataType == DataType.Circle)
            {
                return (Circle)MValue;
            }
            return null;

        }
        /// <summary>
        /// 获取Point类型的值
        /// </summary>
        /// <returns>如果不是Point类型返回Null</returns>
        public XY GetValuePointD()
        {
            if (MDataType == DataType.XY)
            {
                return (XY)MValue;
            }
            return null;

        }
    }
}
