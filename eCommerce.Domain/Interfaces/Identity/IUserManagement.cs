using eCommerce.Domain.Entities.Identity;
using System.Security.Claims;

namespace eCommerce.Domain.Interfaces.Authentications
{
    public interface IUserManagement
    {
        Task<bool> CreateUserAsync(AppUser user);
        Task<bool> LoginUser(AppUser user);
        //Task<bool> LogoutUser(AppUser user);
        Task<AppUser?> GetUserByEmailAsync(string email);
        Task<AppUser> GetUserByIdAsync(string id);
        Task<IEnumerable<AppUser>?> GetUsersByEmailAsync();
        Task<int> RemoveUserByEmail(string email);
        //Task<int> RemoveUserById(string id);
        Task<List<Claim>> GetUserClaimsAsync(string email);

    } 
}
