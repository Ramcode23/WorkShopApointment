using AppoimentsWorkShop.DTOs;
using AppoimentsWorkShop.Helpers;
using AppoimentsWorkShop.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppoimentsWorkShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly JwtSettings _jwtSettings;
        private readonly IUserHelper _userHelper;

        public AccountController(
            JwtSettings jwtSettings,
            IUserHelper userHelper)
        {

            _jwtSettings = jwtSettings;
            _userHelper = userHelper;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserLogins>> Login([FromBody] UserLogins userLogins)
        {
            try
            {
                var Token = new UserTokens();
                //var valid = Logins.Any(user => user.Name.Equals(userLogins.Password, StringComparison.OrdinalIgnoreCase));
                var valid = await _userHelper.PasswordSignInAsync(userLogins);


                if (valid.Succeeded)
                {
                    var user = await _userHelper.GetUserByEmailAsync(userLogins.UserName);
                    var rol = await _userHelper.GetRoleAsync(user);

                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = userLogins.UserName,
                        EmailId = userLogins.UserName,
                        Rol = rol[0].Value,
                        GuidId = Guid.NewGuid(),

                    },
                    _jwtSettings);
                }
                else
                {
                 
                    return BadRequest("Wrong password");
                }

               


                return Ok(new
                {

                    Token = Token,
                    Msj = "Welcome"
                });
            }
            catch (Exception ex)
            {

                throw new Exception("GetToken Error", ex);
            }

        }

        [HttpPost("register")]

        public async Task<ActionResult<UserLogins>> Register([FromBody] RegisterUser registeruser)
        {
            var Token = new UserTokens();
            var isExits = await _userHelper.GetUserByEmailAsync(registeruser.Email);
            if (isExits == null)
            {
                var rest = await _userHelper.RegisterUserAsync(registeruser,"client");
                var credencials = new UserLogins { Password = registeruser.Password };
                if (rest.Succeeded)
                {
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = registeruser.Email,
                        EmailId = registeruser.Email,
                        GuidId = Guid.NewGuid(),
                        Rol = "client"
                    },
                 _jwtSettings);
                    return Ok(new
                    {

                        Token = Token,
                        Msj ="Welcome"
                    });
                }
                else
                {
                    return BadRequest(rest.Errors);
                }
            }
            return BadRequest("Email aready exist");


        }

        [HttpPost("createmanager")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "workshop")]
        public async Task<ActionResult<UserLogins>> createAdmin([FromBody] RegisterUser registeruser)
        {
            var Token = new UserTokens();
            var isExits = await _userHelper.GetUserByEmailAsync(registeruser.Email);

            if (isExits == null)
            {
                var rest = await _userHelper.RegisterUserAsync(registeruser,"workshop");
                var credencials = new UserLogins { Password = registeruser.Password };
                if (rest.Succeeded)
                {
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = registeruser.Email,
                        EmailId = registeruser.Email,
                        GuidId = Guid.NewGuid(),
                        Rol = "Admin"

                    },
                 _jwtSettings);

                    return Ok(new
                    {

                        Token = Token,
                        Msj = "Welcome"
                    });
                }
                else
                {
                    return BadRequest(rest.Errors);
                }
            }
            return BadRequest("Email aready exist");


        }

    
    }

}


