using Microsoft.EntityFrameworkCore;

namespace Breakpoint.Domain.Models
{
	public class BreakpointContext : DbContext
	{
		public BreakpointContext(DbContextOptions<BreakpointContext> options) : base(options)
		{
			InitializeData();
		}

		public DbSet<Laptop> Laptops { get; set; }
		public DbSet<User> Users { get; set; }

		private void InitializeData()
		{
			if (!Laptops.Any())
			{
				var laptops = new List<Laptop> {
					new Laptop {
						Id = 1,
						Name = "Laptop 1"
					},
					new Laptop {
						Id = 2,
						Name = "Laptop 2"
					}
			};

				Laptops.AddRange(laptops);
				SaveChanges();
			}

			if (!Users.Any())
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

				Users.AddRange(users);
				SaveChanges();
			}
		}
	}
}
