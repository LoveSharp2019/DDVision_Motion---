using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModbusTcpHelper;
using LightControllerHelper;
using VSE.Core;
using VSE;
using System.IO;
using HalconDotNet;

namespace Start
{
    public partial class Form1 : UIControls.UserFormBase
    {
        private LightController_QSX_PSH light = null;
        private string ValueText = "";

        //自动
        Queue<HImage> QeNGImages = new Queue<HImage>();
        Queue<Queue<XYU>> QeQePoses11 = new Queue<Queue<XYU>>();
        Queue<Queue<string>> QeQeStrings11 = new Queue<Queue<string>>();
        Queue<Queue<XYU>> QeQePoses22 = new Queue<Queue<XYU>>();
        Queue<Queue<string>> QeQeStrings22 = new Queue<Queue<string>>();
        Dictionary<string, Queue<HImage>> DicQeImages = new Dictionary<string, Queue<HImage>>();
        Dictionary<string, Queue<ProResult>> DicQeJobRet = new Dictionary<string, Queue<ProResult>>();
        Dictionary<string, Queue<Queue<string>>> DicQeStrings = new Dictionary<string, Queue<Queue<string>>>();
        Dictionary<string, Queue<Queue<XYU>>> DicQeJobPose = new Dictionary<string, Queue<Queue<XYU>>>();

        private object obj = new object();
        private TcpIPHelper_Server tcpIPHelper_Server = new TcpIPHelper_Server("Server");

        public Form1()
        {
            InitializeComponent();
            VSE.Start.MachineStateChangeEvent += Start_MachineStateChangeEvent;
            VSE.Start.JobRunDoneEvent += Start_JobRunDoneEvent;
            VSE.Start.ProductNewsClearEvent += Start_ProductNewsClearEvent;
            VSE.Start.UpdateJobNameListEvent += Start_UpdateJobNameListEvent;
            TcpIPHelper_Server.updateSocketStatus += updateTcpIPStatus;
            TcpIPHelper_Server.receiveClientMessage += updateTcpIPMessage;
        }

        private void Start_UpdateJobNameListEvent(VSE.JobNameListEventArgs e)
        {
            DicQeImages.Clear();
            DicQeJobRet.Clear();
            DicQeStrings.Clear();
            DicQeJobPose.Clear();
            for (int i = 0; i < e.JobNameList.Length; i++)
            {
                DicQeImages.Add(e.JobNameList[i], new Queue<HImage>());//对应Job名字，对应检测图片
                DicQeJobRet.Add(e.JobNameList[i], new Queue<ProResult>());//对应Job名字，对应检测结果
                DicQeStrings.Add(e.JobNameList[i], new Queue<Queue<string>>());//对应Job名字，对应检测信息
                DicQeJobPose.Add(e.JobNameList[i], new Queue<Queue<XYU>>());//对应Job名字，对应检测区域位置信息
            }
        }

        private void Start_JobRunDoneEvent(VSE.JobRunDoneEventArgs e)
        {
            #region 判断（手动）
            ////判断Job状态，如果true,检索到产品，如果false,未检索到产品，或检索不全
            //if (e.BJobStatus)
            //{
            //    #region 界面结果显示
            //    if (e.JobName == "模切检测_量测" || e.JobName == "模切检测9020_缺陷1" || e.JobName == "模切检测CE037_1")
            //    {
            //        label7.Text = e.JobName;
            //        label9.Text = e.ElapsedTime.ToString() + " ms";

            //        ValueText = "";

            //        foreach (OutT item in e.Result)
            //        {
            //            switch (item.MDataType)
            //            {
            //                case DataType.Bool:
            //                    if (item.GetValueB() == true)
            //                        ValueText += item.ToolName + ": " + "OK" + "\r\n";
            //                    else
            //                        ValueText += item.ToolName + ": " + "NG" + "\r\n";
            //                    break;
            //                case DataType.Double:
            //                    ValueText += item.ToolName + ": " + (Convert.ToDouble(item.GetValueD().ToString())).ToString("0.000") + ";\r\n";
            //                    break;
            //                case DataType.String:
            //                    ValueText += item.ToolName + ": " + item.GetValueS().ToString() + ";\r\n";
            //                    break;
            //                case DataType.Int:
            //                    ValueText += item.ToolName + ": " + item.GetValueI().ToString() + ";\r\n";
            //                    break;
            //                case DataType.Line:
            //                    Line L = item.GetValueLine();
            //                    ValueText += item.ToolName + ": " + String.Format("Line 起点 XY:{0}=={1} 终点 XY:{2}=={3}", L.起点.X.ToString(), L.起点.Y.ToString(), L.终点.X.ToString(), L.终点.Y.ToString()) + ";\r\n";
            //                    break;
            //                case DataType.Circle:
            //                    ValueText += item.ToolName + ": " + item.GetValueCircle().ToString() + ";\r\n";
            //                    break;
            //                case DataType.Pose:
            //                    if (item.GetValuePose() == null)
            //                    {
            //                        ValueText += item.ToolName + ": " + "未匹配到模板;\r\n";
            //                    }
            //                    else
            //                    {
            //                        XYU p = item.GetValuePose();
            //                        ValueText += String.Format("{0} XYR: {1}; {2}; {3};", item.ToolName, p.Point.X, p.Point.Y, p.U) + "\r\n";
            //                    }
            //                    break;
            //                case DataType.XY:
            //                    XY p1 = item.GetValuePointD();
            //                    ValueText += String.Format("{0} XY: {1}; {2};", item.ToolName, p1.X, p1.Y) + "\r\n";
            //                    break;
            //                default:
            //                    ValueText += item.ToString() + "\r\n";
            //                    break;
            //            }
            //        }
            //    }

            //    txtResults.Text = ValueText;

            //    #endregion

            //    #region Job检测结果判断
            //    if (e.JobName == "模切检测_缺陷")//主Job1
            //    {
            //        label_CT.Text = e.ElapsedTime.ToString();
            //        Queue<XYU> QePoses = new Queue<XYU>();
            //        Queue<string> QeStrings = new Queue<string>();

            //        bool isJobOKNG = true;
            //        ProResult pRet = new ProResult();
            //        pRet.jobName = e.JobName;
            //        pRet.bProStatus = e.BProStatus;
            //        pRet.bJobStatus = e.BJobStatus;

            //        //区域2异物有无
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[0].GetValuePoseCount() > 0)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域2有异物");
            //                QePoses.Enqueue(e.Result[0].GetValuePose());
            //            }
            //        }
            //        //区域2异物有无
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[1].GetValuePoseCount() != 1)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域2有异物");
            //                QePoses.Enqueue(e.Result[1].GetValuePose());
            //            }
            //        }

