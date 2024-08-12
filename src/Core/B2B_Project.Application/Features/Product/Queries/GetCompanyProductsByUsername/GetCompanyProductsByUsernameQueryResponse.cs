using B2B_Project.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.Features.Product.Queries.GetCompanyProductsByUsername
{
    public class GetCompanyProductsByUsernameQueryResponse
    {
        public List<DTOs.Product.GetCompanyProductsByUsername>? Products { get; set; }
    }
}
