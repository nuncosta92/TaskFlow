using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Interfaces;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string name, string email, string password)
        {
            try
            {
                var user = await _userService.RegisterAsync(name, email, password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userService.LoginAsync(email, password);
            if (user == null)
            {
                return Unauthorized(new {message = "Invalid username or password." });
            }

            // For now, just returning user. Later we will generate JWT token.
            return Ok(new { token = user });
        }
    }
}
