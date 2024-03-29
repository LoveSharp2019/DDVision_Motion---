using System.ComponentModel;
using System.Windows.Forms;

namespace Lxc.VisionPlus.ImageView
{
    partial class ThresholdUnit
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelAxis = new System.Windows.Forms.Panel();
            this.groupBox9 = new LXCSystem.Control.CommonCtrl.LxcGroupBox();
            this.labelDeviation = new System.Windows.Forms.Label();
            this.labelMean = new System.Windows.Forms.Label();
            this.labelRange = new System.Windows.Forms.Label();
            this.labelPeak = new System.Windows.Forms.Label();
            this.labelRangeX = new System.Windows.Forms.Label();
            this.labelPeakX = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox9.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAxis
            // 
            this.panelAxis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAxis.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelAxis.Location = new System.Drawing.Point(3, 4);
            this.panelAxis.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelAxis.Name = "panelAxis";
            this.panelAxis.Size = new System.Drawing.Size(578, 247);
            this.panelAxis.TabIndex = 0;
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.Color.Transparent;
            this.groupBox9.BorderColor = System.Drawing.Color.Gray;
            this.groupBox9.BorderStyle = LXCSystem.Control.Base.Common.EnumBorderStyle.XStyle;
            this.groupBox9.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox9.CaptionFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox9.Controls.Add(this.labelDeviation);
            this.groupBox9.Controls.Add(this.labelMean);
            this.groupBox9.Controls.Add(this.labelRange);
            this.groupBox9.Controls.Add(this.labelPeak);
            this.groupBox9.Controls.Add(this.labelRangeX);
            this.groupBox9.Controls.Add(this.labelPeakX);
            this.groupBox9.Controls.Add(this.label23);
            this.groupBox9.Controls.Add(this.label22);
            this.groupBox9.Controls.Add(this.label19);
            this.groupBox9.Controls.Add(this.label18);
            this.groupBox9.Controls.Add(this.lblY);
            this.groupBox9.Controls.Add(this.lblX);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox9.Location = new System.Drawing.Point(3, 259);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox9.Size = new System.Drawing.Size(578, 162);
            this.groupBox9.TabIndex = 1;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "统计";
            this.groupBox9.TextMargin = 6;
            // 
            // labelDeviation
            // 
            this.labelDeviation.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDeviation.ForeColor = System.Drawing.Color.White;
            this.labelDeviation.Location = new System.Drawing.Point(314, 135);
            this.labelDeviation.Name = "labelDeviation";
            this.labelDeviation.Size = new System.Drawing.Size(100, 24);
            this.labelDeviation.TabIndex = 0;
            this.labelDeviation.Text = "0";
            this.labelDeviation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelMean
            // 
            this.labelMean.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMean.ForeColor = System.Drawing.Color.White;
            this.labelMean.Location = new System.Drawing.Point(314, 110);
            this.labelMean.Name = "labelMean";
            this.labelMean.Size = new System.Drawing.Size(100, 24);
            this.labelMean.TabIndex = 1;
            this.labelMean.Text = "0";
            // 
            // labelRange
            // 
            this.labelRange.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelRange.ForeColor = System.Drawing.Color.White;
            this.labelRange.Location = new System.Drawing.Point(314, 85);
            this.labelRange.Name = "labelRange";
            this.labelRange.Size = new System.Drawing.Size(134, 25);
            this.labelRange.TabIndex = 2;
            this.labelRange.Text = "0 ... 0";
            // 
            // labelPeak
            // 
            this.labelPeak.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPeak.ForeColor = System.Drawing.Color.White;
            this.labelPeak.Location = new System.Drawing.Point(314, 61);
            this.labelPeak.Name = "labelPeak";
            this.labelPeak.Size = new System.Drawing.Size(100, 24);
            this.labelPeak.TabIndex = 3;
            this.labelPeak.Text = "0";
            // 
            // labelRangeX
            // 
            this.labelRangeX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelRangeX.ForeColor = System.Drawing.Color.White;
            this.labelRangeX.Location = new System.Drawing.Point(180, 85);
            this.labelRangeX.Name = "labelRangeX";
            this.labelRangeX.Size = new System.Drawing.Size(100, 25);
            this.labelRangeX.TabIndex = 6;
            this.labelRangeX.Text = "0 ... 0";
            // 
            // labelPeakX
            // 
            this.labelPeakX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPeakX.ForeColor = System.Drawing.Color.White;
            this.labelPeakX.Location = new System.Drawing.Point(180, 61);
            this.labelPeakX.Name = "labelPeakX";
            this.labelPeakX.Size = new System.Drawing.Size(100, 24);
            this.labelPeakX.TabIndex = 7;
            this.labelPeakX.Text = "0";
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.ForeColor = System.Drawing.Color.Silver;
            this.label23.Location = new System.Drawing.Point(22, 135);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(124, 24);
            this.label23.TabIndex = 8;
            this.label23.Text = "偏差:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.ForeColor = System.Drawing.Color.Silver;
            this.label22.Location = new System.Drawing.Point(22, 110);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(124, 24);
            this.label22.TabIndex = 9;
            this.label22.Text = "平均值:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.Silver;
            this.label19.Location = new System.Drawing.Point(22, 85);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(124, 24);
            this.label19.TabIndex = 10;
            this.label19.Text = "范围:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.ForeColor = System.Drawing.Color.Silver;
            this.label18.Location = new System.Drawing.Point(22, 61);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(124, 24);
            this.label18.TabIndex = 11;
            this.label18.Text = "峰值:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblY
            // 
            this.lblY.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblY.ForeColor = System.Drawing.Color.Silver;
            this.lblY.Location = new System.Drawing.Point(314, 24);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(122, 24);
            this.lblY.TabIndex = 12;
            this.lblY.Text = "灰度值";
            // 
            // lblX
            // 
            this.lblX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblX.ForeColor = System.Drawing.Color.Silver;
            this.lblX.Location = new System.Drawing.Point(180, 24);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(122, 24);
            this.lblX.TabIndex = 13;
            this.lblX.Text = "x-值";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 584F));
            this.tableLayoutPanel1.Controls.Add(this.panelAxis, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox9, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(584, 425);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ThresholdUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ThresholdUnit";
            this.Size = new System.Drawing.Size(584, 425);
            this.Load += new System.EventHandler(this.ThresholdUnit_Load);
            this.Resize += new System.EventHandler(this.ThresholdUnit_Resize);
            this.groupBox9.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelAxis;

        private LXCSystem.Control.CommonCtrl.LxcGroupBox groupBox9;

        private Label labelDeviation;

        private Label labelMean;

        private Label labelRange;

        private Label labelPeak;

        private Label labelRangeX;

        private Label labelPeakX;

        private Label label23;

        private Label label22;

        private Label label19;

        private Label label18;

        private Label lblY;

        private Label lblX;

        private TableLayoutPanel tableLayoutPanel1;
    }
}
