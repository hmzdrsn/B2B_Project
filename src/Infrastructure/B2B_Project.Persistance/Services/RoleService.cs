using B2B_Project.Application.Services;
using B2B_Project.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Persistance.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public RoleService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> AssignRoleAsync(string userId,string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return false;
            }

            IdentityResult result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<List<AppRole>> GetAllRole()
        {
            return await _roleManager.Roles.ToListAsync();
        }
    }
}
