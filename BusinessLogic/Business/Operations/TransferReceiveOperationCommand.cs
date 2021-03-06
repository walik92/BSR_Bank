﻿using System;
using System.Threading.Tasks;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces.Operations;
using BusinessLogic.Model.Interfaces;
using RepozytoriumDB.DTO.Operations;
using RepozytoriumDB.IRepository;

namespace BusinessLogic.Business.Operations
{
    /// <summary>
    ///     Operacja Transfer odbieranie
    /// </summary>
    public class TransferReceiveOperationCommand : IOperationCommand
    {
        private readonly ITransferModel _transferModel;

        public TransferReceiveOperationCommand(ITransferModel transferModel)
        {
            _transferModel = transferModel;
        }

        public async Task Execute(IAccountRepository accountRepository)
        {
            var checksum = NumberAccountHelper.GetChecksum(_transferModel.AccountTo);
            var number = NumberAccountHelper.GetNumberAccount(_transferModel.AccountTo);

            var accountTo = await accountRepository.GetAccountByNumberAndCheckSumAsync(checksum, number);

            accountTo.Balance += _transferModel.GetAmount;
            await Save(accountRepository, accountTo);
        }

        private async Task Save(IAccountRepository accountRepository, RepozytoriumDB.DTO.Account account)
        {
            var operation = new TransferReceiveOperation();
            operation.Title = _transferModel.Title;
            operation.Amount = _transferModel.GetAmount;
            operation.Balance = account.Balance;
            operation.Account = account;
            operation.Source = NumberAccountHelper.ClearNumber(_transferModel.AccountFrom);
            operation.Date = DateTime.Now;
            accountRepository.OperationRepository.Add(operation);
            await accountRepository.SaveAsync();
        }
    }
}