using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Collections.Generic;
using VSE.Core;

namespace VSE
{
    [Serializable]
    internal class MorphologyTool : ToolBase
    {
        public ToolPar toolPar = new ToolPar();
        /// <summary>
        /// 搜索区域
        /// </summary>
        internal List<ROI> L_regions = new List<ROI>();
        private List<ItemType> l_item = new List<ItemType>();
        public List<ItemType> L_item
        { 
            get {
                if (l_item==null)
                {
                    l_item = new List<ItemType>();
                }
                return l_item; }
            //set { l_item = value; }
        }
      
        /// <summary>
        /// 运行工具
        /// </summary>
        /// <param name="updateImage">是否刷新图像</param>
        public override void Run(bool updateImage, bool b, string toolName)
        {
            try
            {
                lock (obj)
                {
                    toolRunStatu =ToolRunStatu.未知原因;
                    HImage image = new HImage();
                    HRegion region = new HRegion();
                
                    image = toolPar.InputPar.图像.CopyImage();
                    region.GenEmptyRegion();
                    region = toolPar.InputPar.搜索区域;
                    for (int i = 0; i < L_item.Count; i++)
                    {
                        if (L_item[i].enable)
                        {

                        switch (L_item[i].type)
                        {
                            case "OpenCircle":
                                OpenCircle OpenCircle = (OpenCircle)L_item[i].item;
                               
                                region= region.OpeningCircle(OpenCircle.r);
     
                                break;
                            case "CloseCircle":
                                CloseCircle CloseCircle = (CloseCircle)L_item[i].item;
                                region = region.ClosingCircle(CloseCircle.r);
                                break;
                            case "DilationCircle":
                                DilationCircle DilationCircle = (DilationCircle)L_item[i].item;
                                region = region.DilationCircle(DilationCircle.r);
                                break;
                            case "ErosionCircle":
                                ErosionCircle ErosionCircle = (ErosionCircle)L_item[i].item;
                                region = region.ErosionCircle(ErosionCircle.r);
                                break;
                            }
                        }
                    }

                    if (updateImage&& region!=null&& region.IsInitialized())
                        GetImageWindowControl().displayHRegion(region,"green");
                    if (L_item.Count==0)
                    {
                        toolPar.ResultPar.region =toolPar.InputPar.搜索区域.Connection();
                    }
                    else
                    {

                        toolPar.ResultPar.region = region;
                    }
                    toolRunStatu =ToolRunStatu.成功;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        [Serializable]
        public class ToolPar : ToolParBase
        {
            private InputPar _inputPar = new InputPar();

            public InputPar InputPar
            {
                get { return _inputPar; }
                set { _inputPar = value; }
            }
            private RunPar _runPar = new RunPar();

            public RunPar RunPar
            {
                get { return _runPar; }
                set { _runPar = value; }
            }
            private ResultPar _resultPar = new ResultPar();

            public ResultPar ResultPar
            {
                get { return _resultPar; }
                set { _resultPar = value; }
            }
        }
        [Serializable]
        public class InputPar
        {
            private HImage _图像;

            public HImage 图像
            {
                get { return _图像; }
                set { _图像 = value; }
            }

            private HRegion _搜索区域;

            public HRegion 搜索区域
            {
                get {
                    if (_搜索区域==null)
                    {
                        HTuple w, h;
                        _图像.GetImageSize(out w,out h);
                           _搜索区域 = new HRegion(0,0,h,w);// _图像.Threshold(0,255.0);
                    }
                    return _搜索区域; }
                set { _搜索区域 = value; }
            }
        }
        [Serializable]
        public class RunPar
        {
            public double threshold = 128;
        }
        [Serializable]
        internal class ResultPar
        {
            private HRegion _region;
            public HRegion region
            {
                get
                {
                    if (_region == null)
                    {
                        //HTuple w, h;
                        //_图像.GetImageSize(out w, out h);//0, 0, h, w
                        _region = new HRegion();// _图像.Threshold(0,255.0);
                    }
                    return _region;
                }
                set { _region = value; }
            }
        }
        [Serializable]
        internal class ItemType
        {
            internal ItemType(string type, string itemName, itemBase item)
            {
                this.type = type;
                this.itemName = itemName;
                this.item = item;
            }
            internal string type;
            internal string itemName;
            internal itemBase item;
            internal bool enable = true;
        }
        [Serializable]
        internal class itemBase
        {

        }
        [Serializable]
       public class OpenCircle : itemBase
        {
            internal double r = 3;
           

        }
        [Serializable]
        public class CloseCircle : itemBase
        {
            internal double r = 3;


        }
        [Serializable]
        public class DilationCircle : itemBase
        {
            internal double r = 3;


        }
        [Serializable]
        public class ErosionCircle : itemBase
        {
            internal double r = 3;


        }
    }
}
