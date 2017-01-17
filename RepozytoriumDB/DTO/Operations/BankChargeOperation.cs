using System.ComponentModel.DataAnnotations.Schema;

namespace RepozytoriumDB.DTO.Operations
{
    public class BankChargeOperation : BaseOperation
    {
        [NotMapped]
        public string Name
        {
            get { return "BankCharge"; }
        }
    }
}