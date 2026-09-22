using ExpenseTracker.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        public record RegisterRequest(
            string FullName,
            string Email,
            string Password
        );

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request.FullName, request.Email, request.Password);
            return Ok(result);
        }

        public record LoginRequest(
            string Email,
            string Password
        );
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request.Email, request.Password);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new
            {
                message = "Logout successful. Delete the token on the client."
            });
        }
    }
}