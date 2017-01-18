using System;
using System.Threading.Tasks;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces.Operations;
using RepozytoriumDB.DTO.Operations;
using RepozytoriumDB.IRepository;

namespace BusinessLogic.Business.Operations
{
    /// <summary>
    ///     Operacja Wpłata bankowa
    /// </summary>
    public class PayInOperationCommand : IOperationCommand
    {
        private readonly string _accountTo;
        private readonly int _amount;

        public PayInOperationCommand(string accountTo, int amount)
        {
            _accountTo = accountTo;
            _amount = amount;
        }

        public async Task Execute(IAccountRepository accountRepository)
        {
            var checksum = NumberAccountHelper.GetChecksum(_accountTo);
            var number = NumberAccountHelper.GetNumberAccount(_accountTo);

            var account = await accountRepository.GetAccountByNumberAndCheckSumAsync(checksum, number);

            var value = (decimal) _amount / 100;
            account.Balance += value;
            await Save(accountRepository, account);
        }

        private async Task Save(IAccountRepository accountRepository, RepozytoriumDB.DTO.Account account)
        {
            var operation = new PayInOperation();
            operation.Amount = (decimal) _amount / 100;
            operation.Balance = account.Balance;
            operation.Account = account;
            operation.Date = DateTime.Now;
            accountRepository.OperationRepository.Add(operation);
            await accountRepository.OperationRepository.SaveAsync();
        }
    }
}