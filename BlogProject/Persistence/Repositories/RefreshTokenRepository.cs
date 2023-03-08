using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
	public class RefreshTokenRepository : EfRepositoryBase<RefreshToken, RepositoryDbContext>, IRefreshTokenRepository
	{
		public RefreshTokenRepository(RepositoryDbContext context) : base(context)
		{

		}
	}
}
