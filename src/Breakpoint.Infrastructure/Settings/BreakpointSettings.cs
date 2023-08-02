using Microsoft.Extensions.Configuration;

namespace Breakpoint.Infrastructure.Settings
{
	public interface IBreakpointSettings
	{
		JwtSettings Jwt { get; }
	}

	public class BreakpointSettings : IBreakpointSettings
	{
		private readonly IConfiguration _configuration;

		public BreakpointSettings(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public JwtSettings Jwt => _configuration.GetSection(nameof(Jwt)).Get<JwtSettings>()!;
	}
}
