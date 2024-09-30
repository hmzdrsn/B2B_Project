using B2B_Project.API.Models;
using B2B_Project.Application.Features.Basket.Commands.AddProductToBasket;
using B2B_Project.Application.Features.Basket.Commands.IncreaseProductQuantityFromBasket;
using B2B_Project.Application.Features.Basket.Commands.ReduceProductQuantityFromBasket;
using B2B_Project.Application.Features.Basket.Commands.RemoveProductFromBasket;
using B2B_Project.Application.Features.Basket.Queries;
using B2B_Project.Application.Features.Basket.Queries.GetBasketItemsByUsername;
using B2B_Project.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace B2B_Project.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BasketController(IMediator mediator, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> GetBasketByUsername(GetBasketByUsernameQueryRequest req)
        {
            var data = await _mediator.Send(req);
            return Ok(data);
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> AddProductToBasket(AddProductToBasketDto req)
        {
            AddProductToBasketCommandRequest model = new()
            {
                Username = User.FindFirstValue(ClaimTypes.Name),
                ProductId = req.ProductId,
                Quantity = req.Quantity,
            };
            var response = await _mediator.Send(model);
            return Ok(response);
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> RemoveProductFromBasket(string productId)
        {
            RemoveProductFromBasketCommandRequest req = new();
            req.UserId = User.FindFirstValue(ClaimTypes.Name);
            req.ProductId = productId;

            var response = await _mediator.Send(req);
            return Ok(response);
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> ReduceProductQuantityFromBasket(string productId)
        {
            ReduceProductQuantityFromBasketRequest req = new();
            req.UserId = User.FindFirstValue(ClaimTypes.Name);
            req.ProductId = productId;

            var response = await _mediator.Send(req);
            return Ok(response);
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> IncreaseProductQuantityFromBasket(string productId)
        {
            IncreaseProductQuantityFromBasketCommandRequest req = new();
            req.UserId = User.FindFirstValue(ClaimTypes.Name);
            req.ProductId = productId;

            var response = await _mediator.Send(req);
            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetBasketItemsByUsername()
        {
            GetBasketItemsByUsernameQueryRequest req = new();
            string user = User.FindFirstValue(ClaimTypes.Name);
            req.Username = user;
            var data = await _mediator.Send(req);
            return Ok(data);
        }
    }
}
