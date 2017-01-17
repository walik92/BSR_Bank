using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace SelfHost.Services.Interfaces
{
    [ServiceContract]
    public interface IServiceAdmin
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Client/{password}", ResponseFormat = WebMessageFormat.Json)]
        Task<int> AddClient(string password);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Account/{clientId}", ResponseFormat = WebMessageFormat.Json)]
        Task<string> AddAccount(string clientId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/BankCharges/{amount}", ResponseFormat = WebMessageFormat.Json)]
        Task AddBankCharges(string amount);
    }
}