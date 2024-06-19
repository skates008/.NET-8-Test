using Microsoft.EntityFrameworkCore;
using Sigma.Domain.Entities;

namespace Sigma.ORM.Context
{
	public partial class SigmaDbContext : DbContext
	{
		public SigmaDbContext(DbContextOptions<SigmaDbContext> options) : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

		public virtual DbSet<Candidate> Candidates { get; set; }

		protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Candidate>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.HasIndex(e => e.Email).IsUnique();
			});

		}
	}
}
