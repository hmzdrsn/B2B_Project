
namespace B2B_Project.Application.Features.ProductDiscount.Queries.GetProductDiscount
{
    public class GetProductDiscountQueryResponse
    {
        public string DiscountId { get; set; } = default!;
        public string DiscountCode { get; set; } = default!;
        public double DiscountAmount { get; set; } = default!;
        public bool isPercentage { get; set; }
    }
}
