namespace VControls
{
    partial class LxcCodeEidt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LxcCodeEidt));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.indentSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outdentSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.uppercaseSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowercaseSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findDialogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findAndReplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.goToLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wordWrapItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indentGuidesItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hiddenCharactersItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom100ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.FileName = new System.Windows.Forms.Label();
            this.TextPanel = new System.Windows.Forms.Panel();
            this.BtnNextSearch = new System.Windows.Forms.Button();
            this.BtnPrevSearch = new System.Windows.Forms.Button();
            this.BtnCloseSearch = new System.Windows.Forms.Button();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.PanelSearch = new System.Windows.Forms.Panel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.PanelSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.menuStrip1.Location = new System.Drawing.Point(1, 1);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(890, 29);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(121, 24);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator1,
            this.selectLineToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.clearSelectionToolStripMenuItem,
            this.toolStripSeparator2,
            this.indentSelectionToolStripMenuItem,
            this.outdentSelectionToolStripMenuItem,
            this.toolStripSeparator3,
            this.uppercaseSelectionToolStripMenuItem,
            this.lowercaseSelectionToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+X";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+V";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // selectLineToolStripMenuItem
            // 
            this.selectLineToolStripMenuItem.Name = "selectLineToolStripMenuItem";
            this.selectLineToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.selectLineToolStripMenuItem.Text = "Select Line";
            this.selectLineToolStripMenuItem.Click += new System.EventHandler(this.selectLineToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+A";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // clearSelectionToolStripMenuItem
            // 
            this.clearSelectionToolStripMenuItem.Name = "clearSelectionToolStripMenuItem";
            this.clearSelectionToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.clearSelectionToolStripMenuItem.Text = "Clear Selection";
            this.clearSelectionToolStripMenuItem.Click += new System.EventHandler(this.clearSelectionToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(193, 6);
            // 
            // indentSelectionToolStripMenuItem
            // 
            this.indentSelectionToolStripMenuItem.Name = "indentSelectionToolStripMenuItem";
            this.indentSelectionToolStripMenuItem.ShortcutKeyDisplayString = "Tab";
            this.indentSelectionToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.indentSelectionToolStripMenuItem.Text = "Indent";
            this.indentSelectionToolStripMenuItem.Click += new System.EventHandler(this.indentSelectionToolStripMenuItem_Click);
            // 
            // outdentSelectionToolStripMenuItem
            // 
            this.outdentSelectionToolStripMenuItem.Name = "outdentSelectionToolStripMenuItem";
            this.outdentSelectionToolStripMenuItem.ShortcutKeyDisplayString = "Shift+Tab";
            this.outdentSelectionToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.outdentSelectionToolStripMenuItem.Text = "Outdent";
            this.outdentSelectionToolStripMenuItem.Click += new System.EventHandler(this.outdentSelectionToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(193, 6);
            // 
            // uppercaseSelectionToolStripMenuItem
            // 
            this.uppercaseSelectionToolStripMenuItem.Name = "uppercaseSelectionToolStripMenuItem";
            this.uppercaseSelectionToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+U";
            this.uppercaseSelectionToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.uppercaseSelectionToolStripMenuItem.Text = "Uppercase";
            this.uppercaseSelectionToolStripMenuItem.Click += new System.EventHandler(this.uppercaseSelectionToolStripMenuItem_Click);
            // 
            // lowercaseSelectionToolStripMenuItem
            // 
            this.lowercaseSelectionToolStripMenuItem.Name = "lowercaseSelectionToolStripMenuItem";
            this.lowercaseSelectionToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+L";
            this.lowercaseSelectionToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.lowercaseSelectionToolStripMenuItem.Text = "Lowercase";
            this.lowercaseSelectionToolStripMenuItem.Click += new System.EventHandler(this.lowercaseSelectionToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.findDialogToolStripMenuItem,
            this.findAndReplaceToolStripMenuItem,
            this.toolStripSeparator7,
            this.goToLineToolStripMenuItem});
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(61, 23);
            this.searchToolStripMenuItem.Text = "Search";
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+F";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.findToolStripMenuItem.Text = "Quick Find...";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findDialogToolStripMenuItem_Click);
            // 
            // findDialogToolStripMenuItem
            // 
            this.findDialogToolStripMenuItem.Name = "findDialogToolStripMenuItem";
            this.findDialogToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Alt+F";
            this.findDialogToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.findDialogToolStripMenuItem.Text = "Find...";
            this.findDialogToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // findAndReplaceToolStripMenuItem
            // 
            this.findAndReplaceToolStripMenuItem.Name = "findAndReplaceToolStripMenuItem";
            this.findAndReplaceToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+H";
            this.findAndReplaceToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.findAndReplaceToolStripMenuItem.Text = "Find and Replace...";
            this.findAndReplaceToolStripMenuItem.Click += new System.EventHandler(this.findAndReplaceToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(238, 6);
            // 
            // goToLineToolStripMenuItem
            // 
            this.goToLineToolStripMenuItem.Name = "goToLineToolStripMenuItem";
            this.goToLineToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+G";
            this.goToLineToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.goToLineToolStripMenuItem.Text = "Go To Line...";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wordWrapItem,
            this.indentGuidesItem,
            this.hiddenCharactersItem,
            this.toolStripSeparator4,
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem,
            this.zoom100ToolStripMenuItem,
            this.toolStripSeparator5,
            this.collapseAllToolStripMenuItem,
            this.expandAllToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(50, 23);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // wordWrapItem
            // 
            this.wordWrapItem.Name = "wordWrapItem";
            this.wordWrapItem.Size = new System.Drawing.Size(221, 24);
            this.wordWrapItem.Text = "Word Wrap";
            this.wordWrapItem.Click += new System.EventHandler(this.wordWrapToolStripMenuItem1_Click);
            // 
            // indentGuidesItem
            // 
            this.indentGuidesItem.Checked = true;
            this.indentGuidesItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.indentGuidesItem.Name = "indentGuidesItem";
            this.indentGuidesItem.Size = new System.Drawing.Size(221, 24);
            this.indentGuidesItem.Text = "Show Indent Guides";
            this.indentGuidesItem.Click += new System.EventHandler(this.indentGuidesToolStripMenuItem_Click);
            // 
            // hiddenCharactersItem
            // 
            this.hiddenCharactersItem.Name = "hiddenCharactersItem";
            this.hiddenCharactersItem.Size = new System.Drawing.Size(221, 24);
            this.hiddenCharactersItem.Text = "Show Whitespace";
            this.hiddenCharactersItem.Click += new System.EventHandler(this.hiddenCharactersToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(218, 6);
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Plus";
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(221, 24);
            this.zoomInToolStripMenuItem.Text = "Zoom In";
            this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.zoomInToolStripMenuItem_Click);
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Minus";
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(221, 24);
            this.zoomOutToolStripMenuItem.Text = "Zoom Out";
            this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
            // 
            // zoom100ToolStripMenuItem
            // 
            this.zoom100ToolStripMenuItem.Name = "zoom100ToolStripMenuItem";
            this.zoom100ToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+0";
            this.zoom100ToolStripMenuItem.Size = new System.Drawing.Size(221, 24);
            this.zoom100ToolStripMenuItem.Text = "Zoom 100%";
            this.zoom100ToolStripMenuItem.Click += new System.EventHandler(this.zoom100ToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(218, 6);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(221, 24);
            this.collapseAllToolStripMenuItem.Text = "Collapse All";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(221, 24);
            this.expandAllToolStripMenuItem.Text = "Expand All";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(49, 23);
            this.toolStripMenuItem1.Text = "编译";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(49, 23);
            this.toolStripMenuItem2.Text = "保存";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // FileName
            // 
            this.FileName.AutoSize = true;
            this.FileName.BackColor = System.Drawing.Color.Transparent;
            this.FileName.Font = new System.Drawing.Font("Segoe UI Light", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileName.ForeColor = System.Drawing.Color.MediumSpringGreen;
            this.FileName.Location = new System.Drawing.Point(573, 4);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(52, 30);
            this.FileName.TabIndex = 4;
            this.FileName.Text = "Title";
            this.FileName.Visible = false;
            // 
            // TextPanel
            // 
            this.TextPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.TextPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextPanel.ForeColor = System.Drawing.Color.White;
            this.TextPanel.Location = new System.Drawing.Point(1, 30);
            this.TextPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.TextPanel.Name = "TextPanel";
            this.TextPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.TextPanel.Size = new System.Drawing.Size(890, 697);
            this.TextPanel.TabIndex = 5;
            // 
            // BtnNextSearch
            // 
            this.BtnNextSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNextSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNextSearch.ForeColor = System.Drawing.Color.White;
            this.BtnNextSearch.Image = ((System.Drawing.Image)(resources.GetObject("BtnNextSearch.Image")));
            this.BtnNextSearch.Location = new System.Drawing.Point(249, 5);
            this.BtnNextSearch.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.BtnNextSearch.Name = "BtnNextSearch";
            this.BtnNextSearch.Size = new System.Drawing.Size(24, 24);
            this.BtnNextSearch.TabIndex = 9;
            this.BtnNextSearch.Tag = "Find next (Enter)";
            this.BtnNextSearch.UseVisualStyleBackColor = true;
            this.BtnNextSearch.Click += new System.EventHandler(this.BtnNextSearch_Click);
            // 
            // BtnPrevSearch
            // 
            this.BtnPrevSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrevSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrevSearch.ForeColor = System.Drawing.Color.White;
            this.BtnPrevSearch.Image = ((System.Drawing.Image)(resources.GetObject("BtnPrevSearch.Image")));
            this.BtnPrevSearch.Location = new System.Drawing.Point(220, 5);
            this.BtnPrevSearch.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.BtnPrevSearch.Name = "BtnPrevSearch";
            this.BtnPrevSearch.Size = new System.Drawing.Size(24, 24);
            this.BtnPrevSearch.TabIndex = 8;
            this.BtnPrevSearch.Tag = "Find previous (Shift+Enter)";
            this.BtnPrevSearch.UseVisualStyleBackColor = true;
            this.BtnPrevSearch.Click += new System.EventHandler(this.BtnPrevSearch_Click);
            // 
            // BtnCloseSearch
            // 
            this.BtnCloseSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCloseSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCloseSearch.ForeColor = System.Drawing.Color.White;
            this.BtnCloseSearch.Image = ((System.Drawing.Image)(resources.GetObject("BtnCloseSearch.Image")));
            this.BtnCloseSearch.Location = new System.Drawing.Point(285, 5);
            this.BtnCloseSearch.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.BtnCloseSearch.Name = "BtnCloseSearch";
            this.BtnCloseSearch.Size = new System.Drawing.Size(24, 24);
            this.BtnCloseSearch.TabIndex = 7;
            this.BtnCloseSearch.Tag = "Close (Esc)";
            this.BtnCloseSearch.UseVisualStyleBackColor = true;
            this.BtnCloseSearch.Click += new System.EventHandler(this.BtnClearSearch_Click);
            // 
            // TxtSearch
            // 
            this.TxtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearch.Location = new System.Drawing.Point(3, 4);
            this.TxtSearch.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(208, 25);
            this.TxtSearch.TabIndex = 6;
            // 
            // PanelSearch
            // 
            this.PanelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelSearch.BackColor = System.Drawing.Color.White;
            this.PanelSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelSearch.Controls.Add(this.BtnNextSearch);
            this.PanelSearch.Controls.Add(this.BtnPrevSearch);
            this.PanelSearch.Controls.Add(this.BtnCloseSearch);
            this.PanelSearch.Controls.Add(this.TxtSearch);
            this.PanelSearch.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PanelSearch.Location = new System.Drawing.Point(571, 1);
            this.PanelSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PanelSearch.Name = "PanelSearch";
            this.PanelSearch.Size = new System.Drawing.Size(318, 34);
            this.PanelSearch.TabIndex = 11;
            this.PanelSearch.Visible = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "txt";
            this.openFileDialog.FileName = "New File";
            this.openFileDialog.Filter = "All files|*.*";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox1.Location = new System.Drawing.Point(1, 727);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(890, 146);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // LxcCodeEidt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.TextPanel);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.PanelSearch);
            this.Controls.Add(this.FileName);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LxcCodeEidt";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(892, 874);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.PanelSearch.ResumeLayout(false);
            this.PanelSearch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem selectLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem indentSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outdentSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem uppercaseSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lowercaseSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findDialogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findAndReplaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem goToLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wordWrapItem;
        private System.Windows.Forms.ToolStripMenuItem indentGuidesItem;
        private System.Windows.Forms.ToolStripMenuItem hiddenCharactersItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoom100ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.Label FileName;
        private System.Windows.Forms.Panel TextPanel;
        private System.Windows.Forms.Button BtnNextSearch;
        private System.Windows.Forms.Button BtnPrevSearch;
        private System.Windows.Forms.Button BtnCloseSearch;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Panel PanelSearch;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }
}
