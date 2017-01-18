using System.Runtime.Serialization;

namespace BusinessLogic.Model
{
    /// <summary>
    ///     Model: Informacje o tokenie
    /// </summary>
    [DataContract(Name = "Token")]
    public class TokenModel
    {
        /// <summary>
        ///     Wartość tokenu
        /// </summary>
        [DataMember]
        public string Value { get; set; }

        /// <summary>
        ///     Czas życia tokenu
        /// </summary>
        [DataMember]
        public int TimeToLive { get; set; }
    }
}