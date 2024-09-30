using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Basket.Commands.ReduceProductQuantityFromBasket
{
    public class ReduceProductQuantityFromBasketRequest : IRequest<HandlerResponse<ReduceProductQuantityFromBasketResponse>>
    {
        public string ProductId { get; set; } = default!;
        public string UserId { get; set; } = default!;
    }
}
