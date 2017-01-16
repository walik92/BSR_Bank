using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using BusinessLogic.Business.Credentials;
using BusinessLogic.Exceptions;
using RepozytoriumDB;
using RepozytoriumDB.Repository;

namespace BusinessLogic.ValidationAttributes
{
    public class TokenValidation : Attribute, IOperationBehavior, IParameterInspector
    {
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
            var tokenValidator = new TokenValidator(new TokenRepository(new BankDbContext()));
            if (inputs[0] == null)
                throw new FaultException("Token can't be empty");

            var token = inputs[0] as string;
            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
                throw new FaultException("Token can't be empty");


            if (!tokenValidator.IsValid(token))
            {
                var fault = new TokenFault {Message = "Invalid or expired token", Token = token};
                throw new FaultException<TokenFault>(fault, "Token Fault");
            }


            return null;
        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
        }
    }
}