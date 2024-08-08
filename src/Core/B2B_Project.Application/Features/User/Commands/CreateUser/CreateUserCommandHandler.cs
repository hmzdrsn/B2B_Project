using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.User.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, HandlerResponse<CreateUserCommandResponse>>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<HandlerResponse<CreateUserCommandResponse>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            if (await _userService.CreateUserAsync(new()
            {
                Username = request.Username,
                Name = request.Name,
                Surname = request.Surname,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword
            }))
            {
                CreateUserCommandResponse response = new()
                {
                    Name = request.Name,
                    UserName = request.Username,
                };
                return new()
                {
                    Message = "Kullanıcı Oluşturuldu",
                    Status = "Created",
                    Data = response
                };
            }
            return new()
            {
                Message = "Kullanıcı oluşturulurken bir hata ile karşılaşıldı.",
            };
        }
    }
}
