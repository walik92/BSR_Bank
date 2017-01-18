using System.Threading.Tasks;
using BusinessLogic.Business.Operations;

namespace BusinessLogic.Interfaces.Admin
{
    /// <summary>
    ///     Obsługa operacji wykonywanych przez administratora systemu bankowego
    /// </summary>
    public interface IAdminManager
    {
        /// <summary>
        ///     Dodaj nowego klienta
        /// </summary>
        /// <param name="password">Hasło klienta</param>
        /// <returns>Identyfikator nowego klienta</returns>
        Task<int> AddClient(string password);

        /// <summary>
        ///     Dodaj nowe konto
        /// </summary>
        /// <param name="clientId">Id Klienta</param>
        /// <returns>Numer nowego konta</returns>
        Task<string> AddAccount(int clientId);

        /// <summary>
        ///     Dodaj opłaty bankowe do wszyskich kont
        /// </summary>
        /// <param name="operation">Obiekt Opłata Bankowa</param>
        /// <returns></returns>
        Task AddBankCharges(BankChargeOperationCommand operation);
    }
}