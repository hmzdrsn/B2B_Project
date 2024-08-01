using B2B_Project.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        
        ICollection<BasketItem>? BasketItems { get; set; }
        ICollection<ProductAttribute>? ProductAttributes { get; set;}
    }
}
