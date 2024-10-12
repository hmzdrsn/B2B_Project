using B2B_Project.Application;
using B2B_Project.Domain.Entities;
using B2B_Project.Persistance.Context;

namespace B2B_Project.Persistance.Repositories
{
    public class DiscountReadRepository : ReadRepository<Discount>, IDiscountReadRepository
    {
        public DiscountReadRepository(B2B_ProjectDbContext context) : base(context)
        {
        }
    }

}
