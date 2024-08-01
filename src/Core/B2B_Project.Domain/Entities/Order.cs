using B2B_Project.Domain.Common;

namespace B2B_Project.Domain.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
