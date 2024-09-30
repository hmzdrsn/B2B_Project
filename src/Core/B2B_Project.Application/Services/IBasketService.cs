using B2B_Project.Application.DTOs.Basket;
using B2B_Project.Application.Features.Basket.Commands.IncreaseProductQuantityFromBasket;
using B2B_Project.Application.Features.Basket.Commands.ReduceProductQuantityFromBasket;
using B2B_Project.Application.Features.Basket.Commands.RemoveProductFromBasket;
using B2B_Project.Application.Features.Basket.Queries;
using B2B_Project.Application.Features.Basket.Queries.GetBasketItemsByUsername;
using B2B_Project.Domain.Entities;

namespace B2B_Project.Application.Services
{
    public interface IBasketService
    {
        Task<Basket?> GetBasketByUsername(GetBasketByUsernameQueryRequest req);
        Task<bool> AddProductToBasket(AddProductToBasket model);
        Task<GetBasketItemsByUsernameQueryResponse> GetBasketItemsByUser(GetBasketItemsByUsernameQueryRequest request);

        Task<bool> RemoveProductFromBasket(RemoveProductFromBasketCommandRequest request);
        Task<bool> ReduceProductQuantityFromBasket(ReduceProductQuantityFromBasketRequest request);
        Task<bool> IncreaseProductQuantityFromBasket(IncreaseProductQuantityFromBasketCommandRequest request);
    }
}
