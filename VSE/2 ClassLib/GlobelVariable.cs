using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSE.Core;
namespace VSE
{
    [Serializable]
    internal class GlobelVariable
    {
        private List<Variable> L_variable = new List<Variable>();
        internal object GetGlobalVariableValue(string name)
        {
            for (int i = 0; i < Project.Instance.curEngine.globelVariable.L_variable.Count; i++)
            {
                if (Project.Instance.curEngine.globelVariable.L_variable[i].name == name)
                { return Project.Instance.curEngine.globelVariable.L_variable[i].value; }
                   
            }
            return null;
        }
        internal void SetGlobalVariableValue(string name, object value)
        {
            for (int i = 0; i < Project.Instance.curEngine.globelVariable.L_variable.Count; i++)
            {
                if (Project.Instance.curEngine.globelVariable.L_variable[i].name == name)
                { 
                    Project.Instance.curEngine.globelVariable.L_variable[i].value = value;
                    break;
                }
            }
        }
        /// <summary>
        /// 外部使用 可读
        /// </summary>
        /// <param name="name"></param>
        /// <param name="V"></param>
        /// <returns></returns>
        internal string ReadVariableValue(string name,ref object V)
        {
            for (int i = 0; i < Project.Instance.curEngine.globelVariable.L_variable.Count; i++)
            {
                if (Project.Instance.curEngine.globelVariable.L_variable[i].name == name)
                {
                    if (Project.Instance.curEngine.globelVariable.L_variable[i].IsRW == IsRW.R)
                    {
                        V = Project.Instance.curEngine.globelVariable.L_variable[i].value;
                        return "OK";
                    }
                    return "变量不可读";
                }

            }
            return "未找到变量";
        }
        /// <summary>
        /// 外部使用 可写
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal string WriteVariableValue(string name, object value)
        {
            for (int i = 0; i < Project.Instance.curEngine.globelVariable.L_variable.Count; i++)
            {
                if (Project.Instance.curEngine.globelVariable.L_variable[i].name == name)
                {
                    if (Project.Instance.curEngine.globelVariable.L_variable[i].IsRW == IsRW.W)
                    {   Project.Instance.curEngine.globelVariable.L_variable[i].value = value;
                        return "OK";
                    }
                    return "变量不可写";
                }
            }
            return "未找到变量";
        }
        internal void AddGlobalVariableValue(Variable v)
        {
            L_variable.Add(v);
           // Win_Monitor.UpdateGlobelVariablelist(true);
        }
        internal void RemoveGlobalVariableValue(Variable v)
        {
            L_variable.Remove(v);
            //Win_Monitor.UpdateGlobelVariablelist(true);
        }
        internal void RemoveGlobalVariableValueAt(int i)
        {
            L_variable.RemoveAt(i);
            //Win_Monitor.UpdateGlobelVariablelist(true);
        }
        internal Variable GetGlobalVariable(int i)
        {
           return L_variable[i];
        }
        internal int GetGlobalVariableCount()
        {
            return L_variable.Count;
        }
        //重写 添加 移除 数量

    }


}
