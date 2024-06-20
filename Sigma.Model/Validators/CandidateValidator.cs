using FluentValidation;
using Sigma.Domain.Entities;

namespace Sigma.Model.Validators
{
	public class CandidateValidator : AbstractValidator<Candidate>
	{
		public CandidateValidator()
		{
			RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email Address must be Valid");
			RuleFor(x => x.FirstName).NotEmpty();
			RuleFor(x => x.LastName).NotEmpty();
			RuleFor(x => x.Comment).NotEmpty();
		}
	}
}