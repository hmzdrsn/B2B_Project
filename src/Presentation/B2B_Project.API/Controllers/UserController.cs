using B2B_Project.Application.Features.User.Commands.CreateUser;
using B2B_Project.Application.Features.User.Commands.LoginUser;
using B2B_Project.Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace B2B_Project.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            GetAllUserQueryRequest getAllUserQueryRequest = new GetAllUserQueryRequest();
            var response =await _mediator.Send(getAllUserQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
