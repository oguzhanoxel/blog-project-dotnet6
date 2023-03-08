using Core.CrossCuttingConcers.Exceptions;
using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services.Pipelines.Authorization
{
	public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>, ISecuredRequest
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			List<string>? roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

			if (roleClaims == null) throw new AuthorizationException("Claims not found.");

			bool isNotMatchedRoleClaimWithRequestRoles = roleClaims
				.FirstOrDefault(roleClaim => request.Roles.Any(role => role == roleClaim))
				.IsNullOrEmpty();
			if (isNotMatchedRoleClaimWithRequestRoles) throw new AuthorizationException("You are not authorized.");

			TResponse response = await next();
			return response;
		}
	}
}
