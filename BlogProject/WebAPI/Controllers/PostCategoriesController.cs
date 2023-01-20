using Contracts.Dtos.PostCategoryDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Commands.PostCategoryCommands.CreatePostCategory;
using Services.Commands.PostCategoryCommands.DeletePostCategory;
using Services.Commands.PostCategoryCommands.UpdatePostCategory;
using Services.Queries.PostCategoryQueries.GetPostCategoryById;
using Services.Queries.PostCategoryQueries.GetPostCategoryList;

namespace WebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PostCategoriesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public PostCategoriesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetList(CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetPostCategoryListQuery());
			return Ok(result);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetById([FromRoute] GetPostCategoryByIdQuery query, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(query);

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreatePostCategoryCommand command, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(command);
			return Created("", result);
		}

		[HttpPut("{Id}")]
		public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] PostCategoryUpdateDto postCategoryUpdateDto, CancellationToken cancellationToken)
		{
			UpdatePostCategoryCommand command = new UpdatePostCategoryCommand()
			{
				Id = Id,
				PostId = postCategoryUpdateDto.PostId,
				CategoryId = postCategoryUpdateDto.CategoryId
			};
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete([FromRoute] DeletePostCategoryCommand command, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}
	}
}
