using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.User.Queries.GetUserShortProperties
{
    public class GetUserShortPropertiesQueryHandler : IRequestHandler<GetUserShortPropertiesQueryRequest, HandlerResponse<List<GetUserShortPropertiesQueryResponse>>>
    {
        private readonly IUserService _userService;

        public GetUserShortPropertiesQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<HandlerResponse<List<GetUserShortPropertiesQueryResponse>>> Handle(GetUserShortPropertiesQueryRequest request, CancellationToken cancellationToken)
        {
            var userList = await _userService.GetUserShortProperties(request);
            return new()
            {
                Data = userList
            };
        }
    }
}
