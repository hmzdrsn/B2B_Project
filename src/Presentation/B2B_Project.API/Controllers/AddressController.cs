using B2B_Project.Application.Features.Address.Commands.CreateAddress;
using B2B_Project.Application.Features.Address.Commands.SetAddressStatus;
using B2B_Project.Application.Features.Address.Queries.GetUserAddress;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace B2B_Project.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetUserAddresses()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            GetUserAddressesQueryRequest req = new();
            req.Username = username;
            var res = await _mediator.Send(req);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddres(CreateAddressCommandRequest req)
        {
            var res = await _mediator.Send(req);
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> SetAddressStatus([FromQuery] string addressId)
        {
            SetAddressStatusCommandRequest req = new();
            req.Username = User.FindFirstValue(ClaimTypes.Name);
            req.AddressId = addressId;
            var res = await _mediator.Send(req);
            return Ok(res);
        }

    }
}
