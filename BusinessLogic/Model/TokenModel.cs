using System.Runtime.Serialization;

namespace BusinessLogic.Model
{
    [DataContract(Name = "Token")]
    public class TokenModel
    {
        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public int TimeToLive { get; set; }
    }
}