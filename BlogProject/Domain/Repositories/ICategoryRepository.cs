using Domain.Entities;

namespace Domain.Repositories
{
	public interface ICategoryRepository
	{
		Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		void Insert(Category category);
		void Remove(Category category);
	}
}
