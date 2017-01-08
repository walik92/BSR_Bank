using System.Runtime.Serialization;

namespace BusinessLogic.Exceptions
{
    [DataContract]
    public class ErrorDetailWeb
    {
        [DataMember(Name = "error")]
        public string Error { get; set; }
    }
}