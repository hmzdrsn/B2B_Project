using B2B_Project.Application.Repositories;
using B2B_Project.Domain.Entities;
using B2B_Project.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Persistance.Repositories
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(B2B_ProjectDbContext context) : base(context)
        {
        }

        public IQueryable<Product> GetProductsByCompany(Guid companyId)
        {
            return Table.Include(x => x.Company).Where(x => x.CompanyId == companyId);
        }
    }
}
