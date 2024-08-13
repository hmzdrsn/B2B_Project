using B2B_Project.Application.DTOs.Basket;
using B2B_Project.Domain.Entities;

namespace B2B_Project.Application.Services
{
    public interface IBasketService
    {
        Task<Basket?> GetBasketByUsername(string username);
        Task<bool> AddProductToBasket(AddProductToBasket model);
    }
}
