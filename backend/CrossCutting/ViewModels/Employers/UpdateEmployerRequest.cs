using System;
using CrossCutting.Enums;
using CrossCutting.Models;
using FluentValidation;

namespace CrossCutting.ViewModels.Employers
{
	public class UpdateEmployerRequest
	{
		public UpdateEmployerRequest()
		{

		}

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DocNumber { get; set; }
        public Role Role { get; set; }
        public DateTime BirthDate { get; set; }

        public FluentValidation.Results.ValidationResult Validate()
        {
            var validator = new UpdateEmployerRequestValidator();
            return validator.Validate(this);
        }

    }

    class UpdateEmployerRequestValidator : AbstractValidator<UpdateEmployerRequest>
    {
        public UpdateEmployerRequestValidator()
        {
            RuleFor(r => r.FirstName)
             .MinimumLength(3)
             .MaximumLength(50)
             .WithMessage("Name must have between 3 and 50 letter");

            RuleFor(r => r.LastName)
             .MinimumLength(3)
             .MaximumLength(50)
             .WithMessage("Last Name must have between 3 and 50 letter");

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("E-mail cannot be empty")
                .EmailAddress().WithMessage("Invalid e-mail");

            RuleFor(r => r.DocNumber)
                .NotEmpty().WithMessage("Document cannot be empty");

            RuleFor(r => r.Role)
                .NotEmpty().WithMessage("User role cannot be empty");

            RuleFor(r=> r.BirthDate)
            .Must(BeAtLeast18YearsOld).WithMessage("User must be at least 18 years old.");
        }

        private bool BeAtLeast18YearsOld(DateTime birthdate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;

            if (birthdate.Date > today.AddYears(-age)) age--;

            return age >= 18;
        }
    }
}

