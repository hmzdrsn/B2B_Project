﻿using B2B_Project.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.Features.Product.Queries
{
    public class GetAllProductQueryRequest : IRequest<HandlerResponse<GetAllProductQueryResponse>>
    {
    }
}
