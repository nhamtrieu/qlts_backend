using System.Data.Common;

namespace MISA.WebFresher062023.Demo.Application
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        DbConnection Connection { get; }
        DbTransaction? Transaction { get; }
        void BeginTransaction();
        Task BeginTransactionAsync();
        void Commit();
        Task CommitAsync();
        void RollBack();
        Task RollBackAsync();
    }
}
