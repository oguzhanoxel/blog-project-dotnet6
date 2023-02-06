using BlogProject.Tests.TestSetup.TestDb;

namespace BlogProject.Tests.TestSetup
{
	public class CommonTestFixture
	{
		public TestDbContext Context { get; set; }

		public CommonTestFixture()
		{
			Context = new TestDbContext();

			Context.AddPosts();
			Context.AddCategories();
			Context.AddPostCategories();
			Context.SaveChanges();
		}
	}	
}
