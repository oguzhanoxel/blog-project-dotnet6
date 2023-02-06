using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Tests.TestSetup.TestDb
{
	public class TestDbContext : DbContext
	{
		public DbSet<Post> Posts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<PostCategory> PostCategories { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase(databaseName: "TestDb");
		}
	}
}
