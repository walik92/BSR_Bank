using System;
using System.Threading.Tasks;
using BusinessLogic.Business.Operations;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces.Admin;
using RepozytoriumDB.DTO;
using RepozytoriumDB.IRepository;

namespace BusinessLogic.Business.Admin
{
    /// <summary>
    ///     Operacje wykonywane przez administratora systemu
    ///     - Dodaj klienta
    ///     - Dodaj konto
    ///     - Dodaj opłaty bankowe
    /// </summary>
    public class AdminManager : IAdminManager
    {
        private readonly IAccountRepository _accountRepository;
        private readonly int _lengthClientId = 8;

        public AdminManager(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<int> AddClient(string password)
        {
            var client = new Client();
            var id = CreateId();
            client.Id = id;
            client.Password = HashHelper.GetHash(password);
            _accountRepository.ClientRepository.Add(client);
            await _accountRepository.SaveAsync();
            return id;
        }

        public async Task<string> AddAccount(int clientId)
        {
            var account = new RepozytoriumDB.DTO.Account();
            var idNextAccount = await GetNextIdAccount();
            account.Number = idNextAccount;
            account.Checksum = NumberAccountHelper.CalculateChecksum(idNextAccount);
            account.Client = await _accountRepository.ClientRepository.GetByIdAsync(clientId);
            _accountRepository.Add(account);
            await _accountRepository.SaveAsync();
            return NumberAccountHelper.GetFullNumberAccount(account.Checksum, account.Number);
        }

        public async Task AddBankCharges(BankChargeOperationCommand operation)
        {
            using (var transaction = _accountRepository.BeginTransaction())
            {
                try
                {
                    await operation.Execute(_accountRepository);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        private int CreateId()
        {
            var rnd = new Random();
            return rnd.Next((int) (1 * Math.Pow(10, _lengthClientId - 1)), (int) (9 * Math.Pow(10, _lengthClientId - 1)));
        }

        private async Task<long> GetNextIdAccount()
        {
            var lastIdAccount = await _accountRepository.GetLastIdAccountAsync();
            return ++lastIdAccount;
        }
    }
}