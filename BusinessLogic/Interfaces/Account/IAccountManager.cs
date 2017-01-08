using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Interfaces.Operations;
using BusinessLogic.Model;

namespace BusinessLogic.Interfaces.Account
{
    public interface IAccountManager
    {
        Task<IEnumerable<AccountModel>> GetAccounts(string token);
        Task<HistoryOfAccountModel> GetHistoryOfAccount(string token, string account, int currentPage, int sizePage);
        Task ExecuteOperation(IOperationCommand operation);
    }
}