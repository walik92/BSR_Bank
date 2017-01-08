using System;
using System.Threading.Tasks;
using RepozytoriumDB.DTO;

namespace RepozytoriumDB.IRepository
{
    public interface IClientRepository : IDisposable
    {
        Task<Client> GetByIdAsync(int id);
        Task<Client> GetByTokenAsync(string token);
        Client GetByToken(string token);
        void Add(Client client);
        Task SaveAsync();
    }
}