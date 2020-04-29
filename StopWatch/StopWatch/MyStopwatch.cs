using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StopWatch
{
    class MyStopwatch
    {
        Stopwatch _stopWatch;
        public MyStopwatch()
        {
            _stopWatch = new Stopwatch();
        }

        public void Start()
        {
            _stopWatch.Start();
        }

        public void Stop() 
        {
            _stopWatch.Stop();
        }

        public void Clear() 
        {
            _stopWatch.Stop();
            _stopWatch.Reset();
        }

        public string GetElapsedTime() 
        {
            return GetElapsedTimeAsString(_stopWatch);
        }

        private string GetElapsedTimeAsString(Stopwatch stopWatch)
        {
            var elapsedTime = stopWatch.ElapsedMilliseconds;
            var milliSeconds = elapsedTime % 1000;
            elapsedTime = elapsedTime / 1000;

            var seconds = elapsedTime % 60;
            elapsedTime = elapsedTime / 60;

            var minutes = elapsedTime % 60;
            elapsedTime = elapsedTime / 60;

            var hours = elapsedTime;

            string.Format(String.Format("{0:00}", hours));

            var timeString = $"{Format(hours)}:{Format(minutes)}:{Format(seconds)} {Format(milliSeconds,"{0:000}")}";
            return timeString;
        }

        private string Format(long number, string pattern ="{0:00}") 
        {
            return string.Format(pattern, number);
        }
    }
}
