using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces.Authentications;
using eCommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eCommerce.Infrastructure.Repositories.Authentication
{
    public class UserManagement(IRoleManagement roleManagement, UserManager<AppUser> userManager, AppDbContext context) : IUserManagement
    {
        public async Task<bool> CreateUserAsync(AppUser user)
        {
            var _user = await GetUserByEmailAsync(user.Email!);
            if (user != null) 
            {
                return false;
            }
            else
            {
                var result = await userManager.CreateAsync(user!, user!.PasswordHash!);
                if (result.Succeeded)
                {
                    await roleManagement.AddUserToRole(user, "User");
                    return true;
                }
                return false;
            }
        }

        public async Task<AppUser?> GetUserByEmailAsync(string email) => await userManager.FindByEmailAsync(email);

        public async Task<AppUser> GetUserByIdAsync(string id)
        {
           var user = await userManager.FindByIdAsync(id);
            return user!;
        }
        public async Task<List<Claim>> GetUserClaimsAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            string? roleName = await roleManagement.GetUserRole(email);
            var claims = new List<Claim>
            {
                new Claim("FullName", user!.fullName!),
                new Claim(ClaimTypes.NameIdentifier, user!.Id!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, roleName!)
            };
            return claims;
        }

        public async Task<IEnumerable<AppUser>?> GetUsersByEmailAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<bool> LoginUser(AppUser user)
        {
            var _user = await GetUserByEmailAsync(user.Email!);
            if(_user == null)
                return false;

            string roleName = await roleManagement.GetUserRole(user.Email!);
            if(string.IsNullOrEmpty(roleName))
                return false;

            return await userManager.CheckPasswordAsync(_user, user.PasswordHash!);
        }

        public async Task<int> RemoveUserByEmail(string email)
        {
            var _user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            context.Users.Remove(_user!);
            return await context.SaveChangesAsync();
        }
    }
}
