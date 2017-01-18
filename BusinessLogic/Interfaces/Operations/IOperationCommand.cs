using System.Threading.Tasks;
using RepozytoriumDB.IRepository;

namespace BusinessLogic.Interfaces.Operations
{
    /// <summary>
    ///     Wykonuje operację bankową (Wzorzec czynnościowy Command)
    /// </summary>
    public interface IOperationCommand
    {
        /// <summary>
        ///     Wykonaj operację bankową
        /// </summary>
        /// <param name="accountRepository">Repozytorium Account</param>
        /// <returns></returns>
        Task Execute(IAccountRepository accountRepository);
    }
}