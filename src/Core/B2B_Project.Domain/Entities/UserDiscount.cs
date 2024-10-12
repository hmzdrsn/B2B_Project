using B2B_Project.Domain.Common;
using B2B_Project.Domain.Identity;

namespace B2B_Project.Domain.Entities
{
    public class UserDiscount : BaseEntity
    {
        public int UsageCount { get; set; }
        public string AppUserId { get; set; } = default!;
        public AppUser User { get; set; } = default!;
        public Guid DiscountId { get; set; }
        public Discount Discount { get; set; } = default!;
    }

}
