using Microsoft.AspNetCore.Mvc;
using Sigma.Domain.Entities;
using Sigma.Service.Interface;

namespace Sigma.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CandidatesController : ControllerBase
	{
		private readonly ICandidateService _candidateService;

		public CandidatesController(
			ICandidateService candidateService
		)
		{
			this._candidateService = candidateService;
		}

		/// <summary>
		/// Add or update candidate
		/// </summary>
		/// <returns></returns>
		/// <param name="candidate">candidate data</param>
		// POST api/candidates
		[HttpPost]
		public async Task<IActionResult> AddUpdateCandidateAsync([FromBody] Candidate candidate)
		{
			return this.Ok(await this._candidateService.AddUpdateCandidateAsync(candidate));
		}

	}
}
