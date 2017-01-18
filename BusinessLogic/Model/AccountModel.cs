using System.Runtime.Serialization;

namespace BusinessLogic.Model
{
    /// <summary>
    ///     Model: Konto bankowe
    /// </summary>
    [DataContract(Name = "Account")]
    public class AccountModel
    {
        /// <summary>
        ///     Numer konta
        /// </summary>
        [DataMember]
        public string Number { get; set; }

        /// <summary>
        ///     Saldo
        /// </summary>
        [DataMember(IsRequired = true)]
        public double Balance { get; set; }
    }
}