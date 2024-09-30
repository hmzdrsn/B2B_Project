using B2B_Project.Application;
using B2B_Project.Application.DTOs.Basket;
using B2B_Project.Application.Features.Basket.Commands.IncreaseProductQuantityFromBasket;
using B2B_Project.Application.Features.Basket.Commands.ReduceProductQuantityFromBasket;
using B2B_Project.Application.Features.Basket.Commands.RemoveProductFromBasket;
using B2B_Project.Application.Features.Basket.Queries;
using B2B_Project.Application.Features.Basket.Queries.GetBasketItemsByUsername;
using B2B_Project.Application.Repositories;
using B2B_Project.Application.Services;
using B2B_Project.Domain.Entities;
using B2B_Project.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Persistance.Services
{
    public class BasketService : IBasketService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly IBasketWriteRepository _basketWriteRepository;
        private readonly IBasketItemWriteRepository _basketItemWriteRepository;
        private readonly IBasketItemReadRepository _basketItemReadRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IImageReadRepository _imageReadRepository;
        public BasketService(UserManager<AppUser> userManager, IBasketReadRepository basketReadRepository, IProductReadRepository productReadRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketWriteRepository basketWriteRepository, IImageReadRepository imageReadRepository, IBasketItemReadRepository basketItemReadRepository)
        {
            _userManager = userManager;
            _basketReadRepository = basketReadRepository;
            _productReadRepository = productReadRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _basketWriteRepository = basketWriteRepository;
            _imageReadRepository = imageReadRepository;
            _basketItemReadRepository = basketItemReadRepository;
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
                    //BasketItems = new List<BasketItem>()
                };

                var result = await _basketWriteRepository.AddAsync(newBasket);
                await _basketWriteRepository.SaveAsync();
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

        public async Task<Basket?> GetBasketByUsername(GetBasketByUsernameQueryRequest req)
        {
            var user = await _userManager.FindByNameAsync(req.Username);
            if (user == null)
            {
                return null;
            }

            var userBasket = await _basketReadRepository
                .Table
                .Include(x => x.BasketItems)
                .FirstOrDefaultAsync(x => x.AppUserId == user.Id);

            if (userBasket == null)
            {
                return null;
            }

            return userBasket;
        }

        public async Task<GetBasketItemsByUsernameQueryResponse> GetBasketItemsByUser(GetBasketItemsByUsernameQueryRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return null;
            }

            var basket = await _basketReadRepository.Table
                .Include(x => x.BasketItems)
                .FirstOrDefaultAsync(x => x.AppUserId == user.Id);
            if (basket == null)
            {
                return null;
            }
            List<BasketItemProduct> basketItemProducts = new();
            decimal totalPrice = 0;
            foreach (var item in basket.BasketItems)
            {
                var product = await _productReadRepository.GetByIdAsync(item.ProductId.ToString());
                var image = await _imageReadRepository.Table.FirstOrDefaultAsync(x => x.EntityId == item.ProductId.ToString());
                if (product != null)
                {
                    BasketItemProduct model = new()
                    {
                        ProductId = product.Id.ToString(),
                        Price = product.Price,
                        ProductImageUrl = image != null ? image.ImageUrl : "",
                        ProductName = product.Name,
                        Quantity = item.Quantity
                    };
                    basketItemProducts.Add(model);
                    totalPrice += (decimal)(model.Price * model.Quantity);
                }
            }
            return new()
            {
                Products = basketItemProducts,
                TotalPrice = totalPrice
            };
        }

        public async Task<bool> ReduceProductQuantityFromBasket(ReduceProductQuantityFromBasketRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserId);
            if (user == null)
            {
                return false;
            }
            var basket = await _basketReadRepository.Table.FirstOrDefaultAsync(x => x.AppUserId == user.Id);
            if (basket == null)
            {
                return false;
            }

            var basketItem = await _basketItemReadRepository.Table
                .FirstOrDefaultAsync(x => x.ProductId.ToString() == request.ProductId);

            if (basketItem == null)
            {
                return false;
            }
            basketItem.Quantity--;

            if (basketItem.Quantity == 0)
            {
                var resRemove = await RemoveProductFromBasket(new() { ProductId = request.ProductId, UserId = user.UserName });
                return resRemove;
            }
            var res = _basketItemWriteRepository.Update(basketItem);
            if (res)
            {
                var resCount = await _basketItemWriteRepository.SaveAsync();
                if (resCount > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> IncreaseProductQuantityFromBasket(IncreaseProductQuantityFromBasketCommandRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserId);
            if (user == null)
            {
                return false;
            }
            var basket = await _basketReadRepository.Table.FirstOrDefaultAsync(x => x.AppUserId == user.Id);
            if (basket == null)
            {
                return false;
            }

            var basketItem = await _basketItemReadRepository.Table
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.ProductId.ToString() == request.ProductId);
            if (basketItem == null)
            {
                return false;
            }
            basketItem.Quantity++;

            if (basketItem.Quantity > basketItem.Product.Stock)
            {
                return false;
            }

            var res = _basketItemWriteRepository.Update(basketItem);
            if (res)
            {
                var resCount = await _basketItemWriteRepository.SaveAsync();
                if (resCount > 0)
                {
                    return true;
                }
            }
            return false;
        }


        public async Task<bool> RemoveProductFromBasket(RemoveProductFromBasketCommandRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserId);
            if (user == null)
            {
                return false;
            }
            var basket = await _basketReadRepository.Table.FirstOrDefaultAsync(x => x.AppUserId == user.Id);
            if (basket == null)
            {
                return false;
            }

            var basketItem = await _basketItemReadRepository.Table
                .FirstOrDefaultAsync(x => x.ProductId.ToString() == request.ProductId);
            if (basketItem == null)
            {
                return false;
            }

            var res = _basketItemWriteRepository.ForceDelete(basketItem);
            var count = await _basketItemWriteRepository.SaveAsync();
            if (count > 0 && res)
            {
                return true;
            }
            return false;
        }
    }
}
