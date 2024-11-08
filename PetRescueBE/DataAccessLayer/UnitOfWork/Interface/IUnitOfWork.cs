using DataAccessLayer.Repository.Interface;

namespace DataAccessLayer.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();


    }
}
