using B2B_Project.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Domain.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public AppUser()
        {
            Id = Guid.NewGuid().ToString();
        }
        public override string Id { get => base.Id; set => base.Id = value; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
