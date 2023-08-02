using Breakpoint.Contracts;
using Breakpoint.Domain.Models;
using Breakpoint.Domain.Repositories;

namespace Breakpoint.Business.Services
{
	public interface ILaptopService
	{
		Task<IEnumerable<LaptopDto>> GetAll();
		Task<LaptopDto?> GetById(int id);
		Task Add(LaptopDto laptopDto);
		Task Update(int id, LaptopDto laptop);
		Task Delete(int id);
	}

	public class LaptopService : ILaptopService
	{
		private readonly ILaptopRepository _laptopRepository;

		public LaptopService(ILaptopRepository laptopRepository)
		{
			_laptopRepository = laptopRepository;
		}

		public async Task<IEnumerable<LaptopDto>> GetAll()
		{
			var laptops = await _laptopRepository.GetAll();

			if (!laptops.Any())
			{
				return null!;
			}

			return laptops.Select(x => new LaptopDto
			{
				Id = x.Id,
				Name = x.Name
			});
		}

		public async Task<LaptopDto?> GetById(int id)
		{
			var laptop = await _laptopRepository.GetById(id);

			if (laptop == null)
			{
				return null;
			}

			return new LaptopDto
			{
				Id = laptop.Id,
				Name = laptop.Name
			};
		}

		public async Task Add(LaptopDto laptopDto)
		{
			var laptop = new Laptop
			{
				Id = laptopDto.Id,
				Name = laptopDto.Name
			};

			await _laptopRepository.Add(laptop);
		}

		public async Task Update(int id, LaptopDto laptopDto)
		{
			var laptop = new Laptop
			{
				Name = laptopDto.Name
			};

			await _laptopRepository.Update(id, laptop);
		}

		public async Task Delete(int id)
		{
			await _laptopRepository.Delete(id);
		}
	}
}
