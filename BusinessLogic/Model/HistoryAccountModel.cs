using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BusinessLogic.Model
{
    [DataContract(Name = "HistoryOfAccount")]
    public class HistoryOfAccountModel
    {
        [DataMember]
        public IEnumerable<OperationModel> Operations { get; set; }

        [DataMember]
        public int CountOfAllPages { get; set; }

        [DataMember]
        public int CurrentPage { get; set; }
    }
}