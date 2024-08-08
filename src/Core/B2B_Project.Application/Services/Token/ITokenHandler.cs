using B2B_Project.Application.DTOs;
using B2B_Project.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.Services.Token
{
    public interface ITokenHandler
    {
        DTOs.Token CreateAccesToken(int second, AppUser user);
    }
}
