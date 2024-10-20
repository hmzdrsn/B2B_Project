﻿using B2B_Project.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.Services
{
    public interface IRoleService
    {
        Task<bool> AssignRoleAsync(string userId, string roleId);
        Task<List<AppRole>> GetAllRole();
    }
}
