using B2B_Project.Application.Features.Order.Commands.CreateOrder;
using B2B_Project.Application.Features.Order.Queries.GetOrdersByCompany;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace B2B_Project.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetOrdersByCompany()
        {
            GetOrdersByCompanyQueryRequest request = new();
            string user = User.FindFirstValue(ClaimTypes.Name);
            request.Username = user;
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }
    }
}
