using B2B_Project.Application.Common.Models;
using B2B_Project.Application.DTOs.Basket;
using MediatR;

namespace B2B_Project.Application.Features.Basket.Commands
{
    public class AddProductToBasketCommandRequest: IRequest<HandlerResponse< AddProductToBasketCommandResponse>>
    {
        public string Username { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
