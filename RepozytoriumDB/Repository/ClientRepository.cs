using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RepozytoriumDB.DTO;
using RepozytoriumDB.IRepository;

namespace RepozytoriumDB.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IBankDbContext _bankDbContext;
        private bool _disposed;

        public ClientRepository(IBankDbContext bankDbContext)
        {
            _bankDbContext = bankDbContext;
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _bankDbContext.Clients.SingleOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Client> GetByTokenAsync(string token)
        {
            var tokenDb = await _bankDbContext.Tokens.FirstAsync(q => q.Value == token);
            return tokenDb.Client;
        }

        public Client GetByToken(string token)
        {
            var tokenDb = _bankDbContext.Tokens.First(q => q.Value == token);
            return tokenDb.Client;
        }

        public async Task SaveAsync()
        {
            await _bankDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Add(Client client)
        {
            _bankDbContext.Clients.Add(client);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _bankDbContext.Dispose();
            _disposed = true;
        }
    }
}