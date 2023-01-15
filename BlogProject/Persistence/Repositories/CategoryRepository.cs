using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
	internal sealed class CategoryRepository : ICategoryRepository
	{
		private readonly RepositoryDbContext _dbContext;

		public CategoryRepository(RepositoryDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _dbContext.Categories.Include(x => x.PostCategories).ToListAsync(cancellationToken);
		}

		public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Categories.Include(x => x.PostCategories).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		}

		public void Insert(Category category)
		{
			_dbContext.Categories.Add(category);
		}

		public void Remove(Category category)
		{
			_dbContext.Categories.Remove(category);
		}
	}
}


