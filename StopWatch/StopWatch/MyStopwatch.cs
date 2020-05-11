using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StopWatch
{
    class MyStopwatch
    {
        Stopwatch _stopWatch;

        List<TimeLog> _timeLogs = new List<TimeLog>();
        public MyStopwatch()
        {
            _stopWatch = new Stopwatch();
        }

        DateTime _startingTime;

        public void Start()
        {
            _stopWatch.Start();
            _startingTime = DateTime.Now;
        }

        public void Stop() 
        {
            var endingTime = DateTime.Now;
            var timeSpan = endingTime - _startingTime;
            _timeLogs.Add(new TimeLog() { StartTime = _startingTime.ToString("HH:mm") , Duration = ((int)timeSpan.TotalSeconds).ToString()});
            _stopWatch.Stop();
        }

        public List<TimeLog> GetTimeLog()
        {
            return _timeLogs;
        }

        public void Clear() 
        {
            _timeLogs = new List<TimeLog>();
            _stopWatch.Stop();
            _stopWatch.Reset();
        }

        public long GetElapsedTime() 
        {
            return _stopWatch.ElapsedMilliseconds;
        }
    }

    public class TimeLog
    {
        public string StartTime { get; set; }
        public string Duration { get; set; }
    }
}
