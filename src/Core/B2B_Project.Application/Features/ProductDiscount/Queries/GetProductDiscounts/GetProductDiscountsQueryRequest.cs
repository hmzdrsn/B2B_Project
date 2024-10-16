
using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.ProductDiscount.Queries.GetProductDiscounts
{
    public class GetProductDiscountsQueryRequest : IRequest<HandlerResponse<List<GetProductDiscountsQueryResponse>>>
    {
        public string Username { get; set; } = default!;
    }
}
