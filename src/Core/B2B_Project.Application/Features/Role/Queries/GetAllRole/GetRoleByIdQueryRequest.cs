using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Role.Queries.GetAllRole
{
    public class GetRoleByIdQueryRequest : IRequest<HandlerResponse<GetRoleByIdQueryResponse>>
    {
        public string RoleId { get; set; } = default!;
    }
}
