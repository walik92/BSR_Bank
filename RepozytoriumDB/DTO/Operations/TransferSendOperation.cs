using System.ComponentModel.DataAnnotations;

namespace RepozytoriumDB.DTO.Operations
{
    public class TransferSendOperation : BaseOperation
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(26)]
        public string Destination { get; set; }
    }
}