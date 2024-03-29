using HalconDotNet;
using Lxc.VisionPlus.ImageView;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Forms;
using VSE.Core;
using System.Diagnostics;

namespace VSE
{
    [Serializable]
    internal class MatchTool : ToolBase
    {
        public MatchTool()
        {
            MaskModel_region.GenEmptyObj();
            MaskFind_region.GenEmptyObj();
        }
        #region 参数定义
        public ToolPar toolPar = new ToolPar();
        /// <summary>
        /// 工具锁
        /// </summary>
        private object obj = new object();
        [NonSerialized]
        public HXLDCont contour = new HXLDCont();

        /// <summary>
        /// 行列间隔像素数
        /// </summary>
        internal int spanPixelNum = 100;
        /// <summary>
        /// 排序模式
        /// </summary>
        internal SortMode sortMode = SortMode.None;
        /// <summary>
        /// 搜索区域图像
        /// </summary>
        public HImage reducedImage
        {
            get;
            set;
        }
        /// <summary>
        /// 模板掩膜区域
        /// </summary>
        internal HRegion MaskModel_region = new HRegion();
        /// <summary>
        /// 搜索区域掩膜区域
        /// </summary>
        internal HRegion MaskFind_region = new HRegion();
        #endregion
        #region 方法
        internal void ShowStandardImage()
        {
            Win_MatchTool.Instance.PImageWin.Image= toolPar.RunPar.StandardImage;
            Win_MatchTool.Instance.PImageWin.displayMessage("训练图像",10, 10);

        }
        
