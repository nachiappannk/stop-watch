using System;
using System.Windows.Input;

namespace StopWatch
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        Action _executeAction;
        Func<bool> _canExecuteFunc;

        public Command(Action executeAction, Func<bool> canExecuteFunc)
        {
            _executeAction = executeAction;
            _canExecuteFunc = canExecuteFunc;
        }

        public Command(Action executeAction)
        {
            _executeAction = executeAction;
            _canExecuteFunc = () => true;
        }

        public void ComputeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
       
        public bool CanExecute(object parameter)
        {
            return _canExecuteFunc.Invoke();
        }

        public void Execute(object parameter)
        {
            _executeAction.Invoke();
        }
    }
}


