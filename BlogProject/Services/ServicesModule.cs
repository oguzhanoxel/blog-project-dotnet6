using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Services
{
	public static class ServicesModule
	{
		public static IServiceCollection AddServicesModule(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());

			return services;
		}
	}
}
