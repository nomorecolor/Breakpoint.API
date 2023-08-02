using Breakpoint.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Breakpoint.Domain.Repositories
{
	public interface ILaptopRepository
	{
		Task<IEnumerable<Laptop>> GetAll();
		Task<Laptop?> GetById(int id);
		Task Add(Laptop laptop);
		Task Update(int id, Laptop laptop);
		Task Delete(int id);
	}

	public class LaptopRepository : ILaptopRepository
	{
		private readonly BreakpointContext _context;

		public LaptopRepository(BreakpointContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Laptop>> GetAll()
		{
			return await _context.Laptops
				.Where(x => !x.IsDeleted)
				.ToListAsync();
		}

		public async Task<Laptop?> GetById(int id)
		{
			return await _context.Laptops.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
		}

		public async Task Add(Laptop laptop)
		{
			await _context.Laptops.AddAsync(laptop);
			await _context.SaveChangesAsync();
		}

		public async Task Update(int id, Laptop laptop)
		{
			var existingLaptop = await _context.Laptops.FirstOrDefaultAsync(x => x.Id == id);

			if (existingLaptop != null)
			{
				existingLaptop.Name = laptop.Name;
			}

			await _context.SaveChangesAsync();
		}

		public async Task Delete(int id)
		{
			var existingLaptop = await _context.Laptops.FirstOrDefaultAsync(x => x.Id == id);

			if (existingLaptop != null)
			{
				existingLaptop.IsDeleted = true;
			}

			await _context.SaveChangesAsync();
		}
	}
}
