using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Sigma.Domain.Entities;
using Sigma.Model.Validators;
using Sigma.ORM.Abstractions.RepositoryPattern;
using Sigma.ORM.Abstractions.UnitOfWorkPattern;
using Sigma.ORM.Context;
using Sigma.Service.Interface;
using Sigma.Service.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables()
	.Build();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SigmaDbContext>(options =>
	options.UseSqlServer(configuration.GetConnectionString("SigmaDatabase"), o =>
	{
		o.MigrationsAssembly(typeof(SigmaDbContext).Assembly.FullName);
		o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
	})
);

builder.Services.AddScoped(typeof(GenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICandidateService, CandidateService>();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<IValidator<Candidate>, CandidateValidator>();

WebApplication app = builder.Build();

await ApplyMigrationsAsync();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

async Task ApplyMigrationsAsync()
{
	try
	{
		using IServiceScope scope = app.Services.CreateScope();
		SigmaDbContext dbContext = scope.ServiceProvider.GetRequiredService<SigmaDbContext>();
		await dbContext.Database.MigrateAsync();
	}
	catch (Exception ex)
	{
		throw ex;
	}
}