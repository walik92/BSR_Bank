using System.Net;
using System.ServiceModel.Web;

namespace BusinessLogic.Exceptions
{
    /// <summary>
    ///     Rzuca wyjątek dla usługi Restowej
    /// </summary>
    public static class WebFaultThrower
    {
        /// <summary>
        ///     Rzuć wyjątkiem
        /// </summary>
        /// <param name="error">Status błędu</param>
        /// <param name="code">Kod odpowiedzi HTTP</param>
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