        /// <summary>
        /// 刷新输出
        /// </summary>
        /// <param name="toolName">工具名称</param>
        internal void UpdateOutput(string toolName)
        {
            try
            {
                List<ToolIO> L_toolIO = Job.FindJobByName(jobName).FindToolInfoByName(toolName).output;
                for (int i = 0; i < L_toolIO.Count; i++)
                {
                    string outputItem = L_toolIO[i].IOName;
                    string[] items = Regex.Split(outputItem, " . ");
                    object value = toolPar;
                    value = GetValue(value, "ResultPar");
                    for (int j = 0; j < items.Length; j++)
                    {
                        value = GetValue(value, items[j]);
                    }
                    Job.FindJobByName(jobName).FindToolInfoByName(toolName).GetOutput(outputItem).value = value;

                    Job.FindJobByName(jobName).GetToolIONodeByNodeText(jobName, toolName, outputItem).ToolTipText = Method.FormatShowTip(value);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        /// <summary>
        /// 清空上次运行的所有输入
        /// </summary>
        internal void ClearLastInput()
        {
            try
            {
                toolPar.InputPar.InPutImage = null;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        /// <summary>
        /// 显示模板
        /// </summary>
        internal void ShowTemplate()
        {
            try
            {
                if (((toolPar.RunPar.matchMode == MatchMode.BasedShape)&& !toolPar.RunPar.ShapeModel.IsInitialized())|| (toolPar.RunPar.matchMode == MatchMode.BasedGray) && !toolPar.RunPar.NccModel.IsInitialized())
                {
                 
                        Win_MatchTool.Instance.Plbl_toolTip.ForeColor = Color.Red;
                        Win_MatchTool.Instance.Plbl_toolTip.Text = "状态：未创建模板";
                        return;
                }
                Win_MatchTool.Instance.PImageWin.Image=(toolPar.RunPar.StandardImage);
                HRegion MaskRegion = new HRegion();
                if (toolPar.RunPar.matchMode == MatchMode.BasedShape)
                {
                    contour = toolPar.RunPar.ShapeModel.GetShapeModelContours(1);
                    HTuple row1, col1, row2, col2;
                    toolPar.RunPar.TotalRegion.SmallestRectangle1(out row1, out col1, out row2, out col2);

                    HTuple area, row, col;
                    area = toolPar.RunPar.TotalRegion.AreaCenter(out row, out col);
                    HHomMat2D homMat2D = new HHomMat2D();
                    homMat2D.HomMat2dIdentity();
                    homMat2D = homMat2D.HomMat2dTranslate(row - row1, col - col1);
                    contour = contour.AffineTransContourXld(homMat2D);
                    if (MaskModel_region != null && MaskModel_region.IsInitialized())
                    {
                        homMat2D.HomMat2dIdentity();
                        homMat2D = homMat2D.HomMat2dTranslate(-row1, -col1);
                        MaskRegion = MaskModel_region.AffineTransRegion(homMat2D, "nearest_neighbor");
                    }
                }
                HImage imageReduced = toolPar.RunPar.StandardImage.ReduceDomain(toolPar.RunPar.TotalRegion);
                imageReduced = imageReduced.CropDomain();
                Win_MatchTool.Instance.imgView1.Image = imageReduced;
                if (toolPar.RunPar.matchMode == MatchMode.BasedShape)
                {
                    Win_MatchTool.Instance.imgView1.displayHRegion(contour, "orange");
                    if (MaskRegion != null && MaskRegion.IsInitialized())
                    {
                        Win_MatchTool.Instance.imgView1.displayHRegion(MaskRegion, "firebrick");
                    }
                }

                Win_MatchTool.Instance.PImageWin.displayHRegion(MaskModel_region, "firebrick");
               // Win_MatchTool.Instance.imgView1.displayHRegion(totalRegion, "green");
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 创建并显示模板
        /// </summary>
        internal void CreateAndShowTemplate()
        {
            try
            {
                if (((toolPar.RunPar.matchMode == MatchMode.BasedShape) && (toolPar.RunPar.ShapeModel == null||!toolPar.RunPar.ShapeModel.IsInitialized()))|| ((toolPar.RunPar.matchMode == MatchMode.BasedGray) && (toolPar.RunPar.NccModel ==null||!toolPar.RunPar.NccModel.IsInitialized())))
                {

                    toolPar.RunPar.StandardImage = toolPar.InputPar.InPutImage;

                }

                if (CreateTemplate() == 0)
                {
                    HRegion MaskRegion = new HRegion();
                    if (toolPar.RunPar.matchMode == MatchMode.BasedShape)
                    {
                        contour = toolPar.RunPar.ShapeModel.GetShapeModelContours(1);
                        HTuple row1, col1, row2, col2;
                        toolPar.RunPar.TotalRegion.SmallestRectangle1(out row1, out col1, out row2, out col2);

                        HTuple area, row, col;
                        area = toolPar.RunPar.TotalRegion.AreaCenter(out row, out col);
                        HHomMat2D homMat2D = new HHomMat2D();
                        homMat2D.HomMat2dIdentity();
                        homMat2D = homMat2D.HomMat2dTranslate(row- row1 , col-col1);
                        contour = contour.AffineTransContourXld(homMat2D);
                        if (MaskModel_region != null&& MaskModel_region.IsInitialized())
                        {
                            homMat2D.HomMat2dIdentity();
                            homMat2D = homMat2D.HomMat2dTranslate(-row1, -col1);
                            MaskRegion = MaskModel_region.AffineTransRegion(homMat2D, "nearest_neighbor");
                        }
                        
                    }

                    //在模板窗口显示模板

                    HImage imageReduced = toolPar.RunPar.StandardImage.ReduceDomain(toolPar.RunPar.TotalRegion);
                    imageReduced=imageReduced.CropDomain();
                    Win_MatchTool.Instance.imgView1.Image = imageReduced;

                    if (toolPar.RunPar.matchMode == MatchMode.BasedShape)
                    {
                        Win_MatchTool.Instance.imgView1.displayHRegion(contour, "orange");
                        if (MaskRegion != null && MaskRegion.IsInitialized())
                        {
                            Win_MatchTool.Instance.imgView1.displayHRegion(MaskRegion, "firebrick");
                        }
                        
                    }
                    
                    Win_MatchTool.Instance.PImageWin.displayHRegion(MaskModel_region, "firebrick");
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        /// <summary>
        /// 创建模板
        /// </summary>
        /// <returns>结果状态返回值：0表示成功 -1表示未知异常 1表示特征过少</returns>
        internal int CreateTemplate()
        {
            try
            {
                HImage template=new HImage();
                HRegion rg = toolPar.RunPar.TemplateRegion.getRegion();
                toolPar.RunPar.TotalRegion = new HRegion(rg);
                if (rg == null||!rg.IsInitialized())
                {
                    toolPar.RunPar.TemplateRegion.getRegion().GenEmptyObj();
                }
                try
                {
                    if (MaskModel_region != null&& MaskModel_region.IsInitialized())
                        toolPar.RunPar.TotalRegion = rg.Difference(MaskModel_region);
                }
                catch { }
                template= toolPar.RunPar.StandardImage.ReduceDomain(toolPar.RunPar.TotalRegion);
                try
                {
                    if (toolPar.RunPar.matchMode == MatchMode.BasedShape)
                        toolPar.RunPar.ShapeModel = template.CreateScaledShapeModel((HTuple)"auto",
                                                    ((HTuple)toolPar.RunPar.startAngle).TupleRad(),
                                                     ((HTuple)toolPar.RunPar.angleRange).TupleRad(),
                                                     (HTuple)("auto"),
                                                     toolPar.RunPar.minScale,
                                                     toolPar.RunPar.maxScale,
                                                     "auto",
                                                     (HTuple)"auto",
                                                     (HTuple)toolPar.RunPar.polarity,
                                                      toolPar.RunPar.autoContrast ? (HTuple)"auto" : (HTuple)toolPar.RunPar.contrast,
                                                     (HTuple)"auto");
                    else
                        toolPar.RunPar.NccModel = template.CreateNccModel((HTuple)"auto",
                                                    ((HTuple)toolPar.RunPar.startAngle).TupleRad(),
                                                     ((HTuple)toolPar.RunPar.angleRange).TupleRad(),
                                                     (HTuple)("auto"),
                                                     "use_polarity");
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("#8510:"))      //特征过少，Halcon报错编号8510
                    {
                        Win_MatchTool.Instance.Plbl_toolTip.Text = "状态：" + "特征过少，无法完成训练（错误代码：0201）";
                        return 1;
                    }
                }
                toolPar.RunPar.isTrained =true;
                return 0;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                toolPar.RunPar.isTrained = false;
                return -1;
            }

        }

        /// <summary>
        /// 用于外界获取区域变量
        /// </summary>
        //public HObject ToolUnionRegions;

        /// <summary>
        /// 运行工具
        /// </summary>
        /// <param name="updateImage">是否刷新图像</param>
        public override void Run(bool updateImage, bool runTool, string toolName)
        {
            try
            {
                lock (obj)
                {
                    #region 检查输入图像及模板是否有创建
                    toolRunStatu = ToolRunStatu.未知原因;
                    if (toolPar.InputPar.InPutImage == null|| !toolPar.InputPar.InPutImage.IsInitialized())
                    {
                        toolRunStatu = ( ToolRunStatu.未指定输入图像);
                        return;
                    }
                    if (!toolPar.RunPar.isTrained)
                    { 
                        toolRunStatu = ( ToolRunStatu.未创建模板);
                        return;
                    }
                    #endregion

                    #region 创建ROI 图像 用于查找模板
                    HImage image =new HImage();
                    if (MaskFind_region !=null&& MaskFind_region.IsInitialized())
                    {
                        reducedImage = toolPar.InputPar.InPutImage.ReduceDomain(toolPar.InputPar.ROIRegion.Difference(MaskFind_region));
                        image = reducedImage;
                    }
                    else
                    {
                        reducedImage = toolPar.InputPar.InPutImage.ReduceDomain(toolPar.InputPar.ROIRegion);
                        image = reducedImage;
                    }
                    #endregion

                    #region 计算匹配结果

                    HTuple rows =new HTuple();
                    HTuple cols=new HTuple();
                    HTuple angles=new HTuple();
                    HTuple scores= new HTuple();
                        
                    try
                    {
                        if (toolPar.RunPar.matchMode == MatchMode.BasedShape)
                        {
                            HTuple temp;

                            image.FindScaledShapeModel(toolPar.RunPar.ShapeModel,
                                                       ((HTuple)toolPar.RunPar.startAngle).TupleRad(),
                                                       ((HTuple)toolPar.RunPar.angleRange - toolPar.RunPar.startAngle).TupleRad(),
                                                       toolPar.RunPar.minScale,
                                                       toolPar.RunPar.maxScale,
                                                       (HTuple)toolPar.RunPar.minScore,
                                                       (HTuple)toolPar.RunPar.matchNum,
                                                       (HTuple)0.5,
                                                       (HTuple)"least_squares",
                                                       (HTuple)0,
                                                       (HTuple)0.9,
                                                        out rows,
                                                        out cols,
                                                        out angles,
                                                        out temp,
                                                        out scores);
                        }
                        else
                        {
                            image.FindNccModel(toolPar.RunPar.NccModel,
                                                        ((HTuple)toolPar.RunPar.startAngle).TupleRad(),
                                                        ((HTuple)toolPar.RunPar.angleRange - toolPar.RunPar.startAngle).TupleRad(),
                                                        (HTuple)toolPar.RunPar.minScore,
                                                        (HTuple)toolPar.RunPar.matchNum,
                                                        (HTuple)0.5,
                                                        (HTuple)"true",
                                                        (HTuple)0,
                                                         out rows,
                                                         out cols,
                                                         out angles,
                                                         out scores);
                        }
                    }
                    catch
                    {
                        toolRunStatu = ToolRunStatu.未匹配到模板;
                        return;
                    }
                    #endregion

                    #region 把匹配结果装入数据结构
                    toolPar.Results.ResultsList.Clear();
                    toolPar.Results.Pose.Clear();
                    if (rows.TupleLength() > 0)
                    {
                        for (int i = 0; i < rows.TupleLength(); i++)
                        {
                            MatchResult matchResult = new MatchResult();
                            matchResult.Row = Math.Round((double)rows[i], 3);
                            matchResult.Col = Math.Round((double)cols[i], 3);
                            matchResult.Angle = Math.Round((double)angles[i], 3);
                            matchResult.Socre = Math.Round((double)scores[i], 3);
                            toolPar.Results.ResultsList.Add(matchResult);
                            //如果不排序在此把Pose装入List
                            if (sortMode == SortMode.None)
                            {
                                XYU xyu = new XYU();
                                xyu.Point.X = Math.Round(matchResult.Col, 3);
                                xyu.Point.Y = Math.Round(matchResult.Row, 3);
                                xyu.U = Math.Round(matchResult.Angle, 3);
                                toolPar.Results.Pose.Add(xyu);
                            }
                        }

                        #region 结果排序
                        MatchResult temp;
                        if (sortMode == SortMode.None)
                        {

                        }
                        else if (sortMode == SortMode.从上至下且从左至右)
                        {
                            for (int i = 0; i < toolPar.Results.ResultsList.Count; i++)
                            {
                                for (int j = i + 1; j < toolPar.Results.ResultsList.Count; j++)
                                {
                                    if (toolPar.Results.ResultsList[i].Row - toolPar.Results.ResultsList[j].Row > spanPixelNum / 2)
                                    {
                                        temp = toolPar.Results.ResultsList[i];
                                        toolPar.Results.ResultsList[i] = toolPar.Results.ResultsList[j];
                                        toolPar.Results.ResultsList[j] = temp;
                                    }
                                }
                            }

                            for (int i = 0; i < toolPar.Results.ResultsList.Count; i++)
                            {
                                for (int j = i + 1; j < toolPar.Results.ResultsList.Count; j++)
                                {
                                    if ((toolPar.Results.ResultsList[i].Col - toolPar.Results.ResultsList[j].Col > spanPixelNum / 2) && (Math.Abs(toolPar.Results.ResultsList[i].Row - toolPar.Results.ResultsList[j].Row) < spanPixelNum / 2))
                                    {
                                        temp = toolPar.Results.ResultsList[i];
                                        toolPar.Results.ResultsList[i] = toolPar.Results.ResultsList[j];
                                        toolPar.Results.ResultsList[j] = temp;
                                    }
                                }
                            }


                        }
                        else
                        {
                            for (int i = 0; i < toolPar.Results.ResultsList.Count - 1; i++)
                            {
                                for (int j = i + 1; j < toolPar.Results.ResultsList.Count; j++)
                                {
                                    if (toolPar.Results.ResultsList[i].Socre < toolPar.Results.ResultsList[j].Socre)
                                    {
                                        temp = toolPar.Results.ResultsList[i];
                                        toolPar.Results.ResultsList[i] = toolPar.Results.ResultsList[j];
                                        toolPar.Results.ResultsList[j] = temp;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region 排序启用 此处把排序后的Pose装入List
                        if (sortMode != SortMode.None)
                        {  
                            for (int i = 0; i < toolPar.Results.ResultsList.Count; i++)
                            {
                                XYU xyu = new XYU();
                                xyu.Point.X = Math.Round(toolPar.Results.ResultsList[i].Col, 3);
                                xyu.Point.Y = Math.Round(toolPar.Results.ResultsList[i].Row, 3);
                                xyu.U = Math.Round(toolPar.Results.ResultsList[i].Angle, 3);
                                toolPar.Results.Pose.Add(xyu);
                            }
                         }
                        #endregion
                    }
                    else
                    {
                        if (Win_MatchTool.Instance.Visible&&MaskFind_region != null && MaskFind_region.IsInitialized())
                        {
                            Win_MatchTool.Instance.PImageWin.displayHRegion(MaskFind_region, "firebrick");
                        }
                        toolRunStatu = ToolRunStatu.未匹配到模板;
                        toolPar.Results.FindCount = 0;
                        return;
                    }
                    toolPar.Results.FindCount = toolPar.Results.Pose.Count;
                    #endregion

                    #region 设定UI界面显示
                    //重新显示图像

                    if (Win_MatchTool.Instance.Visible)
                    {
                        Win_MatchTool.Instance.PImageWin.Image = toolPar.InputPar.InPutImage;
                        Win_MatchTool.Instance.dgv_matchResult.Rows.Clear();
                    }

                    if (toolPar.InputPar.ROIRegion != null && toolPar.RunPar.showSearchRegion)
                    {
                        if (runTool)
                        {
                            Win_MatchTool.Instance.PImageWin.displayHRegion(toolPar.InputPar.ROIRegion, "green", "margin");
                            if (MaskFind_region!=null&&MaskFind_region.IsInitialized())
                            {
                                Win_MatchTool.Instance.PImageWin.displayHRegion(MaskFind_region, "firebrick");
                            }
                        }
                        else
                        {
                            GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                            {
                                GetImageWindowControl().displayHRegion(toolPar.InputPar.ROIRegion, "green", "margin");
                            }));
                        }
                    }

                    for (int i = 0; i < toolPar.Results.ResultsList.Count; i++)
                    {
                        //显示匹配特征
                        if (toolPar.RunPar.showFeature && toolPar.RunPar.matchMode == MatchMode.BasedShape)
                        {
                            HXLDCont countor = new HXLDCont() ;
                            countor= toolPar.RunPar.ShapeModel.GetShapeModelContours(new HTuple(1));
                            HHomMat2D homMat2D = new HHomMat2D();
                            homMat2D.HomMat2dIdentity();
                            homMat2D=homMat2D.HomMat2dTranslate( toolPar.Results.ResultsList[i].Row, toolPar.Results.ResultsList[i].Col);
                            homMat2D=homMat2D.HomMat2dRotate((HTuple)toolPar.Results.ResultsList[i].Angle, (HTuple)toolPar.Results.ResultsList[i].Row, (HTuple)toolPar.Results.ResultsList[i].Col);
                            HXLDCont countorAfterTrans=new HXLDCont();
                            countorAfterTrans=countor.AffineTransContourXld(homMat2D);
                            if (runTool)
                                Win_MatchTool.Instance.PImageWin.displayHRegion(countorAfterTrans, "orange");
                            else
                            {
                                GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                                {
                                    GetImageWindowControl().displayHRegion(countorAfterTrans, "orange");
                                }));
                            }
                            if (Win_MatchTool.Instance.Visible)
        
                            Win_MatchTool.Instance.PImageWin.displayHRegion(countorAfterTrans, "orange");
                        }

                        if (toolPar.RunPar.showTemplate)
                        {
                            HTuple area, row, col;
                            area= toolPar.RunPar.TotalRegion.AreaCenter(out row, out col);
                            HHomMat2D homMat2D1=new HHomMat2D();
                            homMat2D1.HomMat2dIdentity();
                            homMat2D1=homMat2D1.HomMat2dTranslate( -row, -col);
                            double roation = Math.Round(toolPar.Results.ResultsList[i].Angle, 3);
                            homMat2D1= homMat2D1.HomMat2dRotate(roation, 0, 0);
                            HXLDCont rectangle1AfterTrans=new HXLDCont();
                            homMat2D1= homMat2D1.HomMat2dTranslate(toolPar.Results.ResultsList[i].Row, toolPar.Results.ResultsList[i].Col);
                            rectangle1AfterTrans= toolPar.RunPar.TotalRegion.AffineTransRegion( homMat2D1, "nearest_neighbor");
                            if (runTool)
                            {
                                Win_MatchTool.Instance.PImageWin.displayHRegion(rectangle1AfterTrans, "green", "margin");
                            }
                            else
                            {
                                GetImageWindowControl().BeginInvoke(new MethodInvoker(() =>
                                {
                                    GetImageWindowControl().displayHRegion(rectangle1AfterTrans, "green", "margin");
                                }));
                            }

                            if (Win_MatchTool.Instance.Visible)
                            {
                                Win_MatchTool.Instance.PImageWin.displayHRegion(rectangle1AfterTrans, "green", "margin");
                            }
                        }

                        //显示中心十字架和序号
                        if (toolPar.RunPar.showCross || toolPar.RunPar.showIndex)
                        {
                            HTuple area, row, col;
                            HOperatorSet.AreaCenter(toolPar.RunPar.TotalRegion, out area, out row, out col);
                            HTuple homMat2D1;
                            HOperatorSet.HomMat2dIdentity(out homMat2D1);
                            HOperatorSet.HomMat2dTranslate(homMat2D1, -row, -col, out homMat2D1);
                            double roation = Math.Round(toolPar.Results.ResultsList[i].Angle, 3);
                            HOperatorSet.HomMat2dRotate(homMat2D1, roation, 0, 0, out homMat2D1);
                            HOperatorSet.HomMat2dTranslate(homMat2D1, toolPar.Results.ResultsList[i].Row, toolPar.Results.ResultsList[i].Col, out homMat2D1);
                            HOperatorSet.AffineTransPoint2d(homMat2D1, row, col, out row, out col);
                            HObject cross;
                            HOperatorSet.GenCrossContourXld(out cross, row, col, new HTuple(20), new HTuple(0));

                            if (runTool)
                            {
                                if (toolPar.RunPar.showCross)
                                    Win_MatchTool.Instance.PImageWin.displayHRegion(cross);
                                if (toolPar.RunPar.showIndex)
                                {
                                    ViewWindow.DisMsg(Win_MatchTool.Instance.PImageWin.hv_window, i + 1, ImgView.CoordSystem.image, row + 20, col + 20, "blue");
                                }
                            }
                            else
                            {
                                if (toolPar.RunPar.showCross)
                                {
                                    GetImageWindowControl().BeginInvoke(new MethodInvoker(() => 
                                    { 
                                        GetImageWindowControl().displayHRegion(cross, "blue"); 
                                    }));
                                   
                                }
                                if (Win_MatchTool.Instance.Visible)
                                    Win_MatchTool.Instance.PImageWin.displayHRegion(cross);
                                if (toolPar.RunPar.showIndex)
                                {
                                    ViewWindow.DisMsg(GetImageWindowControl().hv_window, i + 1, ImgView.CoordSystem.image, row + 20, col + 20, "blue");
                                }
                                if (Win_MatchTool.Instance.Visible)
                                {
                                    ViewWindow.DisMsg(Win_MatchTool.Instance.PImageWin.hv_window, i + 1, ImgView.CoordSystem.image, row + 20, col + 20, "blue");
                                }
                            }
                        }
                    }

                    #endregion

                    toolRunStatu =ToolRunStatu.成功;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }

        }
        #endregion
        #region 参数
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
            private Results _resultPar = new Results();

            public Results Results
            {
                get { return _resultPar; }
                set { _resultPar = value; }
            }
        }
        [Serializable]
        public class InputPar
        {
            private HImage inPutImage;

            public HImage InPutImage
            {
                get { return inPutImage; }
                set { inPutImage = value; }
            }
            private HRegion _searchRegion;

            public HRegion ROIRegion
            {
                get
                {
                    if (_searchRegion == null)
                    {
                        HTuple w, h;
                        inPutImage.GetImageSize(out w, out h);
                        _searchRegion = new HRegion(0, 0, h, w);// _图像.Threshold(0,255.0);
                    }
                    return _searchRegion;
                }
                set { _searchRegion = value; }
            }
        }
        [Serializable]
        public class RunPar
        {
            /// <summary>
            /// 是否训练
            /// </summary>
            internal bool isTrained = false;
            /// <summary>
            /// 最小缩放
            /// </summary>
            internal double minScale = 0.8;
            /// <summary>
            /// 最大缩放
            /// </summary>
            internal double maxScale = 1.2;
            /// <summary>
            /// 匹配算法
            /// </summary>
            internal MatchMode matchMode = MatchMode.BasedShape;
            /// <summary>
            /// 最小匹配分数
            /// </summary>
            internal double minScore = 0.5;
            /// <summary>
            /// 匹配个数
            /// </summary>
            internal int matchNum = 1;
            /// <summary>
            /// 对比度
            /// </summary>
            internal bool autoContrast = true;
            /// <summary>
            /// 起始角度
            /// </summary>
            internal int startAngle = -30;
            /// <summary>
            /// 角度范围
            /// </summary>
            internal int angleRange = 30;
            /// <summary>
            /// 角度步长
            /// </summary>
            internal int angleStep = 1;
            /// <summary>
            /// 对比度
            /// </summary>
            internal int contrast = 30;
            /// <summary>
            /// 极性
            /// </summary>
            internal string polarity = "use_polarity";
            /// <summary>
            /// 是否显示匹配到的模板
            /// </summary>
            internal bool showTemplate = true;
            /// <summary>
            /// 是否显示中心十字架
            /// </summary>
            internal bool showCross = true;
            /// <summary>
            /// 是否显示特征
            /// </summary>
            internal bool showFeature = true;
            /// <summary>
            /// 显示结果序号
            /// </summary>
            internal bool showIndex = true;
            /// <summary>
            /// 是否显示搜索区域
            /// </summary>
            internal bool showSearchRegion = true;
            /// <summary>
            /// 形状匹配模型
            /// </summary>
            public HShapeModel ShapeModel
            {
                get;
                set;
            }
            /// <summary>
            /// 灰度匹配模型
            /// </summary>
            public HNCCModel NccModel
            {
                get;
                set;
            }
            /// <summary>
            /// 训练图像
            /// </summary>
            public HImage StandardImage
            {
                get;
                set;
            }
            /// <summary>
            /// 模板区域
            /// </summary>
            public ROI TemplateRegion
            {
                get;
                set;
            }
            internal HRegion TotalRegion
            {
                get;
                set;
            }
        }
        [Serializable]
        public class Results
        {
            private List<XYU> pose;

            public List<XYU> Pose
            {
                get {
                    if (pose==null)
                    {
                        pose = new List<XYU>();
                    }
                    return pose; }
                set { pose = value; }
            }
            private List<MatchResult> resultsList;
            public List<MatchResult> ResultsList
            {
                get {
                    if (resultsList==null)
                    {
                        resultsList = new List<MatchResult>();
                    }
                    return resultsList; }
                set { resultsList = value; }
            }
            private int count;

            public int FindCount
            {
                get { return count; }
                set { count = value; }
            }
        }
        #endregion

    }
}
