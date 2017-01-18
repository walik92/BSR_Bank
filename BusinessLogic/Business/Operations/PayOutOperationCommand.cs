using System;
using System.Threading.Tasks;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces.Operations;
using RepozytoriumDB.DTO.Operations;
using RepozytoriumDB.IRepository;

namespace BusinessLogic.Business.Operations
{
    /// <summary>
    ///     Operacja Wypłata bankowa
    /// </summary>
    public class PayOutOperationCommand : IOperationCommand
    {
        private readonly string _accountFrom;
        private readonly int _amount;

        public PayOutOperationCommand(string accountFrom, int amount)
        {
            _amount = amount;
            _accountFrom = accountFrom;
        }

        public async Task Execute(IAccountRepository accountRepository)
        {
            var checksum = NumberAccountHelper.GetChecksum(_accountFrom);
            var number = NumberAccountHelper.GetNumberAccount(_accountFrom);

            var account = await accountRepository.GetAccountByNumberAndCheckSumAsync(checksum, number);

            var value = (decimal) _amount / 100;
            account.Balance -= value;
            await Save(accountRepository, account);
        }

        private async Task Save(IAccountRepository accountRepository, RepozytoriumDB.DTO.Account account)
        {
            var operation = new PayOutOperation();
            operation.Amount = -(decimal) _amount / 100;
            operation.Balance = account.Balance;
            operation.Account = account;
            operation.Date = DateTime.Now;
            accountRepository.OperationRepository.Add(operation);
            await accountRepository.OperationRepository.SaveAsync();
        }
    }
}