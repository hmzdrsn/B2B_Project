using B2B_Project.Application.Repositories;
using B2B_Project.Domain.Entities;
using B2B_Project.Persistance.Context;

namespace B2B_Project.Persistance.Repositories
{
    public class AttributeTypeReadRepository : ReadRepository<AttributeType>, IAttributeTypeReadRepository
    {
        public AttributeTypeReadRepository(B2B_ProjectDbContext context) : base(context)
        {
        }
    }
}
