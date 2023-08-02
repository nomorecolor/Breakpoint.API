using Microsoft.EntityFrameworkCore;

namespace Breakpoint.Domain.Models
{
	public class BreakpointContext : DbContext
	{
		public BreakpointContext(DbContextOptions<BreakpointContext> options) : base(options)
		{
		}

		public DbSet<Laptop> Laptops { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
