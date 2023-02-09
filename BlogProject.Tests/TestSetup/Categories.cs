using BlogProject.Tests.TestSetup.TestDb;
using Domain.Entities;

namespace BlogProject.Tests.TestSetup
{
	public static class Categories
	{
		public static void AddCategories(this TestDbContext context)
		{
			context.Categories.AddRange(
				new Category { Title = "A category" },
				new Category { Title = "B category" },
				new Category { Title = "Empty category" }
			);
		}
	}
}
