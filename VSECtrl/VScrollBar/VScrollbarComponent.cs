using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VControls.Helper;

namespace VControls.VScrollBar
{
    [ProvideProperty("UserCustomScrollbar", typeof(Control))]
    public class VScrollbarComponent : Component, IExtenderProvider
    {
        public enum Theme
        {
            Dark,
            Light,
     
        }
        Theme scrollBarTheme= Theme.Dark;
        public Theme ScrollBarTheme
        { 
            get { return scrollBarTheme; }
            set { scrollBarTheme=value; }
        }
        public VScrollbarComponent()
        {

        }

        public VScrollbarComponent(IContainer container)
        {
            container.Add(this);
        }

        Dictionary<Control, bool> m_controlCache = new Dictionary<Control, bool>();
        public bool CanExtend(object extendee)
        {
            if (extendee is ScrollableControl)
            {
                ScrollableControl control = (ScrollableControl)extendee;
                if (control.AutoScroll == true)
                {
                    return true;
                }
            }
            else if (extendee is TreeView)
            {
                TreeView control = (TreeView)extendee;
                if (control.Scrollable)
                {
                    return true;
                }
            }
            else if (extendee is TextBox)
            {
                TextBox control = (TextBox)extendee;
                if (control.Multiline && control.ScrollBars != ScrollBars.None)
                {
                    return true;
                }
            }
            return false;
        }

        [Browsable(true), Category("自定义属性"), Description("是否使用自定义滚动条"), DisplayName("UserCustomScrollbar"), Localizable(true)]
        public bool GetUserCustomScrollbar(Control control)
        {
            if (m_controlCache.ContainsKey(control))
                return m_controlCache[control];
            return true;
        }

        public void SetUserCustomScrollbar(Control control, bool blnUserCustomScrollbar)
        {
            m_controlCache[control] = blnUserCustomScrollbar;
            if (!blnUserCustomScrollbar)
                return;
            control.VisibleChanged += control_VisibleChanged;
            control.SizeChanged += control_SizeChanged;
            control.LocationChanged += control_LocationChanged;
            control.Disposed += control_Disposed;

            if (control is TreeView)
            {
                TreeView tv = (TreeView)control;
                tv.MouseWheel += tv_MouseWheel;
                tv.AfterSelect += tv_AfterSelect;
                tv.AfterExpand += tv_AfterExpand;
                tv.AfterCollapse += tv_AfterCollapse;
            }
            else if (control is TextBox)
            {
                TextBox txt = (TextBox)control;
                txt.MouseWheel += txt_MouseWheel;
                txt.TextChanged += txt_TextChanged;
                txt.KeyDown += txt_KeyDown;
            }
            control_SizeChanged(control, null);
        }


        void control_Disposed(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            if (m_lstVCache.ContainsKey(control) && m_lstVCache[control].Parent != null)
            {
                m_lstVCache[control].Parent.Controls.Remove(m_lstVCache[control]);
                m_lstVCache.Remove(control);
            }
        }

        void control_LocationChanged(object sender, EventArgs e)
        {
            ResetScrollLocation(sender);
        }

