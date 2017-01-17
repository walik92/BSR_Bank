using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Exceptions;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces.Operations;
using BusinessLogic.Model;
using Newtonsoft.Json;
using RepozytoriumDB.DTO.Operations;
using RepozytoriumDB.IRepository;

namespace BusinessLogic.Business.Operations
{
    public class TransferSendOperationCommand : IOperationCommand
    {
        private readonly TransferSoapModel _transferModel;

        public TransferSendOperationCommand(TransferSoapModel transferSoapModel)
        {
            _transferModel = transferSoapModel;
        }


        public async Task Execute(IAccountRepository accountRepository)
        {
            var checksum = NumberAccountHelper.GetChecksum(_transferModel.AccountFrom);
            var number = NumberAccountHelper.GetNumberAccount(_transferModel.AccountFrom);

            var accountFrom = await accountRepository.GetAccountByNumberAsync(checksum, number);

            accountFrom.Balance -= _transferModel.GetAmount;
            await Save(accountRepository, accountFrom);

            if (NumberAccountHelper.IsAccountInMyBank(_transferModel.AccountTo))
            {
                var transferReceiver = new TransferReceiveOperationCommand(_transferModel);
                await transferReceiver.Execute(accountRepository);
            }
            else
            {
                await SendTransferToAnotherBank();
            }
        }

        private async Task Save(IAccountRepository accountRepository, RepozytoriumDB.DTO.Account account)
        {
            var operation = new TransferSendOperation();
            operation.Title = _transferModel.Title;
            operation.Amount = -_transferModel.GetAmount;
            operation.Balance = account.Balance;
            operation.Account = account;
            operation.Date = DateTime.Now;
            operation.Destination = NumberAccountHelper.ClearNumber(_transferModel.AccountTo);
            accountRepository.OperationRepository.Add(operation);
            await accountRepository.SaveAsync();
        }

        private async Task SendTransferToAnotherBank()
        {
            var idBank = NumberAccountHelper.ExtractIdBank(_transferModel.AccountTo);
            var ip = BankIdMappingHelper.GetIP(idBank);
            var url = $"http://{ip}/transfer";

            var username = ConfigurationManager.AppSettings["User"];
            var password = ConfigurationManager.AppSettings["Password"];

            //        HttpClientHandler httpClientHandler = new HttpClientHandler()
            //        {
            ////            Proxy = new WebProxy(string.Format("127.0.0.1:{1}", proxyServerSettings.Address,
            ////proxyServerSettings.Port), false),
            //            //UseProxy = true
            //        };

            using (var client = new HttpClient())
            {
                var authValue = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));
                client.DefaultRequestHeaders.Authorization = authValue;

                var content = new StringContent(JsonConvert.SerializeObject(_transferModel), Encoding.UTF8,
                    "application/json");

                var response = await client.PostAsync(url, content);
                if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.NotFound)
                {
                    var valueResponse = await response.Content.ReadAsStringAsync();
                    var errorDetailWeb = JsonConvert.DeserializeObject<ErrorDetailWeb>(valueResponse);
                    throw new FaultException($"The bank receiver responded : {errorDetailWeb.Error}");
                }
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var valueResponse = await response.Content.ReadAsStringAsync();
                    var errorDetailWeb = JsonConvert.DeserializeObject<ErrorDetailWeb>(valueResponse);
                    throw new Exception($"Error authorization in receiver bank. details: {errorDetailWeb.Error}");
                }
                if (response.StatusCode != HttpStatusCode.Created)
                {
                    var valueResponse = await response.Content.ReadAsStringAsync();
                    var errorDetailWeb = JsonConvert.DeserializeObject<ErrorDetailWeb>(valueResponse);
                    throw new Exception(
                        $"Error in receiver bank. details: {errorDetailWeb.Error} code: {response.StatusCode}");
                }
            }
        }
    }
}