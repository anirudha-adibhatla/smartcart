using System;
using System.Windows.Input;

namespace Makeathon.Common.Commands
{
    class ButtonCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action commandTask;
        private Func<bool> canCommandExcecute;

        public ButtonCommand(Action _commandTask, Func<bool> _canCommandExecute)
        {
            commandTask = _commandTask;
            canCommandExcecute = _canCommandExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canCommandExcecute();
        }

        public void Execute(object parameter)
        {
            commandTask();
        }
    }
}
