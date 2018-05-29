using System;
using System.Runtime.InteropServices;

namespace IGExtensions.Framework
{
    ///<summary>
    ///</summary>
    public class NativeAssembly
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        // This static method is required because Win32 does not support
        // GetWindowLongPtr directly
        public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
        {
            return IntPtr.Size == 8 ? GetWindowLongPtr64(hWnd, nIndex) : GetWindowLongPtr32(hWnd, nIndex);
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            return IntPtr.Size == 8 ? SetWindowLongPtr64(hWnd, nIndex, dwNewLong) : new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern bool DrawMenuBar(IntPtr hWnd);

        public const int GWL_WNDPROC = (-4);
        public const int GWL_HINSTANCE = (-6);
        public const int GWL_HWNDPARENT = (-8);
        public const int GWL_STYLE = (-16);
        public const int GWL_EXSTYLE = (-20);
        public const int GWL_USERDATA = (-21);
        public const int GWL_ID = (-12);

        public const UInt32 SWP_FRAMECHANGED = 0x0020;
        public const UInt32 SWP_NOSIZE = 0x0001;
        public const UInt32 SWP_NOMOVE = 0x0002;
        public const int SWP_NOZORDER = 0x0004;
        public const int WS_SYSMENU = 0x00080000;
        public const int WS_EX_DLGMODALFRAME = 0x0001;
        public const int WS_EX_CONTEXTHELP = 0x0400;

        public const uint MF_BYCOMMAND = 0x00000000;
        public const uint MF_ENABLED = 0x00000000;
        public const uint MF_GRAYED = 0x00000001;
        public const uint MF_DISABLED = 0x00000002;
        public const uint MF_BYPOSITION = 0x00000400;

        public const int SC_SIZE = 0xF000;
        public const int SC_MOVE = 0xF010;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_NEXTWINDOW = 0xF040;
        public const int SC_PREVWINDOW = 0xF050;
        public const int SC_CLOSE = 0xF060;
        public const int SC_VSCROLL = 0xF070;
        public const int SC_HSCROLL = 0xF080;
        public const int SC_MOUSEMENU = 0xF090;
        public const int SC_KEYMENU = 0xF100;
        public const int SC_ARRANGE = 0xF110;
        public const int SC_RESTORE = 0xF120;
        public const int SC_TASKLIST = 0xF130;
        public const int SC_SCREENSAVE = 0xF140;
        public const int SC_HOTKEY = 0xF150;
        public const int SC_DEFAULT = 0xF160;
        public const int SC_MONITORPOWER = 0xF170;
        public const int SC_CONTEXTHELP = 0xF180;
        public const int SC_SEPARATOR = 0xF00F;

        public const int WM_SHOWWINDOW = 0x00000018;
        public const int WM_CLOSE = 0x10;
        public const uint WM_SETICON = 0x0080;
    }
}
