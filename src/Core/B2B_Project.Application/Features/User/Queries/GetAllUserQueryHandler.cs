using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace B2B_Project.Application.Features.User.Queries
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQueryRequest, HandlerResponse<GetAllUserQueryResponse>>
    {
        private readonly IUserService _userService;

        public GetAllUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<HandlerResponse<GetAllUserQueryResponse>> Handle(GetAllUserQueryRequest request, CancellationToken cancellationToken)
        {
            GetAllUserQueryResponse response = new()
            {
                Users = await _userService.GetAllUserAsync()
            };
            return new()
            {
                Data = response,
            };
        }
    }
}
