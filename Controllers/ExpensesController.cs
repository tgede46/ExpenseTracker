using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Models;
using ExpenseTracker.Repositories;
using ExpenseTracker.Dto;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // GET: api/expenses
        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            var userId = User.FindFirst("sub")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var expenses = await _expenseService.GetAllExpensesAsync(userId);
            return Ok(expenses);
        }

        // GET: api/expenses/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            var userId = User.FindFirst("sub")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var expense = await _expenseService.GetExpenseByIdAsync(id, userId);
            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        public record ExpenseCreateDto(
            string Title,
            string Description,
            decimal Price,
            DateTime Date
        );
        // POST: api/expenses
        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] ExpenseCreateDto expenseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst("sub")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var expense = new Expense
            {
                Title = expenseDto.Title,
                Description = expenseDto.Description,
                Price = expenseDto.Price,
                Date = expenseDto.Date,
                UserId = userId
            };

            var createdExpense = await _expenseService.CreateExpenseAsync(expense);
            return CreatedAtAction(nameof(GetExpenseById), new { id = createdExpense.Id }, createdExpense);
        }

        public record ExpenseUpdateDto(
            string Title,
            string Description,
            decimal Price,
            DateTime Date
        );
        // PUT: api/expenses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, [FromBody] ExpenseUpdateDto expenseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
    }

            var userId = User.FindFirst("sub")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var expense = await _expenseService.GetExpenseByIdAsync(id, userId);
            if (expense == null)
            {
                return NotFound();
            }

            expense.Title = expenseDto.Title;
            expense.Description = expenseDto.Description;
            expense.Price = expenseDto.Price;
            expense.Date = expenseDto.Date;

            var updatedExpense = await _expenseService.UpdateExpenseAsync(expense, userId);
            return Ok(updatedExpense);
        }
    }

}