using Moq;
using Sigma.Domain.Entities;
using Sigma.Model.Models.Response;
using Sigma.ORM.Abstractions.RepositoryPattern;
using Sigma.ORM.Abstractions.UnitOfWorkPattern;
using Sigma.Service.Interface;
using Sigma.Service.Services;

namespace Sigma.UnitTest
{
	public class UnitTest
	{
		public class UserServiceTests
		{
			private readonly Mock<IGenericRepository<Candidate>> _mockCandidateRepository;
			private readonly Mock<IUnitOfWork> _mockUnitOfWork;
			private readonly ICandidateService _candidateService;

			public UserServiceTests()
			{
				_mockUnitOfWork = new Mock<IUnitOfWork>();
				_mockCandidateRepository = new Mock<IGenericRepository<Candidate>>();
				_mockUnitOfWork.Setup(x => x.Repository<Candidate>()).Returns(_mockCandidateRepository.Object);
				_candidateService = new CandidateService(_mockUnitOfWork.Object);
			}

			[Fact]
			public async Task AddOrUpdateUser_ShouldCreateUser_WhenUserDoesNotExist()
			{
				Candidate newCandidate = new Candidate
				{
					Email = "amit@example.com",
					FirstName = "Amit",
					LastName = "Karmacharya",
					Phone = "11111",
					PreferredCallTime = "9am-3pm",
					LinkedInProfile = "https://www.linkedin.com/amit",
					GitHubProfile = "https://github.com/amit",
					Comment = "Test Comment"
				};

				_mockCandidateRepository.Setup(repo => repo.GetAllAsQueryable())
					.Returns(new List<Candidate>().AsQueryable());

				_mockCandidateRepository.Setup(repo => repo.AddAsync(newCandidate))
					.ReturnsAsync(newCandidate);

				ResponseViewModel<Candidate> result = await _candidateService.AddUpdateCandidateAsync(newCandidate);

				Assert.True(result.Success);
				Assert.Equal(newCandidate.FirstName, result.Data.FirstName);
				Assert.Equal(newCandidate.LastName, result.Data.LastName);
				Assert.Equal(newCandidate.Email, result.Data.Email);
				Assert.Equal(newCandidate.Comment, result.Data.Comment);
			}

			[Fact]
			public async Task AddOrUpdateUser_ShouldUpdateUser_WhenUserExists()
			{
				Candidate existingCandidate = new Candidate
				{
					Email = "amit@example.com",
					FirstName = "Amit",
					LastName = "Karmacharya",
					Phone = "11111",
					PreferredCallTime = "9am-3pm",
					LinkedInProfile = "https://www.linkedin.com/amit",
					GitHubProfile = "https://github.com/amit",
					Comment = "Test Comment"
				};

				Candidate updatedCandidate = new Candidate
				{
					Email = "amit@example.com",
					FirstName = "Amit",
					LastName = "Test",
					Phone = "2222",
					PreferredCallTime = "9am-1pm",
					LinkedInProfile = "https://www.linkedin.com/amit",
					GitHubProfile = "https://github.com/amit",
					Comment = "Test Comment"
				};

				_mockCandidateRepository.Setup(repo => repo.GetAllAsQueryable())
					.Returns((new List<Candidate> { existingCandidate }).AsQueryable());

				_mockCandidateRepository.Setup(repo => repo.Update(It.IsAny<Candidate>()))
					.Callback<Candidate>(candidate =>
					{
						existingCandidate.FirstName = candidate.FirstName;
						existingCandidate.LastName = candidate.LastName;
						existingCandidate.Comment = candidate.Comment;
					});

				ResponseViewModel<Candidate> result = await _candidateService.AddUpdateCandidateAsync(updatedCandidate);

				Assert.True(result.Success);
				Assert.Equal(updatedCandidate.FirstName, result.Data.FirstName);
				Assert.Equal(updatedCandidate.LastName, result.Data.LastName);
				Assert.Equal(updatedCandidate.Email, result.Data.Email);
				Assert.Equal(updatedCandidate.Comment, result.Data.Comment);
			}
		}
	}
}