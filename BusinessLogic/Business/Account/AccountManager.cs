using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using BusinessLogic.Interfaces.Account;
using BusinessLogic.Interfaces.Operations;
using BusinessLogic.Model;
using RepozytoriumDB.DTO.Operations;
using RepozytoriumDB.IRepository;

namespace BusinessLogic.Business.Account
{
    public class AccountManager : IAccountManager
    {
        private readonly IAccountRepository _accountRepository;


        public AccountManager(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<AccountModel>> GetAccounts(string token)
        {
            var accountsDb =
                await _accountRepository.GetAccountsAsync(token);
            var accounts = new List<AccountModel>();
            foreach (var account in accountsDb)
            {
                var accountModel = new AccountModel();
                accountModel.Number = NumberAccountManager.GetFullNumberAccount(account.Checksum, account.Number);
                accountModel.Balance = (double) account.Balance;
                accounts.Add(accountModel);
            }


            if (accounts == null || !accounts.Any())
                throw new FaultException("User hasn't any accounts.");
            return accounts;
        }

        public async Task ExecuteOperation(IOperationCommand operation)
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

        public async Task<HistoryOfAccountModel> GetHistoryOfAccount(string token, string account, int currentPage,
            int sizePage)
        {
            var checksum = NumberAccountManager.GetChecksum(account);
            var number = NumberAccountManager.GetNumberAccount(account);

            var operationsDb = await _accountRepository.OperationRepository.GetOperationsAsync(checksum, number,
                currentPage,
                sizePage);

            var enumerable = operationsDb as IList<Operation> ?? operationsDb.ToList();
            if (operationsDb == null || !enumerable.Any())
                throw new FaultException("Account hasn't any operations.");

            var operations = new List<OperationModel>();
            foreach (var operation in enumerable)
            {
                var operationModel = new OperationModel();
                operationModel.Balance = (double) operation.Balance;
                operationModel.Amount = (double) operation.Amount;
                operationModel.Date = operation.Date;
                if (operation is TransferReceiveOperation)
                {
                    operationModel.Title = (operation as TransferReceiveOperation).Title;
                    operationModel.Details =
                        $"Source account: {NumberAccountManager.FormatNumber((operation as TransferReceiveOperation).Source)}";
                }
                if (operation is TransferSendOperation)
                {
                    operationModel.Title = (operation as TransferSendOperation).Title;
                    operationModel.Details =
                        $"Destination account: {NumberAccountManager.FormatNumber((operation as TransferSendOperation).Destination)}";
                }
                if (operation is PayOutOperation)
                    operationModel.Details = (operation as PayOutOperation).Name;
                if (operation is PayInOperation)
                    operationModel.Details = (operation as PayInOperation).Name;

                operations.Add(operationModel);
            }


            var result = new HistoryOfAccountModel();
            result.Operations = operations;
            result.CurrentPage = currentPage;
            result.CountOfAllPages = await _accountRepository.OperationRepository.GetCountOfAllPagesAsync(checksum,
                number,
                sizePage);
            return result;
        }
    }
}