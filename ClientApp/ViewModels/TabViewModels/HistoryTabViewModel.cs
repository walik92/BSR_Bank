using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ClientApp.Helpers;
using ClientApp.ServiceBankReference;
using ClientApp.ViewModels.TabViewModels.Base;

namespace ClientApp.ViewModels.TabViewModels
{
    public class HistoryTabViewModel : TabViewModelBase
    {
        private ICommand _changePageCommand;
        private int _countAllOfPages;
        private int _currentPage;
        private HistoryOfAccount _historyOfAccount;

        private bool _isEnabledNextButton;
        private bool _isEnabledPreviousButton;
        private Account _selectedAccount;

        public bool IsEnabledPreviousButton
        {
            get { return _isEnabledPreviousButton; }
            set
            {
                _isEnabledPreviousButton = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsEnabledNextButton
        {
            get { return _isEnabledNextButton; }
            set
            {
                _isEnabledNextButton = value;
                NotifyPropertyChanged();
            }
        }

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                NotifyPropertyChanged();
                if (_currentPage > 1)
                    IsEnabledPreviousButton = true;
                else
                    IsEnabledPreviousButton = false;
            }
        }

        public int SizePage { get; set; } = 10;

        public int CountAllOfPages
        {
            get { return _countAllOfPages; }
            set
            {
                _countAllOfPages = value;
                NotifyPropertyChanged();
                if (CurrentPage >= CountAllOfPages)
                    IsEnabledNextButton = false;
                else
                    IsEnabledNextButton = true;
            }
        }

        protected override bool IsOperationWithSuccessResult
        {
            get { return false; }
        }

        public Visibility VisibilityTable { get; set; }

        public Account SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                NotifyPropertyChanged();
                CurrentPage = 1;
                WorkerAsync(GetHistoryOfAccount);
            }
        }

        public HistoryOfAccount HistoryOfAccount
        {
            get { return _historyOfAccount; }
            set
            {
                _historyOfAccount = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand ChangePageCommand
        {
            get { return _changePageCommand ?? (_changePageCommand = new CommandHandlerAsync(ChangePageAction)); }
        }

        private async Task GetHistoryOfAccount()
        {
            if (SelectedAccount == null)
                throw new ArgumentException("Please select account");

            HistoryOfAccount = await ServiceBankExecutor.GetHistoryOfAccount(SelectedAccount.Number, CurrentPage - 1,
                SizePage);
            CountAllOfPages = HistoryOfAccount.CountOfAllPages;
        }

        private async void ChangePageAction(object param)
        {
            if (int.Parse(param.ToString()) == -1)
                CurrentPage--;
            else
                CurrentPage++;

            await WorkerAsync(GetHistoryOfAccount);
        }
    }
}