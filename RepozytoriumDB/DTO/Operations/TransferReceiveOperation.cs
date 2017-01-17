﻿using System.ComponentModel.DataAnnotations;

namespace RepozytoriumDB.DTO.Operations
{
    public class TransferReceiveOperation : BaseOperation
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(26)]
        public string Source { get; set; }
    }
}