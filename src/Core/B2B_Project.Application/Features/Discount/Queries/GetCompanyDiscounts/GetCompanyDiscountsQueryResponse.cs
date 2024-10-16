
namespace B2B_Project.Application.Features.Discount.Queries.GetCompanyDiscounts
{
    public class GetCompanyDiscountsQueryResponse
    {
        public string DiscountId { get; set; } = default!;
        public string DiscountCode { get; set; } = default!;
        public double DiscountAmount { get; set; }
        public bool isPercentage { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public int MaxUsagePerUser { get; set; }
        public Guid CompanyId { get; set; }
    }
}
