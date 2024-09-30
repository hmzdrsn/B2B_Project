using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Basket.Commands.ReduceProductQuantityFromBasket
{
    public class ReduceProductQuantityFromBasketHandler : IRequestHandler<ReduceProductQuantityFromBasketRequest, HandlerResponse<ReduceProductQuantityFromBasketResponse>>
    {
        private readonly IBasketService _basketService;

        public ReduceProductQuantityFromBasketHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<HandlerResponse<ReduceProductQuantityFromBasketResponse>> Handle(ReduceProductQuantityFromBasketRequest request, CancellationToken cancellationToken)
        {
            if (await _basketService.ReduceProductQuantityFromBasket(request))
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
