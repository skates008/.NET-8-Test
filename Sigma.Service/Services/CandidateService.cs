using Sigma.Domain.Entities;
using Sigma.Model.Models.Response;
using Sigma.ORM.Abstractions.RepositoryPattern;
using Sigma.ORM.Abstractions.UnitOfWorkPattern;
using Sigma.Service.Interface;

namespace Sigma.Service.Services
{
	public class CandidateService : ICandidateService
	{
		private readonly IGenericRepository<Candidate> _candidateRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CandidateService(
			IUnitOfWork unitOfWork
		)
		{
			this._candidateRepository = unitOfWork.Repository<Candidate>();
			this._unitOfWork = unitOfWork;
		}

		public async Task<ResponseViewModel<Candidate>> AddUpdateCandidateAsync(Candidate model)
		{
			Candidate existingCandidate = (await _candidateRepository.GetAllAsync())
				.FirstOrDefault(x => string.Equals(x.Email, model.Email, StringComparison.OrdinalIgnoreCase));

			if (existingCandidate == null)
			{
				Candidate candidate = new Candidate()
				{
					Email = model.Email,
					FirstName = model.FirstName,
					LastName = model.LastName,
					Phone = model.Phone,
					GitHubProfile = model.GitHubProfile,
					LinkedInProfile = model.LinkedInProfile,
					PreferredCallTime = model.PreferredCallTime,
					Comment = model.Comment
				};

				await this._candidateRepository.AddAsync(candidate);
			}
			else
			{
				existingCandidate.FirstName = model.FirstName;
				existingCandidate.LastName = model.LastName;
				existingCandidate.Phone = model.Phone;
				existingCandidate.PreferredCallTime = model.PreferredCallTime;
				existingCandidate.LinkedInProfile = model.LinkedInProfile;
				existingCandidate.GitHubProfile = model.GitHubProfile;
				existingCandidate.Comment = model.Comment;

				this._candidateRepository.Update(existingCandidate);
			}

			await this._unitOfWork.SaveChangesAsync();

			return new ResponseViewModel<Candidate>()
			{
				Success = true,
				Data = model
			};
		}
	}
}
