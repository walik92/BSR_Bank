using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BusinessLogic.Model
{
    /// <summary>
    ///     Model: Historia operacji konta
    /// </summary>
    [DataContract(Name = "HistoryOfAccount")]
    public class HistoryOfAccountModel
    {
        /// <summary>
        ///     Lista operacji
        /// </summary>
        [DataMember]
        public IEnumerable<OperationModel> Operations { get; set; }

        /// <summary>
        ///     Liczba wszystkich stron
        /// </summary>
        [DataMember]
        public int CountOfAllPages { get; set; }

        /// <summary>
        ///     Aktualny numer strony
        /// </summary>
        [DataMember]
        public int CurrentPage { get; set; }
    }
}