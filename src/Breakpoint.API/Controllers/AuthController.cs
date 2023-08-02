using Breakpoint.Business.Services;
using Breakpoint.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Breakpoint.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost]
		public async Task<IActionResult> Authenticate([FromBody] AuthDto authDto)
		{
			if(await _authService.ValidateLogin(authDto))
			{
				var accessToken = await _authService.GenerateAccessToken(authDto);

				return Ok(accessToken);
			}

			return Unauthorized();
		}
	}
}
