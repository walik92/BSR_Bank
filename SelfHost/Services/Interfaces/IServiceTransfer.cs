using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using BusinessLogic.Model;
using BusinessLogic.ValidationAttributes;

namespace SelfHost.Services.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceTransfer
    {
        [BasicAuthorizationValidation]
        [OperationContract]
        [OperationValidation]
        [WebInvoke(Method = "POST", UriTemplate = "/transfer", ResponseFormat = WebMessageFormat.Json)]
        Task TransferReceive(TransferRestModel transferRestModel);
    }
}