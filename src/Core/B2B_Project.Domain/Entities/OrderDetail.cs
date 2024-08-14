using B2B_Project.Domain.Common;

namespace B2B_Project.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int Quantity{ get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

    }
}
