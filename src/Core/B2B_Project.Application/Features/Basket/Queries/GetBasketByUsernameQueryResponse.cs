using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.Features.Basket.Queries
{
    public class GetBasketByUsernameQueryResponse
    {
        public DTOs.Basket.GetBasketByUsername? Basket { get; set; }
    }
}
