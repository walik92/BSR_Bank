using System;
using System.Windows.Input;

namespace ClientApp.Helpers
{
    public class CommandHandler : ICommand
    {
        protected readonly Func<bool> canExecute;

        protected readonly Action<object> execute;

        public CommandHandler(Action<object> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public CommandHandler(Action<object> execute)
            : this(execute, null)
        {
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExecute != null)
                    CommandManager.RequerySuggested += value;
            }

            remove
            {
                if (canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        public virtual bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute();
        }

        public virtual void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}