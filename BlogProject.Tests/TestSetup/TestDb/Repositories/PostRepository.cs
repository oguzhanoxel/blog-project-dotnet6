using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Repositories;

namespace BlogProject.Tests.TestSetup.TestDb
{
	public class PostRepository : EfRepositoryBase<Post, TestDbContext>, IPostRepository
	{
		public PostRepository(TestDbContext context) : base(context)
		{

		}
	}
}
