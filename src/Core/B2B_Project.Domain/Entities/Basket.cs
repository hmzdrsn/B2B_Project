using B2B_Project.Domain.Common;
using B2B_Project.Domain.Identity;

namespace B2B_Project.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public AppUser? AppUser { get; set; }
        public string? AppUserId { get; set; }
        public string? Status { get; set; }
        public ICollection<BasketItem>? BasketItems { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
