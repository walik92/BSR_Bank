using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepozytoriumDB.DTO
{
    public class Token
    {
        [Key]
        public int Id { get; set; }

        [StringLength(250)]
        [Column("Token")]
        [Required]
        public string Value { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        public virtual Client Client { get; set; }
    }
}