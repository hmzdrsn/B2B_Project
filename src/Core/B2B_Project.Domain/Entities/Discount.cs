using B2B_Project.Domain.Common;

namespace B2B_Project.Domain.Entities
{
    public class Discount : BaseEntity
    {
        public string DiscountCode { get; set; } = default!;
        public double DiscountRate { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public int MaxUsagePerUser { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = default!;
    }
}
