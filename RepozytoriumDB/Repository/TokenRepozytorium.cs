using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RepozytoriumDB.DTO;
using RepozytoriumDB.IRepository;

namespace RepozytoriumDB.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IBankDbContext _bankDbContext;
        private IClientRepository _clientRepository;
        private bool _disposed;

        public TokenRepository(IBankDbContext bankDbContext)
        {
            _bankDbContext = bankDbContext;
        }

        public IClientRepository ClientRepository
        {
            get
            {
                if (_clientRepository == null)
                    _clientRepository = new ClientRepository(_bankDbContext);
                return _clientRepository;
            }
        }

        public async Task<Token> GetByValueTokenAsync(string token)
        {
            return await _bankDbContext.Tokens.FirstOrDefaultAsync(q => q.Value == token);
        }

        public Token GetByValueToken(string token)
        {
            return _bankDbContext.Tokens.FirstOrDefault(q => q.Value == token);
        }

        public void Add(string token, Client client)
        {
            _bankDbContext.Tokens.Add(new Token {Value = token, Client = client, CreateDate = DateTime.Now});
        }

        public async Task Save()
        {
            await _bankDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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