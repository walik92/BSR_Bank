using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepozytoriumDB.DTO.Operations
{
    /// <summary>
    ///     Data Transfer Object Operacja bankowa
    /// </summary>
    [Table("Operations")]
    public abstract class BaseOperation
    {
        /// <summary>
        ///     Identyfikator operacji
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Kwota winien/ma
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///     Saldo po operacji
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        ///     Data operacji
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public virtual Account Account { get; set; }
    }
}