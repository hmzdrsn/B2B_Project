using B2B_Project.Domain.Common;
using B2B_Project.Domain.Identity;

namespace B2B_Project.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public Guid? AppUserId { get; set; }
        public string? Status { get; set; }
        public AppUser? AppUser { get; set; }
        ICollection<BasketItem>? BasketItems { get; set; }
        ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
