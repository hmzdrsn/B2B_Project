using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.ProductDiscount.Queries.GetProductDiscount
{
    public class GetProductDiscountQueryRequest : IRequest<HandlerResponse<List<GetProductDiscountQueryResponse>>>
    {
        public string ProductId { get; set; } = default!;
    }
}
