using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Basket.Commands.IncreaseProductQuantityFromBasket
{
    public class IncreaseProductQuantityFromBasketCommandHandler : IRequestHandler<IncreaseProductQuantityFromBasketCommandRequest, HandlerResponse<IncreaseProductQuantityFromBasketCommandResponse>>
    {
        private readonly IBasketService _basketService;

        public IncreaseProductQuantityFromBasketCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<HandlerResponse<IncreaseProductQuantityFromBasketCommandResponse>> Handle(IncreaseProductQuantityFromBasketCommandRequest request, CancellationToken cancellationToken)
        {
            if (await _basketService.IncreaseProductQuantityFromBasket(request))
            {
                return new()
                {
                    Message = "Quantity Successfully Updated!"
                };
            }
            return new()
            {
                Message = "An error ocurred while updating quantity!"
            };
        }
    }
}
