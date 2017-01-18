using System.Runtime.Serialization;
using BusinessLogic.Model.Interfaces;

namespace BusinessLogic.Model
{
    /// <summary>
    ///     Model: Transfer(wykorzystywany do operacji w usłudze REST)
    ///     osobny model dla usługi REST aby nazwy pól były zgodne z konwencja underscores
    /// </summary>
    [DataContract(Name = "Transfer")]
    public class TransferRestModel : ITransferModel
    {
        [DataMember(Name = "sender_account")]
        public string AccountFrom { get; set; }

        [DataMember(Name = "receiver_account")]
        public string AccountTo { get; set; }

        [DataMember(Name = "amount", EmitDefaultValue = false)]
        public int Amount { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        public decimal GetAmount => (decimal) Amount / 100;
    }
}