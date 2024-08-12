using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.DTOs.Basket
{
    public class GetBasketByUsername
    {
        public Guid BasketId { get; set; }
        public string? Status { get; set; }
    }
}
