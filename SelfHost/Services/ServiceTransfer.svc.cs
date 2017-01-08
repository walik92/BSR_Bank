using System;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using BusinessLogic.Business.Operations;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces.Account;
using BusinessLogic.Model;
using log4net;
using SelfHost.Services.Interfaces;

namespace SelfHost.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ServiceTransfer : IServiceTransfer
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IAccountManager _accountManager;

        public ServiceTransfer(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public async Task TransferReceive(TransferRestModel transferRestModel)
        {
            try
            {
                var transferReceiver = new TransferReceiveOperationCommand(transferRestModel);
                await _accountManager.ExecuteOperation(transferReceiver);

                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Created;
            }
            catch (FaultException ex)
            {
                WebFaultThrower.Throw(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                log.Error("Error in TransferReceive", ex);
                WebFaultThrower.Throw("An error occurred, please try again later.", HttpStatusCode.InternalServerError);
            }
        }
    }
}