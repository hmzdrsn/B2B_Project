using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Role.Commands.GetAllRole
{

    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQueryRequest, HandlerResponse<GetAllRoleQueryResponse>>
    {
        private readonly IRoleService _roleService;

        public GetAllRoleQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<HandlerResponse<GetAllRoleQueryResponse>> Handle(GetAllRoleQueryRequest request, CancellationToken cancellationToken)
        {
            var roleList = await _roleService.GetAllRole();
            GetAllRoleQueryResponse res = new()
            {
                Roles = roleList
            };
            return new()
            {
                Data = res,
            };
        }
    }
}
