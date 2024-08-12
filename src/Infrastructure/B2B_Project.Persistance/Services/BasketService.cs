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
        public BasketService(UserManager<AppUser> userManager, IBasketReadRepository basketReadRepository)
        {
            _userManager = userManager;
            _basketReadRepository = basketReadRepository;
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
