using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSE.Core;

namespace VSE
{

    /// <summary>
    /// 工具信息类
    /// </summary>
    [Serializable]
    public class ToolInfo
    {
        public ToolInfo()
        {
            enable = true;
            toolType = ToolType.None;
            toolName = string.Empty;
            tool = new ToolBase();
            input = new List<ToolIO>();
            output = new List<ToolIO>();
        }
        public ToolInfo(ToolType toolType, ToolBase tool, string jobName, string toolName)
        {
            enable = true;
            this.toolType = toolType;
            this.toolName = toolName;
            this.tool = tool;
            this.tool.jobName = jobName;
            input = new List<ToolIO>();
            output = new List<ToolIO>();
        }

        /// <summary>
        /// 工具是否启用
        /// </summary>
        public bool enable;
        /// <summary>
        /// 工具名称
        /// </summary>
        public string toolName;
        /// <summary>
        /// 工具类型
        /// </summary>
        public ToolType toolType;
        /// <summary>
        /// 工具对象
        /// </summary>
        public ToolBase tool;
        /// <summary>
        /// 工具描述信息
        /// </summary>
        public string toolTipInfo = "无";
        /// <summary>
        /// 工具输入字典集合
        /// </summary>
        public List<ToolIO> input;
        /// <summary>
        /// 工具输出字典集合
        /// </summary>
        public List<ToolIO> output;


        /// <summary>
        /// 以IO名获取IO对象
        /// </summary>
        /// <param name="IOName"></param>
        /// <returns></returns>
        public ToolIO GetInput(string IOName)
        {
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].IOName == IOName)
                    return input[i];
            }
            return new ToolIO();
        }
        /// <summary>
        /// 以IO名获取IO对象
        /// </summary>
        /// <param name="IOName"></param>
        /// <returns></returns>
        public ToolIO GetOutput(string IOName)
        {
            for (int i = 0; i < output.Count; i++)
            {
                if (output[i].IOName == IOName)
                    return output[i];
            }
            return new ToolIO();
        }
        /// <summary>
        /// 移除工具输入项
        /// </summary>
        /// <param name="IOName"></param>
        public void RemoveInputIO(string nodeText)
        {
            for (int i = 0; i < input.Count; i++)
            {
                //if (input[i].IOName == IOName)
                //    input.RemoveAt(i);
                if (input[i].value.ToString() == string.Empty)      //未连接源
                {
                    if (string.Format("<--{0}", input[i].IOName) == nodeText)
                        input.RemoveAt(i);
                }
                else    //已连接源
                {
                    if (string.Format("<--{0}{1}", input[i].IOName, input[i].value.ToString()) == nodeText)
                        input.RemoveAt(i);
                }
            }
        }
        /// <summary>
        /// 移除工具输出项
        /// </summary>
        /// <param name="IOName"></param>
        public void RemoveOutputIO(string IOName)
        {
            for (int i = 0; i < output.Count; i++)
            {
                if (output[i].IOName == IOName)
                    output.RemoveAt(i);
            }
        }

    }
}
