using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Repositories;
using MediatR;
using Services.AuthService;
using Services.Dtos.AuthDtos;
using Services.Rules;

namespace Services.Commands.AuthCommands.Register
{
	public class RegisterCommand : IRequest<RegisteredDto>
	{
		public UserForRegisterDto UserForRegisterDto { get; set; }
		public string IpAddress { get; set; }

		public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
		{
			private readonly IUserRepository _userRepository;
			private readonly AuthBusinessRules _authBusinessRules;
			private readonly IAuthService _authService;

			public RegisterCommandHandler(IUserRepository userRepository, AuthBusinessRules authBusinessRules, IAuthService authService)
			{
				_userRepository = userRepository;
				_authBusinessRules = authBusinessRules;
				_authService = authService;
			}

			public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
			{
				await _authBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.UserForRegisterDto.Email);

				byte[] passwordHash, passwordSalt;
				HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

				User newUser = new()
				{
					FirstName = request.UserForRegisterDto.FirstName,
					LastName = request.UserForRegisterDto.LastName,
					Email = request.UserForRegisterDto.Email,
					PasswordHash = passwordHash,
					PasswordSalt = passwordSalt,
					Status = true
				};

				User createdUser = await _userRepository.CreateAsync(newUser);

				AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);
				RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAddress);
				RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

				RegisteredDto registeredDto = new()
				{
					RefreshToken = addedRefreshToken,
					AccessToken = createdAccessToken,
				};

				return registeredDto;
			}
		}
	}
}
