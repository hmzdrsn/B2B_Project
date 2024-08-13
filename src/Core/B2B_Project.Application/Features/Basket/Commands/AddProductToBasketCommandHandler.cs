using B2B_Project.Application.Common.Models;
using B2B_Project.Application.DTOs.Basket;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Basket.Commands
{

    public class AddProductToBasketCommandHandler : IRequestHandler<AddProductToBasketCommandRequest, HandlerResponse<AddProductToBasketCommandResponse>>
    {
        private readonly IBasketService _basketService;

        public AddProductToBasketCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<HandlerResponse<AddProductToBasketCommandResponse>> Handle(AddProductToBasketCommandRequest request, CancellationToken cancellationToken)
        {
            AddProductToBasket model = new()
            {
                Username = request.Username,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
            };

            if (await _basketService.AddProductToBasket(model))
            {
                return new()
                {
                    Message = "Product Added To Basket"
                };
            }
            return new()
            {
                Message = "Error"
            };
        }
    }
}
