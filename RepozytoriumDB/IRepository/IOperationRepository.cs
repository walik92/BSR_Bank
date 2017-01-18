using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepozytoriumDB.DTO.Operations;

namespace RepozytoriumDB.IRepository
{
    /// <summary>
    ///     Operacje wykonywane na obiekcie Operation
    /// </summary>
    public interface IOperationRepository : IDisposable
    {
        /// <summary>
        ///     Pobierz operacje wykonane na koncie
        /// </summary>
        /// <param name="checksum">Suma kontrola konta</param>
        /// <param name="number">Numer konta</param>
        /// <param name="currentPage">Numer strony</param>
        /// <param name="sizePage">Rozmiar strony</param>
        /// <returns></returns>
        Task<IEnumerable<BaseOperation>> GetOperationsOfAccountAsync(byte checksum, long number, int currentPage,
            int sizePage);

        /// <summary>
        ///     Dodaj operację wykonaną na koncie
        /// </summary>
        /// <param name="operation">Operation</param>
        void Add(BaseOperation operation);

        /// <summary>
        ///     Pobierz liczbę stron operacji wykonanych na koncie
        /// </summary>
        /// <param name="checksum">Suma kontrolna konta</param>
        /// <param name="number">Numer konta</param>
        /// <param name="sizePage">Rozmiar strony</param>
        /// <returns></returns>
        Task<int> GetCountOfAllPagesAsync(byte checksum, long number, int sizePage);

        /// <summary>
        ///     Zapisz wykonane operacje
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}