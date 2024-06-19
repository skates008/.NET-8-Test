using Sigma.Model.Models.Request;
using Sigma.Model.Models.Response;
using Sigma.Domain.Entities;

namespace Sigma.Service.Interface
{
	public interface ICandidateService
	{
		IQueryable<Candidate> GetAllAgenciesAsQueryable();
		Task<ResponseViewModel> AddUpdateCandidateAsync(CandidateRequestViewModel model);

	}
}
