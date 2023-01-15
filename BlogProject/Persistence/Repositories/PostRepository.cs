using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
	internal sealed class PostRepository : IPostRepository
	{
		private readonly RepositoryDbContext _dbContext;

		public PostRepository(RepositoryDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Post>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _dbContext.Posts.Include(x => x.PostCategories).ToListAsync(cancellationToken);
		}

		public async Task<Post> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Posts.Include(x => x.PostCategories).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		}

		public void Insert(Post post)
		{
			_dbContext.Posts.Add(post);
		}

		public void Remove(Post post)
		{
			_dbContext.Posts.Remove(post);
		}
	}
}
