using System.Runtime.Serialization;

namespace BusinessLogic.Model
{
    [DataContract(Name = "Credential")]
    public class CredentialModel
    {
        [DataMember(IsRequired = true)]
        public int Id { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}