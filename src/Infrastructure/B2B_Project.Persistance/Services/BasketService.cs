using B2B_Project.Application.DTOs.Basket;
using B2B_Project.Application.Repositories;
using B2B_Project.Application.Services;
using B2B_Project.Domain.Entities;
using B2B_Project.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Persistance.Services
{
    public class BasketService : IBasketService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly IBasketWriteRepository _basketWriteRepository;
        private readonly IBasketItemWriteRepository _basketItemWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        public BasketService(UserManager<AppUser> userManager, IBasketReadRepository basketReadRepository, IProductReadRepository productReadRepository, IBasketItemWriteRepository basketItemWriteRepository)
        {
            _userManager = userManager;
            _basketReadRepository = basketReadRepository;
            _productReadRepository = productReadRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
        }

        public async Task<bool> AddProductToBasket(AddProductToBasket model)
        {
            // Kullanıcıyı bul
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return false;
            }

            // Kullanıcının sepetini bul
            var basket = await _basketReadRepository.Table
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(x => x.AppUserId == user.Id);

            if (basket == null)
            {
                // Kullanıcının henüz bir sepeti yoksa, yeni bir sepet oluştur
                Basket newBasket = new Basket
                {
                    AppUserId = user.Id,
                    Status = "Aktif",
                    BasketItems = new List<BasketItem>()
                };

                var result = await _basketWriteRepository.AddAsync(newBasket);
                await _basketItemWriteRepository.SaveAsync();
                if (result)
                {
                    // Sepete yeni bir ürün ekle
                    BasketItem basketItem = new BasketItem
                    {
                        ProductId = model.ProductId,
                        BasketId = newBasket.Id,
                        Quantity = model.Quantity
                    };

                    await _basketItemWriteRepository.AddAsync(basketItem);
                    await _basketItemWriteRepository.SaveAsync();
                    return true;
                }
            }
            else
            {
                // Sepetteki ürünleri bul
                var existingBasketItem = basket.BasketItems
                    .FirstOrDefault(bi => bi.ProductId == model.ProductId);

                if (existingBasketItem != null)
                {
                    // Ürün zaten sepette varsa, miktarı artır
                    existingBasketItem.Quantity += model.Quantity;
                    //_basketItemWriteRepository.Update(existingBasketItem);
                    await _basketItemWriteRepository.SaveAsync();
                }
                else
                {
                    // Ürün sepette yoksa, yeni bir sepet ürünü oluştur
                    BasketItem basketItem = new BasketItem
                    {
                        ProductId = model.ProductId,
                        BasketId = basket.Id,
                        Quantity = model.Quantity
                    };

                    await _basketItemWriteRepository.AddAsync(basketItem);
                    await _basketItemWriteRepository.SaveAsync();
                }

                return true;
            }

            return false;
        }

        public async Task<Basket?> GetBasketByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return null;
            }

            var userBasket = await _basketReadRepository
                .Table
                .Include(x=>x.BasketItems)
                .FirstOrDefaultAsync(x => x.AppUserId == user.Id);

            if (userBasket == null)
            {
                return null;
            }

            return userBasket;
        }
    }
}
