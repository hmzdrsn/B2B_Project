using B2B_Project.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace B2B_Project.Application.Features.Product.Commands.CreateProduct
{

    public class CreateProductCommandRequest : IRequest<HandlerResponse<CreateProductCommandResponse>>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string ProductCode { get; set; } //
        public int? Stock { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? CompanyId { get; set; }
        public List<IFormFile>? ProductImages { get; set; }
    }
}
