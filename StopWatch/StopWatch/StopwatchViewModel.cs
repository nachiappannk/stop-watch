using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace StopWatch
{
    public class StopwatchViewModel : INotifyPropertyChanged
    {
        public Command ExpandCommand { get; set; }
        public Command ContractCommand { get; set; }
        public Command PausePlayCommand { get; set; }
        public Command ClearCommand { get; set; }

        MyStopwatch _stopWath = new MyStopwatch();

        public event PropertyChangedEventHandler PropertyChanged;

        List<Mode> _modes = new List<Mode>() { Mode.ExpandContractButtonOnly, Mode.Time, Mode.StartStopButton, Mode.Detailed };

        bool _isPlaying = false;

        Action<Action> _executor;

        public void SetExecutor(Action<Action> executor)
        {
            _executor = executor;
        }

        public Mode _currentMode = Mode.Time;
        public Mode CurrentMode 
        {
            get => _currentMode;
            set => SetValue(ref _currentMode, value); 
        }
        
        private void SetValue<T>(ref T t, T value, [CallerMemberName] string callerName = "") where T: IComparable
        {
            if (value == null) return;
            if ((value.CompareTo(t) != 0) || (t == null))
            {
                t = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callerName));
            }
        }

        private string _time;
        public string Time 
        {
            get => _time;
            set => SetValue(ref _time, value);       
        }

        public StopwatchViewModel()
        {
            _stopWath.Start();
            ExpandCommand = new Command(Expand, CanExpand);
            ContractCommand = new Command(Contract, CanContract);
            
            UpdateTimeAsync();
        }

        private bool CanExpand()
        {
            if (CurrentMode == Mode.Detailed) return false;
            return true;
        }

        private bool CanContract()
        {
            if (CurrentMode == Mode.ExpandContractButtonOnly) return false;
            return true;
        }

        private void Expand()
        {
            CurrentMode = GetNextMode(CurrentMode);
        }

        private void Contract()
        {
            CurrentMode = GetPreviousMode(CurrentMode);
        }

        private async void UpdateTimeAsync()
        {

            while (true)
            {
                if (_executor != null) 
                {
                    var elapsedTime = _stopWath.GetElapsedTime();
                    _executor.Invoke(() =>
                    {
                        Time = elapsedTime;
                    });
                }
                await Task.Delay(100);
            }
        }

        private Mode GetNextMode(Mode mode)
        {
            var index = _modes.IndexOf(mode);
            index++;
            if (index >= _modes.Count) return mode;
            return _modes[index];
        }

        private Mode GetPreviousMode(Mode mode)
        {
            var index = _modes.IndexOf(mode);
            index--;
            if (index <= 0) return mode;
            return _modes[index];
        }
    }
}


