using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace VControls.Helper
{
    internal class VNativeMethods
    {
        /// <summary>
        /// Enum ComboBoxButtonState
        /// </summary>
        public enum ComboBoxButtonState
        {
            /// <summary>
            /// The state system none
            /// </summary>
            STATE_SYSTEM_NONE,
            /// <summary>
            /// The state system invisible
            /// </summary>
            STATE_SYSTEM_INVISIBLE = 32768,
            /// <summary>
            /// The state system pressed
            /// </summary>
            STATE_SYSTEM_PRESSED = 8
        }

        /// <summary>
        /// Struct RECT
        /// </summary>
        public struct RECT
        {
            /// <summary>
            /// The left
            /// </summary>
            public int Left;

            /// <summary>
            /// The top
            /// </summary>
            public int Top;

            /// <summary>
            /// The right
            /// </summary>
            public int Right;

            /// <summary>
            /// The bottom
            /// </summary>
            public int Bottom;

            /// <summary>
            /// Gets the rect.
            /// </summary>
            /// <value>The rect.</value>
            public Rectangle Rect
            {
                get
                {
                    return new Rectangle(this.Left, this.Top, this.Right - this.Left, this.Bottom - this.Top);
                }
            }

            /// <summary>
            /// Gets the size.
            /// </summary>
            /// <value>The size.</value>
            public Size Size
            {
                get
                {
                    return new Size(this.Right - this.Left, this.Bottom - this.Top);
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="RECT" /> struct.
            /// </summary>
            /// <param name="left">The left.</param>
            /// <param name="top">The top.</param>
            /// <param name="right">The right.</param>
            /// <param name="bottom">The bottom.</param>
            public RECT(int left, int top, int right, int bottom)
            {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="RECT" /> struct.
            /// </summary>
            /// <param name="rect">The rect.</param>
            public RECT(Rectangle rect)
            {
                this.Left = rect.Left;
                this.Top = rect.Top;
                this.Right = rect.Right;
                this.Bottom = rect.Bottom;
            }

            /// <summary>
            /// Froms the xywh.
            /// </summary>
            /// <param name="x">The x.</param>
            /// <param name="y">The y.</param>
            /// <param name="width">The width.</param>
            /// <param name="height">The height.</param>
            /// <returns>NativeMethods.RECT.</returns>
            public static VNativeMethods.RECT FromXYWH(int x, int y, int width, int height)
            {
                return new VNativeMethods.RECT(x, y, x + width, y + height);
            }

            /// <summary>
            /// Froms the rectangle.
            /// </summary>
            /// <param name="rect">The rect.</param>
            /// <returns>NativeMethods.RECT.</returns>
            public static VNativeMethods.RECT FromRectangle(Rectangle rect)
            {
                return new VNativeMethods.RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
            }
        }

        /// <summary>
        /// Struct PAINTSTRUCT
        /// </summary>
        public struct PAINTSTRUCT
        {
            /// <summary>
            /// The HDC
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.hdc”赋值，字段将一直保持其默认值
            public IntPtr hdc;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.hdc”赋值，字段将一直保持其默认值

            /// <summary>
            /// The f erase
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.fErase”赋值，字段将一直保持其默认值 0
            public int fErase;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.fErase”赋值，字段将一直保持其默认值 0

            /// <summary>
            /// The rc paint
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.rcPaint”赋值，字段将一直保持其默认值
            public VNativeMethods.RECT rcPaint;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.rcPaint”赋值，字段将一直保持其默认值

            /// <summary>
            /// The f restore
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.fRestore”赋值，字段将一直保持其默认值 0
            public int fRestore;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.fRestore”赋值，字段将一直保持其默认值 0

            /// <summary>
            /// The f inc update
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.fIncUpdate”赋值，字段将一直保持其默认值 0
            public int fIncUpdate;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.fIncUpdate”赋值，字段将一直保持其默认值 0

            /// <summary>
            /// The reserved1
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.Reserved1”赋值，字段将一直保持其默认值 0
            public int Reserved1;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.Reserved1”赋值，字段将一直保持其默认值 0

            /// <summary>
            /// The reserved2
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.Reserved2”赋值，字段将一直保持其默认值 0
            public int Reserved2;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.Reserved2”赋值，字段将一直保持其默认值 0

            /// <summary>
            /// The reserved3
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.Reserved3”赋值，字段将一直保持其默认值 0
            public int Reserved3;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.Reserved3”赋值，字段将一直保持其默认值 0

            /// <summary>
            /// The reserved4
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.Reserved4”赋值，字段将一直保持其默认值 0
            public int Reserved4;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.Reserved4”赋值，字段将一直保持其默认值 0

            /// <summary>
            /// The reserved5
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.Reserved5”赋值，字段将一直保持其默认值 0
            public int Reserved5;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.Reserved5”赋值，字段将一直保持其默认值 0

            /// <summary>
            /// The reserved6
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.Reserved6”赋值，字段将一直保持其默认值 0
            public int Reserved6;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.Reserved6”赋值，字段将一直保持其默认值 0

            /// <summary>
            /// The reserved7
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.Reserved7”赋值，字段将一直保持其默认值 0
            public int Reserved7;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.Reserved7”赋值，字段将一直保持其默认值 0

            /// <summary>
            /// The reserved8
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.Reserved8”赋值，字段将一直保持其默认值 0
            public int Reserved8;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.PAINTSTRUCT.Reserved8”赋值，字段将一直保持其默认值 0
        }

        /// <summary>
        /// Struct ComboBoxInfo
        /// </summary>
        public struct ComboBoxInfo
        {
            /// <summary>
            /// The cb size
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.ComboBoxInfo.cbSize”赋值，字段将一直保持其默认值 0
            public int cbSize;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.ComboBoxInfo.cbSize”赋值，字段将一直保持其默认值 0

            /// <summary>
            /// The rc item
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.ComboBoxInfo.rcItem”赋值，字段将一直保持其默认值
            public VNativeMethods.RECT rcItem;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.ComboBoxInfo.rcItem”赋值，字段将一直保持其默认值

            /// <summary>
            /// The rc button
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.ComboBoxInfo.rcButton”赋值，字段将一直保持其默认值
            public VNativeMethods.RECT rcButton;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.ComboBoxInfo.rcButton”赋值，字段将一直保持其默认值

            /// <summary>
            /// The state button
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.ComboBoxInfo.stateButton”赋值，字段将一直保持其默认值
            public VNativeMethods.ComboBoxButtonState stateButton;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.ComboBoxInfo.stateButton”赋值，字段将一直保持其默认值

            /// <summary>
            /// The HWND combo
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.ComboBoxInfo.hwndCombo”赋值，字段将一直保持其默认值
            public IntPtr hwndCombo;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.ComboBoxInfo.hwndCombo”赋值，字段将一直保持其默认值

            /// <summary>
            /// The HWND edit
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.ComboBoxInfo.hwndEdit”赋值，字段将一直保持其默认值
            public IntPtr hwndEdit;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.ComboBoxInfo.hwndEdit”赋值，字段将一直保持其默认值

            /// <summary>
            /// The HWND list
            /// </summary>
#pragma warning disable CS0649 // 从未对字段“VNativeMethods.ComboBoxInfo.hwndList”赋值，字段将一直保持其默认值
            public IntPtr hwndList;
#pragma warning restore CS0649 // 从未对字段“VNativeMethods.ComboBoxInfo.hwndList”赋值，字段将一直保持其默认值
        }

        /// <summary>
        /// The wm paint
        /// </summary>
        public const int WM_PAINT = 15;

        /// <summary>
        /// The wm setredraw
        /// </summary>
        public const int WM_SETREDRAW = 11;

        /// <summary>
        /// The false
        /// </summary>
        public static readonly IntPtr FALSE = IntPtr.Zero;

        /// <summary>
        /// The true
        /// </summary>
        public static readonly IntPtr TRUE = new IntPtr(1);

        /// <summary>
        /// Gets the ComboBox information.
        /// </summary>
        /// <param name="hwndCombo">The HWND combo.</param>
        /// <param name="info">The information.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("user32.dll")]
        public static extern bool GetComboBoxInfo(IntPtr hwndCombo, ref VNativeMethods.ComboBoxInfo info);

        /// <summary>
        /// Gets the window rect.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="lpRect">The lp rect.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, ref VNativeMethods.RECT lpRect);

        /// <summary>
        /// Begins the paint.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="ps">The ps.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr BeginPaint(IntPtr hWnd, ref VNativeMethods.PAINTSTRUCT ps);

        /// <summary>
        /// Ends the paint.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="ps">The ps.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("user32.dll")]
        public static extern bool EndPaint(IntPtr hWnd, ref VNativeMethods.PAINTSTRUCT ps);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        [DllImport("user32.dll")]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
    }
}
