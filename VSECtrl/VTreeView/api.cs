using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace System
{
    public class Win32API
    {
        private const int WS_HSCROLL = 0x100000;
        private const int WS_VSCROLL = 0x200000;
        private const int GWL_STYLE = (-16);
        public const int SB_HORZ = 0;
        public const int SB_VERT = 1;
        public const int SB_CTL = 2;
        public const int SB_BOTH = 3;
        public const int SW_SHOW = 1;


        [DllImport("shell32.dll")]
        public static extern uint DragQueryFile(int hDrop, uint iFile, StringBuilder lpszFile, uint cch);
        [DllImport("shell32.dll")]
        public static extern void DragAcceptFiles(IntPtr hWnd, bool fAccept);

        [DllImport("shell32.dll")]
        public static extern void DragFinish(int hDrop);
        const int WM_DROPFILES = 0x0233;


        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize); ////// 释放内存 

        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]
        public static extern IntPtr LoadCursor(IntPtr hInstance, CursorType cursor);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        public extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        public static Cursor Hand
        {
            get
            {
                IntPtr h = LoadCursor(IntPtr.Zero, CursorType.IDC_HAND);
                return new Cursor(h);
            }
        }
        /// <summary>
        /// 判断是否出现垂直滚动条
        /// </summary>
        /// <param name="ctrl">待测控件</param>
        /// <returns>出现垂直滚动条返回true，否则为false</returns>
        public static bool IsVerticalScrollBarVisible(Control ctrl)
        {
            if (!ctrl.IsHandleCreated)
                return false;

            return (GetWindowLong(ctrl.Handle, GWL_STYLE) & WS_VSCROLL) != 0;
        }

        /// <summary>
        /// 判断是否出现水平滚动条
        /// </summary>
        /// <param name="ctrl">待测控件</param>
        /// <returns>出现水平滚动条返回true，否则为false</returns>
        public static bool IsHorizontalScrollBarVisible(Control ctrl)
        {
            if (!ctrl.IsHandleCreated)
                return false;
            return (GetWindowLong(ctrl.Handle, GWL_STYLE) & WS_HSCROLL) != 0;
        }
    }

    public enum CursorType : uint
    {
        IDC_ARROW = 32512U,
        IDC_IBEAM = 32513U,
        IDC_WAIT = 32514U,
        IDC_CROSS = 32515U,
        IDC_UPARROW = 32516U,
        IDC_SIZE = 32640U,
        IDC_ICON = 32641U,
        IDC_SIZENWSE = 32642U,
        IDC_SIZENESW = 32643U,
        IDC_SIZEWE = 32644U,
        IDC_SIZENS = 32645U,
        IDC_SIZEALL = 32646U,
        IDC_NO = 32648U,
        IDC_HAND = 32649U,
        IDC_APPSTARTING = 32650U,
        IDC_HELP = 32651U
    }
    public class NativeInterop
    {
        public const int WM_PRINTCLIENT = 0x0318;
        public const int PRF_CLIENT = 0x00000004;

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public static bool IsWinXP
        {
            get
            {
                OperatingSystem OS = Environment.OSVersion;
                return (OS.Platform == PlatformID.Win32NT) &&
                    ((OS.Version.Major > 5) || ((OS.Version.Major == 5) && (OS.Version.Minor == 1)));
            }
        }

        public static bool IsWinVista
        {
            get
            {
                OperatingSystem OS = Environment.OSVersion;
                return (OS.Platform == PlatformID.Win32NT) && (OS.Version.Major >= 6);
            }
        }
    }
}
