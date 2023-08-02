using Breakpoint.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Breakpoint.Business
{
	public static class ServiceInstaller
	{
		public static IServiceCollection AddService(this IServiceCollection services)
		{
			services.AddTransient<ILaptopService, LaptopService>();
			services.AddTransient<IUserService, UserService>();

			return services;
		}
	}
}
