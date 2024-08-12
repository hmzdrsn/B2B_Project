using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.Features.Product.Queries.GetProductsByCompany
{
    public class GetProductsByCompanyQueryResponse
    {
        public List<DTOs.Product.GetProductsByCompany>? Products{ get; set; }
    }
}
