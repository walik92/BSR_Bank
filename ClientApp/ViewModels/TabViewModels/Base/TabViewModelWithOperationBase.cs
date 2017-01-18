using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using ClientApp.Helpers;
using ClientApp.ServiceBankReference;

namespace ClientApp.ViewModels.TabViewModels.Base
{
    public abstract class TabViewModelWithOperationBase : TabViewModelBase
    {
        private string _amount;

        private ICommand _confirmCommand;
        private Account _selectedAccount;

        public Account SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                NotifyPropertyChanged("SelectedAccount");
            }
        }

        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                NotifyPropertyChanged("Amount");
            }
        }

        public ICommand ConfrimCommand
        {
            get { return _confirmCommand ?? (_confirmCommand = new CommandHandlerAsync(Action)); }
        }

        private async void Action(object param)
        {
            Success = null;
            await ConfirmAction();
            if (!string.IsNullOrEmpty(Success))
                Clear();
        }

        private void Clear()
        {
            ClearForm();
            SelectedAccount = null;
            Amount = "";
        }

        public abstract Task ConfirmAction();
        protected abstract void ClearForm();

        public string GetSelectedAccount()
        {
            if (SelectedAccount == null)
                throw new ArgumentException("Please select account from");
            return SelectedAccount.Number;
        }

        public int GetAmount()
        {
            float result;
            if (!float.TryParse(Amount.Replace(".", ","), out result))
                throw new ArgumentException("Amount value is incorrect");

            if (!Regex.IsMatch(Amount.Replace(".", ","), @"^[0-9]*(\,[0-9]{1,2})?$"))
                throw new ArgumentException("Amount value is incorrect");

            result *= 100;
            if (result > int.MaxValue)
                throw new ArgumentException("Amount value is too high");
            return (int) result;
        }
    }
}