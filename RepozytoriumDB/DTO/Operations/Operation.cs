using System;
using System.ComponentModel.DataAnnotations;

namespace RepozytoriumDB.DTO.Operations
{
    public abstract class Operation
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