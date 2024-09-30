using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.User.Queries.GetUserShortProperties
{
    public class GetUserShortPropertiesQueryRequest : IRequest<HandlerResponse<List<GetUserShortPropertiesQueryResponse>>>
    {
        public string Username { get; set; } = default!;

    }
}
