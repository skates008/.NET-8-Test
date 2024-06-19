using Microsoft.AspNetCore.Mvc;
using Sigma.Model.Models.Request;
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
		/// <param name="model">candidate data</param>
		// POST api/candidates
		[HttpPost]
		public async Task<IActionResult> AddUpdateCandidateAsync(CandidateRequestViewModel model)
		{
			return this.Ok(await this._candidateService.AddUpdateCandidateAsync(model));
		}

	}
}
