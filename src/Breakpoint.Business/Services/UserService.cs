using Breakpoint.Contracts;
using Breakpoint.Domain.Models;
using Breakpoint.Domain.Repositories;

namespace Breakpoint.Business.Services
{
	public interface IUserService
	{
		public Task<IEnumerable<UserDto>> GetAll();
		public Task<UserDto?> GetById(int id);
		public Task Add(UserDto userDto);
		public Task Update(int id, UserDto userDto);
		public Task Delete(int id);
	}

	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<IEnumerable<UserDto>> GetAll()
		{
			var laptops = await _userRepository.GetAll();

			if (!laptops.Any())
			{
				return null!;
			}

			return laptops.Select(x => new UserDto
			{
				Id = x.Id,
				FirstName = x.FirstName,
				LastName = x.LastName
			});
		}

		public async Task<UserDto?> GetById(int id)
		{
			var user = await _userRepository.GetById(id);

			if (user == null)
			{
				return null;
			}

			return new UserDto
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName
			};
		}

		public async Task Add(UserDto userDto)
		{
			var user = new User
			{
				Id = userDto.Id,
				FirstName = userDto.FirstName,
				LastName = userDto.LastName
			};

			await _userRepository.Add(user);
		}

		public async Task Update(int id, UserDto userDto)
		{
			var user = new User
			{
				FirstName = userDto.FirstName,
				LastName = userDto.LastName
			};

			await _userRepository.Update(id, user);
		}

		public async Task Delete(int id)
		{
			await _userRepository.Delete(id);
		}
	}
}
