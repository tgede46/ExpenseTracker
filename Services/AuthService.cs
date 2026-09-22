using ExpenseTracker.Models;
using ExpenseTracker.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> RegisterAsync(
            string fullName,
            string email,
            string password)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return string.Join(
                    ", ",
                    result.Errors.Select(error => error.Description));
            }

            return "User registered successfully.";
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null ||
                !await _userManager.CheckPasswordAsync(user, password))
            {
                return "Invalid email or password.";
            }

            return "Login successful.";
        }

        public Task LogoutAsync()
        {
            return Task.CompletedTask;
        }
    }
}