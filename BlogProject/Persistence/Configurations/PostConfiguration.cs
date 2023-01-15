using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
	internal sealed class PostConfiguration : IEntityTypeConfiguration<Post>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Post> builder)
		{
			builder.ToTable("Posts").HasKey(post => post.Id);

			builder.Property(post => post.Title).HasMaxLength(100);
			builder.Property(post => post.Text).IsRequired();

			builder.HasMany(post => post.PostCategories)
				.WithOne()
				.HasForeignKey(postCategory => postCategory.PostId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
