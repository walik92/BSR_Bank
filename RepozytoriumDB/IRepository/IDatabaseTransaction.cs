using System;

namespace RepozytoriumDB.IRepository
{
    public interface IDatabaseTransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}