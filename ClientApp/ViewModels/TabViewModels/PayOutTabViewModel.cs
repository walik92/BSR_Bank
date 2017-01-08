using System.Threading.Tasks;
using ClientApp.Helpers;
using ClientApp.ViewModels.TabViewModels.Base;

namespace ClientApp.ViewModels.TabViewModels
{
    public class PayOutTabViewModel : TabViewModelWithOperationBase
    {
        protected override bool IsOperationWithSuccessResult
        {
            get { return true; }
        }

        public override async Task ConfirmAction()
        {
            await WorkerAsync(CreatePayOut);
        }

        private async Task CreatePayOut()
        {
            await ServiceBankExecutor.PayOut(GetSelectedAccount(), GetAmount());
            Success = "Success PayOut";
        }

        protected override void ClearForm()
        {
        }
    }
}