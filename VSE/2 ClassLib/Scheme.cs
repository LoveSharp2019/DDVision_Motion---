using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Reflection;
using LightController;
using HalconDotNet;
using VSE.Core;
using VControls;

namespace VSE
{
    [Serializable]
    public class Scheme : ICloneable
    {

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        internal Dictionary<DateTime, string> D_historyAlarm = new Dictionary<DateTime, string>();


        internal GlobelVariable globelVariable = new GlobelVariable();
        // internal SmartPosTable smartPosTable = new SmartPosTable();

        internal string GetNewName(string type)
        {
            int i;
            for (i = 0; i < 1000; i++)
            {
                bool exist = false;
                for (int j = 0; j < globelVariable.GetGlobalVariableCount(); j++)
                {
                    if (globelVariable.GetGlobalVariable(j).name == type + i)
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    return type + i;
                }
            }

            return "";
        }

        internal static void OpenScheme()
        {

            //保存当前方案
            Project.SaveProject();
            // SaveAll();
            Project.Instance.configuration.Save();

            try
            {
                System.Windows.Forms.OpenFileDialog dig_openFileDialog = new System.Windows.Forms.OpenFileDialog
                {
                    Title = ("请指定工程文件"),
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                    Filter = "工程文件(*.pjt)|*.pjt"
                };
                if (dig_openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Job.isDrawing = true;
                    Project.Instance.configuration.L_recentlyOpendFile.Insert(0, dig_openFileDialog.FileName);
                    Project.Instance.configuration.Save();
                    Project.SaveProject();

                    Win_Main.openProject = true;

                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(dig_openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    Project.Instance = (Project)formatter.Deserialize(stream);
                    stream.Close();

                    if (Project.Instance.L_engineList.Count > 0)
                    {
                        Win_Main.Instance.XXXXXXXXXXXXXlbl_title.Text = Project.Instance.configuration.ProgramTitle;//软件显示专用检测项目名称
                        Win_Main.Instance.lbl_curEngine.Text = string.Format("当前方案：{0}", Project.Instance.curEngine.schemeName);//主窗体显示当前项目名称
                        Win_Job.Instance.ToolBoxEdit_ProjectName.Text = string.Format("当前流程：{0}", Project.Instance.curEngine.schemeName);//Job编辑器显示当前选项项目名
                        Project.Instance.curEngine = Project.Instance.L_engineList[0];//所有方案中默认选第一个
                        Win_Job.Instance.listView1.Nodes.Clear();//清空Job集合

                        for (int i = 0; i < Project.Instance.L_engineList[0].L_jobList.Count; i++)
                        {
                            Job.InportJob(Project.Instance.curEngine.L_jobList[i]);
                            Job.OnFlushList();
                        }
                    }

                    Win_EngineManager.Instance.cbx_engineList.Items.Clear();
                    Win_Main.Instance.lbl_curEngine.DropDownItems.Clear();
                    for (int i = 0; i < Project.Instance.L_engineList.Count; i++)
                    {
                        Win_EngineManager.Instance.cbx_engineList.Items.Add(Project.Instance.L_engineList[i].schemeName);
                        Win_Main.Instance.lbl_curEngine.DropDownItems.Add(Project.Instance.L_engineList[i].schemeName);//主窗体方案集合
                    }
                   // Job.isDrawing = false;
                    Win_Main.Instance.OutputMsg("项目导入成功",Win_Log.InfoType.tip);

                }

            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }

        }
        /// <summary>
        /// 方案名称
        /// </summary>
        internal string schemeName = "未创建";
        /// <summary>
        /// 流程集合
        /// </summary>
        internal List<Job> L_jobList = new List<Job>();


        /// <summary>
        /// 通过流程名获取流程
        /// </summary>
        /// <param name="jobName">流程名</param>
        /// <returns>流程</returns>
        internal Job FindJobByName(string jobName)
        {
            try
            {
                for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
                {
                    if ((Project.Instance.curEngine.L_jobList[i]).jobName == jobName)
                        return Project.Instance.curEngine.L_jobList[i];
                }
                Win_MessageBox.Instance.MessageBoxShow("未找到名为" + jobName + "的流程（错误代码：00001）");
                return null;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return null;
            }
        }
        /// <summary>
        /// 加载方案
        /// </summary>
        internal static Scheme LoadScheme(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
            try
            {
                Project.Instance.curEngine = (Scheme)formatter.Deserialize(stream);
                Project.Instance.L_engineList.Add(Project.Instance.curEngine);
                stream.Close();

                Win_Job.Instance.listView1.Nodes.Clear();
                for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
                {
                    Job.InportJob(Project.Instance.curEngine.L_jobList[i]);
                }
                Win_Main.Instance.OutputMsg("方案加载成功",Win_Log.InfoType.tip);
                return Project.Instance.curEngine;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                stream.Close();
                return new Scheme();
            }
        }

        /// <summary>
        /// 加载方案
        /// </summary>
        internal static Scheme LoadScheme(Scheme engine)
        {
            try
            {
                Win_Main.Instance.XXXXXXXXXXXXXlbl_title.Text = Project.Instance.configuration.ProgramTitle;
                Win_Main.Instance.lbl_curEngine.Text = string.Format("当前方案：{0}", engine.schemeName);
                Win_Job.Instance.listView1.Nodes.Clear();
                Win_Job.Instance.tbc_jobs.Controls.Clear();
                Job.OnFlushList();
               
                return Project.Instance.curEngine;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return new Scheme();
            }
        }

        /// <summary>
        /// 导入方案
        /// </summary>
        internal static void InportScheme()
        {
            try
            {
                System.Windows.Forms.OpenFileDialog dig_openFileDialog = new System.Windows.Forms.OpenFileDialog
                {
                    Title = "请选择方案文件",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                    Filter = "方案文件(*.pjt)|*.pjt"
                };
                if (dig_openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(dig_openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    Scheme TempEngine= (Scheme)formatter.Deserialize(stream);
                    stream.Close();

                    Project.Instance.L_engineList.Add(TempEngine);
                    //Win_Job.Instance.listView1.Nodes.Clear();
                    //for (int i = 0; i < TempEngine.L_jobList.Count; i++)
                    //{
                    //    Job.InportJob(Project.Instance.curEngine.L_jobList[i]);
                    //}
                    Win_Main.Instance.OutputMsg( "方案导入成功",Win_Log.InfoType.tip);
                   

                    Win_EngineManager.Instance.cbx_engineList.Items.Add(TempEngine.schemeName);
                    Win_Main.Instance.lbl_curEngine.DropDownItems.Add(TempEngine.schemeName);
                    if (Win_EngineManager.Instance.cbx_engineList.Items.Count > 0)
                    {
                        Win_EngineManager.Instance.cbx_engineList.Text = TempEngine.schemeName;
                        Win_Main.Instance.lbl_curEngine.DropDownItems[Win_Main.Instance.lbl_curEngine.DropDownItems.Count - 1].Select();
                        Win_Main.Instance.lbl_curEngine.Text = "当前方案：" + TempEngine.schemeName;
                        Win_Job.Instance.ToolBoxEdit_ProjectName.Text = "当前流程：" + TempEngine.schemeName;
                    }
                    Scheme.SwitchScheme(TempEngine.schemeName);

                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /* 利用反射实现深拷贝*/
        public static object DeepCopy(object _object)
        {
            Type T = _object.GetType();
            object o = Activator.CreateInstance(T);
            PropertyInfo[] PI = T.GetProperties();
            for (int i = 0; i < PI.Length; i++)
            {
                PropertyInfo P = PI[i];
                P.SetValue(o, P.GetValue(_object, null), null);
            }
            return o;
        }

        /// <summary>
        /// 删除方案
        /// </summary>
        internal static void DeleteScheme()
        {
            try
            {
                Win_ConfirmBox.Instance.lbl_info.Text = ( "\r\n确定要删除当前方案吗？");
                Win_ConfirmBox.Instance.ShowDialog();
                if (Win_ConfirmBox.Instance.Result != ConfirmBoxResult.Yes)
                {
                    return;
                }
                string schemeName = Win_EngineManager.Instance.cbx_engineList.Text;
                if (Project.Instance.curEngine.schemeName == schemeName)
                {
                    Win_Job.Instance.listView1.Nodes.Clear();
                }

                if (Project.Instance.L_engineList.Count > 0)
                {
                    Project.Instance.L_engineList.Remove(Project.Instance.L_engineList[Win_EngineManager.Instance.cbx_engineList.SelectedIndex]);
                    //同步判断,删除tbc_jobs中内容
                    if (Win_Job.Instance.tbc_jobs.Controls.Count == 1)
                    {
                        Win_Job.Instance.tbc_jobs.Controls.RemoveAt(0);
                    }

                    Win_EngineManager.Instance.cbx_engineList.Items.Remove(schemeName);
                    RemoveDropDownItems(schemeName);
                    string BasePath = string.Format(Application.StartupPath + "\\Config\\Project\\Vision\\{0}\\{1}\\", Project.Instance.configuration.ProgramTitle, schemeName);
                    if (System.IO.Directory.Exists(BasePath))
                    {
                        try
                        {
                            Directory.Delete(BasePath); 
                            Directory.Delete(Application.StartupPath + @"\参数\料号参数\Product\" + schemeName);
                        }
                        catch { }
                    }
                    Win_Main.Instance.OutputMsg("删除方案：" + schemeName, Win_Log.InfoType.tip);

                    if (Project.Instance.L_engineList.Count > 0)
                    {
                        //Project.Instance.curEngine = Project.Instance.L_engineList[0];
                        Win_EngineManager.Instance.cbx_engineList.Text = Project.Instance.L_engineList[0].schemeName;
                        Win_Main.Instance.lbl_curEngine.Text = Project.Instance.L_engineList[0].schemeName;
                        Scheme.SwitchScheme(Project.Instance.L_engineList[0].schemeName);
                    }
                    else
                    {
                        Project.Instance.curEngine = new Scheme();
                        Win_Main.Instance.lbl_curEngine.Text = "当前方案：未创建";
                        Win_Job.Instance.ToolBoxEdit_ProjectName.Text = "当前流程：未创建";
                        Win_EngineManager.Instance.cbx_engineList.Text = "当前方案：未创建";
                        Win_EngineManager.Instance.dgv_engineInfo.Items.Clear();
                        Win_EngineManager.Instance.dgv_engineInfo.RowCount = 1;
                        for (int i = 0; i < 1; i++)
                        {
                            Win_EngineManager.Instance.dgv_engineInfo.Items[i].CellText = String.Format("{0,2}  {1}", (i + 1), "当前流程：未创建");
                        }
                    }
                    // Job.isDrawing = false;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        internal static void DeleteScheme1()
        {
            try
            {
                Win_ConfirmBox.Instance.lbl_info.Text = ("\r\n确定要删除当前方案吗？");
                Win_ConfirmBox.Instance.ShowDialog();
                if (Win_ConfirmBox.Instance.Result != ConfirmBoxResult.Yes)
                {
                    return;
                }

                //当前项目名
                string schemeName = Win_Main.Instance.lbl_curEngine.Text.Split('：')[1].ToString();

                //如果是当前项目，清空listView1列表
                if (Project.Instance.curEngine.schemeName == schemeName)
                {
                    Win_Job.Instance.listView1.Nodes.Clear();
                }

                if (Project.Instance.L_engineList.Count > 0)
                {

                    for (int i = 0; i < Project.Instance.L_engineList.Count; i++)
                    {
                        if (Project.Instance.L_engineList[i].schemeName == schemeName)
                        {
                            //移除项目中选中方案
                            Project.Instance.L_engineList.Remove(Project.Instance.L_engineList[i]);
                            if (Win_Job.Instance.tbc_jobs.Controls.Count == 1)
                            {
                                //同步判断,删除tbc_jobs中内容
                                Win_Job.Instance.tbc_jobs.Controls.RemoveAt(0);
                            }
                        }
                    }
                    //选项栏对应清空
                    Win_EngineManager.Instance.cbx_engineList.Items.Remove(schemeName);
                    //移除主窗口lbl_curEngine中方案名称
                    RemoveDropDownItems(schemeName);
                    string BasePath = string.Format(Application.StartupPath + "\\Config\\Project\\Vision\\{0}\\{1}\\", Project.Instance.configuration.ProgramTitle, schemeName);
                    if (System.IO.Directory.Exists(BasePath))
                    {
                        try
                        {
                            Directory.Delete(BasePath);
                            Directory.Delete(Application.StartupPath + @"\参数\料号参数\Product\" + schemeName);
                        }
                        catch { }
                    }

                    Win_Main.Instance.OutputMsg("删除方案：" + schemeName, Win_Log.InfoType.tip);
                    if (Project.Instance.L_engineList.Count > 0)
                    {
                        //Project.Instance.curEngine = Project.Instance.L_engineList[0];
                        Win_EngineManager.Instance.cbx_engineList.Text = Project.Instance.L_engineList[0].schemeName;
                        Win_Main.Instance.lbl_curEngine.Text = Project.Instance.L_engineList[0].schemeName;
                        Scheme.SwitchScheme(Project.Instance.L_engineList[0].schemeName);
                    }
                    else
                    {
                        Project.Instance.curEngine = new Scheme();
                        Win_Main.Instance.lbl_curEngine.Text = "当前方案：未创建";
                        Win_Job.Instance.ToolBoxEdit_ProjectName.Text = "当前流程：未创建";
                    }
                    // Job.isDrawing = false;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }

        /// <summary>
        /// 移除主窗口lbl_curEngine中方案名称
        /// </summary>
        /// <param name="Key"></param>
        private static void RemoveDropDownItems(String Key)
        {
            for (int i = 0; i < Win_Main.Instance.lbl_curEngine.DropDownItems.Count; i++)
            {
                if (Win_Main.Instance.lbl_curEngine.DropDownItems[i].Text== Key)
                {
                    Win_Main.Instance.lbl_curEngine.DropDownItems.RemoveAt(i);
                    return;
                }
            }
          
        }

        /// <summary>
        /// 导出方案
        /// </summary>
        internal static void ExportScheme()
        {
            try
            {
                if (Project.Instance.curEngine.L_jobList.Count > 0)
                {
                    System.Windows.Forms.SaveFileDialog dig_saveFileDialog = new System.Windows.Forms.SaveFileDialog
                    {
                        FileName = Project.Instance.curEngine.schemeName,
                        Title = "请选择方案保存路径",
                        Filter = "方案文件(*.pjt)|*.pjt",
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
                    };
                    if (dig_saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        IFormatter formatter = new BinaryFormatter();
                        Stream stream = new FileStream(dig_saveFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                        formatter.Serialize(stream, Project.Instance.curEngine);
                        stream.Close();

                        //更新结果下拉框
                        Win_Main.Instance.OutputMsg("方案导出成功",Win_Log.InfoType.tip);
                    }
                }
                else
                {
                    Win_Main.Instance.OutputMsg("当前项目尚未添加方案，不可导出",Win_Log.InfoType.error);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }


        /// <summary>
        /// 克隆方案
        /// </summary>
        public static class ObjectCopier
        {
            public static Engine Clone<Engine>(Engine source)
            {
                if (!typeof(Engine).IsSerializable)
                {
                    throw new ArgumentException("The type must be serializable.", "source");
                }

                if (source == null)
                {
                    return default;
                }

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new MemoryStream();
                using (stream)
                {
                    formatter.Serialize(stream, source);
                    stream.Seek(0, SeekOrigin.Begin);
                    return (Engine)formatter.Deserialize(stream);
                }
            }
        }

        /// <summary>
        /// 选择方案
        /// </summary>
        /// <param name="name"></param>
        internal static void SwitchScheme(string name)
        {
            string OldSchemeName = Project.Instance.curEngine.schemeName;
            if (name == OldSchemeName)
            {
                Win_Main.Instance.lbl_curEngine.Text = string.Format("当前方案：{0}", OldSchemeName);
                return;
            }
            Project.Instance.curEngine = Project.Instance.FindEngineByName(name);
            Win_EngineManager.Instance.dgv_engineInfo.RowCount= Project.Instance.curEngine.L_jobList.Count;
            for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
            {
                Win_EngineManager.Instance.dgv_engineInfo.Items[i].CellText = String.Format("{0,2}  {1}", (i+1),Project.Instance.curEngine.L_jobList[i].jobName);
            }
            Win_EngineManager.Instance.tbx_engineName.Text = name;
            Win_Job.Instance.ToolBoxEdit_ProjectName.Text = string.Format("当前流程：{0}", name);
          
            //切换当前方案
            Scheme.LoadScheme(Scheme.FindSchemeByName(name));
            Win_Main.Instance.dockPanel.Visible = true;

            if (Project.Instance.curEngine.schemeName == name && Project.Instance.curEngine.schemeName != "未创建")
            {
                Win_Main.Instance.OutputMsg(string.Format("已由方案 [{0}] 成功切换到方案 [{1}] ", OldSchemeName, name), Win_Log.InfoType.tip);
            }
            //初始化图像窗口
            for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
            {
                HWindowControl dd = new HWindowControl();       //此处必须声明一个全部变量把它存起来，否则会被GC给清理掉
                Win_Main.d.Add(dd);
                Project.Instance.curEngine.L_jobList[i].www = dd.HalconID;
                Start.JobSwitchMark_Hard = true;//切换方案时，硬触发切换标识
                Start.JobSwitchMark_Soft = true;//切换方案时，软触发切换标识
                Project.Instance.curEngine.L_jobList[i].Run(true);
            }
        }

        /// <summary>
        /// 创建方案
        /// </summary>
        internal static void CreateScheme()
        {
            try
            {
            Again:
                Win_InputMessage.Instance.XXXXXXXXXXXXXlbl_title.Text = ("请输入新方案名");
                Win_InputMessage.Instance.btn_confirm.Text = ("确定");
                //////Win_InputMessage.Instance.passwordChar = false;
                Win_InputMessage.Instance.txt_input.DefaultText = "请输入新方案名";
                Win_InputMessage.Instance.txt_input.TextStr = "示例方案";
                Win_InputMessage.Instance.ShowDialog();
                string jobName = Win_InputMessage.Instance.input;
                if (jobName == "")
                    return;

                //检查此名称的流程是否已存在
                if (CheckSchemeExist(jobName))
                {
                    Win_MessageBox.Instance.MessageBoxShow(("\r\n已存在此名称的方案，方案名不可重复，请重新输入"));
                    goto Again;
                }
                //检查此名称是否含有特殊字符\
                if (jobName.Contains(@"\"))
                {
                    Win_MessageBox.Instance.MessageBoxShow(("\r\n方案名中不能含有 \\ 等特殊字符 ，请重新输入"));
                    goto Again;
                }


                Win_Main.Instance.OutputMsg("创建了新方案，方案名为：" + jobName, Win_Log.InfoType.tip);
                Scheme engine = new Scheme
                {
                    schemeName = jobName
                };
                Project.Instance.L_engineList.Add(engine);

                Win_EngineManager.Instance.cbx_engineList.Items.Add(jobName);
                Win_Main.Instance.lbl_curEngine.DropDownItems.Add(jobName);
                if (Win_EngineManager.Instance.cbx_engineList.Items.Count > 0)
                {
                    //Win_EngineManager.Instance.cbx_engineList.Text = "1";
                    //string ssss = jobName;
                    Win_EngineManager.Instance.cbx_engineList.Text = jobName;
                    Win_Main.Instance.lbl_curEngine.DropDownItems[Win_Main.Instance.lbl_curEngine.DropDownItems.Count - 1].Select();
                    Win_Main.Instance.lbl_curEngine.Text = "当前方案：" + jobName;
                    Win_Job.Instance.ToolBoxEdit_ProjectName.Text = "当前流程：" + jobName;
                }
                Start.OnNewProject(jobName);
                Scheme.SwitchScheme(jobName);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
            }
        }
        /// <summary>
        /// 克隆当前方案
        /// </summary>
        internal static void CloneScheme()
        {
            try
            {
            Again:
                Win_InputMessage.Instance.XXXXXXXXXXXXXlbl_title.Text = ("请输入新方案名");
                Win_InputMessage.Instance.btn_confirm.Text = ("确定");
                Win_InputMessage.Instance.passwordChar = false;
                Win_InputMessage.Instance.txt_input.TextStr = string.Empty;
                Win_InputMessage.Instance.ShowDialog();
                string newEngineName = Win_InputMessage.Instance.input;
                if (newEngineName != string.Empty)
                {
                    //检查此名称的流程是否已存在
                    if (CheckSchemeExist(newEngineName))
                    {
                        Win_MessageBox.Instance.MessageBoxShow(("\r\n已存在此名称的方案，方案名不可重复，请重新输入"));
                        goto Again;
                    }
                    Win_Log.Instance.OutputMsg("克隆了新方案，方案名为：" + newEngineName,Win_Log.InfoType.tip);

                    string sourceEngineName = Project.Instance.curEngine.schemeName;
                    Scheme engine = ObjectCopier.Clone(Project.Instance.curEngine);
                    engine.schemeName = newEngineName;

                    Project.Instance.L_engineList.Add(engine);
                    Win_Main.Instance.SaveAll();
                    Win_EngineManager.Instance.cbx_engineList.Items.Add(newEngineName);
                    Win_EngineManager.Instance.cbx_engineList.Text = newEngineName;
                    Win_Main.Instance.lbl_curEngine.DropDownItems.Add(newEngineName);
                    Win_Log.Instance.OutputMsg("方案克隆成功",Win_Log.InfoType.tip);

                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        /// <summary>
        /// 通过方案名获取方案
        /// </summary>
        /// <param name="jobName">方案名</param>
        /// <returns>方案</returns>
        public static Scheme FindSchemeByName(string engineName)
        {
            try
            {
                for (int i = 0; i < Project.Instance.L_engineList.Count; i++)
                {
                    if (Project.Instance.L_engineList[i].schemeName == engineName)
                        return Project.Instance.L_engineList[i];
                }
                Win_MessageBox.Instance.MessageBoxShow("未找到名为" + engineName + "的方案（错误代码：00001）");
                return null;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return null;
            }
        }
        /// <summary>
        /// 判断是否已经存在此名称的方案
        /// </summary>
        /// <param name="engineName">方案名</param>
        /// <returns>是否已存在</returns>
        private static bool CheckSchemeExist(string engineName)
        {
            try
            {
                try
                {
                    for (int i = 0; i < Project.Instance.L_engineList.Count; i++)
                    {
                        if ((Project.Instance.L_engineList[i]).schemeName == engineName)
                            return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Log.SaveError(Project.Instance.configuration.dataPath,ex);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
                return false;
            }
        }

    }
}
