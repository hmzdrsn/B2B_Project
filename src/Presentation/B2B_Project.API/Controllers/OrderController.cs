using B2B_Project.Application.Features.Order.Commands.CreateOrder;
using B2B_Project.Application.Features.Order.Commands.UpdateOrderStatus;
using B2B_Project.Application.Features.Order.Queries.GetOrdersByCompany;
using B2B_Project.Application.Features.Order.Queries.GetOrderWithDetailsById;
using B2B_Project.Application.Features.Order.Queries.GetUserOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        public async Task<IActionResult> GetUserOrders()
        {
            GetUserOrdersQueryRequest request = new();
            string user = User.FindFirstValue(ClaimTypes.Name);
            request.Username = user;
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderById([FromQuery] GetOrderWithDetailsByIdQueryRequest request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            CreateOrderCommandRequest request = new();
            request.Username = User.FindFirstValue(ClaimTypes.Name);
            request.OrderStatusId = "8C2606C8-2F37-4872-8734-E99C78FBE824";
            var res = await _mediator.Send(request);
            return Ok(res);
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(UpdateOrderStatusCommandRequest request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

    }
}
