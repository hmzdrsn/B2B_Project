using B2B_Project.Domain.Common;

namespace B2B_Project.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        bool Remove(T entity);
        bool ForceDelete(T entity);
        bool Update(T entity);
        Task<int> SaveAsync();
    }
}
