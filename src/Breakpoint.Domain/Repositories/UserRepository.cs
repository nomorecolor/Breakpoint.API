using Breakpoint.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Breakpoint.Domain.Repositories
{
	public interface IUserRepository
	{
		public Task<IEnumerable<User>> GetAll();
		public Task<User?> GetById(int id);
		public Task Add(User user);
		public Task Update(int id, User user);
		public Task Delete(int id);
	}

	public class UserRepository : IUserRepository
	{
		private readonly BreakpointContext _context;

		public UserRepository(BreakpointContext context)
		{
			_context = context;

			InitializeData();
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

		private void InitializeData()
		{
			if (!_context.Users.Any())
			{
				var users = new List<User> {
					new User {
						Id = 1,
						FirstName = "Admin",
						LastName = "Admin",
						Username = "admin",
						Password = "admin"
					},
					new User {
						Id = 2,
						FirstName = "First Name",
						LastName = "Last Name",
						Username = "first",
						Password = "first"
					}
				};

				_context.Users.AddRange(users);
				_context.SaveChanges();
			}
		}
	}
}
