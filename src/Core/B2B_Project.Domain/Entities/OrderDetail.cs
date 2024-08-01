using B2B_Project.Domain.Common;

namespace B2B_Project.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public string? Address { get; set; }
        public string? Description { get; set; }
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
        public Guid? BasketId { get; set; }
        public Basket? Basket { get; set; }
    }
}
