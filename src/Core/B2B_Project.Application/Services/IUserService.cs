using B2B_Project.Application.DTOs.User;
using B2B_Project.Application.Features.User.Queries.GetUserShortProperties;

namespace B2B_Project.Application.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(CreateUser model);
        Task<List<GetAllUser>> GetAllUserAsync();
        Task<List<GetUserShortPropertiesQueryResponse>> GetUserShortProperties(GetUserShortPropertiesQueryRequest request);
    }
}
