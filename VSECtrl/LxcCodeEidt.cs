using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScintillaNET;
using System.IO;
using System.CodeDom.Compiler;

namespace VControls
{
    public partial class LxcCodeEidt : UserControl
    {
        ScintillaNET.Scintilla TextArea;
        Form f = new Form();
        string path = "";
        public LxcCodeEidt()
        {
            InitializeComponent();
            EidtInit();
        }
        public void EidtInit()
        { // CREATE CONTROL
            TextArea = new ScintillaNET.Scintilla();
            TextPanel.Controls.Add(TextArea);

            // BASIC CONFIG
            TextArea.Dock = System.Windows.Forms.DockStyle.Fill;
            TextArea.TextChanged += (this.OnTextChanged);

            // INITIAL VIEW CONFIG
            TextArea.WrapMode = WrapMode.None;
            TextArea.IndentationGuides = IndentView.LookBoth;

            // STYLING
            InitColors();
            InitSyntaxColoring();

            // NUMBER MARGIN
            InitNumberMargin();

            // BOOKMARK MARGIN
            InitBookmarkMargin();

            // CODE FOLDING MARGIN
            InitCodeFolding();

            // DRAG DROP
            InitDragDropFile();

            // DEFAULT FILE
            LoadDataFromFile("../../MainForm.cs");

            // INIT HOTKEYS
            InitHotkeys();
            // INIT HEADTEXT
            TextArea.Text = HeadText();
        }
        private void InitColors()
        {

            TextArea.SetSelectionBackColor(true, IntToColor(0x114D9C));

        }
        private void OnTextChanged(object sender, EventArgs e)
        {

        }
        private void InitHotkeys()
        {

            // register the hotkeys with the form
            HotKeyManager.AddHotKey(f, OpenSearch, Keys.F, true);
            HotKeyManager.AddHotKey(f, OpenFindDialog, Keys.F, true, false, true);
            HotKeyManager.AddHotKey(f, OpenReplaceDialog, Keys.R, true);
            HotKeyManager.AddHotKey(f, OpenReplaceDialog, Keys.H, true);
            HotKeyManager.AddHotKey(f, Uppercase, Keys.U, true);
            HotKeyManager.AddHotKey(f, Lowercase, Keys.L, true);
            HotKeyManager.AddHotKey(f, ZoomIn, Keys.Oemplus, true);
            HotKeyManager.AddHotKey(f, ZoomOut, Keys.OemMinus, true);
            HotKeyManager.AddHotKey(f, ZoomDefault, Keys.D0, true);
            HotKeyManager.AddHotKey(f, CloseSearch, Keys.Escape);

            // remove conflicting hotkeys from scintilla
            TextArea.ClearCmdKey(Keys.Control | Keys.F);
            TextArea.ClearCmdKey(Keys.Control | Keys.R);
            TextArea.ClearCmdKey(Keys.Control | Keys.H);
            TextArea.ClearCmdKey(Keys.Control | Keys.L);
            TextArea.ClearCmdKey(Keys.Control | Keys.U);

        }

