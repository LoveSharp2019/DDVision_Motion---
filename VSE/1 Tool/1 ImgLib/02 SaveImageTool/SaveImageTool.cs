using HalconDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using VSE.Core;

namespace VSE
{
    [Serializable]
    internal class SaveImageTool : ToolBase
    {
        internal SaveImageTool()
        {
            string jobname = Win_Job.Instance.JobEdit_JobName.Text.Split('：')[1];
         
           
            
            imageSavePath = string.Format("D:\\DDVision\\Image\\{0}\\原始图像", jobname);
        }
        static Queue<Imagee> queue = new Queue<Imagee>();
        internal string imageSavePath = string.Empty;
        private new object obj = new object();
        internal string imageFormat = "tif";
        internal int saveDays = 7;
        internal bool expandTime = true;
        internal bool autoClear = true;
        internal bool autoCreateDirectory = true;
        internal string imageName = "图像";
        internal ImageSource imageSource = ImageSource.InputImage;
        internal void ResetTool()
        {
            try
            {
                imageSavePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                imageFormat = "tiff";
                saveDays = 7;
                expandTime = true;
                autoClear = true;
                autoCreateDirectory = true;
                imageName = "图像";
                Win_SaveImageTool.Instance.tbx_imageSavePath.Text = imageSavePath;
                Win_SaveImageTool.Instance.textBox2.Text = imageName;
                Win_SaveImageTool.Instance.comboBox1.Text = imageFormat;
                Win_SaveImageTool.Instance.textBox1.Text = saveDays.ToString();
                Win_SaveImageTool.Instance.checkBox1.Checked = expandTime;
                Win_SaveImageTool.Instance.checkBox2.Checked = autoClear;
                Win_SaveImageTool.Instance.checkBox3.Checked = autoCreateDirectory;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void ClearFile()
        {
            try
            {
                DateTime now = DateTime.Now;
                string[] fileList = Directory.GetDirectories(imageSavePath);
                for (int i = 0; i < fileList.Length; i++)
                {
                    DirectoryInfo dir = new System.IO.DirectoryInfo(fileList[i]);
                    DateTime dt = dir.CreationTime;
                    int tep = (now - dt).Days;
                    if ((now - dt).Days > saveDays)
                    {
                        File.Delete(fileList[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
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
                    if (!Directory.Exists(imageSavePath))
                    {
                        Directory.CreateDirectory(imageSavePath);
                    }
                    string directoryName = string.Empty;
                    if (autoCreateDirectory)
                    {
                        DateTime curTime = DateTime.Now;
                        if (!Directory.Exists(imageSavePath + "\\" + curTime.ToString("yyyy_MM_dd")))
                        {
                            Directory.CreateDirectory(imageSavePath + "\\" + curTime.ToString("yyyy_MM_dd"));
                        }
                        directoryName = "\\" + curTime.ToString("yyyy_MM_dd");
                    }
                    else
                    {
                        directoryName = string.Empty;
                    }

                    string fileName = string.Empty;
                    if (expandTime)
                    {
                        fileName = imageSavePath + directoryName + "\\" + imageName + DateTime.Now.ToString("HH_mm_ss ffff") + "." + imageFormat;
                    }
                    else
                    {
                        int index;
                        for (index = 1; index < 10000; index++)
                        {
                            fileName = imageSavePath + directoryName + "\\"+ imageName + index + "." + imageFormat;
                            if (!File.Exists(fileName))
                                break;
                        }
                    }

                    HObject image;

                    if (imageSource == ImageSource.WindowImage)
                    {
                        Lxc.VisionPlus.ImageView.ImgView ff = GetImageWindowControl();

                        if (ff != null)
                        {
                            string temp1 = string.Empty;
                            if (imageFormat == "tif")
                                temp1 = "tiff";
                            else
                                temp1 = imageFormat;

                           
                            image= ff.hv_window.DumpWindowImage();


                            //////HOperatorSet.DumpWindowImage(out image, Job.FindJobByName(jobName).www);

                            //HOperatorSet.WriteImage(image, temp1, 0, fileName);


                            Imagee iii = new Imagee();
                            iii.image = image;
                            iii.format = temp1;
                            iii.fileName = fileName;

                            queue.Enqueue(iii);

                            bb = true;

                            if (ttt == null || !ttt.IsAlive)
                            {
                                ttt = new Thread(SaveImage);
                                ttt.IsBackground = true;
                                ttt.Start();
                            }
                        }
                        else
                        {
                            Win_Log.Instance.OutputMsg("请先选中图像窗口",Win_Log.InfoType.error);
                        }
                    }
                    else
                    {
                        string temp = string.Empty;
                        if (imageFormat == "tif")
                            temp = "tiff";
                        else
                            temp = imageFormat;

                        //HOperatorSet.WriteImage(toolPar.InputPar.图像, temp, 0, fileName);
                        //此处使用队列来存储凸显，因为直接存储图像的话，连续存储两张图像之间必须间隔1秒以上，否则部分图像就没有被正常存储，只能出此下册
                        Imagee iii = new Imagee();
                        iii.image = toolPar.InputPar.图像;
                        iii.format = temp;
                        iii.fileName = fileName;

                        queue.Enqueue(iii);

                        bb = true;

                        if (ttt == null || !ttt.IsAlive)
                        {
                            ttt = new Thread(SaveImage);
                            ttt.IsBackground = true;
                            ttt.Start();
                        }


                    }

                    Thread th = new Thread(ClearFile);
                    th.IsBackground = true;
                    th.Start();


                    toolRunStatu =ToolRunStatu.成功;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        static Thread ttt;
        public static bool bb = false;
        [Serializable]
        public struct Imagee
        {
            public HObject image;
            public string format;
            public string fileName;
        }
        private static void SaveImage()
        {
            try
            {
                while (bb)
                {
                    if (queue.Count > 0)
                    {
                        Imagee image = queue.Dequeue();
                        HOperatorSet.WriteImage(image.image, image.format, 0, image.fileName);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        bb = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        internal ToolPar toolPar = new ToolPar();

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
            private HObject _图像;
            public HObject 图像
            {
                get { return _图像; }
                set { _图像 = value; }
            }
        }
        [Serializable]
        public class RunPar
        {

        }
        [Serializable]
        internal class ResultPar
        {
        }



    }
    internal enum ImageSource
    {
        InputImage,
        WindowImage,
    }
}
