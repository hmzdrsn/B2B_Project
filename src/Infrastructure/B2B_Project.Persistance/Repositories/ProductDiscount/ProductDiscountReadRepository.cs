using B2B_Project.Application.Repositories;
using B2B_Project.Domain.Entities;
using B2B_Project.Persistance.Context;

namespace B2B_Project.Persistance.Repositories
{
    public class ProductDiscountReadRepository : ReadRepository<ProductDiscount>, IProductDiscountReadRepository
    {
        public ProductDiscountReadRepository(B2B_ProjectDbContext context) : base(context)
        {
        }
    }
}
