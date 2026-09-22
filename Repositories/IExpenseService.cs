using ExpenseTracker.Models;


namespace ExpenseTracker.Repositories
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetAllExpensesAsync(string userId);
        Task<Expense?> GetExpenseByIdAsync(int id, string userId);
        Task<Expense> CreateExpenseAsync(Expense expense);
        Task<Expense> UpdateExpenseAsync(Expense expense, string userId);
        Task<bool> DeleteExpenseAsync(int id, string userId);
    }
}