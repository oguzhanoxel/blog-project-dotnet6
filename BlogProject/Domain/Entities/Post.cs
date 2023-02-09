using Core.Persistence.Repositories;

namespace Domain.Entities
{
	public class Post : Entity
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Text { get; set; }

		public ICollection<PostCategory>? PostCategories { get; set; }
	}
}
