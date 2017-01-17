using System.Data.Entity;
using RepozytoriumDB.DTO;
using RepozytoriumDB.DTO.Operations;

namespace RepozytoriumDB
{
    public class BankDbContext : DbContext, IBankDbContext
    {
        public BankDbContext() : base("BankDB")
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BaseOperation> Operations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().Property(e => e.Balance).HasPrecision(19, 2);
            modelBuilder.Entity<BaseOperation>().Property(e => e.Balance).HasPrecision(19, 2);
            modelBuilder.Entity<BaseOperation>().Property(e => e.Amount).HasPrecision(19, 2);
            modelBuilder.Entity<PayOutOperation>().ToTable("PayOutOperations");
            modelBuilder.Entity<PayInOperation>().ToTable("PayInOperations");
            modelBuilder.Entity<TransferSendOperation>().ToTable("TransferSendOperations");
            modelBuilder.Entity<TransferReceiveOperation>().ToTable("TransferReceiveOperations");
            modelBuilder.Entity<BankChargeOperation>().ToTable("BankChargeOperations");
        }
    }
}