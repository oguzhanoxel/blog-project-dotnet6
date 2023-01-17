using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
	public class PostRepository : EfRepositoryBase<Post, RepositoryDbContext>, IPostRepository
	{
		public PostRepository(RepositoryDbContext context) : base(context)
		{
			
		}
	}
}
