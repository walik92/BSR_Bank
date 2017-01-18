using System.ComponentModel.DataAnnotations.Schema;

namespace RepozytoriumDB.DTO.Operations
{
    /// <summary>
    ///     Data Transfer Object Operacaja Wypłata bankowa
    /// </summary>
    public class PayOutOperation : BaseOperation
    {
        [NotMapped]
        public string Name
        {
            get { return "PayOut"; }
        }
    }
}