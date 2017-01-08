using System;
using System.Configuration;
using System.Net;
using System.Reflection;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using BusinessLogic.Exceptions;
using log4net;

namespace BusinessLogic.Validators.ParameterInspectors
{
    public class ValidateBasicAuthentication : IParameterInspector
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private int _index;

        public ValidateBasicAuthentication()
            : this(0)
        {
        }

        public ValidateBasicAuthentication(int index)
        {
            _index = index;
        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            try
            {
                var user = ConfigurationManager.AppSettings["User"];
                var password = ConfigurationManager.AppSettings["Password"];

                if (string.IsNullOrEmpty(user) && string.IsNullOrEmpty(password))
                    return null;

                var auth = WebOperationContext.Current.IncomingRequest.Headers.Get("Authorization");
                if (auth != null)
                    if (auth.StartsWith("Basic "))
                    {
                        var cred = Encoding.UTF8.GetString(Convert.FromBase64String(auth.Substring("Basic ".Length)));
                        var parts = cred.Split(':');
                        if (user != parts[0] || password != parts[1])
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
    }
}