        void control_SizeChanged(object sender, EventArgs e)
        {
            if (VControlHelper.IsDesignMode())
            {
                return;
            }
            else
            {
                var control = sender as Control;

                bool blnHasVScrollbar = control.IsHandleCreated && (VControlHelper.GetWindowLong(control.Handle, STYLE) & VSCROLL) != 0;
                bool blnHasHScrollbar = control.IsHandleCreated && (VControlHelper.GetWindowLong(control.Handle, STYLE) & HSCROLL) != 0;
                if (blnHasVScrollbar)
                {
                    if (!m_lstVCache.ContainsKey(control))
                    {
                        if (control.Parent != null)
                        {
                            VVScrollbar barV = new VVScrollbar();
                            barV.Width = SystemInformation.VerticalScrollBarWidth;

                            barV.Scroll += barV_Scroll;
                            m_lstVCache[control] = barV;
                            if (blnHasHScrollbar)
                            {
                                barV.Height = control.Height - barV.Width - 2;
                            }
                            else
                            {
                                barV.Height = control.Height - 2;
                            }
                            SetVMaxNum(control);
                            switch (ScrollBarTheme)
                            {
                                case Theme.Dark:
                                    barV.ThumbColor = Color.FromArgb(224, 224, 224);
                                    barV.FillColor = Color.Gray;
                                    break;
                                case Theme.Light:
                                    break;
                                default:
                                    break;
                            }
                            barV.Location = new System.Drawing.Point(control.Right - barV.Width - 1, control.Top + 1);
                            control.Parent.Controls.Add(barV);
                            int intControlIndex = control.Parent.Controls.GetChildIndex(control);
                            control.Parent.Controls.SetChildIndex(barV, intControlIndex);
                        }
                    }
                    else
                    {
                        SetVMaxNum(control);
                    }
                }
                else
                {
                    if (m_lstVCache.ContainsKey(control) && m_lstVCache[control].Parent != null)
                    {
                        m_lstVCache[control].Parent.Controls.Remove(m_lstVCache[control]);
                        m_lstVCache.Remove(control);
                    }
                }

                if (blnHasHScrollbar)
                {
                    if (!m_lstHCache.ContainsKey(control))
                    {
                        if (control.Parent != null)
                        {
                            VHScrollbar barH = new VHScrollbar();
                            barH.Height = SystemInformation.HorizontalScrollBarHeight;

                            barH.Scroll += barH_Scroll;
                            m_lstHCache[control] = barH;
                            if (blnHasHScrollbar)
                            {
                                barH.Width = control.Width - barH.Height - 2;
                            }
                            else
                            {
                                barH.Width = control.Width - 2;
                            }
                            SetHMaxNum(control);
                            barH.Location = new System.Drawing.Point(control.Left + 1, control.Bottom - barH.Height - 1);
                            control.Parent.Controls.Add(barH);
                            int intControlIndex = control.Parent.Controls.GetChildIndex(control);
                            control.Parent.Controls.SetChildIndex(barH, intControlIndex);
                        }
                    }
                    else
                    {
                        SetHMaxNum(control);
                    }
                }
                else
                {
                    if (m_lstHCache.ContainsKey(control))
                    {
                        if (m_lstHCache[control].Visible && m_lstHCache[control].Parent != null)
                        {
                            m_lstHCache[control].Parent.Controls.Remove(m_lstHCache[control]);
                        }
                    }
                }
            }
            ResetScrollLocation(sender);
        }

