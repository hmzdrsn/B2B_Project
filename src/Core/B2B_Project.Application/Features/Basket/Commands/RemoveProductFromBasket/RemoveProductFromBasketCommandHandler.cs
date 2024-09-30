using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Basket.Commands.RemoveProductFromBasket
{
    public class RemoveProductFromBasketCommandHandler : IRequestHandler<RemoveProductFromBasketCommandRequest, HandlerResponse<RemoveProductFromBasketCommandResponse>>
    {
        private readonly IBasketService _basketService;

        public RemoveProductFromBasketCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<HandlerResponse<RemoveProductFromBasketCommandResponse>> Handle(RemoveProductFromBasketCommandRequest request, CancellationToken cancellationToken)
        {
            if (await _basketService.RemoveProductFromBasket(request))
            {
                return new()
                {
                    Message = "Product Successfully Deleted from Basket"
                };
            }
            return new()
            {
                Message = "An Error Occurred While Deleting the Product!"
            };
        }
    }
}
