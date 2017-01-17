using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepozytoriumDB.DTO.Operations
{
    [Table("Operations")]
    public abstract class BaseOperation
    {
        [Key]
        public int Id { get; set; }

        public decimal Amount { get; set; }
        public decimal Balance { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public virtual Account Account { get; set; }
    }
}