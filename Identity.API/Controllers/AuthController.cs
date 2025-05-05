using Identity.Application.DTOs;
using Identity.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            if (!result) return BadRequest("User already exists.");
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var success = await _authService.LoginAsync(dto);
            if (!success) return Unauthorized();
            return Ok("Login successful.");
        }
    }
}
