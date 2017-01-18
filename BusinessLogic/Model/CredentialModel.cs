using System.Runtime.Serialization;

namespace BusinessLogic.Model
{
    /// <summary>
    ///     Model: Dane uwierzytelniania klienta
    /// </summary>
    [DataContract(Name = "Credential")]
    public class CredentialModel
    {
        /// <summary>
        ///     Id Klienta
        /// </summary>
        [DataMember(IsRequired = true)]
        public int Id { get; set; }

        /// <summary>
        ///     Hasło klienta
        /// </summary>
        [DataMember]
        public string Password { get; set; }
    }
}