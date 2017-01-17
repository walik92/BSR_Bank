using System;
using System.Threading.Tasks;
using BusinessLogic.Interfaces.Operations;
using RepozytoriumDB.DTO.Operations;
using RepozytoriumDB.IRepository;

namespace BusinessLogic.Business.Operations
{
    public class BankChargeOperationCommand : IOperationCommand
    {
        private readonly int _amount;

        public BankChargeOperationCommand(int amount)
        {
            _amount = amount;
        }

        public async Task Execute(IAccountRepository accountRepository)
        {
            var accounts = await accountRepository.GetAccountsAll();
            foreach (var account in accounts)
            {
                var value = (decimal) _amount / 100;
                account.Balance -= value;
                Add(accountRepository, account);
            }
            await accountRepository.OperationRepository.SaveAsync();
        }

        private void Add(IAccountRepository accountRepository, RepozytoriumDB.DTO.Account account)
        {
            var operation = new BankChargeOperation();
            operation.Amount = -(decimal) _amount / 100;
            operation.Balance = account.Balance;
            operation.Account = account;
            operation.Date = DateTime.Now;
            accountRepository.OperationRepository.Add(operation);
        }
    }
}