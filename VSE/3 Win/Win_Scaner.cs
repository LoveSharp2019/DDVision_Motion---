using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    internal partial class Win_Scaner : Form
    {
        internal Win_Scaner()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 扫码枪对象
        /// </summary>
        private static Scaner scaner;
        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Win_Scaner _instance;
        public static Win_Scaner Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_Scaner();
                return _instance;
            }
        }


        /// <summary>
        /// 加载参数到界面
        /// </summary>
        /// <param name="scaner_1"></param>
        internal void LoadPar(Scaner scaner_1)
        {
            try
            {
                scaner = scaner_1;
                tbx_clientName.Text = scaner.Name;
                bool contains = false;
                for (int i = 0; i < cbx_portName.Items.Count; i++)
                {
                    if (cbx_portName.Items[i].ToString() == scaner.portName)
                    {
                        contains = true;
                        break;
                    }
                }
                if (!contains)
                    cbx_portName.Items.Add(scaner.portName);
                cbx_portName.Text = scaner.portName;
                cbx_baudRate.Text = scaner.baudRate.ToString();
                tbx_dataBit.Text = scaner.dataBit.ToString();
                cbx_stopBit.SelectedIndex = (int)scaner.stopBit;
                cbx_parityBit.SelectedIndex = (int)scaner.parity;

                tbx_trigCmd.Text = scaner.TrigCmd;
                tbx_scanNum.Text = scaner.failNum.ToString();
                if (scaner.endChar == string.Empty)
                {
                    btn_endCharNone.BackColor = Color.Gray;
                    btn_endCharEnter.BackColor = Color.Gainsboro;
                }
                else
                {
                    btn_endCharNone.BackColor = Color.Gainsboro;
                    btn_endCharEnter.BackColor = Color.Gray;
                }

                //////if (scaner.FindSerialPortByName ().IsOpen )
                //////    btn_connect.Text = "断开";
                //////else
                //////    btn_connect.Text = "连接";
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }


        private void Win_Serial_Load(object sender, EventArgs e)
        {
            try
            {
                cbx_portName.Items.Clear();
                string[] strs = SerialPort.GetPortNames();
                for (int i = 0; i < strs.Length; i++)
                {
                    cbx_portName.Items.Add(strs[i]);
                }

                cbx_parityBit.Items.Clear();
                foreach (var item in Enum.GetValues(typeof(Parity)))
                {
                    cbx_parityBit.Items.Add(item.ToString());
                }

                cbx_stopBit.Items.Clear();
                foreach (var item in Enum.GetValues(typeof(StopBits)))
                {
                    cbx_stopBit.Items.Add(item.ToString());
                }

                //if (cbx_portName.Items.Length > 0)
                //    cbx_portName.SelectedIndex = 0;
                //if (cbx_parityBit.Items.Length > 0)
                //    cbx_parityBit.SelectedIndex = 0;
                //if (cbx_stopBit.Items.Length > 0)
                //    cbx_stopBit.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void cbx_portName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Win_DeviceManager.cancel)
                return;

            scaner.portName = cbx_portName.Text.Trim();
        }
        private void cbx_baudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Win_DeviceManager.cancel)
                    return;

                scaner.baudRate = Convert.ToInt32(cbx_baudRate.Text.Trim());
            }
            catch { }
        }
        private void tbx_dataBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Win_DeviceManager.cancel)
                    return;

                scaner.dataBit = Convert.ToInt32(tbx_dataBit.Text.Trim());
            }
            catch { }
        }
        private void cbx_stopBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Win_DeviceManager.cancel)
                return;

            scaner.stopBit = (StopBits)Enum.Parse(typeof(StopBits), cbx_stopBit.Text);
        }
        private void cbx_parityBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Win_DeviceManager.cancel)
                return;

            scaner.parity = (Parity)Enum.Parse(typeof(Parity), cbx_parityBit.Text);
        }
        private void lnk_clear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tbx_output.Clear();
        }
        private void btn_connect_Clicked()
        {
            scaner.Init();
        }
        private void btn_close_Clicked()
        {
            scaner.Close();
        }
        private void btn_send_Clicked()
        {
            try
            {
                if (tbx_sendMsg.Text.Trim() == string.Empty)
                {
                    Win_MessageBox.Instance.MessageBoxShow("\r\n不能发送空字符串");
                    return;
                }
                scaner.Send(tbx_sendMsg.Text.Trim());
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void tbx_trigCmd_TextStrChanged(string textStr)
        {
            scaner.TrigCmd = tbx_trigCmd.Text.Trim();
        }
        private void tbx_scanNum_TextStrChanged(string textStr)
        {
            scaner.failNum = Convert.ToInt16(tbx_scanNum.Text.Trim());
        }
        private void cButton2_Clicked()
        {
            scaner.Scan();
        }
        private void btn_endCharNone_Click(object sender, EventArgs e)
        {
            scaner.endChar = string.Empty;
            btn_endCharNone.BackColor = Color.Gray;
            btn_endCharEnter.BackColor = Color.Gainsboro;
        }
        private void btn_endCharEnter_Click(object sender, EventArgs e)
        {
            scaner.endChar = "\r\n";
            btn_endCharNone.BackColor = Color.Gainsboro;
            btn_endCharEnter.BackColor = Color.Gray;
        }

     

    }
}
