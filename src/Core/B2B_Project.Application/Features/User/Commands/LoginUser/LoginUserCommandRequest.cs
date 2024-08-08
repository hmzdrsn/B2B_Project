using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.User.Commands.LoginUser
{
    public class LoginUserCommandRequest: IRequest<HandlerResponse< LoginUserCommandResponse>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
