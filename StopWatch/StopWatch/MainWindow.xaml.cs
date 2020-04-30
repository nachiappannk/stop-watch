using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StopWatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyStopwatch stopWatch = new MyStopwatch();
        Maximizer _maximizer;
        RecentActivityDectector _recentActivityDectector;
        public MainWindow()
        {
            _recentActivityDectector = new RecentActivityDectector(1000 * 60);
            InitializeComponent();
            this.Topmost = true;
            this.Left = 2;
            this.Top = SystemParameters.PrimaryScreenHeight - this.Height - 40;
            _maximizer = new Maximizer(this, 100);
            UpdateTimeAsync();

            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }


        void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    stopWatch.Stop();
                    break;
                case SessionSwitchReason.SessionUnlock:
                    stopWatch.Start();
                    break;
            }
        }


        private void Log(string s) 
        {
            Debug.WriteLine(s);
        }

        private async void UpdateTimeAsync()
        {
            while (true) {

                var elapsedTime = stopWatch.GetElapsedTime();
                var isIdle = _recentActivityDectector.IsIdle();
                Dispatcher.Invoke(() =>
                {
                    if (isIdle) stopWatch.Stop();
                    UpdateTime(elapsedTime);
                    _maximizer.Process();
                });
                await Task.Delay(100);
            }
        }

        private void UpdateTime(string timeToDisplay) 
        {
            time.Text = timeToDisplay;
        }

        private void OnClear(object sender, RoutedEventArgs e)
        {
            stopWatch.Clear();
        }

        private void OnStart(object sender, RoutedEventArgs e)
        {
            stopWatch.Start();
        }
        private void OnStop(object sender, RoutedEventArgs e)
        {
            stopWatch.Stop();
        }
    }
}


