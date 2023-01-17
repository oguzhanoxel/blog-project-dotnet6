using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
	public class PostCategoryRepository : EfRepositoryBase<PostCategory, RepositoryDbContext>, IPostCategoryRepository
	{
		public PostCategoryRepository(RepositoryDbContext context) : base(context)
		{
			
		}
	}
}


