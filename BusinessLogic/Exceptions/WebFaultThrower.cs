using System.Net;
using System.ServiceModel.Web;

namespace BusinessLogic.Exceptions
{
    public static class WebFaultThrower
    {
        public static void Throw(string error, HttpStatusCode code)
        {
            if (WebOperationContext.Current != null)
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";

            var wfc = new WebFaultException<ErrorDetailWeb>
            (new ErrorDetailWeb
            {
                Error = error
            }, code);
            throw wfc;
        }
    }
}