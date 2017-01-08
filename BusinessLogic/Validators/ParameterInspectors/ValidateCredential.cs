using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using BusinessLogic.Model;

namespace BusinessLogic.Validators.ParameterInspectors
{
    public class ValidateCredential : IParameterInspector
    {
        private int index;

        public ValidateCredential()
            : this(0)
        {
        }

        public ValidateCredential(int index)
        {
            this.index = index;
        }


        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            var credential = inputs[0] as CredentialModel;
            if (credential == null)
                throw new FaultException("The Credential can't be empty.");
            if (credential.Id == 0)
                throw new FaultException("The Id Client can't be empty.");
            if (string.IsNullOrEmpty(credential.Password))
                throw new FaultException("The Password can't be empty.");


            return null;
        }
    }
}