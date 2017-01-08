using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RepozytoriumDB.DTO.Operations;
using RepozytoriumDB.IRepository;

namespace RepozytoriumDB.Repository
{
    public class OperationRepository : IOperationRepository
    {
        private readonly IBankDbContext _bankDbContext;
        private bool disposed;

        public OperationRepository(IBankDbContext bankDbContext)
        {
            _bankDbContext = bankDbContext;
        }

        public async Task<IEnumerable<Operation>> GetOperationsAsync(byte checksum, long number, int currentPage,
            int sizePage)
        {
            return
                await _bankDbContext.Operations.Where(q => q.Account.Checksum == checksum && q.Account.Number == number)
                    .OrderByDescending(q => q.Date)
                    .Skip(currentPage * sizePage)
                    .Take(sizePage)
                    .ToListAsync();
        }

        public void Add(Operation operation)
        {
            _bankDbContext.Operations.Add(operation);
        }

        public async Task<int> GetCountOfAllPagesAsync(byte checksum, long number, int sizePage)
        {
            var count =
                await _bankDbContext.Operations.Where(q => q.Account.Checksum == checksum && q.Account.Number == number)
                    .CountAsync();
            return (int) Math.Ceiling((double) count / sizePage);
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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    _bankDbContext.Dispose();
            disposed = true;
        }
    }
}