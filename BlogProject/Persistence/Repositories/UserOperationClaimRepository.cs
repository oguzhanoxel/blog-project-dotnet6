using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
	public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, RepositoryDbContext>, IUserOperationClaimRepository
	{
		public UserOperationClaimRepository(RepositoryDbContext context) : base(context)
		{

		}
	}
}