        private void SetVMaxNum(Control control)
        {
            if (!m_lstVCache.ContainsKey(control))
                return;
            var into = VControlHelper.GetVScrollBarInfo(control.Handle);
            var intoH = VControlHelper.GetHScrollBarInfo(control.Handle);
            VVScrollbar barV = m_lstVCache[control];
            if (control is ScrollableControl)
            {
                barV.Maximum = (control as ScrollableControl).VerticalScroll.Maximum;
                barV.Value = (control as ScrollableControl).VerticalScroll.Value;
            }
            else if (control is TreeView)
            {
                barV.Maximum = GetTreeNodeMaxY(control as TreeView);
                barV.Value = (control as TreeView).AutoScrollOffset.Y;
            }
            else if (control is TextBox)
            {
                TextBox txt = (TextBox)control;
                int intTxtMaxHeight = 0;
                int intTextHeight = 0;
                using (var g = txt.CreateGraphics())
                {
                    intTxtMaxHeight = (int)g.MeasureString(txt.Text, txt.Font).Height;
                    intTextHeight = (int)g.MeasureString(txt.Text.Substring(0, txt.SelectionStart), txt.Font).Height;
                }
                barV.Maximum = intTxtMaxHeight;
                barV.Value = (control as TextBox).AutoScrollOffset.Y;
            }
        }
        private void SetHMaxNum(Control control)
        {
            if (!m_lstHCache.ContainsKey(control))
                return;
            VHScrollbar barH = m_lstHCache[control];
            if (control is ScrollableControl)
            {
                barH.Maximum = (control as ScrollableControl).HorizontalScroll.Maximum;
                barH.Value = (control as ScrollableControl).HorizontalScroll.Value;
            }
            else if (control is TreeView)
            {
                barH.Maximum = GetTreeNodeMaxX(control as TreeView);
                barH.Value = (control as TreeView).AutoScrollOffset.X;
            }
            else if (control is TextBox)
            {
                TextBox txt = (TextBox)control;
                int intTxtMaxWidth = 0;
                int intTextWidth = 0;
                using (var g = txt.CreateGraphics())
                {
                    intTxtMaxWidth = (int)g.MeasureString(txt.Text, txt.Font).Width;
                    intTextWidth = (int)g.MeasureString(txt.Text.Substring(0, txt.SelectionStart), txt.Font).Width;
                }
                barH.Maximum = intTxtMaxWidth;
                barH.Value = (control as TextBox).AutoScrollOffset.Y;
            }
        }
        /// <summary>
        /// Resets the v scroll location.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ResetScrollLocation(object sender)
        {
            Control control = (Control)sender;
            bool blnHasVScrollbar = control.IsHandleCreated && (VControlHelper.GetWindowLong(control.Handle, STYLE) & VSCROLL) != 0;
            bool blnHasHScrollbar = control.IsHandleCreated && (VControlHelper.GetWindowLong(control.Handle, STYLE) & HSCROLL) != 0;
            if (control.Visible)
            {
                if (m_lstVCache.ContainsKey(control))
                {
                    m_lstVCache[control].Location = new System.Drawing.Point(control.Right - m_lstVCache[control].Width, control.Top);
                    if (blnHasHScrollbar)
                    {
                        m_lstVCache[control].Height = control.Height - m_lstVCache[control].Width;
                    }
                    else
                    {
                        m_lstVCache[control].Height = control.Height;
                    }
                }

                if (m_lstHCache.ContainsKey(control))
                {
                    m_lstHCache[control].Location = new System.Drawing.Point(control.Left, control.Bottom - m_lstHCache[control].Height);
                    if (blnHasHScrollbar)
                    {
                        m_lstHCache[control].Width = control.Width - m_lstHCache[control].Height;
                    }
                    else
                    {
                        m_lstHCache[control].Width = control.Width;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the VisibleChanged event of the control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void control_VisibleChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            if (!control.Visible)
            {
                if (m_lstVCache.ContainsKey(control) && m_lstVCache[control].Parent != null)
                {
                    m_lstVCache[control].Parent.Controls.Remove(m_lstVCache[control]);
                    m_lstVCache.Remove(control);
                }

                if (m_lstHCache.ContainsKey(control) && m_lstHCache[control].Parent != null)
                {
                    m_lstHCache[control].Parent.Controls.Remove(m_lstHCache[control]);
                    m_lstHCache.Remove(control);
                }
            }
        }

        private const int HSCROLL = 0x100000;
        private const int VSCROLL = 0x200000;
        private const int STYLE = -16;

        private Dictionary<Control, VVScrollbar> m_lstVCache = new Dictionary<Control, VVScrollbar>();
        private Dictionary<Control, VHScrollbar> m_lstHCache = new Dictionary<Control, VHScrollbar>();

        void barV_Scroll(object sender, EventArgs e)
        {
            VVScrollbar bar = (VVScrollbar)sender;
            if (m_lstVCache.ContainsValue(bar))
            {
                Control c = m_lstVCache.FirstOrDefault(p => p.Value == bar).Key;
                if (c is ScrollableControl)
                {
                    (c as ScrollableControl).AutoScrollPosition = new Point((c as ScrollableControl).AutoScrollPosition.X, bar.Value);
                }
                else if (c is TreeView)
                {
                    TreeView tv = (c as TreeView);
                    SetTreeViewVScrollLocation(tv, tv.Nodes, bar.Value);
                }
                else if (c is TextBox)
                {
                    TextBox txt = (c as TextBox);
                    SetTextBoxVScrollLocation(txt, bar.Value);
                }
            }
        }

        void barH_Scroll(object sender, EventArgs e)
        {
            VHScrollbar bar = (VHScrollbar)sender;
            if (m_lstHCache.ContainsValue(bar))
            {
                Control c = m_lstHCache.FirstOrDefault(p => p.Value == bar).Key;
                if (c is ScrollableControl)
                {
                    (c as ScrollableControl).AutoScrollPosition = new Point(bar.Value, (c as ScrollableControl).AutoScrollPosition.Y);
                }
                else if (c is TreeView)
                {
                    //TreeView tv = (c as TreeView);
                    //SetTreeViewVScrollLocation(tv, tv.Nodes, bar.Value);
                }
                else if (c is TextBox)
                {
                    //TextBox txt = (c as TextBox);
                    //SetTextBoxVScrollLocation(txt, bar.Value);
                }
            }
        }

        #region Treeview处理    English:Treeview\u5904\u7406
        void tv_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            control_SizeChanged(sender as Control, null);
        }

        void tv_AfterExpand(object sender, TreeViewEventArgs e)
        {
            control_SizeChanged(sender as Control, null);
        }
        /// <summary>
        /// Gets the tree node 最大高度
        /// </summary>
        /// <param name="tv">The tv.</param>
        /// <returns>System.Int32.</returns>
        private int GetTreeNodeMaxY(TreeView tv)
        {
            TreeNode tnLast = tv.Nodes[tv.Nodes.Count - 1];
        begin:
            if (tnLast.IsExpanded && tnLast.Nodes.Count > 0)
            {
                tnLast = tnLast.LastNode;
                goto begin;
            }
            return tnLast.Bounds.Bottom;
        }

        private int GetTreeNodeMaxX(TreeView tv)
        {
            return tv.Nodes.Count != 0 ? tv.Nodes[0].Bounds.Right : 0;
        }

        void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView tv = (TreeView)sender;
            if (m_lstVCache.ContainsKey(tv))
            {
                m_lstVCache[tv].Value = tv.Nodes.Count > 0 ? Math.Abs(tv.Nodes[0].Bounds.Top) : 0;
            }
        }

        void tv_MouseWheel(object sender, MouseEventArgs e)
        {
            TreeView tv = (TreeView)sender;
            if (m_lstVCache.ContainsKey(tv))
            {
                m_lstVCache[tv].Value = tv.Nodes.Count > 0 ? Math.Abs(tv.Nodes[0].Bounds.Top) : 0;
            }
        }
        /// <summary>
        /// Sets the TreeView scroll location.
        /// </summary>
        /// <param name="tv">The tv.</param>
        /// <param name="tns">The TNS.</param>
        /// <param name="intY">The int y.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool SetTreeViewVScrollLocation(TreeView tv, TreeNodeCollection tns, int intY)
        {
            for (int i = 0; i < tns.Count; i++)
            {
                if (intY >= tns[i].Bounds.Top - tv.Nodes[0].Bounds.Top - 3 && intY <= tns[i].Bounds.Bottom - tv.Nodes[0].Bounds.Top + 3)
                {
                    tns[i].EnsureVisible();
                    return true;
                }
                else if (tns[i].IsExpanded && tns[i].Nodes.Count > 0)
                {
                    bool bln = SetTreeViewVScrollLocation(tv, tns[i].Nodes, intY);
                    if (bln)
                        return true;
                }
            }
            return false;
        }
        #endregion

        #region TextBox处理    English:TextBox Processing

        void txt_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            control_SizeChanged(txt, null);
            SetVMaxNum(txt);
            if (m_lstVCache.ContainsKey(txt))
            {
                using (var g = txt.CreateGraphics())
                {
                    var size = g.MeasureString(txt.Text.Substring(0, txt.SelectionStart), txt.Font);
                    m_lstVCache[txt].Value = (int)size.Height;
                }
            }
        }
        private void SetTextBoxVScrollLocation(TextBox txt, int intY)
        {
            using (var g = txt.CreateGraphics())
            {
                for (int i = 0; i < txt.Lines.Length; i++)
                {
                    string str = string.Join("\n", txt.Lines.Take(i + 1));
                    var size = g.MeasureString(str, txt.Font);
                    if (size.Height >= intY)
                    {
                        txt.SelectionStart = str.Length;
                        txt.ScrollToCaret();
                        return;
                    }
                }
            }
        }

