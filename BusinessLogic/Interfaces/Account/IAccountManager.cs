using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Interfaces.Operations;
using BusinessLogic.Model;

namespace BusinessLogic.Interfaces.Account
{
    /// <summary>
    ///     Obsługa operacji wykonywanych na obiekcie Konto
    /// </summary>
    public interface IAccountManager
    {
        /// <summary>
        ///     Pobierz konta uwierzytelnionego użytkownika
        /// </summary>
        /// <param name="token">Token uwierzytelnionego użytkownika</param>
        /// <returns>Lista kont użytkownika</returns>
        Task<IEnumerable<AccountModel>> GetAccounts(string token);

        /// <summary>
        ///     Pobierz historię operacji konta
        /// </summary>
        /// <param name="token">Token uwierzytelnionego użytkownika</param>
        /// <param name="account">Numer konta</param>
        /// <param name="currentPage">Numer strony</param>
        /// <param name="sizePage">Rozmiar strony</param>
        /// <returns></returns>
        Task<HistoryOfAccountModel> GetHistoryOfAccount(string token, string account, int currentPage, int sizePage);

        /// <summary>
        ///     Wykonaj operację
        /// </summary>
        /// <param name="operation">Obiekt typu IOperationCommand</param>
        /// <returns></returns>
        Task ExecuteOperation(IOperationCommand operation);
    }
}