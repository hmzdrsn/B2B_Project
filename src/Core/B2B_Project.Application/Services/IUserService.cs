using B2B_Project.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(CreateUser model);
        Task<List<GetAllUser>> GetAllUserAsync();
    }
}
