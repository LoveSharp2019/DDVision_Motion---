using VSE.Properties;
using LightController;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Windows.Forms;
using VControls;
using VSE.Core;

namespace VSE
{
    internal partial class Win_DeviceManager : FormBase
    {
        internal Win_DeviceManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当前窗体类型
        /// </summary>
        private static CurForm curForm = CurForm.None;
        /// <summary>
        /// 在添加设备到列表时，不需要重新加载设备
        /// </summary>
        internal  static  bool cancel = false;
        /// <summary>
        /// 窗体对象实例
        /// </summary>
        private static Win_DeviceManager _instance;
        internal static Win_DeviceManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Win_DeviceManager();
                return Win_DeviceManager._instance;
            }
        }


        private void btn_addDevice_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                System.Drawing.Point p = new System.Drawing.Point();
                p.X = this.Location.X + panel3.Location.X + btn_addDevice.Location.X + 20;
                p.Y = this.Location.Y + panel3.Location.Y + btn_addDevice.Location.Y + 22;
                contextMenuStrip1.Show(p);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void btn_addDevice_Click(object sender, EventArgs e)
        {
            try
            {
                System.Drawing.Point p = new System.Drawing.Point();
                p.X = this.Location.X + panel3.Location.X + btn_addDevice.Location.X + 20;
                p.Y = this.Location.Y + panel3.Location.Y + btn_addDevice.Location.Y + 22;
                contextMenuStrip1.Show(p);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void TCPIP服务端ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cancel = true;
                Win_InputMessage.Instance.XXXXXXXXXXXXXlbl_title.Text = ("请输入TCP/IP服务端名称");
                Win_InputMessage.Instance.btn_confirm.Text = ("确定");
                Win_InputMessage.Instance.txt_input.DefaultText = "请输入自定义的TCP/IP服务端名称";
                Win_InputMessage.Instance.txt_input.TextStr = "服务端1";
                Win_InputMessage.Instance.ShowDialog();
                string Str = Win_InputMessage.Instance.input;
                if (Str == string.Empty)
                    return;

                int idx = dgv_deviceList.Rows.Add();
                dgv_deviceList.Rows[idx].Tag = "TCPSever";
                dgv_deviceList.Rows[idx].Height = 30;
                dgv_deviceList.Rows[idx].Cells[0].Value = Str;
                dgv_deviceList.Rows[idx].Cells[1].Value = Resources.服务器;
                dgv_deviceList.Rows[idx].Cells[2].Value = Resources.客户端;

                if (curForm != CurForm.TCPSever)
                {
                    curForm = CurForm.TCPSever;
                    pnl_formPnl.Controls.Clear();
                    Win_TCPServer.Instance.TopLevel = false;
                    Win_TCPServer.Instance.Parent = pnl_formPnl;
                    Win_TCPServer.Instance.Dock = DockStyle.Top;
                    Win_TCPServer.Instance.Show();
                }

                TCPSever tcpSever = new TCPSever(Str);

                Project.Instance.L_TCPSever.Add(tcpSever);
                Win_TCPServer.Instance.LoadPar(tcpSever);
                dgv_deviceList.Rows[dgv_deviceList.Rows.Count - 1].Selected = true;
                cancel = false;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void TCPIP客户端ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cancel = true;
                Win_InputMessage.Instance.XXXXXXXXXXXXXlbl_title.Text = ("请输入TCP/IP客户端名称");
                Win_InputMessage.Instance.btn_confirm.Text = ("确定");
                Win_InputMessage.Instance.txt_input.DefaultText = "请输入自定义的TCP/IP客户端名称";
                Win_InputMessage.Instance.txt_input.TextStr = "客户端1";
                Win_InputMessage.Instance.ShowDialog();
                string clientName = Win_InputMessage.Instance.input;
                if (clientName == string.Empty)
                    return;

                int idx = dgv_deviceList.Rows.Add();
                dgv_deviceList.Rows[idx].Tag = "TCPClient";
                dgv_deviceList.Rows[idx].Height = 30;
                dgv_deviceList.Rows[idx].Cells[0].Value = clientName;
                dgv_deviceList.Rows[idx].Cells[1].Value = Resources.客户端;
                dgv_deviceList.Rows[idx].Cells[2].Value = Resources.客户端;

                if (curForm != CurForm.TCPClient)
                {
                    curForm = CurForm.TCPClient;
                    pnl_formPnl.Controls.Clear();
                    Win_TCPClient.Instance.TopLevel = false;
                    Win_TCPClient.Instance.Parent = pnl_formPnl;
                    Win_TCPClient.Instance.Dock = DockStyle.Top;
                    Win_TCPClient.Instance.Show();
                }

                TCPClient tcpClient = new TCPClient(clientName);

                Project.Instance.L_TCPClient.Add(tcpClient);
                Win_TCPClient.Instance.LoadPar(tcpClient);

                ToolStripItem tsb = new ToolStripStatusLabel("", Resources.客户端);
                tsb.AutoSize = false;
                tsb.Width = 20;
                tsb.Name = clientName;
                tsb.ToolTipText = string.Format("名称：{0}\r\n状态：{1}\r\nIP    : {2}\r\nPort : {3}", clientName, "未连接", "192.168.0.1", 10004);
                Win_Main.Instance.statusStrip1.Items.Insert(1, tsb);
                dgv_deviceList.Rows[dgv_deviceList.Rows.Count - 1].Selected = true;
                cancel = false;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void 奥普特光源控制器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cancel = true;
                Win_InputMessage.Instance.XXXXXXXXXXXXXlbl_title.Text = ("请输入光源控制器名称");
                Win_InputMessage.Instance.btn_confirm.Text = ("确定");
                Win_InputMessage.Instance.txt_input.DefaultText = "请输入自定义的光源控制器名称";
                Win_InputMessage.Instance.txt_input.TextStr = "光源控制器1";

                Win_InputMessage.Instance.ShowDialog();
                string Str = Win_InputMessage.Instance.input;
                if (Str == string.Empty)
                    return;

                int idx = dgv_deviceList.Rows.Add();
                dgv_deviceList.Rows[idx].Tag = "LightController";
                dgv_deviceList.Rows[idx].Height = 30;
                dgv_deviceList.Rows[idx].Cells[0].Value = Str;
                dgv_deviceList.Rows[idx].Cells[1].Value = Resources.光照度_允乐;
                dgv_deviceList.Rows[idx].Cells[2].Value = Resources.客户端;

                if (curForm != CurForm.LightController)
                {
                    curForm = CurForm.LightController;
                    pnl_formPnl.Controls.Clear();
                    Win_LightController.Instance.TopLevel = false;
                    Win_LightController.Instance.Parent = pnl_formPnl;
                    Win_LightController.Instance.Dock = DockStyle.Top;
                    Win_LightController.Instance.Show();
                }

                LightController_CST lightController_CST = new LightController_CST();
                lightController_CST.Name = Str;
                lightController_CST.OpenController();

                Project.Instance.L_lightController.Add(lightController_CST);
                Win_LightController.Instance.LoadPar(lightController_CST);
                dgv_deviceList.Rows[dgv_deviceList.Rows.Count - 1].Selected = true;
                cancel = false;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void 删除toolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string deviceType = dgv_deviceList.SelectedRows[0].Tag.ToString();
                switch (deviceType)
                {
                    case "TCPSever":
                        for (int i = 0; i < Project.Instance.L_TCPSever.Count; i++)
                        {
                            if (Project.Instance.L_TCPSever[i].Name == dgv_deviceList.SelectedRows[0].Cells[0].Value.ToString())
                            {
                                Project.Instance.L_TCPSever[i].Close();
                                Project.Instance.L_TCPSever.RemoveAt(i);
                            }
                        }
                        for (int i = 0; i < TCPSever.L_STCPSever.Count; i++)
                        {
                            if (TCPSever.L_STCPSever[i].severName == dgv_deviceList.SelectedRows[0].Cells[0].Value.ToString())
                            {
                                TCPSever.L_STCPSever.RemoveAt(i);
                            }
                        }
                        break;
                    case "TCPClient":
                        for (int i = 0; i < Project.Instance.L_TCPClient.Count; i++)
                        {
                            if (Project.Instance.L_TCPClient[i].Name == dgv_deviceList.SelectedRows[0].Cells[0].Value.ToString())
                            {
                                Project.Instance.L_TCPClient[i].Close();
                                Project.Instance.L_TCPClient.RemoveAt(i);
                            }
                        }
                        foreach (KeyValuePair<string, Socket> item in TCPClient.L_socket)
                        {
                            if (item.Key == dgv_deviceList.SelectedRows[0].Cells[0].Value.ToString())
                            {
                                TCPClient.L_socket.Remove(item.Key);
                                break;
                            }
                        }
                        break;
                    case "LightController":
                        for (int i = 0; i < Project.Instance.L_lightController.Count; i++)
                        {
                            if (Project.Instance.L_lightController[i].Name == dgv_deviceList.SelectedRows[0].Cells[0].Value.ToString())
                                Project.Instance.L_lightController.RemoveAt(i);
                        }
                        break;
                    case "Scaner":
                        for (int i = 0; i < Project.Instance.L_Scaner.Count; i++)
                        {
                            if (Project.Instance.L_Scaner[i].Name == dgv_deviceList.SelectedRows[0].Cells[0].Value.ToString())
                                Project.Instance.L_Scaner.RemoveAt(i);
                        }
                        break;
                    case "Serial":
                        for (int i = 0; i < Project.Instance.L_Serial .Count; i++)
                        {
                            if (Project.Instance.L_Serial[i].Name == dgv_deviceList.SelectedRows[0].Cells[0].Value.ToString())
                                Project.Instance.L_Serial.RemoveAt(i);
                        }
                        break;
                }
                dgv_deviceList.Rows.RemoveAt(dgv_deviceList.SelectedRows[0].Index);
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void dgv_deviceList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (cancel)
                    return;
                if (dgv_deviceList.SelectedRows.Count == 0)
                {
                    pnl_formPnl.Controls.Clear();
                    return;
                }

                string deviceType = ((DataGridView)sender).SelectedRows[0].Tag.ToString();
                switch (deviceType)
                {
                    case "LightController":
                        if (curForm != CurForm.LightController)
                        {
                            curForm = CurForm.LightController;
                            pnl_formPnl.Controls.Clear();
                            Win_LightController.Instance.TopLevel = false;
                            Win_LightController.Instance.Parent = pnl_formPnl;
                            Win_LightController.Instance.Dock = DockStyle.Top;
                            Win_LightController.Instance.Show();
                        }

                        for (int i = 0; i < Project.Instance.L_lightController.Count; i++)
                        {
                            if (Project.Instance.L_lightController[i].Name == dgv_deviceList.SelectedRows[0].Cells[0].Value.ToString())
                                Win_LightController.Instance.LoadPar(Project.Instance.L_lightController[i]);
                        }
                        break;
                    case "TCPClient":
                        if (curForm != CurForm.TCPClient)
                        {
                            curForm = CurForm.TCPClient;
                            pnl_formPnl.Controls.Clear();
                            Win_TCPClient.Instance.TopLevel = false;
                            Win_TCPClient.Instance.Parent = pnl_formPnl;
                            Win_TCPClient.Instance.Dock = DockStyle.Top;
                            Win_TCPClient.Instance.Show();
                        }

                        for (int i = 0; i < Project.Instance.L_TCPClient.Count; i++)
                        {
                            if (Project.Instance.L_TCPClient[i].Name == dgv_deviceList.SelectedRows[0].Cells[0].Value.ToString())
                                Win_TCPClient.Instance.LoadPar(Project.Instance.L_TCPClient[i]);
                        }
                        break;
                    case "TCPSever":
                        if (curForm != CurForm.TCPSever)
                        {
                            curForm = CurForm.TCPSever;
                            pnl_formPnl.Controls.Clear();
                            Win_TCPServer.Instance.TopLevel = false;
                            Win_TCPServer.Instance.Parent = pnl_formPnl;
                            Win_TCPServer.Instance.Dock = DockStyle.Top;
                            Win_TCPServer.Instance.Show();
                        }

                        for (int i = 0; i < Project.Instance.L_TCPSever.Count; i++)
                        {
                            if (Project.Instance.L_TCPSever[i].Name == dgv_deviceList.SelectedRows[0].Cells[0].Value.ToString())
                                Win_TCPServer.Instance.LoadPar(Project.Instance.L_TCPSever[i]);
                        }
                        break;
                    case "Scaner":
                        if (curForm != CurForm.Scaner)
                        {
                            curForm = CurForm.Scaner;
                            pnl_formPnl.Controls.Clear();
                            Win_Scaner.Instance.TopLevel = false;
                            Win_Scaner.Instance.Parent = pnl_formPnl;
                            Win_Scaner.Instance.Dock = DockStyle.Top;
                            Win_Scaner.Instance.Show();
                        }

                        for (int i = 0; i < Project.Instance.L_Scaner.Count; i++)
                        {
                            if (Project.Instance.L_Scaner[i].Name == dgv_deviceList.SelectedRows[0].Cells[0].Value.ToString())
                                Win_Scaner.Instance.LoadPar(Project.Instance.L_Scaner[i]);
                        }
                        break;
                    case "Serial":
                        if (curForm != CurForm.Serial )
                        {
                            curForm = CurForm.Serial;
                            pnl_formPnl.Controls.Clear();
                            Win_Serial.Instance.TopLevel = false;
                            Win_Serial.Instance.Parent = pnl_formPnl;
                            Win_Serial.Instance.Dock = DockStyle.Top;
                            Win_Serial.Instance.Show();
                        }

                        for (int i = 0; i < Project.Instance.L_Serial .Count; i++)
                        {
                            if (Project.Instance.L_Serial[i].Name == dgv_deviceList.SelectedRows[0].Cells[0].Value.ToString())
                                Win_Serial.Instance.LoadPar(Project.Instance.L_Serial[i]);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dgv_deviceList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_deviceList.Rows[e.RowIndex].Cells[0].Selected = true;
        }

        private void 扫码枪ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cancel = true;
                Win_InputMessage.Instance.XXXXXXXXXXXXXlbl_title.Text = ("请输入扫码枪名称");
                Win_InputMessage.Instance.btn_confirm.Text = ("确定");
                Win_InputMessage.Instance.txt_input.DefaultText = "请输入自定义的扫码枪名称";
                Win_InputMessage.Instance.txt_input.TextStr = "扫码枪1";
                Win_InputMessage.Instance.ShowDialog();
                string name = Win_InputMessage.Instance.input;
                if (name == string.Empty)
                    return;

                int idx = dgv_deviceList.Rows.Add();
                dgv_deviceList.Rows[idx].Tag = "Scaner";
                dgv_deviceList.Rows[idx].Height = 30;
                dgv_deviceList.Rows[idx].Cells[0].Value = name;
                dgv_deviceList.Rows[idx].Cells[1].Value = Resources.客户端;
                dgv_deviceList.Rows[idx].Cells[2].Value = Resources.客户端;

                if (curForm != CurForm.Scaner)
                {
                    curForm = CurForm.Scaner;
                    pnl_formPnl.Controls.Clear();
                    Win_Scaner.Instance.TopLevel = false;
                    Win_Scaner.Instance.Parent = pnl_formPnl;
                    Win_Scaner.Instance.Dock = DockStyle.Top;
                    Win_Scaner.Instance.Show();
                }

                Scaner scaner = new Scaner(name);

                Project.Instance.L_Scaner.Add(scaner);
                Win_Scaner.Instance.LoadPar(scaner);

                ToolStripItem tsb = new ToolStripStatusLabel("", Resources.客户端);
                tsb.AutoSize = false;
                tsb.Width = 20;
                tsb.Name = name;
                tsb.ToolTipText = string.Format("名称：{0}\r\n状态：{1}\r\nIP    : {2}\r\nPort : {3}", name, "未连接", "192.168.0.1", 10004);
                Win_Main.Instance.statusStrip1.Items.Insert(1, tsb);
                dgv_deviceList.Rows[dgv_deviceList.Rows.Count - 1].Selected = true;
                cancel = false;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }

        private void 全屏显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cancel = true;
                Win_InputMessage.Instance.XXXXXXXXXXXXXlbl_title.Text = ("请输入设备名称");
                Win_InputMessage.Instance.btn_confirm.Text = ("确定");
                Win_InputMessage.Instance.txt_input.DefaultText = "请输入自定义的设备名称";
                Win_InputMessage.Instance.txt_input.TextStr = "串口通讯1";
                Win_InputMessage.Instance.ShowDialog();
                string name = Win_InputMessage.Instance.input;
                if (name == string.Empty)
                    return;

                int idx = dgv_deviceList.Rows.Add();
                dgv_deviceList.Rows[idx].Tag = "Serial";
                dgv_deviceList.Rows[idx].Height = 30;
                dgv_deviceList.Rows[idx].Cells[0].Value = name;
                dgv_deviceList.Rows[idx].Cells[1].Value = Resources.客户端;
                dgv_deviceList.Rows[idx].Cells[2].Value = Resources.客户端;

                if (curForm != CurForm.Scaner)
                {
                    curForm = CurForm.Scaner;
                    pnl_formPnl.Controls.Clear();
                    Win_Serial.Instance.TopLevel = false;
                    Win_Serial.Instance.Parent = pnl_formPnl;
                    Win_Serial.Instance.Dock = DockStyle.Top;
                    Win_Serial.Instance.Show();
                }

                Serial serial = new Serial(name);

                Project.Instance.L_Serial.Add(serial);
                Win_Serial.Instance.LoadPar(serial);

                ToolStripItem tsb = new ToolStripStatusLabel("", Resources.客户端);
                tsb.AutoSize = false;
                tsb.Width = 20;
                tsb.Name = name;
                tsb.ToolTipText = string.Format("名称：{0}\r\n状态：{1}\r\nIP    : {2}\r\nPort : {3}", name, "未连接", "192.168.0.1", 10004);
                Win_Main.Instance.statusStrip1.Items.Insert(1, tsb);
                dgv_deviceList.Rows[dgv_deviceList.Rows.Count - 1].Selected = true;
                cancel = false;
            }
            catch (Exception ex)
            {
                Log.SaveError(Project.Instance.configuration.dataPath,ex);
            }
        }
        public enum CurForm
        {
            None,
            Scaner,
            TCPSever,
            TCPClient,
            LightController,
            Serial,
        }

        private void pLC通讯ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 实时ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}
