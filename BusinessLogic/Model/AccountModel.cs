using System.Runtime.Serialization;

namespace BusinessLogic.Model
{
    [DataContract(Name = "Account")]
    public class AccountModel
    {
        [DataMember]
        public string Number { get; set; }

        [DataMember(IsRequired = true)]
        public double Balance { get; set; }
    }
}