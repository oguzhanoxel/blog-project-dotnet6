using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Repositories;

namespace BlogProject.Tests.TestSetup.TestDb
{
	public class PostCategoryRepository : EfRepositoryBase<PostCategory, TestDbContext>, IPostCategoryRepository
	{
		public PostCategoryRepository(TestDbContext context) : base(context)
		{

		}
	}
}
