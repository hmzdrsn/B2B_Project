using B2B_Project.Application;
using B2B_Project.Domain.Entities;
using B2B_Project.Persistance.Context;
using B2B_Project.Persistance.Repositories;

namespace B2B_Project.Persistance
{
    public class AddressWriteRepository : WriteRepository<Address>, IAddressWriteRepository
    {
        public AddressWriteRepository(B2B_ProjectDbContext context) : base(context)
        {
        }
    }
}
