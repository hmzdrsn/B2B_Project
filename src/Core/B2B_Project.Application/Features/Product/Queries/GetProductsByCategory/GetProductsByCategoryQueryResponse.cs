using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.Features.Product.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryQueryResponse
    {
        public List<DTOs.Product.GetProductsByCategoryDto>? Products { get; set; }
    }
}
