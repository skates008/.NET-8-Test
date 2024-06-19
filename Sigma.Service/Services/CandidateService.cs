using Sigma.Model.Models.Request;
using Sigma.Model.Models.Response;
using Sigma.ORM.Abstractions.RepositoryPattern;
using Sigma.ORM.Abstractions.UnitOfWorkPattern;
using Sigma.Service.Interface;
using Sigma.Domain.Entities;

namespace Sigma.Service.Services
{
	public class CandidateService : ICandidateService
	{
		private readonly IGenericRepository<Candidate> _candidateRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CandidateService(
			IGenericRepository<Candidate> candidateRepository,
			IUnitOfWork unitOfWork
		)
		{
			this._candidateRepository = candidateRepository;
			this._unitOfWork = unitOfWork;
		}

		public IQueryable<Candidate> GetAllAgenciesAsQueryable()
		{
			throw new NotImplementedException();
		}

		public Task<ResponseViewModel> AddUpdateCandidateAsync(CandidateRequestViewModel model)
		{
			throw new NotImplementedException();
		}
	}
}
