using System;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.Threading.Tasks;
using BusinessLogic.Business.Operations;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces.Admin;
using log4net;
using SelfHost.Services.Interfaces;

namespace SelfHost.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ServiceAdmin : IServiceAdmin
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IAdminManager _adminManager;

        public ServiceAdmin(IAdminManager adminManager)
        {
            _adminManager = adminManager;
        }

        public async Task<int> AddClient(string password)
        {
            try
            {
                return await _adminManager.AddClient(password);
            }
            catch (Exception ex)
            {
                Logger.Error("Error in AddClient method", ex);
                WebFaultThrower.Throw(ex.Message, HttpStatusCode.InternalServerError);
                throw;
            }
        }

        public async Task<string> AddAccount(string clientId)
        {
            try
            {
                var id = int.Parse(clientId);
                return await _adminManager.AddAccount(id);
            }
            catch (Exception ex)
            {
                Logger.Error("Error in AddAccount method", ex);
                WebFaultThrower.Throw(ex.Message, HttpStatusCode.InternalServerError);
                throw;
            }
        }

        public async Task AddBankCharges(string amount)
        {
            try
            {
                var value = int.Parse(amount);
                var bankChargeOperation = new BankChargeOperationCommand(value);
                await _adminManager.AddBankCharges(bankChargeOperation);
            }
            catch (DbUpdateException ex)
            {
                var updateException = (UpdateException) ex.InnerException;
                var sqlException = (SqlException) updateException.InnerException;

                foreach (SqlError error in sqlException.Errors)
                {
                    // TODO: Do something with your errors
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error in AddBankCharge method", ex);
                WebFaultThrower.Throw(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}