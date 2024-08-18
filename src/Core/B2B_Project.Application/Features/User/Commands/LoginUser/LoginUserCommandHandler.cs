using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.User.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, HandlerResponse<LoginUserCommandResponse>>
    {
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<HandlerResponse<LoginUserCommandResponse>> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            LoginUserCommandResponse response = new()
            { Token = await _authService.LoginAsync(request.Username, request.Password, 15000) };
            return new()
            {
                Data = response,
            };
        }
    }
}
