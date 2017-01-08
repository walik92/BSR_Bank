using System.Threading.Tasks;
using ClientApp.Helpers;
using ClientApp.ViewModels.TabViewModels.Base;

namespace ClientApp.ViewModels.TabViewModels
{
    public class TransferTabViewModel : TabViewModelWithOperationBase
    {
        private string _accountTo;
        private string _title;

        protected override bool IsOperationWithSuccessResult
        {
            get { return true; }
        }

        public string AccountTo
        {
            get { return _accountTo; }
            set
            {
                _accountTo = value;
                NotifyPropertyChanged("AccountTo");
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        public override async Task ConfirmAction()
        {
            await WorkerAsync(CreateTransfer);
        }

        private async Task CreateTransfer()
        {
            await ServiceBankExecutor.Transfer(GetSelectedAccount(), AccountTo, GetAmount(), Title);
            Success = "Success Transfer";
        }

        protected override void ClearForm()
        {
            Title = "";
            AccountTo = "";
        }
    }
}