using B2B_Project.Application.Repositories;
using B2B_Project.Domain.Common;
using B2B_Project.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly B2B_ProjectDbContext _context;

        public ReadRepository(B2B_ProjectDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll()
        {
            return Table.AsQueryable();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var query = Table.AsQueryable();
            return await query.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }
    }
}