            //        pRet.bResult = isJobOKNG;

            //        QeJob1.Enqueue(pRet);
            //        QeImages.Enqueue(e.Image);
            //        QeQePoses1.Enqueue(QePoses);
            //        QeQeStrings1.Enqueue(QeStrings);
            //    }
            //    else if (e.JobName == "模切检测_量测")//从Job2
            //    {
            //        Queue<XYU> QePoses = new Queue<XYU>();
            //        Queue<string> QeStrings = new Queue<string>();

            //        bool isJobOKNG = true;
            //        ProResult pRet = new ProResult();
            //        pRet.jobName = e.JobName;
            //        pRet.bProStatus = e.BProStatus;
            //        pRet.bJobStatus = e.BJobStatus;

            //        #region 模切检测
            //        //黑色PING左到圆心距
            //        if (isJobOKNG)
            //        {
            //            if (Convert.ToBoolean(e.Result[5].GetValueB()) == false)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("黑色PI左到圆心距距离NG");
            //            }
            //        }
            //        //黑色PING右到圆心距
            //        if (isJobOKNG)
            //        {
            //            if (Convert.ToBoolean(e.Result[7].GetValueB()) == false)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("黑色PI右到圆心距距离NG");
            //            }
            //        }
            //        //黑色PI倾斜
            //        if (isJobOKNG)
            //        {
            //            if (Convert.ToBoolean(e.Result[9].GetValueB()) == false)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("黑色PI倾斜NG");
            //            }
            //        }
            //        //黑色PING有无
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[0].GetValuePoseCount() != 1)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("黑色PI缺失");
            //            }
            //        }
            //        //胶水1有无
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[1].GetValueLine() == null)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("胶水1缺失");
            //            }
            //        }
            //        //胶水2有无
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[2].GetValueLine() == null)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("胶水2缺失");
            //            }
            //        }
            //        //胶水3有无
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[3].GetValueCircle() == null)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("胶水3缺失");
            //            }
            //        }
            //        //异物
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[10].GetValuePoseCount() > 0)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域1有异物");
            //                QePoses.Enqueue(e.Result[10].GetValuePose());
            //            }
            //        }
            //        #endregion

            //        pRet.bResult = isJobOKNG;

            //        QeJob2.Enqueue(pRet);
            //        QeQePoses2.Enqueue(QePoses);
            //        QeQeStrings2.Enqueue(QeStrings);
            //    }
            //    #endregion

            //    #region 9020型号模切检测

            //    if (e.JobName == "模切检测9020_缺陷1")
            //    {
            //        label_CT.Text = e.ElapsedTime.ToString();
            //        Queue<XYU> QePoses = new Queue<XYU>();
            //        Queue<string> QeStrings = new Queue<string>();

            //        bool isJobOKNG = true;
            //        ProResult pRet = new ProResult();
            //        pRet.jobName = e.JobName;
            //        pRet.bProStatus = e.BProStatus;
            //        pRet.bJobStatus = e.BJobStatus;

            //        //胶水1有无1
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[0].GetValueLine() == null)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("胶水1缺失1");
            //            }
            //        }
            //        //胶水1有无2
            //        if (isJobOKNG)
            //        {
            //            if (Convert.ToBoolean(e.Result[1].GetValueB()) == false)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("胶水1缺失2");
            //            }
            //        }
            //        //胶水2有无1
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[2].GetValueLine() == null)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("胶水2缺失1");
            //            }
            //        }
            //        //胶水1有无2
            //        if (isJobOKNG)
            //        {
            //            if (Convert.ToBoolean(e.Result[3].GetValueB()) == false)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("胶水2缺失2");
            //            }
            //        }
            //        //胶水3有无
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[4].GetValueLine() == null)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("胶水3缺失");
            //            }
            //        }
            //        //区域1-1异物有无
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[5].GetValuePoseCount() > 0)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域1有异物");
            //            }
            //        }
            //        //区域1-2异物有无
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[6].GetValuePoseCount() > 0)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域1有异物");
            //            }
            //        }

            //        pRet.bResult = isJobOKNG;

            //        QeJob1.Enqueue(pRet);
            //        QeImages.Enqueue(e.Image);
            //        QeQePoses1.Enqueue(QePoses);
            //        QeQeStrings1.Enqueue(QeStrings);

            //    }
            //    else if (e.JobName == "模切检测9020_缺陷2")
            //    {

            //        Queue<XYU> QePoses = new Queue<XYU>();
            //        Queue<string> QeStrings = new Queue<string>();

            //        bool isJobOKNG = true;
            //        ProResult pRet = new ProResult();
            //        pRet.jobName = e.JobName;
            //        pRet.bProStatus = e.BProStatus;
            //        pRet.bJobStatus = e.BJobStatus;

            //        //区域2-1异物有无
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[0].GetValuePoseCount() > 0)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域2有异物");
            //                QePoses.Enqueue(e.Result[0].GetValuePose());
            //            }
            //        }
            //        //区域2-2异物有无
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[1].GetValuePoseCount() > 0)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域2有异物");
            //                QePoses.Enqueue(e.Result[1].GetValuePose());
            //            }
            //        }

            //        pRet.bResult = isJobOKNG;

            //        QeJob2.Enqueue(pRet);
            //        QeQePoses2.Enqueue(QePoses);
            //        QeQeStrings2.Enqueue(QeStrings);
            //    }

            //    #endregion

            //    #region CE037型号模切检测
            //    if (e.JobName == "模切检测CE037_1")
            //    {
            //        label_CT.Text = e.ElapsedTime.ToString();
            //        Queue<XYU> QePoses = new Queue<XYU>();
            //        Queue<string> QeStrings = new Queue<string>();

            //        bool isJobOKNG = true;
            //        ProResult pRet = new ProResult();
            //        pRet.jobName = e.JobName;
            //        pRet.bProStatus = e.BProStatus;
            //        pRet.bJobStatus = e.BJobStatus;

            //        //胶水1有无1
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[0].GetValueLine() == null)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("胶水1缺失1");
            //            }
            //        }
            //        //胶水1有无2
            //        if (isJobOKNG)
            //        {
            //            if (Convert.ToBoolean(e.Result[1].GetValueB()) == false)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("胶水1缺失2");
            //            }
            //        }
            //        //胶水2有无1
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[2].GetValueLine() == null)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("胶水2缺失1");
            //            }
            //        }
            //        //胶水1有无2
            //        if (isJobOKNG)
            //        {
            //            if (Convert.ToBoolean(e.Result[3].GetValueB()) == false)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("胶水2缺失2");
            //            }
            //        }
            //        //胶水3有无
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[4].GetValueCircle() == null)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("胶水3缺失");
            //            }
            //        }
            //        //区域1-1异物有无
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[5].GetValuePoseCount() > 0)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域1有异物");
            //            }
            //        }
            //        //区域1-2异物有无
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[6].GetValuePoseCount() > 0)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域1有异物");
            //            }
            //        }
            //        if (isJobOKNG)
            //        {
            //            if (Convert.ToBoolean(e.Result[7].GetValueB()) == false)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域1有堵孔");
            //            }
            //        }
            //        if (isJobOKNG)
            //        {
            //            if (Convert.ToBoolean(e.Result[8].GetValueB()) == false)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域1有堵孔");
            //            }
            //        }
            //        if (isJobOKNG)
            //        {
            //            if (Convert.ToBoolean(e.Result[9].GetValueB()) == false)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域1有堵孔");
            //            }
            //        }
            //        if (isJobOKNG)
            //        {
            //            if (Convert.ToBoolean(e.Result[10].GetValueB()) == false)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域1有堵孔");
            //            }
            //        }

            //        pRet.bResult = isJobOKNG;

            //        QeJob1.Enqueue(pRet);
            //        QeImages.Enqueue(e.Image);
            //        QeQePoses1.Enqueue(QePoses);
            //        QeQeStrings1.Enqueue(QeStrings);
            //    }
            //    else if (e.JobName == "模切检测CE037_2")
            //    {

            //        Queue<XYU> QePoses = new Queue<XYU>();
            //        Queue<string> QeStrings = new Queue<string>();

            //        bool isJobOKNG = true;
            //        ProResult pRet = new ProResult();
            //        pRet.jobName = e.JobName;
            //        pRet.bProStatus = e.BProStatus;
            //        pRet.bJobStatus = e.BJobStatus;

            //        //区域2-1异物有无
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[0].GetValuePoseCount() > 0)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域2有异物");
            //                QePoses.Enqueue(e.Result[0].GetValuePose());
            //            }
            //        }
            //        //区域2-2异物有无
            //        if (isJobOKNG)
            //        {
            //            if (e.Result[1].GetValuePoseCount() > 0)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域2有异物");
            //                QePoses.Enqueue(e.Result[1].GetValuePose());
            //            }
            //        }
            //        if (isJobOKNG)
            //        {
            //            if (Convert.ToBoolean(e.Result[2].GetValueB()) == false)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域2有堵孔");
            //            }
            //        }
            //        if (isJobOKNG)
            //        {
            //            if (Convert.ToBoolean(e.Result[3].GetValueB()) == false)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域2有堵孔");
            //            }
            //        }
            //        if (isJobOKNG)
            //        {
            //            if (Convert.ToBoolean(e.Result[4].GetValueB()) == false)
            //            {
            //                isJobOKNG = false;
            //                QeStrings.Enqueue("产品检测区域2有堵孔");
            //            }
            //        }

            //        pRet.bResult = isJobOKNG;
            //        QeJob2.Enqueue(pRet);
            //        QeQePoses2.Enqueue(QePoses);
            //        QeQeStrings2.Enqueue(QeStrings);
            //    }

            //    #endregion
            //}
            //else
            //{
            //    ProResult pRet = new ProResult();
            //    pRet.jobName = e.JobName;
            //    pRet.bProStatus = e.BProStatus;
            //    pRet.bJobStatus = e.BJobStatus;
            //    pRet.bResult = false;

            //    if (e.JobName == "模切检测_缺陷" || e.JobName == "模切检测9020_缺陷1" || e.JobName == "模切检测CE037_1")//主要Job,有相机
            //    {
            //        Queue<XYU> QePoses = new Queue<XYU>();
            //        Queue<string> QeStrings = new Queue<string>();
            //        QeStrings.Enqueue("模切检测Job1处理失败");

            //        QeJob1.Enqueue(pRet);
            //        QeImages.Enqueue(e.Image);
            //        QeQePoses1.Enqueue(QePoses);
            //        QeQeStrings1.Enqueue(QeStrings);
            //    }
            //    else if (e.JobName == "模切检测_量测" || e.JobName == "模切检测9020_缺陷2" || e.JobName == "模切检测CE037_2")//从属Job,无相机
            //    {
            //        Queue<XYU> QePoses = new Queue<XYU>();
            //        Queue<string> QeStrings = new Queue<string>();
            //        QeStrings.Enqueue("模切检测Job2处理失败");

            //        QeJob2.Enqueue(pRet);
            //        QeQePoses2.Enqueue(QePoses);
            //        QeQeStrings2.Enqueue(QeStrings);
            //    }
            //    txtResults.Text = "";
            //}

            #endregion

            #region 判断（自动）
            lock (obj)
            {
                ProResult pRet = new ProResult();
                pRet.jobName = e.JobName;
                pRet.bProStatus = e.BProStatus;
                pRet.bJobStatus = e.BJobStatus;
                pRet.strMessage[0] = e.JobName;
                pRet.strMessage[1] = e.ElapsedTime.ToString() + " ms";
                Queue<string> QeStrings = new Queue<string>();
                Queue<XYU> QePoses = new Queue<XYU>();

                if (e.BJobStatus)
                {
                    ValueText = "";
                    bool isJobOKNG = true;
                    foreach (OutT item in e.Result)
                    {
                        switch (item.MDataType)
                        {
                            case DataType.Bool:
                                if (item.GetValueB() == true)
                                    ValueText += item.ToolName + ": " + "OK" + "\r\n";
                                else
                                    ValueText += item.ToolName + ": " + "NG" + "\r\n";
                                break;
                            case DataType.Double:
                                ValueText += item.ToolName + ": " + (Convert.ToDouble(item.GetValueD().ToString())).ToString("0.000") + ";\r\n";
                                break;
                            case DataType.String:
                                ValueText += item.ToolName + ": " + item.GetValueS().ToString() + ";\r\n";
                                break;
                            case DataType.Int:
                                ValueText += item.ToolName + ": " + item.GetValueI().ToString() + ";\r\n";
                                break;
                            case DataType.Line:
                                Line L = item.GetValueLine();
                                ValueText += item.ToolName + ": " + String.Format("Line 起点 XY:{0}=={1} 终点 XY:{2}=={3}", L.起点.X.ToString(), L.起点.Y.ToString(), L.终点.X.ToString(), L.终点.Y.ToString()) + ";\r\n";
                                break;
                            case DataType.Circle:
                                ValueText += item.ToolName + ": " + item.GetValueCircle().ToString() + ";\r\n";
                                break;
                            case DataType.Pose:
                                if (item.GetValuePose() == null)
                                {
                                    ValueText += item.ToolName + ": " + "未找到特征;\r\n";
                                }
                                else
                                {
                                    XYU p = item.GetValuePose();
                                    ValueText += String.Format("{0} XYR: {1}; {2}; {3};", item.ToolName, p.Point.X, p.Point.Y, p.U) + "\r\n";
                                }
                                break;
                            case DataType.XY:
                                XY p1 = item.GetValuePointD();
                                ValueText += String.Format("{0} XY: {1}; {2};", item.ToolName, p1.X, p1.Y) + "\r\n";
                                break;
                            default:
                                ValueText += item.ToString() + "\r\n";
                                break;
                        }
                    }
                    pRet.strMessage[2] = ValueText;
                    foreach (OutT item in e.Result)
                    {
                        switch (item.MDataType)
                        {
                            case DataType.Bool:
                                if (Convert.ToBoolean(item.GetValueB()) == false)
                                {
                                    isJobOKNG = false;
                                    if (item.MToolType == ToolType.VerdictMeasure)
                                    {
                                        QeStrings.Enqueue("公差超过设定规格");
                                    }
                                    else if (item.MToolType == ToolType.LLAngle)
                                    {
                                        QeStrings.Enqueue("角度值超过设定规格，检查是否胶水缺失！");
                                    }
                                    else if (item.MToolType == ToolType.RRegionGraySubtract)
                                    {
                                        QeStrings.Enqueue("区域灰度差超过设定规格，检查是否堵孔！");
                                    }
                                }
                                break;
                            //case DataType.Double:
                            //    break;
                            //case DataType.String:
                            //    break;
                            //case DataType.Int:
                            //    break;
                            case DataType.Line:
                                if (item.GetValueLine() == null)
                                {
                                    isJobOKNG = false;
                                    QeStrings.Enqueue("抓线异常，检查是否胶水缺失，或者轮廓绘制异常！");
                                }
                                break;
                            case DataType.Circle:
                                if (item.GetValueCircle() == null)
                                {
                                    isJobOKNG = false;
                                    QeStrings.Enqueue("抓圆异常，检查是否胶水缺失，或者轮廓绘制异常！");
                                }
                                break;
                            case DataType.Pose:
                                if (item.MToolType == ToolType.BlobAnalyse)
                                {
                                    if (item.GetValuePoseCount() != 1)
                                    {
                                        //
                                    }
                                }
                                break;
                            //case DataType.XY:
                            //    break;
                            default:
                                break;
                        }
                    }
                    pRet.bResult = isJobOKNG;
                }
                else
                {
                    ValueText = "";
                    pRet.strMessage[2] = "产品检测异常！";
                    pRet.bResult = false;
                    QeStrings.Enqueue(String.Format("模切检测Job:{0}处理失败", e.JobName));
                }

                //OK & NG 都有
                DicQeJobRet[e.JobName].Enqueue(pRet);//添加检测信息
                DicQeImages[e.JobName].Enqueue(e.Image);//添加检测图片
                DicQeStrings[e.JobName].Enqueue(QeStrings);//添加检测结果信息
                DicQeJobPose[e.JobName].Enqueue(QePoses);//添加检测结果位置信息
             
            }
            #endregion
        }

        private object objLock = new object();
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool isProductOKNG = true;//判断产品OK/NG
            HImage hImage = new HImage();

            #region 判断&显示(自动)
            int iCountJob = DicQeJobRet.Count;
            string[] strKeys = DicQeJobRet.Keys.ToArray();
            if (iCountJob == 0)
            { 
                //空操作
            }
            else if (iCountJob == 1)
            {
                lock (objLock)
                {
                    if (DicQeJobPose[strKeys[0]].Count > 0)
                    {
                        ProResult p1 = DicQeJobRet[strKeys[0]].Dequeue();
                        hImage = DicQeImages[strKeys[0]].Dequeue();
                        Queue<XYU> QEPose1 = DicQeJobPose[strKeys[0]].Dequeue();
                        Queue<string> QEstrMsg1 = DicQeStrings[strKeys[0]].Dequeue();

                        if (p1.bJobStatus)
                        {
                            imgView1.SetResult(p1.bResult);
                            if (p1.bResult)
                            {
                                //OK
                                isProductOKNG = true;
                                if (!VSE.Start.isFirstLoadJob)
                                {
                                    label_OK.Text = (Convert.ToDouble(label_OK.Text) + 1).ToString();
                                }
                            }
                            else
                            {
                                //NG
                                isProductOKNG = false;
                                if (!VSE.Start.isFirstLoadJob)
                                {
                                    label_NG.Text = (Convert.ToDouble(label_NG.Text) + 1).ToString();
                                }
                            }
                        }
                        else
                        {
                            isProductOKNG = false;
                            if (!VSE.Start.isFirstLoadJob)
                            {
                                label_NG.Text = (Convert.ToDouble(label_NG.Text) + 1).ToString();
                            }
                        }

                        if (isProductOKNG)
                        {
                            //第一次加载图片不发送NG信号
                            if (VSE.Start.isFirstLoadJob)
                            {
                                VSE.Start.isFirstLoadJob = false;
                            }
                            else
                            {
                                //发送OK指令
                                try
                                {
                                    tcpIPHelper_Server.Send(tcpIPHelper_Server.ClientConnectedList[0], "Vtm,1,End");
                                }
                                catch { }
                            }

                            //桌面控件显示
                            pictureBox_ProStatus.BackColor = Color.Aqua;
                            label_ProStatus.BackColor = Color.Aqua;
                            label_ProStatus.Text = "OK";
                            SaveImage(hImage, true); //OK图片保存
                        }
                        else
                        {
                            //发送NG指令,匹配成功，产品NG
                            //第一次加载图片，不发送NG信号
                            if (VSE.Start.isFirstLoadJob)
                            {
                                VSE.Start.isFirstLoadJob = false;
                            }
                            else
                            {
                                try
                                {
                                    tcpIPHelper_Server.Send(tcpIPHelper_Server.ClientConnectedList[0], "Vtm,0,End");
                                }
                                catch { }

                                QeNGImages.Enqueue(hImage);
                                QeQePoses11.Enqueue(QEPose1);
                                QeQeStrings11.Enqueue(QEstrMsg1);
                            }

                            //桌面控件显示
                            pictureBox_ProStatus.BackColor = Color.Red;
                            label_ProStatus.BackColor = Color.Red;
                            label_ProStatus.Text = "NG";
                            SaveImage(hImage, false);//NG图片保存
                            //非设备运行取图时，比如从文件或文件夹读图时，显示NG图片及其信息
                            if (!VSE.Start.ImgEnqueueFlag)
                            {
                                DisplayNGImagesNews();
                            }
                        }

                        label_CT.Text = p1.strMessage[1].ToString();
                        label7.Text = p1.strMessage[0].ToString();
                        label9.Text = p1.strMessage[1].ToString();
                        txtResults.Text = "";
                        txtResults.Text = p1.strMessage[2].ToString();

                        label_QE1.Text = DicQeJobRet[strKeys[0]].Count.ToString();
                    }
                }
            }
            else if (iCountJob == 2)
            {
                lock (objLock)
                {
                    if (DicQeJobRet[strKeys[0]].Count > 0 && DicQeJobRet[strKeys[1]].Count > 0)
                    {
                        ProResult p1 = DicQeJobRet[strKeys[0]].Dequeue();
                        ProResult p2 = DicQeJobRet[strKeys[1]].Dequeue();
                        hImage = DicQeImages[strKeys[0]].Dequeue();
                        DicQeImages[strKeys[1]].Dequeue();
                        Queue<XYU> QEPose1 = DicQeJobPose[strKeys[0]].Dequeue();
                        Queue<XYU> QEPose2 = DicQeJobPose[strKeys[1]].Dequeue();
                        Queue<string> QEstrMsg1 = DicQeStrings[strKeys[0]].Dequeue();
                        Queue<string> QEstrMsg2 = DicQeStrings[strKeys[1]].Dequeue();
                        if (p1.bJobStatus && p2.bJobStatus)
                        {
                            imgView1.SetResult(p1.bResult);
                            imgView2.SetResult(p2.bResult);
                            //双检测产品状态均OK，有产品
                            if (p1.bResult && p2.bResult)
                            {
                                //OK
                                isProductOKNG = true;
                                if (!VSE.Start.isFirstLoadJob)
                                {
                                    label_OK.Text = (Convert.ToDouble(label_OK.Text) + 1).ToString();
                                }
                            }
                            else
                            {
                                //NG
                                isProductOKNG = false;
                                if (!VSE.Start.isFirstLoadJob)
                                {
                                    label_NG.Text = (Convert.ToDouble(label_NG.Text) + 1).ToString();
                                }
                            }
                        }
                        else if ((p1.bJobStatus && !p2.bJobStatus) || (!p1.bJobStatus && p2.bJobStatus))
                        {
                            //双检测产品状态有且只有一个NG，视为有产品，产品NG
                            isProductOKNG = false;
                            if (!VSE.Start.isFirstLoadJob)
                            {
                                label_NG.Text = (Convert.ToDouble(label_NG.Text) + 1).ToString();
                            }
                        }
                        else if (!p1.bJobStatus && !p2.bJobStatus)
                        {
                            //双检测产品状态均NG，视为无产品
                            isProductOKNG = false;
                            if (!VSE.Start.isFirstLoadJob)
                            {
                                label_NG.Text = (Convert.ToDouble(label_NG.Text) + 1).ToString();
                            }
                        }

                        if (isProductOKNG)
                        {
                            //第一次加载图片不发送NG信号
                            if (VSE.Start.isFirstLoadJob)
                            {
                                VSE.Start.isFirstLoadJob = false;
                            }
                            else
                            {
                                //发送OK指令
                                try
                                {
                                    tcpIPHelper_Server.Send(tcpIPHelper_Server.ClientConnectedList[0], "Vtm,1,End");
                                }
                                catch { }
                            }

                            //桌面控件显示
                            pictureBox_ProStatus.BackColor = Color.Aqua;
                            label_ProStatus.BackColor = Color.Aqua;
                            label_ProStatus.Text = "OK";
                            SaveImage(hImage, true); //OK图片保存
                        }
                        else
                        {
                            //第一次加载图片，不发送NG信号
                            if (VSE.Start.isFirstLoadJob)
                            {
                                VSE.Start.isFirstLoadJob = false;
                            }
                            else
                            {
                                //发送NG指令,匹配成功，产品NG
                                try
                                {
                                    tcpIPHelper_Server.Send(tcpIPHelper_Server.ClientConnectedList[0], "Vtm,0,End");
                                }
                                catch { }

                                QeNGImages.Enqueue(hImage);
                                QeQePoses11.Enqueue(QEPose1);
                                QeQePoses22.Enqueue(QEPose2);
                                QeQeStrings11.Enqueue(QEstrMsg1);
                                QeQeStrings22.Enqueue(QEstrMsg2);
                            }

                            //桌面控件显示
                            pictureBox_ProStatus.BackColor = Color.Red;
                            label_ProStatus.BackColor = Color.Red;
                            label_ProStatus.Text = "NG";
                            SaveImage(hImage, false);//NG图片保存
                                                     //非设备运行取图时，比如从文件或文件夹读图时，显示NG图片及其信息
                            if (!VSE.Start.ImgEnqueueFlag)
                            {
                                DisplayNGImagesNews();
                            }
                        }

                        label_CT.Text = p1.strMessage[1].ToString();
                        label7.Text = p1.strMessage[0].ToString();
                        label9.Text = p1.strMessage[1].ToString();
                        txtResults.Text = "";
                        txtResults.Text = p1.strMessage[2].ToString() + p2.strMessage[2].ToString();

                        label_QE1.Text = DicQeJobRet[strKeys[0]].Count.ToString();
                        label_QE2.Text = DicQeJobRet[strKeys[1]].Count.ToString();
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("暂不支持图片分3个或3个以上线程同步处理功能!");
            }
           
            label_Total.Text = (Convert.ToDouble(label_OK.Text) + Convert.ToDouble(label_NG.Text)).ToString();
            label_Yeild.Text = (Convert.ToDouble(label_OK.Text) * 100 / Convert.ToDouble(label_Total.Text)).ToString("0.00") + "%";
            Application.DoEvents();
            #endregion
        }

        private bool PLCIsReady = false;//运动板卡启动/停止信号监控

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (PLCIsReady)//判断PLC是否启动
            {
                if (SOFT_START == true)
                {
                    VSE.Start.ImgEnqueueFlag = true;
                }
                else
                {
                    VSE.Start.ImgEnqueueFlag = false;
                }
            }
            else
            {
                VSE.Start.ImgEnqueueFlag = false;
            }
        }

        private void DisplayNGImagesNews()
        {
            #region 手动
            //if (QeQePoses11.Count > 0 && QeQePoses22.Count > 0)
            //{
            //    HImage NGImage = QeNGImages.Dequeue();
            //    Queue<XYU> QePoses11 = QeQePoses11.Dequeue();
            //    Queue<XYU> QePoses21 = QeQePoses22.Dequeue();
            //    Queue<string> QeStrings11 = QeQeStrings11.Dequeue();
            //    Queue<string> QeStrings21 = QeQeStrings22.Dequeue();
            //    imgView3.BeginInvoke(new MethodInvoker(() =>
            //    {
            //        imgView3.ClearWindow();
            //        imgView3.Image = NGImage;
            //        if (QePoses11.Count > 0)
            //        {
            //            for (int i = 0; i < QePoses11.Count; i++)
            //            {
            //                HXLDCont xldRectangle = new HXLDCont();
            //                XYU xYU = QePoses11.Dequeue();
            //                if (xYU == null)
            //                    continue;
            //                xldRectangle.GenRectangle2ContourXld((double)xYU.Point.X, (double)xYU.Point.Y, 0.0, 100, 80);
            //                imgView3.displayHRegion(xldRectangle.GenRegionContourXld("margin"), "red", "margin");
            //            }
            //        }

            //        if (QePoses21.Count > 0)
            //        {
            //            for (int i = 0; i < QePoses21.Count; i++)
            //            {
            //                HXLDCont xldRectangle = new HXLDCont();
            //                XYU xYU = QePoses21.Dequeue();
            //                if (xYU == null)
            //                    continue;
            //                xldRectangle.GenRectangle2ContourXld((double)xYU.Point.X, (double)xYU.Point.Y, 0.0, 100, 80);
            //                imgView3.displayHRegion(xldRectangle.GenRegionContourXld("margin"), "red", "margin");
            //            }
            //        }

            //        int iRow1 = 100;
            //        int iCloumn1 = 100;
            //        int iCloumn2 = 2100;
            //        int iInterval = 100;

            //        if (QeStrings11.Count > 0)
            //        {
            //            for (int i = 0; i < QeStrings11.Count; i++)
            //            {
            //                imgView3.displayMessage(QeStrings11.Dequeue(), iRow1 + iInterval * i, iCloumn1);
            //            }
            //        }

            //        if (QeStrings21.Count > 0)
            //        {
            //            for (int i = 0; i < QeStrings21.Count; i++)
            //            {
            //                imgView3.displayMessage(QeStrings21.Dequeue(), iRow1 + iInterval * i, iCloumn2);
            //            }
            //        }
            //    }));
            //}

            #endregion

            #region 自动
            if (QeNGImages.Count > 0 && QeQePoses11.Count > 0 && QeQePoses22.Count > 0)
            {
                HImage NGImage = QeNGImages.Dequeue();
                Queue<XYU> QePoses11 = QeQePoses11.Dequeue();
                Queue<XYU> QePoses21 = QeQePoses22.Dequeue();
                Queue<string> QeStrings11 = QeQeStrings11.Dequeue();
                Queue<string> QeStrings21 = QeQeStrings22.Dequeue();
                imgView3.Invoke(new MethodInvoker(() =>
                {
                    imgView3.ClearWindow();
                    imgView3.Image = NGImage;

                    if (QePoses11.Count > 0)
                    {
                        for (int i = 0; i < QePoses11.Count; i++)
                        {
                            HXLDCont xldRectangle = new HXLDCont();
                            XYU xYU = QePoses11.Dequeue();
                            if (xYU == null)
                                continue;
                            xldRectangle.GenRectangle2ContourXld((double)xYU.Point.X, (double)xYU.Point.Y, 0.0, 100, 80);
                            imgView3.displayHRegion(xldRectangle.GenRegionContourXld("margin"), "red", "margin");
                        }
                    }

                    if (QePoses21.Count > 0)
                    {
                        for (int i = 0; i < QePoses21.Count; i++)
                        {
                            HXLDCont xldRectangle = new HXLDCont();
                            XYU xYU = QePoses21.Dequeue();
                            if (xYU == null)
                                continue;
                            xldRectangle.GenRectangle2ContourXld((double)xYU.Point.X, (double)xYU.Point.Y, 0.0, 100, 80);
                            imgView3.displayHRegion(xldRectangle.GenRegionContourXld("margin"), "red", "margin");
                        }
                    }

                    int iRow1 = 100;
                    int iCloumn1 = 100;
                    int iCloumn2 = 2100;
                    int iInterval = 150;

                    if (QeStrings11.Count > 0)
                    {
                        for (int i = 0; i < QeStrings11.Count; i++)
                        {
                            imgView3.displayMessage(QeStrings11.Dequeue(), iRow1 + iInterval * i, iCloumn1);
                        }
                    }

                    if (QeStrings21.Count > 0)
                    {
                        for (int i = 0; i < QeStrings21.Count; i++)
                        {
                            imgView3.displayMessage(QeStrings21.Dequeue(), iRow1 + iInterval * i, iCloumn2);
                        }
                    }
                }));
            }
            else if (QeNGImages.Count > 0 && QeQePoses11.Count > 0 && QeQePoses22.Count == 0)
            {
                HImage NGImage = QeNGImages.Dequeue();
                Queue<XYU> QePoses11 = QeQePoses11.Dequeue();
                Queue<string> QeStrings11 = QeQeStrings11.Dequeue();
                imgView3.Invoke(new MethodInvoker(() =>
                {
                    imgView3.ClearWindow();
                    imgView3.Image = NGImage;

                    if (QePoses11.Count > 0)
                    {
                        for (int i = 0; i < QePoses11.Count; i++)
                        {
                            HXLDCont xldRectangle = new HXLDCont();
                            XYU xYU = QePoses11.Dequeue();
                            if (xYU == null)
                                continue;
                            xldRectangle.GenRectangle2ContourXld((double)xYU.Point.X, (double)xYU.Point.Y, 0.0, 100, 80);
                            imgView3.displayHRegion(xldRectangle.GenRegionContourXld("margin"), "red", "margin");
                        }
                    }
                    int iRow1 = 100;
                    int iCloumn1 = 100;
                    int iInterval = 100;
                    if (QeStrings11.Count > 0)
                    {
                        for (int i = 0; i < QeStrings11.Count; i++)
                        {
                            imgView3.displayMessage(QeStrings11.Dequeue(), iRow1 + iInterval * i, iCloumn1);
                        }
                    }
                }));
            }
            #endregion
        }

        private void updateTcpIPStatus(UpdateSocketStatusEventArgs e)
        {
            if (e.Connect)
            {
                pic_Status11.BackColor = Color.Aqua;
            }
            else
            {
                pic_Status11.BackColor = Color.Red;
            }
        }

        private void updateTcpIPMessage(ReceiveClientMessageEventArgs e)
        {
            listBox1.Items.Add(e.Message);
            //客户端信息解析
            string[] strArray = e.Message.Split(',').ToArray();
            switch (strArray[1])
            {
                case "0":
                    PLCIsReady = false;//视觉软件停止
                    break;

                case "1":
                    PLCIsReady = true;//视觉软件启动
                    break;

                case "2":
                    //调取NG图片
                    DisplayNGImagesNews();
                    break;
            }

        }

        private void SaveImage(HImage hImage, bool isOKNG)
        {
            if (isOKNG)
            {
                if (!Directory.Exists("D:\\Image\\OKImage"))
                {
                    Directory.CreateDirectory("D:\\Image\\OKImage");
                }
                Task.Factory.StartNew(() =>
                {
                    //hImage.WriteImage("bmp", 0, string.Format("D:\\Image\\OKImage\\{0}.bmp", DateTime.Now.ToString("yyyyMMddhhmmssffff")));
                });
            }
            else
            {
                if (!Directory.Exists("D:\\Image\\NGImage"))
                {
                    Directory.CreateDirectory("D:\\Image\\NGImage");
                }
                Task.Factory.StartNew(() =>
                {
                    hImage.WriteImage("bmp", 0, string.Format("D:\\Image\\NGImage\\{0}.bmp", DateTime.Now.ToString("yyyyMMddhhmmssffff")));
                });
            }
        }

        bool SOFT_START = false;

        private void Start_MachineStateChangeEvent(VSE.MachineStateChangeEventArgs e)
        {
            label1.BackColor = Color.Gray;
            label2.BackColor = Color.Gray;
            label3.BackColor = Color.Gray;
            label6.BackColor = Color.Gray;
            switch (e.MacState)
            {
                case VSE.state.start://VSE启动信号进入
                    SOFT_START = true;
                    btnClear_Click(null, null);
                    if (PLCIsReady)//判断PLC是否启动
                    {
                        VSE.Start.ImgEnqueueFlag = true;
                    }
                    else
                    {
                        VSE.Start.ImgEnqueueFlag = false;
                    }
                    label1.BackColor = Color.Aqua;
                    break;

                case VSE.state.stop:
                    VSE.Start.ImgEnqueueFlag = false;
                    SOFT_START = false;
                    label3.BackColor = Color.Red;
                    break;

                case VSE.state.pause:
                    VSE.Start.ImgEnqueueFlag = false;
                    SOFT_START = false;
                    label2.BackColor = Color.Orange;
                    break;

                case VSE.state.reset:
                    VSE.Start.ImgEnqueueFlag = false;
                    label6.BackColor = Color.Yellow;
                    break;

                default:
                    break;
            }
        }

        private void Start_ProductNewsClearEvent()
        {
            btnClear_Click(null, null);//异常强制关停时，消息队列清零
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            light = new LightController_QSX_PSH(txt_Port1.Text.ToString(), Convert.ToInt32(txt_BaudRate.Text));
            btnConnect1_Click(null, null);
            btnClear_Click(null, null);
            timer1.Enabled = true;
            timer2.Enabled = true;
            imgView3.SetImageViewColor(Color.DarkRed);

            tcpIPHelper_Server.Listen();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (light.InitSucceed)
            {
                light.CloseController();
            }
            tcpIPHelper_Server.Close();
            timer1.Enabled = false;
            timer2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = true;
            button2.Enabled = true;
            btnClear_Click(null, null);

            VSE.Start.SetVSEMachineState(VSE.state.start);                      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button3.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = true;
            VSE.Start.SetVSEMachineState(VSE.state.stop);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            button1.Enabled = true;
            button3.Enabled = false;
            button2.Enabled = true;
            VSE.Start.SetVSEMachineState(VSE.state.pause);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            VSE.Start.SetVSEMachineState(VSE.state.reset);
        }

        private void btn_Output_OK_Click(object sender, EventArgs e)
        {
     
        }

        private void btn_Output_NG_Click(object sender, EventArgs e)
        {
          
        }

        private void btnConnect1_Click(object sender, EventArgs e)
        {
            if (btnConnect1.Text == "连接")
            {
                try
                {
                    light.OpenController();
                    if (light.InitSucceed)
                    {
                        btnConnect1.Text = "断开连接";
                        pic_Status1.BackColor = Color.Aqua;
                        pic_Status12.BackColor = Color.Aqua;
                        btnConnect1.BackColor = Color.Red;
                    }
                    else
                    {
                        btnConnect1.Text = "连接";
                        pic_Status1.BackColor = Color.Red;
                        pic_Status12.BackColor = Color.Red;
                        btnConnect1.BackColor = Color.Aqua;
                    }
                }
                catch
                {
                    btnConnect1.Text = "连接";
                    btnConnect1.BackColor = Color.Aqua;
                    pic_Status1.BackColor = Color.Red;
                    pic_Status12.BackColor = Color.Red;
                }
            }
            else if (btnConnect1.Text == "断开连接")
            {
                light.CloseController();
                btnConnect1.Text = "连接";
                pic_Status1.BackColor = Color.Red;
                pic_Status12.BackColor = Color.Red;
                btnConnect1.BackColor = Color.Aqua;
            }
        }

        private void cbx_CH1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_CH1.Checked)
            {
                light.OpenChannel(1);
            }
            else
            {
                light.CloseChannel(1);
            }
        }

        private void cbx_CH2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_CH2.Checked)
            {
                light.OpenChannel(2);
            }
            else
            {
                light.CloseChannel(2);
            }
        }

        private void trackBar_CH1_ValueChanged(object sender, EventArgs e)
        {
            light.SetValue(1, trackBar_CH1.Value);
        }

        private void trackBar_CH2_ValueChanged(object sender, EventArgs e)
        {
            light.SetValue(2, trackBar_CH2.Value);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            #region 手动判定
            //QeJob1.Clear();
            //QeJob2.Clear();
            //QeProMsg1.Clear();  
            //QeProMsg2.Clear();
            //QeResults.Clear();
            //QeImages.Clear();
            //QeQePoses1.Clear();
            //QeQePoses2.Clear();
            //QeQeStrings1.Clear();
            //QeQeStrings2.Clear();
            //label_QE1.Text = QeJob1.Count.ToString();
            //label_QE2.Text = QeJob2.Count.ToString();
            #endregion

            QeNGImages.Clear();
            QeQePoses11.Clear();
            QeQePoses22.Clear();
            QeQeStrings11.Clear();
            QeQeStrings22.Clear();

            if (DicQeJobRet.Count == 1)
            {
                string[] strKesArr = DicQeJobRet.Keys.ToArray();
                DicQeJobRet[strKesArr[0]].Clear();
                DicQeImages[strKesArr[0]].Clear();
                DicQeStrings[strKesArr[0]].Clear();
                DicQeJobPose[strKesArr[0]].Clear();
                label_QE1.Text = DicQeJobRet[strKesArr[0]].Count.ToString();
                label_QE2.Text = "0";
            }
            else if (DicQeJobRet.Count == 2)
            {
                string[] strKesArr = DicQeJobRet.Keys.ToArray();
                DicQeJobRet[strKesArr[0]].Clear();
                DicQeJobRet[strKesArr[1]].Clear();
                DicQeImages[strKesArr[0]].Clear();
                DicQeImages[strKesArr[1]].Clear();
                DicQeStrings[strKesArr[0]].Clear();
                DicQeStrings[strKesArr[1]].Clear();
                DicQeJobPose[strKesArr[0]].Clear();
                DicQeJobPose[strKesArr[1]].Clear();
                label_QE1.Text = DicQeJobRet[strKesArr[0]].Count.ToString();
                label_QE2.Text = DicQeJobRet[strKesArr[1]].Count.ToString();
            }

        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            label_OK.Text = "0";
            label_NG.Text = "0";
            label_Total.Text = (Convert.ToDouble(label_OK.Text) + Convert.ToDouble(label_NG.Text)).ToString();
            label_Yeild.Text = (Convert.ToDouble(label_OK.Text) * 100 / Convert.ToDouble(label_Total.Text)).ToString("0.00") + "%";
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                tcpIPHelper_Server.Send(tcpIPHelper_Server.ClientConnectedList[0], textBox1.Text.ToString());
            }
            catch { }
        }

 
    }

    public class ProResult
    {
        public string jobName;
        public bool bJobStatus;
        public bool bProStatus;
        public string[] strMessage = new string[3];
        public bool bResult;
    }

}
