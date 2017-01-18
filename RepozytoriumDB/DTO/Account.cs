using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepozytoriumDB.DTO
{
    /// <summary>
    ///     Data Transfer Object Konto
    /// </summary>
    public class Account
    {
        /// <summary>
        ///     Identyfiaktor konta
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Saldo
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        ///     Suma kontrolna
        /// </summary>
        public byte Checksum { get; set; }

        /// <summary>
        ///     Unikalny identyfikator (numer) konta
        /// </summary>
        [Index(IsUnique = true)]
        public long Number { get; set; }

        [Required]
        public virtual Client Client { get; set; }
    }
}