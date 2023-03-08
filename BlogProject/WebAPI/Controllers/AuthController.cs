using Core.Security.Dtos;
using Core.Security.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Commands.AuthCommands.Login;
using Services.Commands.AuthCommands.Register;
using Services.Dtos.AuthDtos;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AuthController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
		{
			RegisterCommand registerCommand = new()
			{
				UserForRegisterDto = userForRegisterDto,
				IpAddress = GetIpAddress()
			};

			RegisteredDto result = await _mediator.Send(registerCommand);
			SetRefreshTokenToCookie(result.RefreshToken);
			return Created("", result.AccessToken);
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
		{
			LoginCommand loginCommand = new()
			{
				UserForLoginDto = userForLoginDto,
				IpAddress = GetIpAddress()
			};

			LoggedInDto result = await _mediator.Send(loginCommand);
			SetRefreshTokenToCookie(result.RefreshToken);
			return Ok(result.AccessToken);
		}

		private void SetRefreshTokenToCookie(RefreshToken refreshToken)
		{
			CookieOptions cookieOptions = new() { HttpOnly = true, };
			Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
		}

		private string? GetIpAddress()
		{
			if (Request.Headers.ContainsKey("X-Forwarded-For")) return Request.Headers["X-Forwarded-For"];
			return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
		}
	}
}
