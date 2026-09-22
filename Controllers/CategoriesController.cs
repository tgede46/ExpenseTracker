using ExpenseTracker.Dto;
using ExpenseTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirst("sub")?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            return Ok(await _categoryService.GetAllAsync(userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = User.FindFirst("sub")?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var category = await _categoryService.GetByIdAsync(id, userId);

            return category == null
                ? NotFound()
                : Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto dto)
        {
            var userId = User.FindFirst("sub")?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var category = await _categoryService.CreateAsync(dto, userId);

            return CreatedAtAction(
                nameof(GetById),
                new { id = category.Id },
                category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            CategoryUpdateDto dto)
        {
            var userId = User.FindFirst("sub")?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var category =
                await _categoryService.UpdateAsync(id, dto, userId);

            return category == null
                ? NotFound()
                : Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirst("sub")?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var deleted = await _categoryService.DeleteAsync(id, userId);

            return deleted
                ? NoContent()
                : NotFound();
        }
    }
}