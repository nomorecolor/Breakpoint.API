using Breakpoint.Business.Services;
using Breakpoint.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Breakpoint.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetAll()
		{
			var users = await _userService.GetAll();

			if (users == null)
			{
				return NotFound();
			}

			return Ok(users);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetById(int id)
		{
			if (id <= 0)
			{
				return BadRequest();
			}

			var user = await _userService.GetById(id);

			if (user == null)
			{
				return NotFound();
			}

			return Ok(user);
		}

		[HttpPost]
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Add(UserDto userDto)
		{
			// TODO: Add dto validator

			await _userService.Add(userDto);

			return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Update(int id, UserDto userDto)
		{
			// TODO: Add dto validator

			if (id <= 0)
			{
				return BadRequest();
			}

			var user = await _userService.GetById(id);

			if (user == null)
			{
				return NotFound();
			}

			await _userService.Update(id, userDto);

			return Ok();
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(int id)
		{
			// TODO: Add dto validator

			if (id <= 0)
			{
				return BadRequest();
			}

			var user = await _userService.GetById(id);

			if (user == null)
			{
				return NotFound();
			}

			await _userService.Delete(id);

			return NoContent();
		}
	}
}
