using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;

namespace CWExpert
{

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO
    {
        public uint cbSize;
        public Rectangle rcWindow;
        public Rectangle rcClient;
        public uint dwStyle;
        public uint dwExStyle;
        public uint dwWindowStatus;
        public uint cxWindowBorders;
        public uint cyWindowBorders;
        public ushort atomWindowType;
        public ushort wCreatorVersion;

        public WINDOWINFO(Boolean? filler)
            : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
        {
            cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
        }
    }

    public class MessageHelper
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowText(int hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("User32.dll")]
        private static extern int RegisterWindowMessage(string lpString);

        [DllImport("User32.dll", EntryPoint = "GetWindowInfo")]
        private static extern bool GetWindowInfo(int hwnd, ref WINDOWINFO winfo);

        [DllImport("User32.dll", EntryPoint = "GetWindowRect")]
        private static extern bool GetWindowRect(int hwnd, ref Rectangle rect);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(String lpClassName, String lpWindowName);

        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        private static extern int FindWindowEx(int hwndParent, int hwndChildAfter, String lpClassName, String lpWindowName);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hWnd, int Msg, int wParam, StringBuilder lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);

        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        private static extern int PostMessage(int hWnd, int Msg, int wParam, int lParam);

        [DllImport("User32.dll", EntryPoint = "SetForegroundWindow")]
        private static extern bool SetForegroundWindow(int hWnd);

        public const int WM_USER = 0x400;
        public const int WM_COPYDATA = 0x4A;
        public int WM_SETTEXT = 0x000c;

        public bool bringAppToFront(int hWnd)
        {
            return SetForegroundWindow(hWnd);
        }

        public int sendWindowsStringMessage(int hWnd, int wParam, string msg)
        {
            int result = 0;

            if (hWnd != 0)
            {
                result = SendMessage(hWnd, WM_SETTEXT, wParam, new StringBuilder(msg));
            }

            return result;
        }

        public int sendWindowsMessage(int hWnd, int Msg, int wParam, int lParam)
        {
            int result = 0;

            if (hWnd != 0)
            {
                result = SendMessage(hWnd, Msg, wParam, lParam);
            }

            return result;
        }

        public int getWindowId(string className, string windowName)
        {

            return FindWindow(className, windowName);

        }

        public int  getWindowIdEx(int hwndParent, int hwndChild, string className, string windowName)
        {

            return FindWindowEx(hwndParent, hwndChild, className, windowName);

        }

        public bool getWindowRect(int hWnd, Rectangle rect)
        {
            return GetWindowRect(hWnd, ref rect);
        } 
        
        public int getLabel(int handy, StringBuilder buffy, int leni)
        {
            return GetWindowText(handy, buffy, leni);
        }

        public WINDOWINFO getWindowInfo(int hwnd)
        {
            WINDOWINFO Winfo = new WINDOWINFO();
            GetWindowInfo(hwnd, ref Winfo);
            return Winfo;
        }
    }
}