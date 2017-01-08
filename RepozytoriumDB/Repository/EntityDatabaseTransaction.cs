using System.Data.Entity;
using RepozytoriumDB.IRepository;

namespace RepozytoriumDB.Repository
{
    public class EntityDatabaseTransaction : IDatabaseTransaction
    {
        private readonly DbContextTransaction _transaction;

        public EntityDatabaseTransaction(IBankDbContext context)
        {
            _transaction = context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}