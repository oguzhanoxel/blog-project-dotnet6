using Core.Security.Entities;
using Core.Security.JWT;

namespace Services.Dtos.AuthDtos
{
	public class RefreshedTokenDto
	{
		public AccessToken AccessToken { get; set; }
		public RefreshToken RefreshToken { get; set; }
	}
}
