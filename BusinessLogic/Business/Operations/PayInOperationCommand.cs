﻿using System;
using System.Threading.Tasks;
using BusinessLogic.Business.Account;
using BusinessLogic.Interfaces.Operations;
using RepozytoriumDB.DTO.Operations;
using RepozytoriumDB.IRepository;

namespace BusinessLogic.Business.Operations
{
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
            var checksum = NumberAccountManager.GetChecksum(_accountTo);
            var number = NumberAccountManager.GetNumberAccount(_accountTo);

            var account = await accountRepository.GetAccountByNumberAsync(checksum, number);

            var value = (decimal) _amount / 100;
            account.Balance += value;
            await Save(accountRepository, account);
        }

        public async Task Save(IAccountRepository accountRepository, RepozytoriumDB.DTO.Account account)
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