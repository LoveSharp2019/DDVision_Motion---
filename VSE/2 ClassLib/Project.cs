using LightController;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    [Serializable]
    public class Project
    {

        internal List<LightController_Base> L_lightController = new List<LightController_Base>();
        internal List<TCPClient> L_TCPClient = new List<TCPClient>();
        internal List<TCPSever> L_TCPSever = new List<TCPSever>();
        internal List<Scaner> L_Scaner = new List<Scaner>();
        internal List<Serial> L_Serial = new List<Serial>();
        /// <summary>
        /// 方案集合
        /// </summary>
        internal List<Scheme> L_engineList = new List<Scheme>();
        /// <summary>
        /// 当前正在使用的方案
        /// </summary>
        public Scheme curEngine = new Scheme();
        /// <summary>
        /// 项目配置
        /// </summary>
        public Configuration configuration = new Configuration();
        /// <summary>
        /// 项目实例
        /// </summary>
        private static Project _instance;

        public static Project Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Project();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        /// <summary>
        /// 根据方案名称查找方案
        /// </summary>
        /// <param name="engineName"></param>
        /// <returns></returns>
        internal Scheme FindEngineByName(string engineName)
        {
            try
            {
                for (int i = 0; i < L_engineList.Count; i++)
                {
                    if (L_engineList[i].schemeName == engineName)
                        return L_engineList[i];
                }
                return new Scheme();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return new Scheme();
            }
        }

        /// <summary>
        /// 加载项目
        /// </summary>
        internal static Project LoadProject(string path)
        {
            try
            {
                Job temp = new Job();     
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);

                try
                {
                    Project.Instance = (Project)formatter.Deserialize(stream);
                }
                catch (Exception ex)
                {
                    Log.SaveError(Project.Instance.configuration.dataPath,ex);
                }
                
                stream.Close();
             
                Win_ImageWindow.Instance.SetLayout();

                if (Project.Instance.L_engineList.Count > 0)
                {
                    Win_Main.Instance.XXXXXXXXXXXXXlbl_title.Text = Project.Instance.configuration.ProgramTitle;
                    Win_Main.Instance.lbl_curEngine.Text = string.Format("当前方案：{0}", Project.Instance.curEngine.schemeName);
                    Win_Job.Instance.ToolBoxEdit_ProjectName.Text = string.Format("当前流程：{0}", Project.Instance.curEngine.schemeName);
                    int iIndex = 0;
                    for (int i = 0; i < Project.Instance.L_engineList.Count; i++)
                    {
                        if (Project.Instance.L_engineList[i].schemeName == Project.Instance.curEngine.schemeName)
                        {
                            iIndex = i;
                            break;
                        }
                    }
                    Project.Instance.curEngine = Project.Instance.L_engineList[iIndex];
                    Win_Job.Instance.listView1.Nodes.Clear();

                    if (Project.Instance.curEngine.L_jobList.Count > 0) 
                    {
                        Job.LoadJob(Project.Instance.curEngine.L_jobList[0]);
                        Job.OnFlushList();

                        //hzy20220727
                        List<string> list = new List<string>();
                        for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
                        {
                            list.Add(Project.Instance.curEngine.L_jobList[i].jobName);
                        }
                        string[] strJobNmaeList = list.ToArray();
                        Start.OnUpdateJobNameList(new JobNameListEventArgs(strJobNmaeList));
                    }
                }
                for (int i = 0; i < Project.Instance.L_engineList.Count; i++)
                {
                    Win_EngineManager.Instance.cbx_engineList.Items.Add(Project.Instance.L_engineList[i].schemeName);
                    Win_Main.Instance.lbl_curEngine.DropDownItems.Add(Project.Instance.L_engineList[i].schemeName);
                }
                Win_Main.Instance.OutputMsg("项目加载成功",Win_Log.InfoType.tip);
                return Project.Instance;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return new Project();
            }
        }

        /// <summary>
        /// 导入项目
        /// </summary>
        internal static void InportProject()
        {
            try
            {
                System.Windows.Forms.OpenFileDialog dig_openFileDialog = new System.Windows.Forms.OpenFileDialog();
                dig_openFileDialog.Title = ("请指定项目文件");
                dig_openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                dig_openFileDialog.Filter = "项目文件(*.pjt)|*.pjt";

                if (dig_openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(dig_openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    Project.Instance = (Project)formatter.Deserialize(stream);
                    stream.Close();

                    if (Project.Instance.L_engineList.Count > 0)
                    {
                        Win_Main.Instance.XXXXXXXXXXXXXlbl_title.Text = Project.Instance.configuration.ProgramTitle;
                        Win_Main.Instance.lbl_curEngine.Text = string.Format("当前方案：{0}", Project.Instance.curEngine.schemeName);
                        Win_Job.Instance.ToolBoxEdit_ProjectName.Text = string.Format("当前流程：{0}", Project.Instance.curEngine.schemeName);

                        Project.Instance.curEngine = Project.Instance.L_engineList[0];
                        Win_Job.Instance.listView1.Nodes.Clear();

                        for (int i = 0; i < Project.Instance.L_engineList[0].L_jobList.Count; i++)
                        {
                            Job.InportJob(Project.Instance.curEngine.L_jobList[i]);
                            Job.OnFlushList();
                        }
                    }

                    Win_EngineManager.Instance.cbx_engineList.Items.Clear();
                    for (int i = 0; i < Project.Instance.L_engineList.Count; i++)
                    {
                        Win_EngineManager.Instance.cbx_engineList.Items.Add(Project.Instance.L_engineList[i].schemeName);
                        //防呆，防止lal_curEngine添加重复项目名称
                        bool isExist = true;
                        for (int j = 0; j < Win_Main.Instance.lbl_curEngine.DropDownItems.Count; j++) 
                        {
                            if (Win_Main.Instance.lbl_curEngine.DropDownItems[j].Text == Project.Instance.L_engineList[i].schemeName)
                            {
                                isExist = false;
                            }
                        }
                        if (isExist)
                        {
                            Win_Main.Instance.lbl_curEngine.DropDownItems.Add(Project.Instance.L_engineList[i].schemeName);
                        }
                    }
                    Win_EngineManager.Instance.cbx_engineList.Text = Project.Instance.L_engineList[0].schemeName;
                    Win_Main.Instance.OutputMsg("方案导入成功",Win_Log.InfoType.tip);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        /// <summary>
        /// 导出项目
        /// </summary>
        internal static void ExportProject()
        {
            try
            {
                if (Project.Instance.L_engineList.Count > 0)
                {
                    System.Windows.Forms.SaveFileDialog dig_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                    dig_saveFileDialog.FileName = Project.Instance.configuration.ProgramTitle;
                    dig_saveFileDialog.Title = ("请指定项目导出路径");
                    dig_saveFileDialog.Filter = "项目文件|*.pjt";
                    dig_saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    if (dig_saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        IFormatter formatter = new BinaryFormatter();
                        Stream stream = new FileStream(dig_saveFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                        formatter.Serialize(stream, Project.Instance);
                        stream.Close();

                        //更新结果下拉框
                        //Win_ImageWindow.Instance.Update_Last_Run_Result_Image_List();
                        Win_Main.Instance.OutputMsg("项目导出成功",Win_Log.InfoType.tip);
                    }
                }
                else
                {
                    Win_Main.Instance.OutputMsg("当前项目尚未添加任何方案，不可导出",Win_Log.InfoType.error);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        /// <summary>
        /// 保存项目
        /// </summary>
        internal static void SaveProject()
        {
            try
            {
                if (Win_Main.openProject)
                    return;

                string[] files = Directory.GetFiles(Application.StartupPath + "\\Config\\Project\\Vision");
                for (int i = 0; i < files.Length; i++)
                {
                    File.SetAttributes(files[i], FileAttributes.Normal);//file为要删除的文件
                    string f = files[i];
                    new FileInfo(f).Attributes = FileAttributes.Normal;
                    File.Delete(f);
                    File.Delete(files[i]);
                }

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(string.Format(Application.StartupPath + "\\Config\\Project\\Vision\\{0}.pjt", Project.Instance.configuration.ProgramTitle), FileMode.OpenOrCreate, FileAccess.Write, FileShare.Delete);
                formatter.Serialize(stream, Project.Instance);
                stream.Close();

                //更新结果下拉框
                //Win_ImageWindow.Instance.Update_Last_Run_Result_Image_List();
                Win_Main.Instance.OutputMsg("保存项目成功",Win_Log.InfoType.tip);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

    }
}
