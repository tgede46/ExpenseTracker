using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? password { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}