using AppoimentsWorkShop.DTOs;
using AppoimentsWorkShop.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AppoimentsWorkShop.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<IdentityResult> RegisterUserAsync(RegisterUser registeruser, string role);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<SignInResult> PasswordSignInAsync(UserLogins credentials);
        Task CheckRoleAsync(string roleName);
        Task<IList<Claim>> GetRoleAsync(User user);
    }
}
