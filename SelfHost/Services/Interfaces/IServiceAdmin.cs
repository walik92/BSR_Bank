using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace SelfHost.Services.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAdminService" in both code and config file together.
    [ServiceContract]
    public interface IServiceAdmin
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AddClient/{password}", ResponseFormat = WebMessageFormat.Json)]
        Task<int> AddClient(string password);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AddAccount/{clientId}", ResponseFormat = WebMessageFormat.Json)]
        Task<string> AddAccount(string clientId);
    }
}