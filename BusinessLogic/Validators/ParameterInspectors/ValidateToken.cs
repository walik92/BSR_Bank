using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using BusinessLogic.Business.Credentials;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces.Credentials;
using RepozytoriumDB;
using RepozytoriumDB.Repository;

namespace BusinessLogic.Validators.ParameterInspectors
{
    public class ValidateToken : IParameterInspector
    {
        private int _index;

        public ValidateToken()
            : this(0)
        {
        }

        public ValidateToken(int index)
        {
            _index = index;
        }

        public ITokenValidator TokenValidator { get; set; }


        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            TokenValidator = new TokenValidator(new TokenRepository(new BankDbContext()));
            if (inputs[0] == null)
                throw new FaultException("Token can't be empty");

            var token = inputs[0] as string;
            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
                throw new FaultException("Token can't be empty");


            if (!TokenValidator.IsValid(token))
            {
                var fault = new TokenFault {Message = "Invalid or expired token", Token = token};
                throw new FaultException<TokenFault>(fault, "Token Fault");
            }


            return null;
        }
    }
}