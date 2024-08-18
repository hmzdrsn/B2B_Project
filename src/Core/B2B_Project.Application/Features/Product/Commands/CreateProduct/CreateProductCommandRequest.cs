using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
