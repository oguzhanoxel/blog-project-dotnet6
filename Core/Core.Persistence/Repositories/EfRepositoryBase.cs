using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.Repositories
{
	public class EfRepositoryBase<TEntity, TContext> : IAsyncRepository<TEntity>
	where TEntity : Entity
	where TContext : DbContext
	{
		private readonly TContext _context;

		public EfRepositoryBase(TContext context)
		{
			_context = context;
		}

		public async Task<TEntity> CreateAsync(TEntity entity)
		{
			_context.Entry(entity).State = EntityState.Added;
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task DeleteAsync(TEntity entity)
		{
			_context.Entry(entity).State = EntityState.Deleted;
			await _context.SaveChangesAsync();
		}

		public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
		{
			return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
		}

		public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
		{
			IQueryable<TEntity> queryable = _context.Set<TEntity>();
			if(predicate is not null) queryable.Where(predicate);
			return await queryable.ToListAsync();
		}
	}
}
