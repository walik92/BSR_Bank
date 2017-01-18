using System.Runtime.Serialization;

namespace BusinessLogic.Exceptions
{
    /// <summary>
    ///     Szczegóły błędu w usłudze Restowej
    /// </summary>
    [DataContract]
    public class ErrorDetailWeb
    {
        /// <summary>
        ///     Status błędu
        /// </summary>
        [DataMember(Name = "error")]
        public string Error { get; set; }
    }
}