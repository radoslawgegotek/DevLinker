using DevLinker.Infrastructure.DataModel.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using DevLinker.Domain.Entities;
using DevLinker.Domain.IRepositories;
using DevLinker.Infrastructure.Repositories;
using DevLinker.Domain.IQueries;
using DevLinker.Infrastructure.Queries;
using DevLinker.Infrastructure.DataModel;

namespace DevLinker.Infrastructure
{
	public static class InfrastructureInstaller
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<DevLinkerContext>(options =>
			{
				options.UseNpgsql(connectionString);
			});

			services.AddIdentity<AppUser, IdentityRole>(options =>
			{
				//Identity config
				options.SignIn.RequireConfirmedAccount = false;
				options.SignIn.RequireConfirmedPhoneNumber = false;
				options.SignIn.RequireConfirmedEmail = false;
				options.Lockout.AllowedForNewUsers = false;

				options.Password.RequireDigit = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequiredLength = 6;
			})
			.AddEntityFrameworkStores<DevLinkerContext>()
			.AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            //Repositories
            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
			services.AddScoped<IIssueRepository, IssueRepository>();
			services.AddScoped<IIssueMemberRepository, IssueMemberRepository>();
			services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
			services.AddScoped<IWorkspaceMemberRepository, WorkspaceMemberRepository>();

			//Queries
			services.AddScoped<IWorkspaceQuery, WorkspaceQuery>();
			services.AddScoped<IWorkspaceMemberQuery, WorkspaceMemberQuery>();
			services.AddScoped<IIssueQuery, IssueQuery>();
			services.AddScoped<IIssueMemberQuery, IssueMemberQuery>();
			
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}
}
