using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Business.Account;
using BusinessLogic.Exceptions;
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
            var checksum = NumberAccountManager.GetChecksum(_transferModel.AccountFrom);
            var number = NumberAccountManager.GetNumberAccount(_transferModel.AccountFrom);

            var accountFrom = await accountRepository.GetAccountByNumberAsync(checksum, number);

            accountFrom.Balance -= _transferModel.GetAmount;
            await Save(accountRepository, accountFrom);

            if (NumberAccountManager.IsAccountInMyBank(_transferModel.AccountTo))
            {
                var transferReceiver = new TransferReceiveOperationCommand(_transferModel);
                await transferReceiver.Execute(accountRepository);
            }
            else
            {
                await SendTransferToAnotherBank();
            }
        }

        public async Task Save(IAccountRepository accountRepository, RepozytoriumDB.DTO.Account account)
        {
            var operation = new TransferSendOperation();
            operation.Title = _transferModel.Title;
            operation.Amount = -_transferModel.GetAmount;
            operation.Balance = account.Balance;
            operation.Account = account;
            operation.Date = DateTime.Now;
            operation.Destination = NumberAccountManager.ClearNumber(_transferModel.AccountTo);
            accountRepository.OperationRepository.Add(operation);
            await accountRepository.SaveAsync();
        }

        private async Task SendTransferToAnotherBank()
        {
            var idBank = NumberAccountManager.ExtractIdBank(_transferModel.AccountTo);
            var ip = MappingOfBanks.GetIP(idBank);
            var url = $"http://{ip}/transfer";

            var username = ConfigurationManager.AppSettings["User"];
            var password = ConfigurationManager.AppSettings["Password"];


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