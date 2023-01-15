using Contracts.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace WebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CategoriesController : ControllerBase
	{
		private readonly IServiceManager _serviceManager;

		public CategoriesController(IServiceManager serviceManager)
		{
			_serviceManager = serviceManager;
		}

		[HttpGet]
		public async Task<IActionResult> GetList(CancellationToken cancellationToken)
		{
			var categoryListDto = await _serviceManager.CategoryService.GetAllAsync(cancellationToken);

			return Ok(categoryListDto);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
		{
			var categoryDto = await _serviceManager.CategoryService.GetByIdAsync(id, cancellationToken);

			return Ok(categoryDto);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken)
		{
			var categoryDto = await _serviceManager.CategoryService.CreateAsync(categoryCreateDto, cancellationToken);
			return Created("", categoryDto);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id,	[FromBody] CategoryUpdateDto categoryUpdateDto, CancellationToken cancellationToken)
		{
			var categoryDto = await _serviceManager.CategoryService.UpdateAsync(id, categoryUpdateDto, cancellationToken);

			return Ok(categoryDto);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			var categoryDto = await _serviceManager.CategoryService.DeleteAsync(id, cancellationToken);

			return Ok(categoryDto);
		}
	}
}
