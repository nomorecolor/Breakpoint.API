using Breakpoint.Business.Services;
using Breakpoint.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Breakpoint.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class LaptopController : ControllerBase
	{
		private readonly ILaptopService _laptopService;

		public LaptopController(ILaptopService laptopService)
		{
			_laptopService = laptopService;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<LaptopDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetAll()
		{
			var laptops = await _laptopService.GetAll();

			if (laptops == null)
			{
				return NotFound();
			}

			return Ok(laptops);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(LaptopDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetById(int id)
		{
			if (id <= 0)
			{
				return BadRequest();
			}

			var laptop = await _laptopService.GetById(id);

			if (laptop == null)
			{
				return NotFound();
			}

			return Ok(laptop);
		}

		[HttpPost]
		[ProducesResponseType(typeof(LaptopDto), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Add(LaptopDto laptopDto)
		{
			// TODO: Add dto validator

			await _laptopService.Add(laptopDto);

			return CreatedAtAction(nameof(GetById), new { id = laptopDto.Id }, laptopDto);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Update(int id, LaptopDto laptopDto)
		{
			// TODO: Add dto validator

			if (id <= 0)
			{
				return BadRequest();
			}

			var laptop = await _laptopService.GetById(id);

			if (laptop == null)
			{
				return NotFound();
			}

			await _laptopService.Update(id, laptopDto);

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

			var laptop = await _laptopService.GetById(id);

			if (laptop == null)
			{
				return NotFound();
			}

			await _laptopService.Delete(id);

			return NoContent();
		}
	}
}
