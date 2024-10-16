using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.ProductDiscount.Commands.RemoveProductDiscountById
{
    public class RemoveProductDiscountByIdCommandRequest : IRequest<HandlerResponse<RemoveProductDiscountByIdCommandResponse>>
    {
        public string ProductDiscountId { get; set; } = default!;
    }
}