        void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                TextBox txt = (TextBox)sender;
                if (m_lstVCache.ContainsKey(txt))
                {
                    using (var g = txt.CreateGraphics())
                    {
                        var size = g.MeasureString(txt.Text.Substring(0, txt.SelectionStart), txt.Font);
                        m_lstVCache[txt].Value = (int)size.Height;
                    }
                }
            }
        }

        void txt_MouseWheel(object sender, MouseEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (m_lstVCache.ContainsKey(txt))
            {
                using (var g = txt.CreateGraphics())
                {
                    StringBuilder str = new StringBuilder();
                    for (int i = 0; i < System.Windows.Forms.SystemInformation.MouseWheelScrollLines; i++)
                    {
                        str.AppendLine("A");
                    }
                    var height = (int)g.MeasureString(str.ToString(), txt.Font).Height;
                    if (e.Delta < 0)
                    {
                        if (height + m_lstVCache[txt].Value > m_lstVCache[txt].Maximum)
                            m_lstVCache[txt].Value = m_lstVCache[txt].Maximum;
                        else
                            m_lstVCache[txt].Value += height;
                    }
                    else
                    {
                        if (m_lstVCache[txt].Value - height < 0)
                            m_lstVCache[txt].Value = 0;
                        else
                            m_lstVCache[txt].Value -= height;
                    }
                }
            }
        }
        #endregion
    }
}
