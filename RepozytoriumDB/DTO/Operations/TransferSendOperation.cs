using System.ComponentModel.DataAnnotations;

namespace RepozytoriumDB.DTO.Operations
{
    /// <summary>
    ///     Data Transfer Object Operacaja bankowa Wysłanie transferu
    /// </summary>
    public class TransferSendOperation : BaseOperation
    {
        /// <summary>
        ///     Tytuł operacji
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        /// <summary>
        ///     Cel transferu
        /// </summary>
        [Required]
        [MaxLength(26)]
        public string Destination { get; set; }
    }
}