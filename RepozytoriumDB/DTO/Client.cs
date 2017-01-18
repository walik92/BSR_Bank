using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepozytoriumDB.DTO
{
    /// <summary>
    ///     Data Transfer Object Klient
    /// </summary>
    public class Client
    {
        /// <summary>
        ///     Identyfikator klienta
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary>
        ///     Zaszyfrowane hasło
        /// </summary>
        [Required]
        [StringLength(64)]
        public string Password { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}