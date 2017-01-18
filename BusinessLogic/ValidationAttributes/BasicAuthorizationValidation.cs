using System;
using System.Configuration;
using System.Net;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using BusinessLogic.Exceptions;
using log4net;

namespace BusinessLogic.ValidationAttributes
{
    /// <summary>
    ///     Atrybut: Walidacja basic authoriation REST service
    /// </summary>
    public class BasicAuthorizationValidation : Attribute, IOperationBehavior, IParameterInspector
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
            try
            {
                var user = ConfigurationManager.AppSettings["User"];
                var password = ConfigurationManager.AppSettings["Password"];

                var auth = WebOperationContext.Current.IncomingRequest.Headers[HttpRequestHeader.Authorization];
                if (auth != null)
                    if (auth.StartsWith("Basic "))
                    {
                        var cred = Encoding.UTF8.GetString(Convert.FromBase64String(auth.Substring("Basic ".Length)));
                        var parts = cred.Split(':');
                        if (user == parts[0] && password == parts[1])
                            return null;
                    }
            }
            catch (Exception e)
            {
                log.Error("Error in validation authentication", e);
                WebFaultThrower.Throw("An error occurred, please try again later.",
                    HttpStatusCode.ServiceUnavailable);
            }

            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
            var wfc = new WebFaultException<ErrorDetailWeb>
            (new ErrorDetailWeb
            {
                Error = "No authorized bank"
            }, HttpStatusCode.Unauthorized);
            throw wfc;
        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
        }
    }
}