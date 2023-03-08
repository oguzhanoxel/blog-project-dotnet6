using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Repositories
{
	public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken>
	{

	}
}
