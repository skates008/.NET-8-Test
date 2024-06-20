using Sigma.Domain.Entities;
using Sigma.Model.Models.Response;

namespace Sigma.Service.Interface
{
	public interface ICandidateService
	{
		Task<ResponseViewModel<Candidate>> AddUpdateCandidateAsync(Candidate model);
	}
}
