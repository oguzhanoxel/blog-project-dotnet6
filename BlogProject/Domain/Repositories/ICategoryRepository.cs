using Core.Persistence.Repositories;
using Domain.Entities;

namespace Domain.Repositories
{
	public interface ICategoryRepository : IAsyncRepository<Category>
	{

	}
}
