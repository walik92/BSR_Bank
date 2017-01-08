using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using RepozytoriumDB.DTO;
using RepozytoriumDB.DTO.Operations;

namespace RepozytoriumDB
{
    public interface IBankDbContext : IDisposable
    {
        DbSet<Client> Clients { get; set; }
        DbSet<Token> Tokens { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Operation> Operations { get; set; }
        Database Database { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        DbEntityEntry Entry(object entity);
    }
}