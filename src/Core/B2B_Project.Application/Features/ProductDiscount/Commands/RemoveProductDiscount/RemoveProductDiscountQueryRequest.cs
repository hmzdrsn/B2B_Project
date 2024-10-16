
using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.ProductDiscount.Commands.RemoveProductDiscount
{
    public class RemoveProductDiscountQueryRequest : IRequest<HandlerResponse<RemoveProductDiscountQueryResponse>>
    {
        public string DiscountId { get; set; } = default!;
        public string ProductId { get; set; } = default!;
    }
}
