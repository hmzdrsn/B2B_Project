using B2B_Project.Application;
using B2B_Project.Domain.Entities;
using B2B_Project.Persistance.Context;
using B2B_Project.Persistance.Repositories;

namespace B2B_Project.Persistance
{
    public class AddressReadRepository : ReadRepository<Address>, IAddressReadRepository
    {
        public AddressReadRepository(B2B_ProjectDbContext context) : base(context)
        {
        }
    }
}
