using System.Threading.Tasks;
using RepozytoriumDB.IRepository;

namespace BusinessLogic.Interfaces.Operations
{
    public interface IOperationCommand
    {
        Task Execute(IAccountRepository accountRepository);
    }
}