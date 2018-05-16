using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace NarcoFeeder.Helpers
{
    class Win32
    {
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public Int32 x;
            public Int32 y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CursorInfo
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public Point ptScreenPos;
        }

        public enum keyState
        {
            KEYDOWN     = 0,
            EXTENDEDKEY = 1,
            KEYUP       = 2
        };

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorInfo(out CursorInfo pci);

        [DllImport("user32.dll")]
        private static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);

        [DllImport("user32.dll")]
        private static extern bool keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool SendNotifyMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const uint WM_LBUTTONDOWN    = 513;
        private const uint WM_LBUTTONUP = 514;
        private const uint WM_LBUTTONDBLCLK = 515;

        private const uint WM_RBUTTONDOWN    = 516;
        private const uint WM_RBUTTONUP      = 517;
        private const string EXENAME = "ShooterGame"; //ShooterGame

        public static Rect GetARKRectangle()
        {
            IntPtr hWnd = ActivateApp(EXENAME);
            if (hWnd!=IntPtr.Zero)
            {
                ARKWindowPtr = hWnd;
                Rect Win32ApiRect = new Rect();
                GetWindowRect(hWnd, ref Win32ApiRect);
                ARKRect = Win32ApiRect;
                //Console.Out.WriteLine("ark window found." + Win32ApiRect.Left + " " + Win32ApiRect.Right + " " + Win32ApiRect.Top + " " + Win32ApiRect.Bottom + " ");
                //break;
            }
            return ARKRect;
        }


        static public IntPtr ActivateApp(string processName)
        {
            Process[] p = Process.GetProcessesByName(processName);

            // Activate the first application we find with this name
            if (p.Count() > 0) {
                //Console.Out.WriteLine("app found: " + processName+" T:"+p[0].MainWindowTitle);
                SetForegroundWindow(p[0].MainWindowHandle);
                return p[0].MainWindowHandle;
            }
            else
            {
                Console.Out.WriteLine("app not found: "+processName);
                return IntPtr.Zero;
            }
        }

        public static void MoveMouse(Point p)
        {
            SetCursorPos(p.x, p.y);
        }


        public static CursorInfo GetCurrentCursor()
        {
            CursorInfo myInfo = new CursorInfo();
            myInfo.cbSize = Marshal.SizeOf(myInfo);
            GetCursorInfo(out myInfo);
            return myInfo;
        }

        public static Point GetCurrentCursorPoint()
        {
            return GetCurrentCursor().ptScreenPos;
        }

        public static void SendKey(string sKeys)
        {
            if (sKeys != " ")
            {
                sKeys = "{" + sKeys + "}";  // {X} : Avoid UTF-8 errors (é, è, ...)
            }

            SendKeys.Send(sKeys);
        }

        public static void SendMouseClick(Point p, int delay)
        {
            long dWord = MakeDWord(p.x, p.y);
            if (ARKWindowPtr == IntPtr.Zero) return;
            SendNotifyMessage(ARKWindowPtr, WM_LBUTTONDOWN, (UIntPtr)1, (IntPtr)dWord);
            Thread.Sleep(delay);
            SendNotifyMessage(ARKWindowPtr, WM_LBUTTONUP, (UIntPtr)1, (IntPtr)dWord);
        }

        public static void SendMouseRClick(Point p, int delay)
        {
            long dWord = MakeDWord(p.x, p.y);

            if (ARKWindowPtr == IntPtr.Zero) return;
            SendNotifyMessage(ARKWindowPtr, WM_RBUTTONDOWN, (UIntPtr)1, (IntPtr)dWord);
            Thread.Sleep(delay);
            SendNotifyMessage(ARKWindowPtr, WM_RBUTTONUP, (UIntPtr)1, (IntPtr)dWord);
        }


        public static void SendMouseDoubleClick(Point p)
        {
            SendMouseClick(p, 30);
            Thread.Sleep(30);
            SendMouseClick(p, 30);
        }

        public static bool SendKeyboardAction(Keys key, keyState state)
        {
            return SendKeyboardAction((byte)key.GetHashCode(), state);
        }

        public static bool SendKeyboardAction(byte key, keyState state)
        {
            return keybd_event(key, 0, (uint)state, (UIntPtr)0);
        }

        private static long MakeDWord(int LoWord, int HiWord)
        {
            return (HiWord << 16) | (LoWord & 0xFFFF);
        }

        static private IntPtr ARKWindowPtr;
        static private Rect ARKRect;
    }
}
