using Sigma.ORM.Abstractions.RepositoryPattern;

namespace Sigma.ORM.Abstractions.UnitOfWorkPattern
{
	public interface IUnitOfWork
	{
		IGenericRepository<T> Repository<T>() where T : class;
		Task<int> SaveChangesAsync();
	}
}
