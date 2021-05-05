using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.Runtime.InteropServices;

namespace UmamusumeSkillOCR
{
    class WindowsUtilities
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hwnd, out WindowRect lpRect);
        public struct WindowRect
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        public static IntPtr GetActiveWindowHandler()
        {
            return GetForegroundWindow();
        }

        public static string GetWindowTitle(IntPtr windowHandler)
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);

            if (GetWindowText(windowHandler, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        public static Rectangle GetWindowArea(IntPtr windowHandler)
        {
            GetWindowRect(windowHandler, out WindowRect rect);
            return new Rectangle()
            {
                X = rect.Left,
                Y = rect.Top,
                Width = rect.Right - rect.Left,
                Height = rect.Bottom - rect.Top
            };
        }

        
    }
}
