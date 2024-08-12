using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Basket.Queries
{
    public class GetBasketByUsernameQueryHandler : IRequestHandler<GetBasketByUsernameQueryRequest, HandlerResponse<GetBasketByUsernameQueryResponse>>
    {
        private readonly IBasketService _basketService;

        public GetBasketByUsernameQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<HandlerResponse<GetBasketByUsernameQueryResponse>> Handle(GetBasketByUsernameQueryRequest request, CancellationToken cancellationToken)
        {
            var basket = await _basketService.GetBasketByUsername(request.Username);

            if(basket == null)
            {
                return new()
                {
                    Message="null amk"
                };
            }

            GetBasketByUsernameQueryResponse response = new()
            {
                Basket = new() { BasketId = basket.Id, Status = basket.Status },
            };

            return new()
            {
                Data = response
            };
        }
    }
}
