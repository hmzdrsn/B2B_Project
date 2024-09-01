using B2B_Project.Application.Common.Models;
using B2B_Project.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace B2B_Project.Application.Features.Role.Queries.GetAllRole
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQueryRequest, HandlerResponse<GetRoleByIdQueryResponse>>
    {
        private readonly RoleManager<AppRole> _roleManager;

        public GetRoleByIdQueryHandler(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<HandlerResponse<GetRoleByIdQueryResponse>> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId);
            if (role != null)
            {
                return new()
                {
                    Data = new() { RoleId = role.Id, Name = role.Name, NormalizedName = role.NormalizedName }
                };
            }
            return new()
            {
                Message = "Role not found!"
            };
        }
    }
}
