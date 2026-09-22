namespace ExpenseTracker.Repositories
{
    public interface IAuthService
    {
        Task<object> RegisterAsync(string fullName, string email, string password);
        Task<object> LoginAsync(string email, string password);
        Task LogoutAsync();
    }
}