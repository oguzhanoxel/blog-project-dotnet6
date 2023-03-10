using FluentValidation;

namespace Services.Commands.PostCommands.UpdatePost
{
	public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
	{
		public UpdatePostCommandValidator()
		{
			RuleFor(p => p.Title)
				.NotEmpty()
				.MinimumLength(10)
				.MaximumLength(75);

			RuleFor(p => p.Text)
				.NotEmpty();
		}
	}
}
