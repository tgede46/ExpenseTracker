using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Dto
{
    public class ExpenseCreateDto
    {
        [Required(ErrorMessage = "Title is required.")]
       public string? Title { get; set; }
       [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }
    }
}