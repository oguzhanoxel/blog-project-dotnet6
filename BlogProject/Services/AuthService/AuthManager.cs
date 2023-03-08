using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Services.AuthService
{
	public class AuthManager : IAuthService
	{
		private readonly IUserOperationClaimRepository _userOperationClaimRepository;
		private readonly IRefreshTokenRepository _refreshTokenRepository;
		private readonly ITokenHelper _tokenHelper;

		public AuthManager(IUserOperationClaimRepository userOperationClaimRepository, ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository)
		{
			_userOperationClaimRepository = userOperationClaimRepository;
			_refreshTokenRepository = refreshTokenRepository;
			_tokenHelper = tokenHelper;
		}

		public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
		{
			RefreshToken addedRefreshToken = await _refreshTokenRepository.CreateAsync(refreshToken);
			return addedRefreshToken;
		}

		public async Task<AccessToken> CreateAccessToken(User user)
		{
			IEnumerable<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id, include: u => u.Include(u => u.OperationClaim));

			IList<OperationClaim> operationClaims =
				userOperationClaims.Select(u => new OperationClaim
				{ Id = u.OperationClaim.Id, Name = u.OperationClaim.Name }).ToList();

			AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
			return accessToken;
		}

		public async Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
		{
			RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
			return await Task.FromResult(refreshToken);
		}
	}
}
