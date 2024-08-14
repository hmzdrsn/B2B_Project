using B2B_Project.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.Features.Order.Queries.GetOrdersByCompany
{
    public class GetOrdersByCompanyQueryResponse
    {
        public List<DTOs.Order.GetOrdersByCompany>? Orders { get; set; }
    }

}
