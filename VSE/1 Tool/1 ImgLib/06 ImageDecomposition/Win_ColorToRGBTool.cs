using System;
using VControls;
using VSE.Core;

namespace VSE
{
    internal partial class Win_ColorToRGBTool : ToolWinBase
    {
        internal Win_ColorToRGBTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_ColorToRGBTool _instance;
        public static Win_ColorToRGBTool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_ColorToRGBTool();
                return _instance;
            }
        }
        /// <summary>
        /// 当前工具所对应的工具对象
        /// </summary>
        internal static ColorToRGBTool colorToRGBTool = new ColorToRGBTool();

        //private void ckb_colorToRGBToolEnable_CheckedChanged(object sender, EventArgs e)
        //{
        //   Job.FindJobByName (jobName ).FindToolInfoByName (toolName ).enable   = ckb_colorToRGBToolEnable.Checked;
        //}
        private void tsb_runTool_Click(object sender, EventArgs e)
        {
            colorToRGBTool.Run(true, true, toolName);
            if (colorToRGBTool.toolRunStatu  != (  ToolRunStatu.成功))
                Win_Main.Instance.OutputMsg(colorToRGBTool.toolRunStatu.ToString(),Win_Log.InfoType.error);
            else
                Win_Main.Instance.OutputMsg(colorToRGBTool.toolRunStatu.ToString(),Win_Log.InfoType.tip);
        }
        private void btn_runColorToRGBTool_Click(object sender, EventArgs e)
        {
            colorToRGBTool.Run(true, true, toolName);
            if (colorToRGBTool.toolRunStatu != (  ToolRunStatu.成功))
                Win_Main.Instance.OutputMsg(colorToRGBTool.toolRunStatu.ToString(),Win_Log.InfoType.error);
            else
                Win_Main.Instance.OutputMsg(colorToRGBTool.toolRunStatu.ToString(),Win_Log.InfoType.tip);
        }
       
    }
}
