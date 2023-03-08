using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Repositories
{
	public interface IUserRepository : IAsyncRepository<User>
	{

	}
}
