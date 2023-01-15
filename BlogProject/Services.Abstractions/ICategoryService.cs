using Contracts.Dtos.CategoryDtos;

namespace Services.Abstractions
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryListDto>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<CategoryDto> GetByIdAsync(int Id, CancellationToken cancellationToken = default);
		Task<CategoryDto> CreateAsync(CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken = default);
		Task<CategoryDto> UpdateAsync(int Id, CategoryUpdateDto categoryUpdateDto, CancellationToken cancellationToken = default);
		Task<CategoryDto> DeleteAsync(int Id, CancellationToken cancellationToken = default);
	}
}
