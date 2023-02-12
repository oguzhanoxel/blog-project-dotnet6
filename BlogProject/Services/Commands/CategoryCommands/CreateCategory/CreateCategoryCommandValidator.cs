using FluentValidation;

namespace Services.Commands.CategoryCommands.CreateCategory
{
	public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
	{
		public CreateCategoryCommandValidator()
		{
			RuleFor(c => c.Title)
				.NotEmpty()
				.MinimumLength(10)
				.MaximumLength(75);
		}
	}
}
