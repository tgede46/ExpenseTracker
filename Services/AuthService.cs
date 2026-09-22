using ExpenseTracker.Models;
using ExpenseTracker.Repositories;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTracker.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        
        public AuthService(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<object> RegisterAsync(
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

            return new
            {
                id = user.Id,   
                fullName = user.FullName,
                email = user.Email,
            };
        }

                public async Task<object> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
        
            if (user == null ||
                !await _userManager.CheckPasswordAsync(user, password))
            {
                return new
                {
                    message = "Invalid email or password."
                };
            }
        
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.UserName!)
            };
        
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        
            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);
        
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(_configuration["Jwt:DurationInMinutes"]!)),
                signingCredentials: credentials);
        
            return new
            {
                id = user.Id,
                fullName = user.FullName,
                email = user.Email,
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public Task LogoutAsync()
        {
            return Task.CompletedTask;
        }
    }
}