using BlogProject.Tests.TestSetup.TestDb;
using Domain.Entities;

namespace BlogProject.Tests.TestSetup
{
	public static class PostCategories
	{
		public static void AddPostCategories(this TestDbContext context)
		{
			context.PostCategories.AddRange(
				new PostCategory { PostId = 1, CategoryId = 1 },
				new PostCategory { PostId = 2, CategoryId = 2 },
				new PostCategory { PostId = 3, CategoryId = 2 }
			);
		}
	}
}
