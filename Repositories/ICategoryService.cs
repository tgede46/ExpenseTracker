using ExpenseTracker.Dto;

namespace ExpenseTracker.Repositories
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> GetAllAsync(string userId);
        Task<CategoryResponseDto?> GetByIdAsync(int id, string userId);
        Task<CategoryResponseDto> CreateAsync(
            CategoryCreateDto dto,
            string userId);
        Task<CategoryResponseDto?> UpdateAsync(
            int id,
            CategoryUpdateDto dto,
            string userId);
        Task<bool> DeleteAsync(int id, string userId);
    }
}