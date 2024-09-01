using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Role.Commands.AssignRole
{
    public class AssignRoleCommandRequest : IRequest<HandlerResponse<AssignRoleCommandResponse>>
    {
        public string UserId { get; set; } = default!;
        public string RoleId { get; set; } = default!;
    }
}
