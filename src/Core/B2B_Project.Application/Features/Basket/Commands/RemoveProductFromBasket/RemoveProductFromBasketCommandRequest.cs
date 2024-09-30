using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Basket.Commands.RemoveProductFromBasket
{
    public class RemoveProductFromBasketCommandRequest : IRequest<HandlerResponse<RemoveProductFromBasketCommandResponse>>
    {
        public string UserId { get; set; } = default!;
        public string ProductId { get; set; } = default!;
    }
}
