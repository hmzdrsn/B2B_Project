using B2B_Project.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.Features.Order.Queries
{
    public class GetOrdersByCompanyQueryResponse
    {
        public List<GetOrdersByCompany>? Orders { get; set; }
    }

}
