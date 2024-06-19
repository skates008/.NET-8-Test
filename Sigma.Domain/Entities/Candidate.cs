﻿namespace Sigma.Domain.Entities
{
	public partial class Candidate
	{
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Phone { get; set; }

		public string Email { get; set; }

		public string PreferredCallTime { get; set; }

		public string LinkedInProfile { get; set; }

		public string GitHubProfile { get; set; }

		public string Comment { get; set; }
	}
}
