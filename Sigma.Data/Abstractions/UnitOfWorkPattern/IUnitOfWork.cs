namespace Sigma.ORM.Abstractions.UnitOfWorkPattern
{
	public interface IUnitOfWork
	{
		Task<int> SaveChangesAsync();
	}
}
