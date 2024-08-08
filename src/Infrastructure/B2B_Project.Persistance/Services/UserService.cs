using B2B_Project.Application.DTOs.User;
using B2B_Project.Application.Services;
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
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> CreateUserAsync(CreateUser model)
        {
            var result = await _userManager.CreateAsync(new()
            {
                UserName = model.Username,
                Name = model.Name,
                Surname = model.Surname,
            }, model.Password);
            if(result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<List<GetAllUser>> GetAllUserAsync()
        {
            var data = await _userManager.Users.Select(user => new GetAllUser()
            {
                Username = user.UserName,
                Name = user.Name,
                Email = user.Email,
            }).ToListAsync();
            return data;
        }
    }
}
