using Contracts.Dtos.PostCategoryDtos;

namespace Services.Abstractions
{
	public interface IPostCategoryService
	{
		Task<IEnumerable<PostCategoryListDto>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<PostCategoryDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		Task<PostCategoryDto> CreateAsync(PostCategoryCreateDto postCategoryCreateDto, CancellationToken cancellationToken = default);
		Task<PostCategoryDto> UpdateAsync(int id, PostCategoryUpdateDto postCategoryUpdateDto, CancellationToken cancellationToken = default);
		Task<PostCategoryDto> DeleteAsync(int id, CancellationToken cancellationToken = default);
	}
}
