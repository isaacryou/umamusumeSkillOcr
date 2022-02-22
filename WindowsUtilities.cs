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

        public static Bitmap GetActiveGameBitmap(int gameWindowX, int gameWindowY, Config programConfig)
        {
            IntPtr handler = GetActiveWindowHandler();

            Rectangle rect = GetWindowArea(handler);

            var targetTitle = GetWindowTitle(handler)?.Trim();

            Bitmap bitmap;

            if (gameWindowX != -1 && gameWindowY != -1)
            {
                rect.X = gameWindowX + programConfig.screenX;

                rect.Width = programConfig.screenWidth;

                rect.Y = gameWindowY + programConfig.screenY;

                rect.Height = programConfig.screenHeight;

                bitmap = new Bitmap(rect.Width, rect.Height);

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    if (g != null)
                    {
                        g.CopyFromScreen(new System.Drawing.Point(rect.Left, rect.Top), System.Drawing.Point.Empty, rect.Size);
                    }
                }

                return bitmap;
            }

            return null;
        }

        public static string GetNumberOnlyString(string originalText)
        {
            return new string(originalText.Where(char.IsDigit).ToArray());
        }
    }
}
