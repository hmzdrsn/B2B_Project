using B2B_Project.Application;
using B2B_Project.Domain.Entities;
using B2B_Project.Persistance.Context;

namespace B2B_Project.Persistance.Repositories
{
    public class DiscountWriteRepository : WriteRepository<Discount>, IDiscountWriteRepository
    {
        public DiscountWriteRepository(B2B_ProjectDbContext context) : base(context)
        {
        }
    }

}
