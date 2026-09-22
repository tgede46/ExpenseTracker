using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
      public class Category
    {
        [Key]
        public int Id { get; set; }
    
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    
        public string UserId { get; set; } = string.Empty;
    
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
    
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }    
}