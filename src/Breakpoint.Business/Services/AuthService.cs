using Breakpoint.Contracts;
using Breakpoint.Domain.Models;
using Breakpoint.Domain.Repositories;
using Breakpoint.Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Breakpoint.Business.Services
{
	public interface IAuthService
	{
		Task<string> GenerateAccessToken(AuthDto authDto);
		Task<bool> ValidateLogin(AuthDto authDto);
	}

	public class AuthService : IAuthService
	{
		private readonly IAuthRepository _authRepository;
		private readonly IBreakpointSettings _breakpointSettings;

		public AuthService(IAuthRepository authRepository, IBreakpointSettings breakpointSettings)
		{
			_authRepository = authRepository;
			_breakpointSettings = breakpointSettings;
		}

		public async Task<string> GenerateAccessToken(AuthDto authDto)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_breakpointSettings.Jwt.Key!));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(expires: DateTime.Now.AddMinutes(15),
				claims: new List<Claim> { new Claim(ClaimTypes.NameIdentifier, authDto.Username!) },
			  signingCredentials: credentials);

			return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
		}

		public async Task<bool> ValidateLogin(AuthDto authDto)
		{
			var user = new User
			{
				Username = authDto.Username,
				Password = authDto.Password
			};

			return await _authRepository.ValidateLogin(user);
		}
	}
}
