using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace StopWatch
{
    public class RecentActivityDectector
    {
        int _numberOfMsForIdleClassification;
        public RecentActivityDectector(int numberOfMsForIdleClassification)
        {
            _numberOfMsForIdleClassification = numberOfMsForIdleClassification;
        }

        public bool IsIdle()
        {
            if (_numberOfMsForIdleClassification <= GetLastInput().TotalMilliseconds) return true;
            return false;
        }

        private static TimeSpan GetLastInput()
        {
            var plii = new LASTINPUTINFO();
            plii.cbSize = (uint)Marshal.SizeOf(plii);
            if (GetLastInputInfo(ref plii))
                return TimeSpan.FromMilliseconds(Environment.TickCount - plii.dwTime);
            else
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }
    }
}
