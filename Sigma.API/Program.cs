using Microsoft.EntityFrameworkCore;
using Sigma.ORM.Context;
using Sigma.Service.Interface;
using Sigma.Service.Services;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables()
	.Build();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICandidateService, CandidateService>();

builder.Services.AddDbContext<SigmaDbContext>(options =>
	options.UseSqlServer(configuration.GetConnectionString("SigmaDatabase"), o =>
	{
		o.MigrationsAssembly(typeof(SigmaDbContext).Assembly.FullName);
		o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
	})
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
