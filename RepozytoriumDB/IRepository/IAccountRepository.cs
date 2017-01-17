using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepozytoriumDB.DTO;

namespace RepozytoriumDB.IRepository
{
    public interface IAccountRepository : IDisposable
    {
        IClientRepository ClientRepository { get; }
        IOperationRepository OperationRepository { get; }
        void Add(Account account);
        Task<long> GetLastIdAccountAsync();
        Task<Account> GetAccountByNumberAsync(byte checksum, long number);
        Account GetAccountByNumber(byte checksum, long number);
        Task SaveAsync();
        Task<IEnumerable<Account>> GetAccountsByTokenAsync(string token);
        Task<IEnumerable<Account>> GetAccountsAll();
        IDatabaseTransaction BeginTransaction();
    }
}