using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.User.Commands.CreateUser
{
    public class CreateUserCommandRequest : IRequest<HandlerResponse<CreateUserCommandResponse>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
