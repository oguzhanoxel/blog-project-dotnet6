using System.Reflection;
using Core.Services.Pipelines.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Services.Rules;

namespace Services
{
	public static class ServicesModule
	{
		public static IServiceCollection AddServicesModule(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			services.AddScoped<PostBusinessRules>();
			services.AddScoped<CategoryBusinessRules>();
			services.AddScoped<PostCategoryBusinessRules>();

			return services;
		}
	}
}
