using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using BusinessLogic.Exceptions;
using BusinessLogic.Model;
using BusinessLogic.ValidationAttributes;

namespace SelfHost.Services.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceClient" in both code and config file together.
    [ServiceContract]
    public interface IServiceBank
    {
        [OperationContract]
        [CredentialValidation]
        Task<TokenModel> Login(CredentialModel credential);

        [OperationContract]
        [TokenValidation]
        [FaultContract(typeof(TokenFault))]
        Task<IEnumerable<AccountModel>> GetAccounts(string token);

        [OperationContract]
        [TokenValidation]
        [OperationValidation]
        [FaultContract(typeof(TokenFault))]
        Task PayIn(string token, int amount, string accountTo);

        [OperationContract]
        [TokenValidation]
        [OperationValidation]
        [FaultContract(typeof(TokenFault))]
        Task PayOut(string token, int amount, string accountFrom);

        [OperationContract(Name = "Transfer")]
        [TokenValidation]
        [OperationValidation]
        [FaultContract(typeof(TokenFault))]
        Task TransferCreate(string token, TransferSoapModel transferSoapModel);

        [OperationContract]
        [TokenValidation]
        [OperationValidation]
        [FaultContract(typeof(TokenFault))]
        Task<HistoryOfAccountModel> GetHistoryOfAccount(string token, string account, int currentPage, int sizePage);
    }
}