using System.ComponentModel.DataAnnotations.Schema;

namespace RepozytoriumDB.DTO.Operations
{
    public class PayInOperation : BaseOperation
    {
        [NotMapped]
        public string Name
        {
            get { return "PayIn"; }
        }
    }
}