using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using BusinessLogic.Model;

namespace BusinessLogic.ValidationAttributes
{
    /// <summary>
    ///     Atrybut: Walidacja uwierzytelnanie użytkownika
    /// </summary>
    public class CredentialValidation : Attribute, IOperationBehavior, IParameterInspector
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
            var credential = inputs[0] as CredentialModel;
            if (credential == null)
                throw new FaultException("The Credential can't be empty.");
            if (credential.Id == 0)
                throw new FaultException("The Id Client can't be empty.");
            if (string.IsNullOrEmpty(credential.Password))
                throw new FaultException("The Password can't be empty.");

            return null;
        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
        }
    }
}