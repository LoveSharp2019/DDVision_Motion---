using VSE.Properties;
using HalconDotNet;
using Lxc.VisionPlus.ImageView.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using VControls;
using VSE.Core;
using System.ComponentModel;

namespace VSE
{
    internal partial class Win_GlobalVariable : FormBase
    {
        internal Win_GlobalVariable()
        {
            InitializeComponent();
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.BackColor = VUI.WinTitleBarBackColor;
            this.tableLayoutPanel1.BackColor = VUI.WinBackColor;
            this.dataGridView1.BackgroundColor = VUI.WinBackColor;
        }
        internal List<ROI> regions = new List<ROI>();
        bool enable = false;
        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_GlobalVariable _instance;
        internal static Win_GlobalVariable Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_GlobalVariable();
                return _instance;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            Win_Monitor.UpdateGlobelVariablelist(true);
            this.Hide();
        }
        internal void LoadVariable(int variableType = 1)
        {
            try
            {
                enable = false;
                dataGridView1.Rows.Clear();
                for (int i = 0; i < Project.Instance.curEngine.globelVariable.GetGlobalVariableCount(); i++)
                {
                    if (Project.Instance.curEngine.globelVariable.GetGlobalVariable(i).variableType == variableType)
                    {
                        int idx = dataGridView1.Rows.Add();
                        dataGridView1.Rows[idx].Cells[0].Value = dataGridView1.Rows.Count;
                        dataGridView1.Rows[idx].Cells[1].Value = Project.Instance.curEngine.globelVariable.GetGlobalVariable(i).type;
                        dataGridView1.Rows[idx].Cells[2].Value = Project.Instance.curEngine.globelVariable.GetGlobalVariable(i).name;
                        dataGridView1.Rows[idx].Cells[3].Value = Project.Instance.curEngine.globelVariable.GetGlobalVariable(i).value;
                        dataGridView1.Rows[idx].Cells[4].Value = Project.Instance.curEngine.globelVariable.GetGlobalVariable(i).IsRW== IsRW.R?"R":"W";
                        dataGridView1.Rows[idx].Cells[5].Value = Project.Instance.curEngine.globelVariable.GetGlobalVariable(i).info;
                    }
                }
                enable = true;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Variable variable = new Variable(dataGridView1.Rows.Count + 1, "Int", Project.Instance.curEngine.GetNewName("Int"), checkBox1.Checked?IsRW.W:IsRW.R);
            Project.Instance.curEngine.globelVariable.AddGlobalVariableValue(variable);
            LoadVariable();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Variable variable = new Variable(dataGridView1.Rows.Count + 1, "Double", Project.Instance.curEngine.GetNewName("Double"), checkBox1.Checked ? IsRW.W : IsRW.R);
            Project.Instance.curEngine.globelVariable.AddGlobalVariableValue(variable);
            LoadVariable();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Variable variable = new Variable(dataGridView1.Rows.Count + 1, "String", Project.Instance.curEngine.GetNewName("String"), checkBox1.Checked ? IsRW.W : IsRW.R);
            Project.Instance.curEngine.globelVariable.AddGlobalVariableValue(variable);
            LoadVariable();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Variable variable = new Variable(dataGridView1.Rows.Count + 1, "Bool", Project.Instance.curEngine.GetNewName("Bool"), checkBox1.Checked ? IsRW.W : IsRW.R);
            Project.Instance.curEngine.globelVariable.AddGlobalVariableValue(variable);
            LoadVariable();
        }

        private void button9_Click(object sender, EventArgs e)
        {
           
            int index = 0;
            for (int i = 0; i < Project.Instance.curEngine.globelVariable.GetGlobalVariableCount(); i++)
            {
                Variable v = Project.Instance.curEngine.globelVariable.GetGlobalVariable(i);
                if (v.variableType == 1)
                {
                    if (index == dataGridView1.SelectedRows[0].Index)
                    {
                        Project.Instance.curEngine.globelVariable.RemoveGlobalVariableValueAt(i);
                        break;
                    }

                    index++;
                }
            }
            dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
        
            //LoadVariable();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
               
                this.Close();
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }


     
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (enable == false)
                return;
            if (e.ColumnIndex==2|| e.ColumnIndex==3|| e.ColumnIndex == 4)
            {
                string name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                object value = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                int index = 0;
                for (int i = 0; i < Project.Instance.curEngine.globelVariable.GetGlobalVariableCount(); i++)
                {
                    Variable v = Project.Instance.curEngine.globelVariable.GetGlobalVariable(i);
                    if (v.variableType==1)
                    {
                        if (index==e.RowIndex)
                        {
                            if (!CheckVariableExist(name))
                            {
                                Project.Instance.curEngine.globelVariable.GetGlobalVariable(i).name = name;
                                Project.Instance.curEngine.globelVariable.GetGlobalVariable(i).value = value;
                                Project.Instance.curEngine.globelVariable.GetGlobalVariable(i).info = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

                            }
                            else
                            {

                                Win_MessageBox.Instance.MessageBoxShow("全局变量已经存在！");
                            }
                            return;
                        }
                        
                        index++;
                    }
                }
              
            }
           
        }
        internal static bool CheckVariableExist(string VariableName)
        {
            try
            {
                for (int i = 0; i < Project.Instance.curEngine.L_jobList.Count; i++)
                {
                    if (Project.Instance.curEngine.globelVariable.GetGlobalVariable(i).name == VariableName)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath, ex);
                return true;
            }
        }
        private void Win_GlobalVariable_VisableChanged(object sender, EventArgs e)
        {
            if (Visible)
            { LoadVariable(lxcRadioButton4.Checked ? 1 : 0);

                this.TopMost = true;
            }

        }


        private void lxcRadioButton4_Click(object sender, EventArgs e)
        {
            LoadVariable(lxcRadioButton4.Checked?1:0);
            if (!lxcRadioButton4.Checked)
            {
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                button8.Visible = false;
                button9.Visible = false;
                dataGridView1.ReadOnly = true;
            }
            else
            {
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button8.Visible = true;
                button9.Visible = true;
                dataGridView1.ReadOnly = false;
            }
        }
    }
}
