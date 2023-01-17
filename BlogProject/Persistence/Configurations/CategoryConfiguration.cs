using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
	internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("Categories").HasKey(category => category.Id);

			builder.Property(category => category.Title).HasMaxLength(100);
			builder.Property(category => category.Description).IsRequired();

			builder.HasMany(category => category.PostCategories)
			.WithOne()
			.HasForeignKey(p => p.CategoryId)
			.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
