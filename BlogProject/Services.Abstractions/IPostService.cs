using Contracts.Dtos.PostDtos;

namespace Services.Abstractions
{
	public interface IPostService
	{
		Task<IEnumerable<PostListDto>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<PostDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		Task<PostDto> CreateAsync(PostCreateDto postCreateDto, CancellationToken cancellationToken = default);
		Task<PostDto> UpdateAsync(int id, PostUpdateDto postUpdateDto, CancellationToken cancellationToken = default);
		Task<PostDto> DeleteAsync(int id, CancellationToken cancellationToken = default);
	}
}
