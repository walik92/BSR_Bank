using System.ComponentModel.DataAnnotations;

namespace RepozytoriumDB.DTO.Operations
{
    public class TransferSendOperation : Operation
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(26)]
        public string Destination { get; set; }
    }
}