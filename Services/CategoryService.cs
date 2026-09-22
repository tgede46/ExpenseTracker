using ExpenseTracker.Data;
using ExpenseTracker.Dto;
using ExpenseTracker.Models;
using ExpenseTracker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync(
            string userId)
        {
            return await _context.Categories
                .Where(category => category.UserId == userId)
                .Select(category => new CategoryResponseDto(
                    category.Id,
                    category.Name,
                    category.Description))
                .ToListAsync();
        }

        public async Task<CategoryResponseDto?> GetByIdAsync(
            int id,
            string userId)
        {
            return await _context.Categories
                .Where(category =>
                    category.Id == id &&
                    category.UserId == userId)
                .Select(category => new CategoryResponseDto(
                    category.Id,
                    category.Name,
                    category.Description))
                .FirstOrDefaultAsync();
        }

        public async Task<CategoryResponseDto> CreateAsync(
            CategoryCreateDto dto,
            string userId)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                UserId = userId
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryResponseDto(
                category.Id,
                category.Name,
                category.Description);
        }

        public async Task<CategoryResponseDto?> UpdateAsync(
            int id,
            CategoryUpdateDto dto,
            string userId)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(category =>
                    category.Id == id &&
                    category.UserId == userId);

            if (category == null)
            {
                return null;
            }

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _context.SaveChangesAsync();

            return new CategoryResponseDto(
                category.Id,
                category.Name,
                category.Description);
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(category =>
                    category.Id == id &&
                    category.UserId == userId);

            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}