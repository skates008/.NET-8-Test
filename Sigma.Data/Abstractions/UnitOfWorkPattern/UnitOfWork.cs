using Microsoft.EntityFrameworkCore;
using Sigma.ORM.Abstractions.RepositoryPattern;
using Sigma.ORM.Context;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Sigma.ORM.Abstractions.UnitOfWorkPattern;

public class UnitOfWork : IUnitOfWork
{
	private readonly SigmaDbContext _context;
	private Hashtable _repositories;

	public UnitOfWork(SigmaDbContext context)
	{
		this._context = context;
	}

	public IGenericRepository<T> Repository<T>() where T : class
	{
		if (this._repositories == null)
		{
			this._repositories = new Hashtable();
		}

		string type = typeof(T).Name;

		if (!this._repositories.ContainsKey(type))
		{
			Type repositoryType = typeof(GenericRepository<>);
			object repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), this._context);

			this._repositories.Add(type, repositoryInstance);
		}

		return (IGenericRepository<T>)this._repositories[type];
	}

	public async Task<int> SaveChangesAsync()
	{
		try
		{
			this.ValidateContextChanges();
			return await this._context.SaveChangesAsync();
		}
		catch (ValidationException ex)
		{
			throw new Exception(this.GetDbEntityValidationExceptionMessage(ex), ex);
		}
	}

	private void ValidateContextChanges()
	{
		IEnumerable entities = this._context.ChangeTracker.Entries()
			.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
			.Select(e => e.Entity);

		foreach (object entity in entities)
		{
			ValidationContext validationContext = new ValidationContext(entity);
			Validator.ValidateObject(entity, validationContext, validateAllProperties: true);
		}
	}

	private string GetDbEntityValidationExceptionMessage(ValidationException ex)
	{
		string errorDetails = ex.Message + "\n";

		foreach (string mem in ex.ValidationResult.MemberNames)
		{
			errorDetails += "Entity of type \"" + mem + "\" in state \"" + "\" has the following validation errors\n";
			errorDetails += ex.ValidationResult.ErrorMessage;
		}

		return errorDetails;
	}
}
