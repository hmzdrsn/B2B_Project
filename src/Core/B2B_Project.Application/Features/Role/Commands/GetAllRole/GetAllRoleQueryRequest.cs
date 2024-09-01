using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Role.Commands.GetAllRole
{
    public class GetAllRoleQueryRequest: IRequest<HandlerResponse<GetAllRoleQueryResponse>>
    {
    }
}
