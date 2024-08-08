using B2B_Project.Application.DTOs;
using B2B_Project.Application.Services;
using B2B_Project.Application.Services.Token;
using B2B_Project.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace B2B_Project.Persistance.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<Token> LoginAsync(string username, string password, int accessTokenLifeTime)
        {
            AppUser user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return new()
                {
                    AccessToken = "",
                };
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccesToken(accessTokenLifeTime, user);
                return token;
            }
            return new()
            {
                AccessToken = "Auth Error!",
            };
        }
    }
}