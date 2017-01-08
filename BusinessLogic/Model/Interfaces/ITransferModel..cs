namespace BusinessLogic.Model.Interfaces
{
    public interface ITransferModel
    {
        string AccountFrom { get; set; }
        string AccountTo { get; set; }
        int Amount { set; }
        string Title { get; set; }

        decimal GetAmount { get; }
    }
}