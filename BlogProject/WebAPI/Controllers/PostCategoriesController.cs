using Contracts.Dtos.PostCategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace WebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PostCategoriesController : ControllerBase
	{
		private readonly IServiceManager _serviceManager;

		public PostCategoriesController(IServiceManager serviceManager)
		{
			_serviceManager = serviceManager;
		}

		[HttpGet]
		public async Task<IActionResult> GetList(CancellationToken cancellationToken)
		{
			var postCategoryListDto = await _serviceManager.PostCategoryService.GetAllAsync(cancellationToken);

			return Ok(postCategoryListDto);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
		{
			var postCategoryDto = await _serviceManager.PostCategoryService.GetByIdAsync(id, cancellationToken);

			return Ok(postCategoryDto);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] PostCategoryCreateDto postCategoryCreateDto, CancellationToken cancellationToken)
		{
			var postCategoryDto = await _serviceManager.PostCategoryService.CreateAsync(postCategoryCreateDto, cancellationToken);
			return Created("", postCategoryDto);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id,	[FromBody] PostCategoryUpdateDto postCategoryUpdateDto, CancellationToken cancellationToken)
		{
			var postCategoryDto = await _serviceManager.PostCategoryService.UpdateAsync(id, postCategoryUpdateDto, cancellationToken);

			return Ok(postCategoryDto);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			var postCategoryDto = await _serviceManager.PostCategoryService.DeleteAsync(id, cancellationToken);

			return Ok(postCategoryDto);
		}
	}
}
