using Contracts.Dtos.CategoryDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Commands.CategoryCommands.CreateCategory;
using Services.Commands.CategoryCommands.DeleteCategory;
using Services.Commands.CategoryCommands.UpdateCategory;
using Services.Queries.CategoryQueries.GetCategoryById;
using Services.Queries.CategoryQueries.GetCategoryList;

namespace WebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CategoriesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CategoriesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetList()
		{
			var result = await _mediator.Send(new GetCategoryListQuery());
			return Ok(result);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetById([FromRoute] GetCategoryByIdQuery query)
		{
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
		{
			var result = await _mediator.Send(command);
			return Created("", result);
		}

		[HttpPut("{Id}")]
		public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] CategoryUpdateDto categoryUpdateDto)
		{
			UpdateCategoryCommand command = new UpdateCategoryCommand()
			{
				Id = Id,
				Title = categoryUpdateDto.Title,
				Description = categoryUpdateDto.Description
			};
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete([FromRoute] DeleteCategoryCommand command)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}
	}
}
