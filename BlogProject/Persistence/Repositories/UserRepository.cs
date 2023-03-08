using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
	public class UserRepository : EfRepositoryBase<User, RepositoryDbContext>, IUserRepository
	{
		public UserRepository(RepositoryDbContext context) : base(context)
		{

		}
	}
}
