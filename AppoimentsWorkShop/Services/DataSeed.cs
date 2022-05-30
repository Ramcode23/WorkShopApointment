using AppoimentsWorkShop.DTOs;
using AppoimentsWorkShop.Helpers;
using AppoimentsWorkShop.Models;

namespace AppoimentsWorkShop.Services
{
    public class DataSeed
    {
        private readonly IUserHelper _userHelper;
        public DataSeed(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await CheckRoles();
            await CheckWorkShopUser();
            await CheckClientUser();
        }

        private async Task CheckWorkShopUser()
        {
            var workshopUser = new User
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                FirstName = "Admin",
                LastName = "Admin",
                Password = "Pass1234@"
            };

            var userExits = await _userHelper.GetUserByEmailAsync(workshopUser.Email);
            if (userExits == null)
            {
                await _userHelper.RegisterUserAsync(new RegisterUser
                {
                    Email = workshopUser.Email,
                    FirstName = "User",
                    LastName = "Admin",
                    Password = workshopUser.Password

                }, "workshop");

                await _userHelper.AddUserToRoleAsync(workshopUser, "workshop");

            }

        }

        private async Task CheckClientUser()
        {
            var workshopUser = new User
            {
                UserName = "user@gmail.com",
                Email = "user@gmail.com",
                FirstName = "Test",
                LastName = "User",
                Password = "Pass1234@"
            };

            var userExits = await _userHelper.GetUserByEmailAsync(workshopUser.Email);
            if (userExits == null)
            {
                await _userHelper.RegisterUserAsync(new RegisterUser
                {
                    Email = workshopUser.Email,
                    FirstName = "Test",
                    LastName = "User",
                    Password = workshopUser.Password

                }, "workshop");

                await _userHelper.AddUserToRoleAsync(workshopUser, "client");

            }

        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("workshop");
            await _userHelper.CheckRoleAsync("client");
        }

    }
}