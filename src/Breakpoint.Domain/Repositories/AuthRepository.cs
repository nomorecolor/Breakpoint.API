using Breakpoint.Domain.Models;

namespace Breakpoint.Domain.Repositories
{
	public interface IAuthRepository
	{
		Task<bool> ValidateLogin(User user);
	}
	public class AuthRepository : IAuthRepository
	{
		private readonly BreakpointContext _context;

		public AuthRepository(BreakpointContext context)
		{
			_context = context;
		}

		public async Task<bool> ValidateLogin(User user)
		{
			return await Task.FromResult(_context.Users.Any(x => x.Username == user.Username && x.Password == user.Password));
		}
	}
}
