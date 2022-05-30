using AppoimentsWorkShop.DTOs;
using AppoimentsWorkShop.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AppoimentsWorkShop.Helpers
{
    public class UserHelper : IUserHelper
    {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ClaimsIdentity _claimsIdentity;
        private readonly IConfiguration _configuration;


        public UserHelper(
             UserManager<User> userManager,
         RoleManager<IdentityRole> roleManager,
         SignInManager<User> signInManager,
           IConfiguration configuration
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<IList<Claim>> GetRoleAsync(User user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
           return await _userManager.FindByNameAsync(email);
            
        }

        public async Task<SignInResult> PasswordSignInAsync(UserLogins credentials)
        {
           return await _signInManager.PasswordSignInAsync(credentials.UserName, credentials.Password, isPersistent: false, lockoutOnFailure: false);
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterUser registeruser,string role)
        {
            var user = new User
            {
                UserName = registeruser.Email,
                Email = registeruser.Email,
                FirstName = registeruser.FirstName,
                LastName = registeruser.LastName,
            };

            var rest = await _userManager.CreateAsync(user, registeruser.Password);
            if (rest.Succeeded)
                await _userManager.AddClaimAsync(user, new Claim("role", role));

            return rest;
        }
    }
}
