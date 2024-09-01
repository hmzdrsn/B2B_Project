using Microsoft.AspNetCore.Identity;

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
