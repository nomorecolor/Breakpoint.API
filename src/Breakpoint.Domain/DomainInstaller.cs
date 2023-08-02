using Breakpoint.Domain.Models;
using Breakpoint.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Breakpoint.Domain
{
	public static class DomainInstaller
	{
		public static IServiceCollection AddDomain(this IServiceCollection services)
		{
			services.AddDbContext<BreakpointContext>(opt => opt.UseInMemoryDatabase(databaseName: "Breakpoint"));

			services.AddTransient<IAuthRepository, AuthRepository>();

			services.AddTransient<ILaptopRepository, LaptopRepository>();
			services.AddTransient<IUserRepository, UserRepository>();

			return services;
		}
	}
}
