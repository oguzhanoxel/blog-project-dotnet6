using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
	public class OperationClaimRepository : EfRepositoryBase<OperationClaim, RepositoryDbContext>, IOperationClaimRepository
	{
		public OperationClaimRepository(RepositoryDbContext context) : base(context)
		{

		}
	}
}
