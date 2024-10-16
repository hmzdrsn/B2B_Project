
namespace B2B_Project.Application.Features.ProductDiscount.Queries.GetProductDiscounts
{
    public class GetProductDiscountsQueryResponse
    {
        public string ProductDiscountId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public string DiscountCode { get; set; } = default!;
    }
}
