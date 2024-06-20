using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Sigma.ORM.Context;

namespace Sigma.ORM.Factory
{

	public class DesignTimeDbContextFactoryBase : IDesignTimeDbContextFactory<SigmaDbContext>
	{
		private IConfiguration Configuration
		{
			get
			{
				return new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.json")
					.Build();
			}
		}

		public SigmaDbContext CreateDbContext(string[] args)
		{
			string connectionString = this.Configuration.GetConnectionString("GameXDbEntities");

			DbContextOptionsBuilder<SigmaDbContext> optionsBuilder = new DbContextOptionsBuilder<SigmaDbContext>();
			optionsBuilder.UseSqlServer(connectionString);

			return new SigmaDbContext(optionsBuilder.Options);
		}
	}
}