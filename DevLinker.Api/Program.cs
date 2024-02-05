using DevLinker.Application;
using DevLinker.Application.Services;
using DevLinker.Domain.IServices;
using DevLinker.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<ICurrentUserService, CurrnetUserService>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultPgConnection")
		?? throw new InvalidOperationException("Connection string not found"));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


builder.Services.AddCors(options =>
{
	options.AddPolicy(name: "DevLinkerWeb",
		policy =>
		{
			policy.WithOrigins(builder.Configuration.GetSection("AppConfig:Audience").Value!)
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials();
		});
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}


app.UseCors("DevLinkerWeb");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
