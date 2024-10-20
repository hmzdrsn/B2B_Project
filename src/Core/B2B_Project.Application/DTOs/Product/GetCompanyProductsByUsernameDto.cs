﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.DTOs.Product
{
    public class GetCompanyProductsByUsernameDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
    }
}
