using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepozytoriumDB.DTO
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        public decimal Balance { get; set; }

        public byte Checksum { get; set; }

        [Index(IsUnique = true)]
        public long Number { get; set; }

        [Required]
        public virtual Client Client { get; set; }
    }
}