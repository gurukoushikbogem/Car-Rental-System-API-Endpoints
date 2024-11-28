using CarRentalSystem.Models;
using CarRentalSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;
        public LoginController(UserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserModel user)
        {
            var result = _userService.RegisterUser(user);
            if (!result) return BadRequest("User already exists.");
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login( UserModel user)
        {
            var temp = _userService.GetUserByEmail(user.Email);
            if (temp == null) return BadRequest("User not found.");
            if (temp.Password != user.Password) return BadRequest("Invalid password.");
            var token = _userService.GenerateToken(temp, temp.Role);
            return Ok(token);
        }
    }
}
