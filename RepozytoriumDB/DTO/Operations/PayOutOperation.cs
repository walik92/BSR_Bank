using System.ComponentModel.DataAnnotations.Schema;

namespace RepozytoriumDB.DTO.Operations
{
    public class PayOutOperation : Operation
    {
        [NotMapped]
        public string Name
        {
            get { return "PayOut"; }
        }
    }
}