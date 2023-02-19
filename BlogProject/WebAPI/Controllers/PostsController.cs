using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Commands.PostCommands.CreatePost;
using Services.Commands.PostCommands.DeletePost;
using Services.Commands.PostCommands.UpdatePost;
using Services.Dtos.PostDtos;
using Services.Queries.PostQueries.GetPostById;
using Services.Queries.PostQueries.GetPostList;
using Services.Queries.PostQueries.GetPostListByCategoryId;

namespace WebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class  PostsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public PostsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetList()
		{
			var postListModel = await _mediator.Send(new GetPostListQuery());
			return Ok(postListModel);
		}

		[HttpGet("GetListByCategoryId/{CategoryId}")]
		public async Task<IActionResult> GetListByCategoryId([FromRoute] GetPostListByCategoryIdQuery query)
		{
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetById([FromRoute] GetPostByIdQuery query)
		{
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreatePostCommand command)
		{
			var result = await _mediator.Send(command);
			return Created("", result);
		}

		[HttpPut("{Id}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PostUpdateDto postUpdateDto)
		{
			UpdatePostCommand command = new UpdatePostCommand()
			{
				Id = id,
				Title = postUpdateDto.Title,
				Text = postUpdateDto.Text
			};
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete([FromRoute] DeletePostCommand command)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}
	}
}
