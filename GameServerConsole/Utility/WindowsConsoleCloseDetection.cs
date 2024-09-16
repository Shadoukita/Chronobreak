using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Chronobreak.GameServerConsole.Utility
{
    public class WindowsConsoleCloseDetection
    {
        static List<ConsoleEventDelegate> handlerList = new();
        public delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);
        public static void SetCloseHandler(ConsoleEventDelegate handler)
        {
            handlerList.Add(handler);
            SetConsoleCtrlHandler(handler, true);
        }
    }
}
