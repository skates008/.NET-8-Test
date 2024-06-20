using System.ComponentModel.DataAnnotations;

namespace Sigma.Domain.Entities
{
	public partial class Candidate
	{
		public int Id { get; set; }

		[StringLength(50)]
		public string FirstName { get; set; }

		[StringLength(50)]
		public string LastName { get; set; }

		[StringLength(15)]
		public string Phone { get; set; }

		[StringLength(100)]
		public string Email { get; set; }

		[StringLength(20)]
		public string PreferredCallTime { get; set; }

		[StringLength(100)]
		public string LinkedInProfile { get; set; }

		[StringLength(100)]
		public string GitHubProfile { get; set; }

		[StringLength(500)]
		public string Comment { get; set; }
	}
}
