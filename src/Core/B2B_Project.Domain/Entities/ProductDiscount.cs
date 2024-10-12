using B2B_Project.Domain.Common;

namespace B2B_Project.Domain.Entities
{
    public class ProductDiscount : BaseEntity
    {
        public Guid DiscountId { get; set; }
        public Discount Discount { get; set; } = default!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;
    }

}
