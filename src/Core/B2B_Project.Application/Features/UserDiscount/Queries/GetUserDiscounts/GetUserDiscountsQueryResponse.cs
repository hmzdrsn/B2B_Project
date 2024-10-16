
namespace B2B_Project.Application.Features.UserDiscount.Queries.GetUserDiscounts
{
    public class GetUserDiscountsQueryResponse
    {
        public string UserDiscountId { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string DiscountCode { get; set; } = default!;

    }
}
