using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Role.Commands.CreateRole
{
    public class CreateRoleCommandRequest : IRequest<HandlerResponse<CreateRoleCommandResponse>>
    {
        public string RoleName { get; set; } = default!;
    }
}
