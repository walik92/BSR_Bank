using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RepozytoriumDB.DTO;
using RepozytoriumDB.IRepository;

namespace RepozytoriumDB.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IBankDbContext _bankDbContext;
        private IClientRepository _clientRepository;
        private bool _disposed;
        private IOperationRepository _operationRepository;

        public AccountRepository(IBankDbContext bankDbContext)
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

        public IOperationRepository OperationRepository
        {
            get
            {
                if (_operationRepository == null)
                    _operationRepository = new OperationRepository(_bankDbContext);
                return _operationRepository;
            }
        }


        public void Add(Account account)
        {
            _bankDbContext.Accounts.Add(account);
        }


        public async Task<long> GetLastIdAccountAsync()
        {
            var lastNumber =
                await _bankDbContext.Accounts.Select(q => q.Number).OrderByDescending(q => q).FirstOrDefaultAsync();
            return lastNumber;
        }

        public async Task<Account> GetAccountByNumberAsync(byte checksum, long number)
        {
            return
                await _bankDbContext.Accounts.Include("Client")
                    .FirstOrDefaultAsync(q => q.Number == number && q.Checksum == checksum);
        }

        public Account GetAccountByNumber(byte checksum, long number)
        {
            return
                _bankDbContext.Accounts.Include("Client")
                    .FirstOrDefault(q => q.Number == number && q.Checksum == checksum);
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

        public IDatabaseTransaction BeginTransaction()
        {
            return new EntityDatabaseTransaction(_bankDbContext);
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync(string token)
        {
            var client = await ClientRepository.GetByTokenAsync(token);
            return client.Accounts;
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