        private void InitSyntaxColoring()
        {

            // Configure the default style
            TextArea.StyleResetDefault();
            TextArea.Styles[Style.Default].Font = "微软雅黑";
            TextArea.Styles[Style.Default].Size = 12;
            TextArea.Styles[Style.Default].BackColor = IntToColor(0x212121);
            TextArea.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
            TextArea.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            TextArea.Styles[Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            TextArea.Styles[Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            TextArea.Styles[Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            TextArea.Styles[Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            TextArea.Styles[Style.Cpp.Number].ForeColor = Color.MediumSpringGreen;// IntToColor(0xFFFFF);
            TextArea.Styles[Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            TextArea.Styles[Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            TextArea.Styles[Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            TextArea.Styles[Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            TextArea.Styles[Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            TextArea.Styles[Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            TextArea.Styles[Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            TextArea.Styles[Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            TextArea.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            TextArea.Styles[Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);

            TextArea.Lexer = Lexer.Cpp;

            TextArea.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            TextArea.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");

        }
        #region Numbers, Bookmarks, Code Folding

        /// <summary>
        /// the background color of the text area
        /// </summary>
        private const int BACK_COLOR = 0x2A211C;

        /// <summary>
        /// default text color of the text area
        /// </summary>
        private const int FORE_COLOR = 0xB7B7B7;

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;

        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = false;

        private void InitNumberMargin()
        {

            TextArea.Styles[Style.LineNumber].BackColor = Color.FromArgb(30, 30, 30);
            TextArea.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            TextArea.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            TextArea.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = TextArea.Margins[NUMBER_MARGIN];
            nums.Width = 45;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 2;

            TextArea.MarginClick += TextArea_MarginClick;
        }

        private void InitBookmarkMargin()
        {

            //TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));

            var margin = TextArea.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.BackColor;
            margin.Mask = (1 << BOOKMARK_MARKER);
            //margin.Cursor = MarginCursor.Arrow;

            var marker = TextArea.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(IntToColor(0xFF003B));
            marker.SetForeColor(IntToColor(0x000000));
            marker.SetAlpha(100);

        }

        private void InitCodeFolding()
        {

            TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));
            TextArea.SetFoldMarginHighlightColor(true, IntToColor(BACK_COLOR));

            // Enable code folding
            TextArea.SetProperty("fold", "1");
            TextArea.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            TextArea.Margins[FOLDING_MARGIN].Type = MarginType.BackColor;
            TextArea.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            TextArea.Margins[FOLDING_MARGIN].Sensitive = true;
            TextArea.Margins[FOLDING_MARGIN].Width = 15;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                TextArea.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                TextArea.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            TextArea.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            TextArea.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            TextArea.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            TextArea.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            TextArea.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            TextArea.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            TextArea.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            TextArea.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        }

        private void TextArea_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = TextArea.Lines[TextArea.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
            }
        }

        #endregion

        #region Drag & Drop File

        public void InitDragDropFile()
        {

            TextArea.AllowDrop = true;
            TextArea.DragEnter += delegate (object sender, DragEventArgs e) {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            };
            TextArea.DragDrop += delegate (object sender, DragEventArgs e) {

                // get file drop
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {

                    Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
                    if (a != null)
                    {

                        string path = a.GetValue(0).ToString();

                        LoadDataFromFile(path);

                    }
                }
            };

        }
        public void LoadDataFromString(string str)
        {
           //  FileName.Text = Path.GetFileName(path);
                TextArea.Text = HeadText() + str;
           
        }
        private void LoadDataFromFile(string path)
        {
            if (File.Exists(path))
            {
                FileName.Text = Path.GetFileName(path);
                TextArea.Text = HeadText() + File.ReadAllText(path);
            }
        }
       // private static string _defaultScriptCSharp = "using System;\r\nusing Lxc.VisionPlus;\r\nusing Lxc.VisionPlus3D;\r\nusing Lxc.VisionPlus.ToolGroup;\r\n\r\npublic class UserScript : HToolGroupBaseScript\r\n{\r\n\r\n  // The GroupRun function is called when the tool group is run.  The default\r\n  // implementation provided here is equivalent to the normal behavior of the\r\n  // tool group.  Modifying this function will allow you to change the behavior\r\n  // when the tool group is run.\r\n  public override bool GroupRun(ref string message, ref ToolResultConstants result)\r\n  {\r\n" + "hScriptSupport.hDebugSupportCodeCSharp" + "    // Run each tool in the tool group using the RunTool function\r\n    for (Int32 toolIdx = 0; toolIdx < toolGroup.Tools.Count; toolIdx++)\r\n      toolGroup.RunTool(toolGroup.Tools[toolIdx], ref message, ref result);\r\n\r\n    // Returning False indicates we ran the tools in script, and they should not be\r\n    // run by VisionPlus \r\n    return false;\r\n  }\r\n\r\n#region \"When the Current Run Record is Created\"\r\n  public override void ModifyCurrentRunRecord(Lxc.VisionPlus.IhRecord currentRecord)\r\n  {\r\n  }\r\n#endregion\r\n\r\n#region \"When the Last Run Record is Created\"\r\n  // Allows you to add or modify the contents of the last run record when it is\r\n  // created.  For example, you might add custom graphics to the run record here.\r\n  public override void ModifyLastRunRecord(Lxc.VisionPlus.IhRecord lastRecord)\r\n  {\r\n  }\r\n#endregion\r\n\r\n#region \"When the Script is Initialized\"\r\n  // Perform any initialization required by your script here\r\n  public override void Initialize(HToolGroup host)\r\n  {\r\n    // DO NOT REMOVE - Call the base class implementation first - DO NOT REMOVE\r\n    base.Initialize(host);\r\n  }\r\n#endregion\r\n\r\n}\r\n";
        private string HeadText()
        {
            return "*==============================================\n*Lixiangchun\n*VSE Script[Halcon]\n*" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n*==============================================\n";
        }
        #endregion

        #region Main Menu Commands

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog.FileName;
                LoadDataFromFile(openFileDialog.FileName);
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSearch();
        }

        private void findDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFindDialog();
        }

        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenReplaceDialog();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.SelectAll();
        }

        private void selectLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Line line = TextArea.Lines[TextArea.CurrentLine];
            TextArea.SetSelection(line.Position + line.Length, line.Position);
        }

        private void clearSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.SetEmptySelection(0);
        }

        private void indentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Indent();
        }

        private void outdentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Outdent();
        }

        private void uppercaseSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uppercase();
        }

        private void lowercaseSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lowercase();
        }

