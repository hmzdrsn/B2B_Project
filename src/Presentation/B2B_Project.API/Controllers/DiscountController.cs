using B2B_Project.API.Models;
using B2B_Project.Application.Features.Discount.Commands.CreateDiscount;
using B2B_Project.Application.Features.Discount.Commands.RemoveDiscount;
using B2B_Project.Application.Features.Discount.Queries.GetCompanyDiscounts;
using B2B_Project.Application.Features.ProductDiscount.Commands.AssignDiscountToProduct;
using B2B_Project.Application.Features.ProductDiscount.Commands.RemoveProductDiscount;
using B2B_Project.Application.Features.ProductDiscount.Commands.RemoveProductDiscountById;
using B2B_Project.Application.Features.ProductDiscount.Queries.GetProductDiscount;
using B2B_Project.Application.Features.ProductDiscount.Queries.GetProductDiscounts;
using B2B_Project.Application.Features.UserDiscount.Commands.AssignDiscountToUser;
using B2B_Project.Application.Features.UserDiscount.Commands.RemoveUserDiscount;
using B2B_Project.Application.Features.UserDiscount.Queries.GetUserDiscounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace B2B_Project.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DiscountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanyDiscounts()
        {
            GetCompanyDiscountsQueryRequest req = new()
            {
                Username = User.FindFirstValue(ClaimTypes.Name),
            };
            var response = await _mediator.Send(req);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateDiscountDto request, CancellationToken cancellationToken)
        {
            CreateDiscountCommandRequest model = new()
            {
                DiscountCode = request.DiscountCode,
                DiscountAmount = request.DiscountAmount,
                isPercentage = request.IsPercentage,
                MaxUsagePerUser = request.MaxUsagePerUser,
                Username = User.FindFirstValue(ClaimTypes.Name),
                ValidFrom = request.ValidFrom,
                ValidUntil = request.ValidUntil,
            };

            var response = await _mediator.Send(model, cancellationToken);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveDiscount([FromQuery] string discountId)
        {
            RemoveDiscountCommandRequest req = new()
            {
                DiscountId = discountId
            };
            var response = await _mediator.Send(req);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AssignDiscountToProduct(string productId, string discountId)
        {
            AssignDiscountToProductCommandRequest req = new();
            req.ProductId = productId;
            req.DiscountId = discountId;
            req.Username = User.FindFirstValue(ClaimTypes.Name);
            var response = await _mediator.Send(req);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductDiscount([FromQuery] GetProductDiscountQueryRequest req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveProductDiscount(RemoveProductDiscountQueryRequest req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AssignDiscountToUser(string usernameTo, string discountId)
        {
            AssignDiscountToUserQueryRequest req = new()
            {
                UsernameTo = usernameTo,
                DiscountId = discountId,
                Username = User.FindFirstValue(ClaimTypes.Name)
            };
            var response = await _mediator.Send(req);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserDiscounts()
        {
            GetUserDiscountsQueryRequest req = new()
            {
                Username = User.FindFirstValue(ClaimTypes.Name)
            };
            var res = await _mediator.Send(req);
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveUserDiscount(string userDiscountId)
        {
            RemoveUserDiscountQueryRequest req = new()
            {
                UserDiscountId = userDiscountId
            };
            var res = await _mediator.Send(req);
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveProductDiscountById(string productDiscountId)
        {
            RemoveProductDiscountByIdCommandRequest req = new()
            {
                ProductDiscountId = productDiscountId
            };
            var res = await _mediator.Send(req);
            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductDiscounts()
        {
            GetProductDiscountsQueryRequest req = new()
            {
                Username = User.FindFirstValue(ClaimTypes.Name)
            };
            var res = await _mediator.Send(req);
            return Ok(res);
        }
    }
}