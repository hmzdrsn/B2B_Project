
using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.ProductDiscount.Commands.AssignDiscountToProduct
{
    public class AssignDiscountToProductCommandRequest : IRequest<HandlerResponse<AssignDiscountToProductCommandResponse>>
    {
        public string Username { get; set; } = default!;
        public string ProductId { get; set; } = default!;
        public string DiscountId { get; set; } = default!;
    }
}
