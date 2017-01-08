using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepozytoriumDB.DTO.Operations;

namespace RepozytoriumDB.IRepository
{
    public interface IOperationRepository : IDisposable
    {
        Task<IEnumerable<Operation>> GetOperationsAsync(byte checksum, long number, int currentPage, int sizePage);
        void Add(Operation operation);
        Task<int> GetCountOfAllPagesAsync(byte checksum, long number, int sizePage);
        Task SaveAsync();
    }
}