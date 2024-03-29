using System;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    public partial class Win_EngineManager : Form 
    {
        public Win_EngineManager()
        {
            InitializeComponent();
            this.BackColor = VUI.WinBackColor;
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_EngineManager _instance;
        public static Win_EngineManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_EngineManager();
                return _instance;
            }
        }
        /// <summary>
        /// 是否忽略执行
        /// </summary>
        internal static  bool ignore = false;

        private void button4_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Scheme.CloneScheme();
        }
        private void btn_confirm_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            //Scheme.ExportScheme();
            Project.ExportProject();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Scheme.DeleteScheme();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            //Scheme.InportScheme();
            Project.InportProject();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Permission.CheckPermission(PermissionLevel.Admin))
                return;
            Scheme.CreateScheme();
        }

        private void cbx_engineList_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                //System.Diagnostics.Debug.WriteLine("数量：" + cbx_engineList.Items.Count);
                //System.Diagnostics.Debug.WriteLine("索引ID：" + cbx_engineList.SelectedIndex);
                //System.Diagnostics.Debug.WriteLine("项目数量：" + Project.Instance.L_engineList.Count);
                //int icount = Project.Instance.L_engineList[cbx_engineList.SelectedIndex].L_jobList.Count;
                Win_EngineManager.Instance.dgv_engineInfo.RowCount = Project.Instance.L_engineList[cbx_engineList.SelectedIndex].L_jobList.Count;
                for (int i = 0; i < Project.Instance.L_engineList[cbx_engineList.SelectedIndex].L_jobList.Count; i++)
                {
                    Win_EngineManager.Instance.dgv_engineInfo.Items[i].CellText = String.Format("{0,2}  {1}", (i + 1), Project.Instance.L_engineList[cbx_engineList.SelectedIndex].L_jobList[i].jobName);
                }
            }
            catch { }
        }

        private void Win_EngineManager_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                try
                {
                    cbx_engineList.Items.Clear();
                    int index = 0;
                    for (int i = 0; i < Project.Instance.L_engineList.Count; i++)
                    {
                        cbx_engineList.Items.Add(Project.Instance.L_engineList[i].schemeName);
                        if (Project.Instance.curEngine.schemeName== Project.Instance.L_engineList[i].schemeName)
                        {
                            index = i;
                        }
                    }
                    cbx_engineList.Text = Project.Instance.curEngine.schemeName;
                    try
                    {
                        cbx_engineList.SelectedIndex = index;
                    }
                    catch { }
                   
                    Win_EngineManager.Instance.TopMost = true;
                }
                catch (Exception ex)
                {
                    Log.SaveError(Project.Instance.configuration.dataPath, ex);
                }

            }
        }

    }
}
