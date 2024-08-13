using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.DTOs.Basket
{
    public class AddProductToBasket
    {
        public string Username { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
