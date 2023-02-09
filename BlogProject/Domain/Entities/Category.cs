using Core.Persistence.Repositories;

namespace Domain.Entities
{
	public class Category : Entity
	{
		public int Id { get; set; }
		public string? Title { get; set; }

		public ICollection<PostCategory>? PostCategories { get; set; }

	}
}
