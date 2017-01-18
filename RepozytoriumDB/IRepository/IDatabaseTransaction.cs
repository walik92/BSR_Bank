using System;

namespace RepozytoriumDB.IRepository
{
    /// <summary>
    ///     Operacje na transakcji
    /// </summary>
    public interface IDatabaseTransaction : IDisposable
    {
        /// <summary>
        ///     Zatwierdź operacje
        /// </summary>
        void Commit();

        /// <summary>
        ///     Wycofaj operacje
        /// </summary>
        void Rollback();
    }
}