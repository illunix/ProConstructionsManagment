using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Commands
{
    public class AsyncRelayCommand<T> : ICommand
    {
        private readonly Func<T, Task> _action;

        private bool _canExecute = true;

        public AsyncRelayCommand(Func<T, Task> task)
        {
            _action = task;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public async void Execute(object parameter)
        {
            var val = parameter;

            if (parameter is IConvertible) val = Convert.ChangeType(parameter, typeof(T), null);

            try
            {
                if (CanExecute(null) == false)
                    return;

                SetCanExecute(false);

                await _action((T) val);
            }
            finally
            {
                SetCanExecute(true);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private void SetCanExecute(bool canExecute)
        {
            _canExecute = canExecute;
            RaiseCanExecuteChanged();
        }

        private static void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}