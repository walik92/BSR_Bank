using System.ComponentModel.DataAnnotations.Schema;

namespace RepozytoriumDB.DTO.Operations
{
    /// <summary>
    ///     Data Transfer Object Operacaja Opłata bankowa
    /// </summary>
    public class BankChargeOperation : BaseOperation
    {
        /// <summary>
        ///     Nazwa operacji
        /// </summary>
        [NotMapped]
        public string Name
        {
            get { return "BankCharge"; }
        }
    }
}