using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;


namespace ExpenseTracker.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }
    }
}