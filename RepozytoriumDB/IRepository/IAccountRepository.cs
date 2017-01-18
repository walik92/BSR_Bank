using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepozytoriumDB.DTO;

namespace RepozytoriumDB.IRepository
{
    /// <summary>
    ///     Operacje wykonywane na obiekcie Account (UnitOfWork)
    /// </summary>
    public interface IAccountRepository : IDisposable
    {
        IClientRepository ClientRepository { get; }
        IOperationRepository OperationRepository { get; }

        /// <summary>
        ///     Dodaj nowe konto
        /// </summary>
        /// <param name="account">Konto bankowe</param>
        void Add(Account account);

        /// <summary>
        ///     Pobierz number konta ostatnio dodanego do bazy danych
        /// </summary>
        /// <returns>Numer ostatnio dodanego konta w DB</returns>
        Task<long> GetLastIdAccountAsync();

        /// <summary>
        ///     Pobierz asynchronicznie konto według sumy kontrolnej i numeru
        /// </summary>
        /// <param name="checksum">Suma kontrolna konta</param>
        /// <param name="number">Numer konta</param>
        /// <returns>Konto</returns>
        Task<Account> GetAccountByNumberAndCheckSumAsync(byte checksum, long number);

        /// <summary>
        ///     Pobierz konto według sumy kontrolnej i numeru
        /// </summary>
        /// <param name="checksum">Suma kontrolna konta</param>
        /// <param name="number">Numer konta</param>
        /// <returns>Konto</returns>
        Account GetAccountByNumberAndCheckSum(byte checksum, long number);

        /// <summary>
        ///     Zapisz asynchronicznie wykonane operacje
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();

        /// <summary>
        ///     Pobierz wszystkie konta użytkownika podając token
        /// </summary>
        /// <param name="token">Token zalogowanego użytkownika</param>
        /// <returns></returns>
        Task<IEnumerable<Account>> GetAccountsByTokenAsync(string token);

        /// <summary>
        ///     Pobierz wszystkie konta w banku
        /// </summary>
        /// <returns>Lista wszystkich kont w banku</returns>
        Task<IEnumerable<Account>> GetAccountsAll();

        /// <summary>
        ///     Rozpocznij transakcję
        /// </summary>
        /// <returns>Transakcja</returns>
        IDatabaseTransaction BeginTransaction();
    }
}