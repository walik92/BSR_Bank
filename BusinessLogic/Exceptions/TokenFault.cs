using System;
using System.Runtime.Serialization;

namespace BusinessLogic.Exceptions
{
    [DataContract]
    [Serializable]
    public class TokenFault
    {
        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}