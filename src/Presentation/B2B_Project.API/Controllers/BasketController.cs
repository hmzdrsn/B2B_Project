using B2B_Project.API.Models;
using B2B_Project.Application.Features.Basket.Commands;
using B2B_Project.Application.Features.Basket.Queries;
using B2B_Project.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> GetBasketByUsername()
        {
            GetBasketByUsernameQueryRequest req = new();
            string user = User.FindFirstValue(ClaimTypes.Name);
            req.Username = user;
            var data =await _mediator.Send(req);
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
    }
}
