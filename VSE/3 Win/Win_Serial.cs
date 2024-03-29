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
    internal partial class Win_Serial : Form
    {
        internal Win_Serial()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 扫码枪对象
        /// </summary>
        private static Serial serial;
        /// <summary>
        /// 窗体实例对象
        /// </summary>
        private static Win_Serial _instance;
        public static Win_Serial Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_Serial();
                return _instance;
            }
        }


        /// <summary>
        /// 加载参数到界面
        /// </summary>
        /// <param name="scaner_1"></param>
        internal void LoadPar(Serial scaner_1)
        {
            try
            {
                serial = scaner_1;
                tbx_clientName.Text = serial.Name;
                bool contains = false;
                for (int i = 0; i < cbx_portName.Items.Count; i++)
                {
                    if (cbx_portName.Items[i].ToString() == serial.portName)
                    {
                        contains = true;
                        break;
                    }
                }
                if (!contains)
                    cbx_portName.Items.Add(serial.portName);
                cbx_portName.Text = serial.portName;
                cbx_baudRate.Text = serial.baudRate.ToString();
                tbx_dataBit.Text = serial.dataBit.ToString();
                cbx_stopBit.SelectedIndex = (int)serial.stopBit;
                cbx_parityBit.SelectedIndex = (int)serial.parity;

                tbx_trigCmd.Text = serial.TrigCmd;
                tbx_scanNum.Text = serial.failNum.ToString();
                if (serial.endChar == string.Empty)
                {
                    btn_endCharNone.BackColor = Color.Gray;
                    btn_endCharEnter.BackColor = Color.Gainsboro;
                }
                else
                {
                    btn_endCharNone.BackColor = Color.Gainsboro;
                    btn_endCharEnter.BackColor = Color.Gray;
                }
                ckb_commHex.Checked = serial.CommHex;

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
      
      
        private void lnk_clear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tbx_output.Clear();
        }
        private void btn_connect_Clicked()
        {
            serial.Init();
        }
        private void btn_close_Clicked()
        {
            serial.Close();
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
                serial.Send(tbx_sendMsg.Text.Trim());
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void tbx_trigCmd_TextStrChanged(string textStr)
        {
            serial.TrigCmd = tbx_trigCmd.Text.Trim();
        }
        private void tbx_scanNum_TextStrChanged(string textStr)
        {
            serial.failNum = Convert.ToInt16(tbx_scanNum.Text.Trim());
        }
        private void cButton2_Clicked()
        {
            serial.Scan();
        }
        private void btn_endCharNone_Click(object sender, EventArgs e)
        {
            serial.endChar = string.Empty;
            btn_endCharNone.BackColor = Color.Gray;
            btn_endCharEnter.BackColor = Color.Gainsboro;
        }
        private void btn_endCharEnter_Click(object sender, EventArgs e)
        {
            serial.endChar = "\r\n";
            btn_endCharNone.BackColor = Color.Gainsboro;
            btn_endCharEnter.BackColor = Color.Gray;
        }
        private void cCheckBox1_CheckChanged(bool Checked)
        {
            serial.CommHex = Checked;
        }

        private void cbx_portName_ComboTextChanged(object sender, EventArgs e)
        {
           
           
        }

        private void cbx_baudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Win_DeviceManager.cancel)
                    return;

                serial.baudRate = Convert.ToInt32(cbx_baudRate.Text.Trim());
            }
            catch { }
        }

        private void cbx_portName_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Win_DeviceManager.cancel)
                return;

            serial.portName = cbx_portName.Text.Trim();
        }

        private void tbx_dataBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Win_DeviceManager.cancel)
                    return;

                serial.dataBit = Convert.ToInt32(tbx_dataBit.Text.Trim());
            }
            catch { }
        }

        private void cbx_stopBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Win_DeviceManager.cancel)
                return;

            serial.stopBit = (StopBits)Enum.Parse(typeof(StopBits), cbx_stopBit.Text);
        }

        private void cbx_parityBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Win_DeviceManager.cancel)
                return;

            serial.parity = (Parity)Enum.Parse(typeof(Parity), cbx_parityBit.Text);
        }
        
    }
}
