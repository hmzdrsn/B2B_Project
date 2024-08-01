using B2B_Project.Application.Repositories;
using B2B_Project.Domain.Entities;
using B2B_Project.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Persistance.Repositories
{
    public class OrderDetailReadRepository : ReadRepository<OrderDetail>, IOrderDetailReadRepository
    {
        public OrderDetailReadRepository(B2B_ProjectDbContext context) : base(context)
        {
        }
    }
}
