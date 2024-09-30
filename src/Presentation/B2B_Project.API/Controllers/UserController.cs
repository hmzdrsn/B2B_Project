using B2B_Project.Application.Features.User.Commands.CreateUser;
using B2B_Project.Application.Features.User.Commands.LoginUser;
using B2B_Project.Application.Features.User.Queries;
using B2B_Project.Application.Features.User.Queries.GetUserShortProperties;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            GetAllUserQueryRequest req = new GetAllUserQueryRequest();
            var response = await _mediator.Send(req);
            return Ok(response);
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetUserShortProperties()
        {
            GetUserShortPropertiesQueryRequest req = new();
            req.Username = User.FindFirstValue(ClaimTypes.Name);
            var response = await _mediator.Send(req);
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
