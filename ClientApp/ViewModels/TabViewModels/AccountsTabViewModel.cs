using ClientApp.ViewModels.TabViewModels.Base;

namespace ClientApp.ViewModels.TabViewModels
{
    public class AccountsTabViewModel : TabViewModelBase
    {
        protected override bool IsOperationWithSuccessResult
        {
            get { return false; }
        }
    }
}