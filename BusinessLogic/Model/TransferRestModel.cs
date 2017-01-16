using System.Runtime.Serialization;
using BusinessLogic.Model.Interfaces;

namespace BusinessLogic.Model
{
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