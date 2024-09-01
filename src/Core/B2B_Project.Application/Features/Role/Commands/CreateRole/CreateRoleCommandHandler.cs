using B2B_Project.Application.Common.Models;
using B2B_Project.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace B2B_Project.Application.Features.Role.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, HandlerResponse<CreateRoleCommandResponse>>
    {
        private readonly RoleManager<AppRole> _roleManager;

        public CreateRoleCommandHandler(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<HandlerResponse<CreateRoleCommandResponse>> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _roleManager.CreateAsync(new()
            {
                Name = request.RoleName
            });

            if (result.Succeeded)
            {
                return new()
                {
                    Message = "Role Created Successfully"
                };
            }
            return new()
            {
                Message = "Error Occurred Role Creating!"
            };
        }
    }
}
