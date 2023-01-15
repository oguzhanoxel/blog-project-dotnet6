using Domain.Entities;

namespace Domain.Repositories
{
	public interface IPostRepository
	{
		Task<IEnumerable<Post>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<Post> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		void Insert(Post post);
		void Remove(Post post);
	}
}
