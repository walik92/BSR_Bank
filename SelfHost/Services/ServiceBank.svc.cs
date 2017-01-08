using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using System.Threading.Tasks;
using BusinessLogic.Business.Operations;
using BusinessLogic.Interfaces.Account;
using BusinessLogic.Interfaces.Credentials;
using BusinessLogic.Model;
using log4net;
using SelfHost.Services.Interfaces;

namespace SelfHost.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ServiceBank : IServiceBank
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IAccountManager _accountManager;
        private readonly ITokenManager _tokenManager;


        public ServiceBank(ITokenManager tokenManager, IAccountManager accountManager)
        {
            _tokenManager = tokenManager;
            _accountManager = accountManager;
        }


        public async Task<TokenModel> Login(CredentialModel credential)
        {
            try
            {
                return await _tokenManager.Build(credential);
            }
            catch (FaultException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("Error in Login method", ex);
                throw new FaultException("An error occurred, please try again later.");
            }
        }

        public async Task<IEnumerable<AccountModel>> GetAccounts(string token)
        {
            try
            {
                return await _accountManager.GetAccounts(token);
            }
            catch (FaultException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("Error in GetAccounts method", ex);
                throw new FaultException("An error occurred, please try again later.");
            }
        }

        public async Task PayIn(string token, int amount, string accountTo)
        {
            try
            {
                var payInOperation = new PayInOperationCommand(accountTo, amount);
                await _accountManager.ExecuteOperation(payInOperation);
            }
            catch (FaultException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("Error in PayIn method", ex);
                throw new FaultException("An error occurred, please try again later.");
            }
        }

        public async Task PayOut(string token, int amount, string accountFrom)
        {
            try
            {
                var payOutOperation = new PayOutOperationCommand(accountFrom, amount);
                await _accountManager.ExecuteOperation(payOutOperation);
            }
            catch (FaultException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("Error in PayOut method", ex);
                throw new FaultException("An error occurred, please try again later.");
            }
        }

        public async Task TransferCreate(string token, TransferSoapModel transferSoapModel)
        {
            try
            {
                var transferSender = new TransferSendOperationCommand(transferSoapModel);
                await _accountManager.ExecuteOperation(transferSender);
            }
            catch (FaultException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("Error in Transfer method", ex);
                throw new FaultException("An error occurred, please try again later.");
            }
        }

        public async Task<HistoryOfAccountModel> GetHistoryOfAccount(string token, string account, int currentPage,
            int sizePage)
        {
            try
            {
                return await _accountManager.GetHistoryOfAccount(token, account, currentPage, sizePage);
            }
            catch (FaultException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("Error in GetOperations method", ex);
                throw new FaultException("An error occurred, please try again later.");
            }
        }
    }
}