using System;
using System.Runtime.Serialization;

namespace BusinessLogic.Model
{
    /// <summary>
    ///     Model: Operacja bankowa
    /// </summary>
    [DataContract(Name = "Operation")]
    public class OperationModel
    {
        /// <summary>
        ///     Nazwa operacji
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        ///     Data wykonania operacji
        /// </summary>
        [DataMember]
        public DateTime Date { get; set; }

        /// <summary>
        ///     Kwota operacji (winien/ma)
        /// </summary>
        [DataMember]
        public double Amount { get; set; }

        /// <summary>
        ///     Saldo po operacji
        /// </summary>
        [DataMember]
        public double Balance { get; set; }

        /// <summary>
        ///     Szczegóły operacji tj. tytuł transferu, konto docelowe/źródłowe
        /// </summary>
        [DataMember]
        public string Details { get; set; }
    }
}