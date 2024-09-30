using B2B_Project.Application.DTOs.User;
using B2B_Project.Application.Features.User.Queries.GetUserShortProperties;
using B2B_Project.Application.Services;
using B2B_Project.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Persistance.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> CreateUserAsync(CreateUser model)
        {
            var result = await _userManager.CreateAsync(new()
            {
                UserName = model.Username,
                Name = model.Name,
                Surname = model.Surname,
            }, model.Password);
            if (result.Succeeded)
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

        public async Task<List<GetUserShortPropertiesQueryResponse>> GetUserShortProperties(GetUserShortPropertiesQueryRequest req)
        {
            var user = await _userManager.FindByNameAsync(req.Username);
            if (user == null)
            {
                return null;
            }
            var res = await _userManager.Users
                .Where(x => x.Id != user.Id)
                .Select(x => new GetUserShortPropertiesQueryResponse()
                {
                    UserId = x.Id,
                    FullName = x.Name + " " + x.Surname,
                    IsOnline = x.IsOnline,
                })
                .ToListAsync();
            return res;
        }
    }
}
