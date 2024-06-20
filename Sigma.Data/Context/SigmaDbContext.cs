using Microsoft.EntityFrameworkCore;
using Sigma.Domain.Entities;

namespace Sigma.ORM.Context
{
	public partial class SigmaDbContext : DbContext
	{
		public SigmaDbContext(DbContextOptions<SigmaDbContext> options) : base(options)
		{

		}

		public virtual DbSet<Candidate> Candidates { get; set; }

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
