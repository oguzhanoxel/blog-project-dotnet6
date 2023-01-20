using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
	public sealed class RepositoryDbContext : DbContext
	{
		public RepositoryDbContext(DbContextOptions options) : base(options)
		{
			
		}

		public DbSet<Post>? Posts { get; set; }
		public DbSet<Category>? Categories { get; set; }
		public DbSet<PostCategory>? PostCategories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);
		}
	}
}
