using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.DTOs.User
{
    public class GetAllUser
    {
        public string Username { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
