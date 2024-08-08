using B2B_Project.Application.DTOs.User;

namespace B2B_Project.Application.Services
{
    public interface IAuthService
    {
        Task<DTOs.Token> LoginAsync(string username, string password, int accessTokenLifeTime);
    }
}
