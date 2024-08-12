﻿using B2B_Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.Services
{
    public interface IBasketService
    {
        Task<Basket?> GetBasketByUsername(string username);
    }
}
