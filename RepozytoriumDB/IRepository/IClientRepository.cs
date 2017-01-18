using System;
using System.Threading.Tasks;
using RepozytoriumDB.DTO;

namespace RepozytoriumDB.IRepository
{
    /// <summary>
    ///     Operacje wykonywane na obiekcie Client
    /// </summary>
    public interface IClientRepository : IDisposable
    {
        /// <summary>
        ///     Pobierz asynchronicznie klienta według identyfikatora
        /// </summary>
        /// <param name="id">Identyfikator klienta</param>
        /// <returns>Client</returns>
        Task<Client> GetByIdAsync(int id);

        /// <summary>
        ///     Pobierz asynchronicznie klienta według tokenu
        /// </summary>
        /// <param name="token">Token zalogowanego klienta</param>
        /// <returns>Client</returns>
        Task<Client> GetByTokenAsync(string token);

        /// <summary>
        ///     Pobierz klienta według tokenu
        /// </summary>
        /// <param name="token">Token zalogowanego klienta</param>
        /// <returns>Client</returns>
        Client GetByToken(string token);

        /// <summary>
        ///     Dodaj Klienta
        /// </summary>
        /// <param name="client">Client</param>
        void Add(Client client);

        /// <summary>
        ///     Zapisz asynchronicznie wykonane operacje
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}