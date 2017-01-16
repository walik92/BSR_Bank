using System;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text.RegularExpressions;
using BusinessLogic.Exceptions;
using BusinessLogic.Helpers;
using BusinessLogic.Model;
using BusinessLogic.Model.Interfaces;
using log4net;
using RepozytoriumDB;
using RepozytoriumDB.DTO;
using RepozytoriumDB.IRepository;
using RepozytoriumDB.Repository;

namespace BusinessLogic.ValidationAttributes
{
    public class OperationValidation : Attribute, IOperationBehavior, IParameterInspector
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IAccountRepository _accountRepository;

        public void AddBindingParameters(OperationDescription operationDescription,
            BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            clientOperation.ParameterInspectors.Add(this);
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(this);
        }

        public void Validate(OperationDescription operationDescription)
        {
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            _accountRepository = new AccountRepository(new BankDbContext());
            switch (operationName)
            {
                case "Transfer":
                    if (inputs[0] is string && inputs[1] is TransferSoapModel)
                        try
                        {
                            var transfer = inputs[1] as TransferSoapModel;

                            ValidTransfer(transfer);
                            ValidAccountDb((string) inputs[0], transfer.AccountFrom, transfer.GetAmount);
                        }
                        catch (FaultException ex)
                        {
                            throw new FaultException(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error in validate operation", ex);
                            throw new FaultException("An error occurred, please try again later.");
                        }
                    break;

                case "TransferReceive":
                    if (inputs[0] == null)
                        WebFaultThrower.Throw("JSON is incorrect", HttpStatusCode.BadRequest);
                    if (inputs[0] is TransferRestModel)
                        try
                        {
                            var transfer = inputs[0] as TransferRestModel;
                            ValidTransfer(transfer);
                            //validate without token
                            ValidAccountDb(transfer.AccountTo);
                            if (!NumberAccountHelper.IsAccountInMyBank(transfer.AccountTo))
                                WebFaultThrower.Throw(
                                    $"The accountTo number '{transfer.AccountTo}' isn't from my bank.",
                                    HttpStatusCode.NotFound);
                        }
                        catch (FaultException ex)
                        {
                            WebFaultThrower.Throw(ex.Message, HttpStatusCode.BadRequest);
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error in validate operation", ex);
                            WebFaultThrower.Throw("An error occurred, please try again later.",
                                HttpStatusCode.ServiceUnavailable);
                        }
                    break;

                case "PayIn":
                    if (inputs[0] is string && inputs[1] is int && inputs[2] is string)
                        try
                        {
                            ValidAmount((int) inputs[1]);
                            ValidateNumberAccount((string) inputs[2]);
                            ValidAccountDb((string) inputs[2]);
                        }
                        catch (FaultException ex)
                        {
                            throw new FaultException(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error in validate operation", ex);
                            throw new FaultException("An error occurred, please try again later.");
                        }

                    break;

                case "PayOut":
                    if (inputs[0] is string && inputs[1] is int && inputs[2] is string)
                        try
                        {
                            ValidAmount((int) inputs[1]);
                            ValidateNumberAccount((string) inputs[2]);
                            var amount = (int) inputs[1] / 100;
                            ValidAccountDb((string) inputs[0], (string) inputs[2], amount);
                        }
                        catch (FaultException ex)
                        {
                            throw new FaultException(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error in validate operation", ex);
                            throw new FaultException("An error occurred, please try again later.");
                        }
                    break;
                case "GetHistoryOfAccount":
                    if (inputs[0] is string && inputs[1] is string && inputs[2] is int && inputs[2] is int)
                        try
                        {
                            ValidateNumberAccount((string) inputs[1]);
                            //sprawdzenie czy istnieje konto oraz czy nalezy do zalogowanego użytkownika
                            ValidAccountDb((string) inputs[0], (string) inputs[1]);
                            if ((int) inputs[2] < 0)
                                throw new FaultException($"The number page is incorrect.");
                            if ((int) inputs[3] <= 0)
                                throw new FaultException($"The size of page is incorrect.");
                        }
                        catch (FaultException ex)
                        {
                            throw new FaultException(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error in validate operation", ex);
                            throw new FaultException("An error occurred, please try again later.");
                        }

                    break;
            }
            return null;
        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
        }

        private void ValidateNumberAccount(string number)
        {
            if (string.IsNullOrEmpty(number))
                throw new FaultException($"The account number can't be empty.");

            var num = number.Replace(" ", null);
            if (!Regex.IsMatch(num, @"^\d{26}$"))
                throw new FaultException($"The account number '{number}' is wrong length.");

            num = num.Remove(0, 2) + "2521" + num.Substring(0, 2);
            var modulo = 0;
            foreach (var znak in num)
                modulo = (10 * modulo + int.Parse(znak.ToString())) % 97;
            if (modulo != 1)
                throw new FaultException($"The account number '{number}' is incorrect.");
        }

        private void ValidTransfer(ITransferModel transferModel)
        {
            ValidateNumberAccount(transferModel.AccountFrom);
            ValidateNumberAccount(transferModel.AccountTo);

            ValidAmount(transferModel.GetAmount);

            if (NumberAccountHelper.EqualsNumbers(transferModel.AccountFrom, transferModel.AccountTo))
                throw new FaultException("Account From is the same as Account To");
            if (string.IsNullOrEmpty(transferModel.Title) || string.IsNullOrWhiteSpace(transferModel.Title))
                throw new FaultException("The Title can't be empty");
            if (transferModel.Title.Length > 200)
                throw new FaultException("The Title length should be less than 200");
        }


        public void ValidAmount(decimal amount)
        {
            if (amount <= 0)
                throw new FaultException("The amount of transfer should be greater than zero.");
        }

        //sprawdza czy istnieje konto o podanym numerze i czy nalezy do zalogowanego użytkownika i czy stan konta pozwala na wykoanie operacji
        private void ValidAccountDb(string token, string numberAccount, decimal amount)
        {
            var account = ValidAccountDb(token, numberAccount);

            if (account.Balance < amount)
                throw new FaultException($"The account {numberAccount} haven't enough money.");
        }

        //sprawdza czy istnieje konto o podanym numerze i czy nalezy do zalogowanego użytkownika
        private Account ValidAccountDb(string token, string numberAccount)
        {
            var client = _accountRepository.ClientRepository.GetByToken(token);

            var account = ValidAccountDb(numberAccount);

            if (account.Client.Id != client.Id)
                throw new FaultException($"Sorry, The account number {numberAccount} doesn't exist to logged client");
            return account;
        }

        //sprawdza czy w bazie istnieje konto o podanym numerze
        private Account ValidAccountDb(string numberAccount)
        {
            return GetAccountByNumber(numberAccount);
        }

        private Account GetAccountByNumber(string numberAccount)
        {
            var number = NumberAccountHelper.GetNumberAccount(numberAccount);
            var checksum = NumberAccountHelper.GetChecksum(numberAccount);

            var account = _accountRepository.GetAccountByNumber(checksum, number);
            if (account == null)
                throw new FaultException($"Sorry, The account number {numberAccount} doesn't exist in my bank");
            return account;
        }
    }
}