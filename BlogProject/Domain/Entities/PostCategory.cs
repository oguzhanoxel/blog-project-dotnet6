using Core.Persistence.Repositories;

namespace Domain.Entities
{
	public class PostCategory : Entity
	{
		public int Id { get; set; }

		public int PostId { get; set; }
		public Post? Post { get; set; }
		public int CategoryId { get; set; }
		public Category? Category { get; set; }
	}
}
