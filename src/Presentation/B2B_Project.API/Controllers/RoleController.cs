using B2B_Project.Application.Features.Role.Commands.AssignRole;
using B2B_Project.Application.Features.Role.Commands.CreateRole;
using B2B_Project.Application.Features.Role.Commands.GetAllRole;
using B2B_Project.Application.Features.Role.Queries.GetAllRole;
using B2B_Project.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace B2B_Project.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;
        public RoleController(IMediator mediator, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            GetAllRoleQueryRequest req = new GetAllRoleQueryRequest();
            var res = await _mediator.Send(req);
            return Ok(res);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUserRoles()
        {
            List<string> roles = new();
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(username);
                roles = _userManager.GetRolesAsync(user).Result.ToList();
            }
            return Ok(roles);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoleById([FromQuery] string roleId)
        {
            GetRoleByIdQueryRequest req = new GetRoleByIdQueryRequest();
            req.RoleId = roleId;
            var res = await _mediator.Send(req);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromQuery] string roleName)
        {
            CreateRoleCommandRequest req = new();
            req.RoleName = roleName;
            var res = await _mediator.Send(req);
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleCommandRequest req)
        {
            var res = await _mediator.Send(req);
            return Ok(res);
        }
    }
}
