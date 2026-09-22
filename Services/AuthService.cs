using ExpenseTracker.Repositories;

namespace ExpenseTracker.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthService _authService;
        
        public AuthService(IAuthService authService)
        {
            _authService = authService;
        }
        
        public async Task<string> RegisterAsync(string fullName, string email, string password)
        {
            return await _authService.RegisterAsync(fullName, email, password);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            return await _authService.LoginAsync(email, password);
        }

        public async Task LogoutAsync()
        {
            await _authService.LogoutAsync();
        }
    }
}