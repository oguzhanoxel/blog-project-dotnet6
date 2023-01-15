using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
	internal sealed class PostCategoryConfiguration : IEntityTypeConfiguration<PostCategory>
	{
		public void Configure(EntityTypeBuilder<PostCategory> builder)
		{
			builder.ToTable("PostCategories").HasKey(postCategory => postCategory.Id);

			builder.Property(postCategory => postCategory.PostId).IsRequired();
			builder.Property(postCategory => postCategory.CategoryId).IsRequired();
		}
	}
}
