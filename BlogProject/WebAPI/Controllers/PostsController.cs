using Contracts.Dtos.PostDtos;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace WebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class  PostsController : ControllerBase
	{
		private readonly IServiceManager _serviceManager;

		public PostsController(IServiceManager serviceManager)
		{
			_serviceManager = serviceManager;
		}

		[HttpGet]
		public async Task<IActionResult> GetList(CancellationToken cancellationToken)
		{
			var postListDto = await _serviceManager.PostService.GetAllAsync(cancellationToken);

			return Ok(postListDto);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
		{
			var postDto = await _serviceManager.PostService.GetByIdAsync(id, cancellationToken);

			return Ok(postDto);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] PostCreateDto postCreateDto, CancellationToken cancellationToken)
		{
			var postDto = await _serviceManager.PostService.CreateAsync(postCreateDto, cancellationToken);
			return Created("", postDto);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id,	[FromBody] PostUpdateDto postUpdateDto, CancellationToken cancellationToken)
		{
			var postDto = await _serviceManager.PostService.UpdateAsync(id, postUpdateDto, cancellationToken);

			return Ok(postDto);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			var postDto = await _serviceManager.PostService.DeleteAsync(id, cancellationToken);

			return Ok(postDto);
		}
	}
}
