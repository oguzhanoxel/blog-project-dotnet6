using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
	public class RepositoryDbContext : DbContext
	{
		public RepositoryDbContext(DbContextOptions options) : base(options)
		{
			
		}

		public DbSet<Post>? Posts { get; set; }
		public DbSet<Category>? Categories { get; set; }
		public DbSet<PostCategory>? PostCategories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostCategory>()
				.HasKey(pc => pc.Id);

			modelBuilder.Entity<PostCategory>()
				.HasOne(pc => pc.Post)
				.WithMany(p => p.PostCategories)
				.HasForeignKey(pc => pc.PostId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<PostCategory>()
				.HasOne(pc => pc.Category)
				.WithMany(c => c.PostCategories)
				.HasForeignKey(pc => pc.CategoryId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
