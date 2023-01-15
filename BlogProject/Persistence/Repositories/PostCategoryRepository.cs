using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
	internal sealed class PostCategoryRepository : IPostCategoryRepository
	{
		private readonly RepositoryDbContext _dbContext;

		public PostCategoryRepository(RepositoryDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<PostCategory>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _dbContext.PostCategories.ToListAsync(cancellationToken);
		}

		public async Task<PostCategory> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			return await _dbContext.PostCategories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		}

		public void Insert(PostCategory postCategory)
		{
			_dbContext.Add(postCategory);
		}

		public void Remove(PostCategory postCategory)
		{
			_dbContext.Remove(postCategory);
		}
	}
}


