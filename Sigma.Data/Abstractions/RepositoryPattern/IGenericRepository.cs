using System.Linq.Expressions;

namespace Sigma.ORM.Abstractions.RepositoryPattern
{
	public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
	{
		IQueryable<TEntity> GetAllAsQueryable();
		TEntity Get<TDataType>(TDataType id) where TDataType : struct;
		Task Patch<TDataType>(TDataType id, TEntity patch) where TDataType : struct;
		Task<TEntity> GetAsync<TDataType>(TDataType id) where TDataType : struct;
		IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
		Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
		TEntity Add(TEntity entity);
		IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
		void Update(TEntity entity);
		void Delete(TEntity entity);
	}
}