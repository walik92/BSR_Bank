using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using BusinessLogic.Validators.ParameterInspectors;

namespace BusinessLogic.Validators
{
    public class TokenValidation : Attribute, IOperationBehavior
    {
        private readonly int _index;


        public TokenValidation() : this(0)
        {
        }

        public TokenValidation(int index)
        {
            _index = index;
        }

        public void AddBindingParameters(OperationDescription operationDescription,
            BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            clientOperation.ParameterInspectors.Add(new ValidateToken(_index));
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(new ValidateToken(_index));
        }

        public void Validate(OperationDescription operationDescription)
        {
        }
    }
}