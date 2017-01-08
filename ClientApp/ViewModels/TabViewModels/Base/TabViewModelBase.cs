using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ClientApp.Helpers;
using ClientApp.ServiceBankReference;
using ClientApp.Views;

namespace ClientApp.ViewModels.TabViewModels.Base
{
    public abstract class TabViewModelBase : ModelBase
    {
        private IEnumerable<Account> _accounts;
        private bool _isSelectedTab;
        private string _success;
        protected virtual bool IsOperationWithSuccessResult { get; }

        public bool IsSelectedTab
        {
            get { return _isSelectedTab; }
            set
            {
                _isSelectedTab = value;
                if (_isSelectedTab)
                    WorkerAsync(GetAccounts);
            }
        }


        public IEnumerable<Account> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                NotifyPropertyChanged("Accounts");
            }
        }

        public string Success
        {
            get { return _success; }
            set
            {
                _success = value;
                NotifyPropertyChanged("Success");
            }
        }


        private async Task GetAccounts()
        {
            var accounts = await ServiceBankExecutor.GetAccounts();
            if (Accounts == null)
                Accounts = accounts;
            else
                foreach (var account in Accounts)
                    account.Balance = accounts.FirstOrDefault(q => q.Number == account.Number).Balance;
        }

        public async Task WorkerAsync(Func<Task> action)
        {
            Success = Error = null;
            VisibilityProgress = Visibility.Visible;
            VisibilityForm = Visibility.Collapsed;
            try
            {
                await action();
            }
            catch (FaultException<TokenFault> ex)
            {
                //todo log exception
                CloseWindow("Your session has expired, Please login again.");
            }
            catch (FaultException ex)
            {
                Error = ex.Message;
            }
            catch (ArgumentException ex)
            {
                Error = ex.Message;
            }
            catch (Exception ex)
            {
                //todo log exceptin
                Error = "Service bank is not available, please try later.";
            }
            VisibilityProgress = Visibility.Collapsed;
            if (string.IsNullOrEmpty(Success) || IsOperationWithSuccessResult)
                VisibilityForm = Visibility.Visible;
            if (!string.IsNullOrEmpty(Error) && !IsOperationWithSuccessResult)
                VisibilityForm = Visibility.Collapsed;
        }

        public async void CloseWindow(string error)
        {
            await Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action<string>(ShowLoginWindow), error);

            await Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                CloseAction);
        }

        private void ShowLoginWindow(string error)
        {
            var form = new Login(error);
            form.Show();
        }
    }
}