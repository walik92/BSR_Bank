﻿using System.Runtime.Serialization;
using BusinessLogic.Model.Interfaces;
using Newtonsoft.Json;

namespace BusinessLogic.Model
{
    /// <summary>
    ///     Model: Transfer(wykorzystywany do operacji w usłudze SOAP)
    /// </summary>
    [DataContract(Name = "Transfer")]
    public class TransferSoapModel : ITransferModel
    {
        [DataMember]
        [JsonProperty("sender_account")]
        public string AccountFrom { get; set; }

        [DataMember]
        [JsonProperty("receiver_account")]
        public string AccountTo { get; set; }

        [DataMember(IsRequired = true)]
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [DataMember]
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonIgnore]
        public decimal GetAmount => (decimal) Amount / 100;
    }
}