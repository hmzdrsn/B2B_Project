using B2B_Project.Application.Services.Token;
using B2B_Project.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace B2B_Project.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private IConfiguration _configuration;
        private RoleManager<AppRole> _roleManager;
        private UserManager<AppUser> _userManager;
        public TokenHandler(IConfiguration configuration, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public Application.DTOs.Token CreateAccesToken(int second, AppUser user)
        {
            List<Claim> claimList = new List<Claim>();
            var roleList = _userManager.GetRolesAsync(user).Result.ToList();
            claimList.Add(new(ClaimTypes.Name, user.UserName));
            if (roleList.Any())
            {
                foreach (var role in roleList)
                {
                    claimList.Add(new(ClaimTypes.Role, role));
                }
            }

            Application.DTOs.Token token = new();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.AddSeconds(second);

            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claimList
                );
            JwtSecurityTokenHandler tokenHandler = new();

            token.AccessToken = tokenHandler.WriteToken(securityToken);

            token.RefreshToken = CreateRefreshToken();

            return token;
        }
        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
