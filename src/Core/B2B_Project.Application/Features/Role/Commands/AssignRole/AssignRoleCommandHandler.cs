using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Role.Commands.AssignRole
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommandRequest, HandlerResponse<AssignRoleCommandResponse>>
    {
        private readonly IRoleService _roleService;

        public AssignRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<HandlerResponse<AssignRoleCommandResponse>> Handle(AssignRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var res = await _roleService.AssignRoleAsync(request.UserId, request.RoleId);
            if (res)
                return new()
                {
                    Message = "Role Assigned"
                };
            return new()
            {
                Message = "Role Assign Error!"
            };
        }
    }
}
