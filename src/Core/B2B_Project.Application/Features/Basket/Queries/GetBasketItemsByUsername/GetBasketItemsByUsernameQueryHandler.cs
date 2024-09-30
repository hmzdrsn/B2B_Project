using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Basket.Queries.GetBasketItemsByUsername
{
    public class GetBasketItemsByUsernameQueryHandler : IRequestHandler<GetBasketItemsByUsernameQueryRequest, HandlerResponse<GetBasketItemsByUsernameQueryResponse>>
    {
        private readonly IBasketService _basketService;

        public GetBasketItemsByUsernameQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<HandlerResponse<GetBasketItemsByUsernameQueryResponse>> Handle(GetBasketItemsByUsernameQueryRequest request, CancellationToken cancellationToken)
        {
            var res = await _basketService.GetBasketItemsByUser(request);
            if (res == null)
            {
                return new()
                {
                    Message = "Basket Items Error!"
                };
            }
            return new()
            {
                Data = res
            };
        }
    }

}
