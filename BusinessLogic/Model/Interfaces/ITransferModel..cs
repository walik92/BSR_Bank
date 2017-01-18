namespace BusinessLogic.Model.Interfaces
{
    /// <summary>
    ///     Model: Transfer
    /// </summary>
    public interface ITransferModel
    {
        /// <summary>
        ///     Konto źródłowe
        /// </summary>
        string AccountFrom { get; set; }

        /// <summary>
        ///     Konto docelowe
        /// </summary>
        string AccountTo { get; set; }

        /// <summary>
        ///     Kwota transferu
        /// </summary>
        int Amount { set; }

        /// <summary>
        ///     Tytuł transferu
        /// </summary>
        string Title { get; set; }

        /// <summary>
        ///     Pobierz kwotę transferu
        /// </summary>
        decimal GetAmount { get; }
    }
}