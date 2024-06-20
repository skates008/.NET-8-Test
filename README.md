# SigmaCandidateTest

**Assumptions**

1. The repository interface IGenericRepository is implemented interact with the database.
2. The Candidate model/entity are defined with appropriate properties.
3. Used FluentValidaiton for Required Fields.
4. The application uses Entity Framework Core as the ORM for database operations.

 **Ways for Improvement**

1. Use of ViewModel instead of Entity
2. Automapper Integration
```
   Eg:
   
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CandidateDto, Candidate>().ReverseMap();
        }
    }
```
4. Middleware or Custom Filter for Exception Handling
5. Service Interface Enhancements/API service standarization:
   
```
Eg:
public interface ICandidateService
{
    Task<Candidate> GetCandidateAsync(int id);
    Task<IEnumerable<Candidate>> GetAllCandidatesAsync();
    Task<Candidate> CreateCandidateAsync(Candidate candidate);
    Task<Candidate> UpdateCandidateAsync(int id, Candidate candidate);
    Task<bool> DeleteCandidateAsync(int id);
}
```

**Total Time Spent**

6-8 hours
