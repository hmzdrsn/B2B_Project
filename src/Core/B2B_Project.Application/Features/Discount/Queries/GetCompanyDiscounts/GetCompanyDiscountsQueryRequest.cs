
using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Discount.Queries.GetCompanyDiscounts
{
    // 1. DeleteDiscountCommandRequest
    public class GetCompanyDiscountsQueryRequest : IRequest<HandlerResponse<List<GetCompanyDiscountsQueryResponse>>>
    {
        public string Username { get; set; } = default!;

    }
}
