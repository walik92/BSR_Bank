using System;
using System.Threading.Tasks;
using RepozytoriumDB.DTO;

namespace RepozytoriumDB.IRepository
{
    public interface ITokenRepository : IDisposable
    {
        IClientRepository ClientRepository { get; }
        Task<Token> GetByTokenAsync(string token);
        Token GetByToken(string token);
        void Add(string token, Client client);
        Task Save();
    }
}