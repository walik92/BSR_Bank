using System;
using System.Threading.Tasks;
using RepozytoriumDB.DTO;

namespace RepozytoriumDB.IRepository
{
    /// <summary>
    ///     Operacje wykonywane na obiekcie Token (UnitOfWork)
    /// </summary>
    public interface ITokenRepository : IDisposable
    {
        IClientRepository ClientRepository { get; }

        /// <summary>
        ///     Pobierz asynchronicznie obiekt Token według wartości tokenu
        /// </summary>
        /// <param name="token">Wartość tokenu</param>
        /// <returns>Token</returns>
        Task<Token> GetByValueTokenAsync(string token);

        /// <summary>
        ///     Pobierz obiekt Token według wartości tokenu
        /// </summary>
        /// <param name="token">Wartość tokenu</param>
        /// <returns>Token</returns>
        Token GetByValueToken(string token);

        /// <summary>
        ///     Dodaj token
        /// </summary>
        /// <param name="token">Wartość tokenu</param>
        /// <param name="client">Klient</param>
        void Add(string token, Client client);

        /// <summary>
        ///     Zapisz wykonane operacje
        /// </summary>
        /// <returns></returns>
        Task Save();
    }
}