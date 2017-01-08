using System;
using System.Runtime.Serialization;

namespace BusinessLogic.Model
{
    [DataContract(Name = "Operation")]
    public class OperationModel
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public double Balance { get; set; }

        [DataMember]
        public string Details { get; set; }
    }
}