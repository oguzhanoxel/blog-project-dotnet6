using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Repositories;

namespace BlogProject.Tests.TestSetup.TestDb
{
	public class CategoryRepository : EfRepositoryBase<Category, TestDbContext>, ICategoryRepository
	{
		public CategoryRepository(TestDbContext context) : base(context)
		{

		}
	}
}
