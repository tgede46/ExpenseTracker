namespace ExpenseTracker.Dto
{
    public record CategoryCreateDto(
        string Name,
        string? Description
    );

    public record CategoryUpdateDto(
        string Name,
        string? Description
    );

    public record CategoryResponseDto(
        int Id,
        string Name,
        string? Description
    );
}