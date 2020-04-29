using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace StopWatch
{
    public class Maximizer
    {
        MainWindow _mainWindow;
        int _numberOfTicks;

        int _elapsedTicks = 0;

        public Maximizer(MainWindow mainWindow, int numberOfTicks)
        {
            _mainWindow = mainWindow;
            _numberOfTicks = numberOfTicks;
        }

        public void Process()
        {
            if (_mainWindow.WindowState == WindowState.Normal) _elapsedTicks = 0;
            if (_mainWindow.WindowState == WindowState.Minimized) _elapsedTicks++;

            if (_elapsedTicks >= _numberOfTicks)
            {
                _elapsedTicks = 0;
                _mainWindow.WindowState = WindowState.Normal;
            }
        }

    }
}
