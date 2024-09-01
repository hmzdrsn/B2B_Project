using B2B_Project.Domain.Identity;

namespace B2B_Project.Application.Features.Role.Commands.GetAllRole
{
    public class GetAllRoleQueryResponse
    {
        public List<AppRole>? Roles { get; set; }
    }
}
