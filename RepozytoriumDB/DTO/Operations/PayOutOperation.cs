﻿using System.ComponentModel.DataAnnotations.Schema;

namespace RepozytoriumDB.DTO.Operations
{
    public class PayOutOperation : BaseOperation
    {
        [NotMapped]
        public string Name
        {
            get { return "PayOut"; }
        }
    }
}