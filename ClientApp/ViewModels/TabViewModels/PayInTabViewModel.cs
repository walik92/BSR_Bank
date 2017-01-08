using System.Threading.Tasks;
using ClientApp.Helpers;
using ClientApp.ViewModels.TabViewModels.Base;

namespace ClientApp.ViewModels.TabViewModels
{
    public class PayInTabViewModel : TabViewModelWithOperationBase
    {
        protected override bool IsOperationWithSuccessResult
        {
            get { return true; }
        }

        public override async Task ConfirmAction()
        {
            await WorkerAsync(CreatePayIn);
        }

        private async Task CreatePayIn()
        {
            await ServiceBankExecutor.PayIn(GetSelectedAccount(), GetAmount());
            Success = "Success PayIn";
        }

        protected override void ClearForm()
        {
        }
    }
}