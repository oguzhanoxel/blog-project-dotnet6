using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Tests.TestSetup.TestDb
{
	public class TestDbContext : DbContext
	{
		public DbSet<Post> Posts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase(databaseName: "TestDb");
		}
	}
}
