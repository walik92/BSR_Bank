using System.Threading.Tasks;
using BusinessLogic.Business.Operations;

namespace BusinessLogic.Interfaces.Admin
{
    public interface IAdminManager
    {
        Task<int> AddClient(string password);
        Task<string> AddAccount(int clientId);
        Task AddBankCharges(BankChargeOperationCommand operation);
    }
}