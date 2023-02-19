using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Persistence.Repositories
{
	public interface IAsyncRepository<T>
	where T : Entity
	{
		Task<IEnumerable<T>> GetListAsync(
			Expression<Func<T,bool>>? predicate = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
			CancellationToken cancellationToken = default);
		Task<T?> GetAsync(
			Expression<Func<T,bool>> predicate,
			Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
			CancellationToken cancellationToken = default);
		Task<T> CreateAsync(T entity);
		Task<T> UpdateAsync(T entity);
		Task DeleteAsync(T entity);
	}
}