        private void wordWrapToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            // toggle word wrap
            wordWrapItem.Checked = !wordWrapItem.Checked;
            TextArea.WrapMode = wordWrapItem.Checked ? WrapMode.Word : WrapMode.None;
        }

        private void indentGuidesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // toggle indent guides
            indentGuidesItem.Checked = !indentGuidesItem.Checked;
            TextArea.IndentationGuides = indentGuidesItem.Checked ? IndentView.LookBoth : IndentView.None;
        }

        private void hiddenCharactersToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // toggle view whitespace
            hiddenCharactersItem.Checked = !hiddenCharactersItem.Checked;
            TextArea.ViewWhitespace = hiddenCharactersItem.Checked ? WhitespaceMode.VisibleAlways : WhitespaceMode.Invisible;
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }

        private void zoom100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomDefault();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.FoldAll(FoldAction.Contract);
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.FoldAll(FoldAction.Expand);
        }


        #endregion

        #region Uppercase / Lowercase

        private void Lowercase()
        {

            // save the selection
            int start = TextArea.SelectionStart;
            int end = TextArea.SelectionEnd;

            // modify the selected text
            TextArea.ReplaceSelection(TextArea.GetTextRange(start, end - start).ToLower());

            // preserve the original selection
            TextArea.SetSelection(start, end);
        }

        private void Uppercase()
        {

            // save the selection
            int start = TextArea.SelectionStart;
            int end = TextArea.SelectionEnd;

            // modify the selected text
            TextArea.ReplaceSelection(TextArea.GetTextRange(start, end - start).ToUpper());

            // preserve the original selection
            TextArea.SetSelection(start, end);
        }

        #endregion

        #region Indent / Outdent

        private void Indent()
        {
            // we use this hack to send "Shift+Tab" to scintilla, since there is no known API to indent,
            // although the indentation function exists. Pressing TAB with the editor focused confirms this.
            GenerateKeystrokes("{TAB}");
        }

        private void Outdent()
        {
            // we use this hack to send "Shift+Tab" to scintilla, since there is no known API to outdent,
            // although the indentation function exists. Pressing Shift+Tab with the editor focused confirms this.
            GenerateKeystrokes("+{TAB}");
        }

        private void GenerateKeystrokes(string keys)
        {
            HotKeyManager.Enable = false;
            TextArea.Focus();
            SendKeys.Send(keys);
            HotKeyManager.Enable = true;
        }

        #endregion

        #region Zoom

        private void ZoomIn()
        {
            TextArea.ZoomIn();
        }

        private void ZoomOut()
        {
            TextArea.ZoomOut();
        }

        private void ZoomDefault()
        {
            TextArea.Zoom = 0;
        }


        #endregion

        #region Quick Search Bar

        bool SearchIsOpen = false;

        private void OpenSearch()
        {

            SearchManager.SearchBox = TxtSearch;
            SearchManager.TextArea = TextArea;

            if (!SearchIsOpen)
            {
                SearchIsOpen = true;
                InvokeIfNeeded(delegate () {
                    PanelSearch.Visible = true;
                    TxtSearch.Text = SearchManager.LastSearch;
                    TxtSearch.Focus();
                    TxtSearch.SelectAll();
                });
            }
            else
            {
                InvokeIfNeeded(delegate () {
                    TxtSearch.Focus();
                    TxtSearch.SelectAll();
                });
            }
        }
        private void CloseSearch()
        {
            if (SearchIsOpen)
            {
                SearchIsOpen = false;
                InvokeIfNeeded(delegate () {
                    PanelSearch.Visible = false;
                    //CurBrowser.GetBrowser().StopFinding(true);
                });
            }
        }

        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
            CloseSearch();
        }

        private void BtnPrevSearch_Click(object sender, EventArgs e)
        {
            SearchManager.Find(false, false);
        }
        private void BtnNextSearch_Click(object sender, EventArgs e)
        {
            SearchManager.Find(true, false);
        }
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchManager.Find(true, true);
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (HotKeyManager.IsHotkey(e, Keys.Enter))
            {
                SearchManager.Find(true, false);
            }
            if (HotKeyManager.IsHotkey(e, Keys.Enter, true) || HotKeyManager.IsHotkey(e, Keys.Enter, false, true))
            {
                SearchManager.Find(false, false);
            }
        }

        #endregion

        #region Find & Replace Dialog

        private void OpenFindDialog()
        {

        }
        private void OpenReplaceDialog()
        {


        }

        #endregion

        #region Utils

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        public void InvokeIfNeeded(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }



        #endregion

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

            //// Build the source file name with the appropriate
            //// language extension.
            //String sourceFile = path;
            //// Compile the source file into an executable output file.
            //CompilerParameters cp = new CompilerParameters();
            //cp.ReferencedAssemblies.Add("system.dll");                //添加程序集 system.dll 的引用
            //cp.ReferencedAssemblies.Add("system.data.dll");            //添加程序集 system.data.dll 的引用
            //cp.ReferencedAssemblies.Add("system.xml.dll");            //添加程序集 system.xml.dll 的引用
            //cp.GenerateExecutable = false;                            //不生成可执行文件
            //cp.GenerateInMemory = true;                                //在内存中运行
            //StringBuilder code = new StringBuilder();                //创建代码串
            ///*
            // *  添加常见且必须的引用字符串
            // */
            //code.Append("using System; /n");
            //code.Append("using System.Data; /n");
            //code.Append("using System.Xml; /n");
            //code.Append("namespace LxcBasicControl { /n");                    //生成代码的命名空间为EvalGuy，和本代码一样
            //code.Append("  public class test { /n");
            //code.Append(TextArea.Text);
            //code.Append("public static void Main(string[] args)");
            //code.Append("{}");
            //code.Append("} }");
            //CompilerResults cr = CompileCode(provider, TextArea.Text, "Test.dll");
            //// CompilerResults cr = provider.CompileAssemblyFromSource();
            //if (cr.Errors.Count > 0)
            //{
            //    // Display compilation errors.
            //    richTextBox1.Text = "Errors encountered while building " +
            //                    sourceFile + " into " + cr.PathToAssembly + ": \r\n\n";
            //    foreach (CompilerError ce in cr.Errors)
            //        richTextBox1.AppendText(ce.ToString() + "\r\n");
            //}
            //else
            //{
            //    richTextBox1.Text = "Source " + sourceFile + " built into " +
            //                    cr.PathToAssembly + " with no errors.";
            //}
        }
        public static CompilerResults CompileCode(CodeDomProvider provider,
                                                  String sourceFile,
                                                  String exeFile)
        {
            // Configure a CompilerParameters that links System.dll
            // and produces the specified executable file.
            String[] referenceAssemblies = { "System.dll" };
            // CompilerParameters cp = new CompilerParameters(referenceAssemblies,
            CompilerParameters cp = new CompilerParameters();// exeFile, false);
            cp.ReferencedAssemblies.Add("system.dll");                //添加程序集 system.dll 的引用
            cp.ReferencedAssemblies.Add("system.data.dll");            //添加程序集 system.data.dll 的引用
            // Generate an executable rather than a DLL file.
            cp.GenerateExecutable = false;

            // Invoke compilation.
            CompilerResults cr = provider.CompileAssemblyFromFile(cp, sourceFile);
            // Return the results of compilation.
            return cr;
        }
        public string Code()
        { 
        return TextArea.Text;
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
           // File.WriteAllText(path, TextArea.Text);

        }
    }
    internal class HotKeyManager
    {

        public static bool Enable = true;

        public static void AddHotKey(Form form, Action function, Keys key, bool ctrl = false, bool shift = false, bool alt = false)
        {
            form.KeyPreview = true;

            form.KeyDown += delegate (object sender, KeyEventArgs e) {
                if (IsHotkey(e, key, ctrl, shift, alt))
                {
                    function();
                }
            };
        }

        public static bool IsHotkey(KeyEventArgs eventData, Keys key, bool ctrl = false, bool shift = false, bool alt = false)
        {
            return eventData.KeyCode == key && eventData.Control == ctrl && eventData.Shift == shift && eventData.Alt == alt;
        }


    }
    internal class SearchManager
    {

        public static ScintillaNET.Scintilla TextArea;
        public static TextBox SearchBox;

        public static string LastSearch = "";

        public static int LastSearchIndex;

        public static void Find(bool next, bool incremental)
        {
            bool first = LastSearch != SearchBox.Text;

            LastSearch = SearchBox.Text;
            if (LastSearch.Length > 0)
            {

                if (next)
                {

                    // SEARCH FOR THE NEXT OCCURANCE

                    // Search the document at the last search index
                    TextArea.TargetStart = LastSearchIndex - 1;
                    TextArea.TargetEnd = LastSearchIndex + (LastSearch.Length + 1);
                    TextArea.SearchFlags = SearchFlags.None;

                    // Search, and if not found..
                    if (!incremental || TextArea.SearchInTarget(LastSearch) == -1)
                    {

                        // Search the document from the caret onwards
                        TextArea.TargetStart = TextArea.CurrentPosition;
                        TextArea.TargetEnd = TextArea.TextLength;
                        TextArea.SearchFlags = SearchFlags.None;

                        // Search, and if not found..
                        if (TextArea.SearchInTarget(LastSearch) == -1)
                        {

                            // Search again from top
                            TextArea.TargetStart = 0;
                            TextArea.TargetEnd = TextArea.TextLength;

                            // Search, and if not found..
                            if (TextArea.SearchInTarget(LastSearch) == -1)
                            {

                                // clear selection and exit
                                TextArea.ClearSelections();
                                return;
                            }
                        }

                    }

                }
                else
                {

                    // SEARCH FOR THE PREVIOUS OCCURANCE

                    // Search the document from the beginning to the caret
                    TextArea.TargetStart = 0;
                    TextArea.TargetEnd = TextArea.CurrentPosition;
                    TextArea.SearchFlags = SearchFlags.None;

                    // Search, and if not found..
                    if (TextArea.SearchInTarget(LastSearch) == -1)
                    {

                        // Search again from the caret onwards
                        TextArea.TargetStart = TextArea.CurrentPosition;
                        TextArea.TargetEnd = TextArea.TextLength;

                        // Search, and if not found..
                        if (TextArea.SearchInTarget(LastSearch) == -1)
                        {

                            // clear selection and exit
                            TextArea.ClearSelections();
                            return;
                        }
                    }

                }

                // Select the occurance
                LastSearchIndex = TextArea.TargetStart;
                TextArea.SetSelection(TextArea.TargetEnd, TextArea.TargetStart);
                TextArea.ScrollCaret();

            }

            SearchBox.Focus();
        }


    }
}