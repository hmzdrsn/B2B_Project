using B2B_Project.Domain.Common;

namespace B2B_Project.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> WhereDynamic(string propertyName, object value, string operation);
        Task<T> GetByIdAsync(string id);
    }
}
