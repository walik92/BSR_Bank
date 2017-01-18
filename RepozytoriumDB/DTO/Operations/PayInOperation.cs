using System.ComponentModel.DataAnnotations.Schema;

namespace RepozytoriumDB.DTO.Operations
{
    /// <summary>
    ///     Data Transfer Object Operacaja Wpłata bankowa
    /// </summary>
    public class PayInOperation : BaseOperation
    {
        [NotMapped]
        public string Name
        {
            get { return "PayIn"; }
        }
    }
}