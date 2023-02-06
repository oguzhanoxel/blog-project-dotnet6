using BlogProject.Tests.TestSetup.TestDb;
using Domain.Entities;

namespace BlogProject.Tests.TestSetup
{
	public static class Categories
	{
		public static void AddCategories(this TestDbContext context)
		{
			context.Categories.AddRange(
				new Category { Title = "A category", Description = "a description"},
				new Category { Title = "B category", Description = "b description"},
				new Category { Title = "Empty category", Description = "empty description"}
			);
		}
	}
}
