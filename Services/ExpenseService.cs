using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Repositories;
using Microsoft.EntityFrameworkCore;


namespace ExpenseTracker.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ApplicationDbContext _context;

        public ExpenseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync(string userId)
        {
            return await _context.Expenses.Where(e => e.UserId == userId).ToListAsync();
        }

        public async Task<Expense?> GetExpenseByIdAsync(int id, string userId)
        {
            return await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
        }

        public async Task<Expense> CreateExpenseAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<bool> UpdateExpenseAsync(Expense expense, string userId)
        {
            var existingExpense = await GetExpenseByIdAsync(expense.Id, userId);
            if (existingExpense == null) return false;

            existingExpense.Title = expense.Title;
            existingExpense.Description = expense.Description;
            existingExpense.Price = expense.Price;
            existingExpense.Date = expense.Date;

            _context.Expenses.Update(existingExpense);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteExpenseAsync(int id, string userId)
        {
            var expense = await GetExpenseByIdAsync(id, userId);
            if (expense == null) return false;

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}