using Breakpoint.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Breakpoint.Domain.Repositories
{
	public interface IUserRepository
	{
		Task<IEnumerable<User>> GetAll();
		Task<User?> GetById(int id);
		Task Add(User user);
		Task Update(int id, User user);
		Task Delete(int id);
	}

	public class UserRepository : IUserRepository
	{
		private readonly BreakpointContext _context;

		public UserRepository(BreakpointContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<User>> GetAll()
		{
			return await _context.Users
				.Where(x => !x.IsDeleted)
				.ToListAsync();
		}

		public async Task<User?> GetById(int id)
		{
			return await _context.Users.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
		}

		public async Task Add(User user)
		{
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
		}

		public async Task Update(int id, User user)
		{
			var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

			if (existingUser != null)
			{
				existingUser.FirstName = user.FirstName;
				existingUser.LastName = user.LastName;
			}

			await _context.SaveChangesAsync();
		}

		public async Task Delete(int id)
		{
			var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

			if (existingUser != null)
			{
				existingUser.IsDeleted = true;
			}

			await _context.SaveChangesAsync();
		}
	}
}
