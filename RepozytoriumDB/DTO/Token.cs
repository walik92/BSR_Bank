using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepozytoriumDB.DTO
{
    /// <summary>
    ///     Data Transfer Object Token
    /// </summary>
    public class Token
    {
        /// <summary>
        ///     Identyfikator konta
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Wartość tokenu, maksymalna długość 250 znaków
        /// </summary>
        [StringLength(250)]
        [Column("Token")]
        [Required]
        public string Value { get; set; }

        /// <summary>
        ///     Data utworzenia
        /// </summary>
        public DateTime CreateDate { get; set; }

        [Required]
        public virtual Client Client { get; set; }
    }
}