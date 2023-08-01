using Breakpoint.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Breakpoint.Domain.Repositories
{
	public interface ILaptopRepository
	{
		public Task<IEnumerable<Laptop>> GetAll();
		public Task<Laptop?> GetById(int id);
		public Task Add(Laptop laptop);
		public Task Update(int id, Laptop laptop);
		public Task Delete(int id);
	}

	public class LaptopRepository : ILaptopRepository
	{
		private readonly BreakpointContext _context;

		public LaptopRepository(BreakpointContext context)
		{
			_context = context;

			InitializeData();
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

		private void InitializeData()
		{
			if (!_context.Laptops.Any())
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

				_context.Laptops.AddRange(laptops);
				_context.SaveChanges();
			}
		}
	}
}
