using Core.CrossCuttingConcers.Exceptions;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Domain.Repositories;
using Services.Constants;

namespace Services.Rules
{
	public class AuthBusinessRules
	{
		private readonly IUserRepository _userRepository;

		public AuthBusinessRules(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task WrongEmailOrPasswordWhenLoggedIn(UserForLoginDto userForLoginDto)
		{
			User user = await _userRepository.GetAsync(u => u.Email == userForLoginDto.Email);
			if (user == null && !HashingHelper
				.VerifyPasswordHash(userForLoginDto.Password, user.PasswordHash, user.PasswordSalt))
				throw new BusinessException(ExceptionConstants.EmailOrPasswordWrong);
		}

		public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
		{
			var user = await _userRepository.GetAsync(user => user.Email == email);
			if (user is null) throw new BusinessException(ExceptionConstants.EmailAlreadyExists);
		}
	}
}
