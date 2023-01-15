using Domain.Entities;

namespace Domain.Repositories
{
	public interface IPostCategoryRepository
	{
		Task<IEnumerable<PostCategory>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<PostCategory> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		void Insert(PostCategory postCategory);
		void Remove(PostCategory postCategory);
	}
}
