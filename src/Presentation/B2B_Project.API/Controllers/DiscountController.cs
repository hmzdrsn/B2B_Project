using B2B_Project.API.Models;
using B2B_Project.Application.Features.Discount.Commands.CreateDiscount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace B2B_Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DiscountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateDiscountDto request, CancellationToken cancellationToken)
        {
            CreateDiscountCommandRequest model = new()
            {
                DiscountCode = request.DiscountCode,
                DiscountRate = request.DiscountRate,
                MaxUsagePerUser = request.MaxUsagePerUser,
                Username = User.FindFirstValue(ClaimTypes.Name),
                ValidFrom = request.ValidFrom,
                ValidUntil = request.ValidUntil,
            };

            var response = await _mediator.Send(model, cancellationToken);
            return Ok(response);
        }
    }
}