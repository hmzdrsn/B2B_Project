using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using MediatR;
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
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string? CategoryId { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
