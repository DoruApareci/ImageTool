using System;
using System.Windows.Input;

namespace ImageTool.Commands
{
    public class BaseCommand : ICommand
    {
        Action<object> _Execute;
        Predicate<object> _CanExecute;

        public BaseCommand(Action<object> executeCommand, Predicate<object> canExecute)
        {
            this._Execute = executeCommand;
            this._CanExecute = canExecute;
        }

        public BaseCommand(Action<object> executeCommand)
        {
            this._Execute = executeCommand;
        }

        public bool CanExecute(object parameter)
        {
            if (_CanExecute == null)
                return true;
            else
            {
                return _CanExecute(parameter);
            }
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _Execute(parameter);
        }
    }
}
