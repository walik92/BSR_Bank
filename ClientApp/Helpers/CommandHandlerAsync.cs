using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ClientApp.Helpers
{
    public class CommandHandlerAsync : CommandHandler
    {
        public CommandHandlerAsync(Action<object> execute, Func<bool> canExecute)
            : base(execute, canExecute)
        {
        }

        public CommandHandlerAsync(Action<object> execute)
            : base(execute)
        {
        }

        public bool IsExecuting { get; private set; }

        public event EventHandler Started;

        public event EventHandler Ended;

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter) && !IsExecuting;
        }

        public override void Execute(object parameter)
        {
            try
            {
                IsExecuting = true;
                if (Started != null)
                    Started(this, EventArgs.Empty);

                var task = Task.Factory.StartNew(() => { execute(parameter); });
                task.ContinueWith(t => { OnRunWorkerCompleted(EventArgs.Empty); },
                    TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                OnRunWorkerCompleted(new RunWorkerCompletedEventArgs(null, ex, true));
            }
        }

        private void OnRunWorkerCompleted(EventArgs e)
        {
            IsExecuting = false;
            if (Ended == null) return;
            Ended(this, e);
        }
    }
}