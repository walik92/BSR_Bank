using System;
using System.ServiceModel;
using System.Threading.Tasks;
using BusinessLogic.Interfaces.Admin;
using SelfHost.Services.Interfaces;

namespace SelfHost.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AdminService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AdminService.svc or AdminService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ServiceAdmin : IServiceAdmin
    {
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
                throw new FaultException(ex.Message);
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
                throw new FaultException(ex.Message);
            }
        }
    }
}