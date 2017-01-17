using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepozytoriumDB.DTO.Operations;

namespace RepozytoriumDB.IRepository
{
    public interface IOperationRepository : IDisposable
    {
        Task<IEnumerable<BaseOperation>> GetOperationsAsync(byte checksum, long number, int currentPage, int sizePage);
        void Add(BaseOperation operation);
        Task<int> GetCountOfAllPagesAsync(byte checksum, long number, int sizePage);
        Task SaveAsync();
    }
}