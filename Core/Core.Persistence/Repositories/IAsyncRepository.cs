using System.Linq.Expressions;

namespace Core.Persistence.Repositories
{
	public interface IAsyncRepository<T>
	where T : Entity
	{
		Task<IEnumerable<T>> GetListAsync(Expression<Func<T,bool>>? predicate = null, CancellationToken cancellationToken = default);
		Task<T?> GetAsync(Expression<Func<T,bool>> predicate, CancellationToken cancellationToken = default);
		Task<T> CreateAsync(T entity);
		Task<T> UpdateAsync(T entity);
		Task<T> DeleteAsync(T entity);
	}
}
