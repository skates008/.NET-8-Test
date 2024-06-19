using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Sigma.ORM.Abstractions.RepositoryPattern
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		protected readonly DbContext _dbContext;
		private bool _disposed;

		public GenericRepository(DbContext dbContext)
		{
			this._dbContext = dbContext;
		}

		public IQueryable<TEntity> GetAllAsQueryable()
		{
			return this._dbContext.Set<TEntity>().AsQueryable();
		}

		public TEntity Get<TDataType>(TDataType id) where TDataType : struct
		{
			return this._dbContext.Set<TEntity>().Find(id);
		}

		public async Task Patch<TDataType>(TDataType id, TEntity patch) where TDataType : struct
		{
			TEntity entity = await this._dbContext.Set<TEntity>().FindAsync(id);
			this._dbContext.Entry(entity).CurrentValues.SetValues(patch);
			this._dbContext.Entry(entity).State = EntityState.Modified;
		}

		public async Task<TEntity> GetAsync<TDataType>(TDataType id) where TDataType : struct
		{
			return await this._dbContext.Set<TEntity>().FindAsync(id);
		}

		public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
		{
			return this._dbContext.Set<TEntity>().Where(predicate).ToList();
		}

		public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await this._dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
		}

		public TEntity Add(TEntity entity)
		{
			EntityEntry entry = this._dbContext.Set<TEntity>().Add(entity);
			return (TEntity)entry.Entity;
		}

		public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
		{
			this._dbContext.Set<TEntity>().AddRange(entities);
			return entities;
		}

		public virtual void Update(TEntity entity)
		{
			this._dbContext.Entry(entity).State = EntityState.Modified;
		}

		public void Delete(TEntity entity)
		{
			this._dbContext.Set<TEntity>().Attach(entity);
			this._dbContext.Entry(entity).State = EntityState.Deleted;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		public virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					this._dbContext.Dispose();
				}
			}

			this._disposed = true;
		}
	}
}