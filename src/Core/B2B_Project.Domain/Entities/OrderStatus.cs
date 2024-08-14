using B2B_Project.Domain.Common;

namespace B2B_Project.Domain.Entities
{
    public class OrderStatus : BaseEntity
    {
        public string Status { get; set; }
        ICollection<Order>? Orders { get; set; }
    }
}
