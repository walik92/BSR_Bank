using System.ComponentModel.DataAnnotations;

namespace RepozytoriumDB.DTO.Operations
{
    /// <summary>
    ///     Data Transfer Object Operacaja bankowa Odbiór transferu
    /// </summary>
    public class TransferReceiveOperation : BaseOperation
    {
        /// <summary>
        ///     Tytuł operacji
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        /// <summary>
        ///     Źródło pochodzenia piniędzy
        /// </summary>
        [Required]
        [MaxLength(26)]
        public string Source { get; set; }
    }
}