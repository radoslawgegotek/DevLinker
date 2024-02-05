using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DevLinker.Application
{
	public static class ApplicationInstaller
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();

			services.AddMediatR(configuration =>
			{
				configuration.RegisterServicesFromAssemblies(assembly);
			});

			services.AddValidatorsFromAssembly(assembly);
			services.AddAutoMapper(assembly);

			return services;
		}
	}
}
