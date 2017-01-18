using System;
using System.Runtime.Serialization;

namespace BusinessLogic.Exceptions
{
    /// <summary>
    ///     Token Exception
    /// </summary>
    [DataContract]
    [Serializable]
    public class TokenFault
    {
        /// <summary>
        ///     Wartość tokenu
        /// </summary>
        [DataMember]
        public string Token { get; set; }

        /// <summary>
        ///     Status błędu
        /// </summary>
        [DataMember]
        public string Message { get; set; }
    }
}