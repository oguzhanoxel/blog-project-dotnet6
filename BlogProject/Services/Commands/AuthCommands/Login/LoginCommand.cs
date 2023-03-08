using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Repositories;
using MediatR;
using Services.AuthService;
using Services.Dtos.AuthDtos;
using Services.Rules;

namespace Services.Commands.AuthCommands.Login
{
	public class LoginCommand : IRequest<LoggedInDto>
	{
		public UserForLoginDto UserForLoginDto { get; set; }
		public string IpAddress { get; set; }

		public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedInDto>
		{
			private readonly AuthBusinessRules _authBusinessRules;
			private readonly IUserRepository _userRepository;
			private readonly IAuthService _authService;

			public LoginCommandHandler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, IAuthService authService)
			{
				_authBusinessRules = authBusinessRules;
				_userRepository = userRepository;
				_authService = authService;
			}

			public async Task<LoggedInDto> Handle(LoginCommand request, CancellationToken cancellationToken)
			{
				await _authBusinessRules.WrongEmailOrPasswordWhenLoggedIn(request.UserForLoginDto);

				User user = await _userRepository.GetAsync(user => user.Email == request.UserForLoginDto.Email);

				AccessToken createdAccessToken = await _authService.CreateAccessToken(user);
				RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
				RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

				LoggedInDto loggedInDto = new()
				{
					RefreshToken = addedRefreshToken,
					AccessToken = createdAccessToken
				};

				return loggedInDto;
			}
		}
	}
}
