using System;
using System.Security.Claims;
using CrossCutting.Enums;
using CrossCutting.Models;
using FluentValidation;

namespace CrossCutting.ViewModels.Employers
{
    public class AddEmployerRequest
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DocNumber { get; set; }
        public Role Role { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }

        public FluentValidation.Results.ValidationResult Validate()
        {
            var validator = new AddEmployerRequestValidator();
            return validator.Validate(this);
        }
    }

    public class AddEmployerRequestValidator : AbstractValidator<AddEmployerRequest>
    {
        public AddEmployerRequestValidator()
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
                .MaximumLength(50).WithMessage("E-mail length must be 50 maximum")
                .EmailAddress().WithMessage("Invalid e-mail");

            RuleFor(r => r.DocNumber)
                .NotEmpty().WithMessage("Document cannot be empty");

            RuleFor(r => r.Role)
                .NotEmpty().WithMessage("Role cannot be empty");

            RuleFor(r=> r.Password)
                .NotEmpty().WithMessage("Password cannot be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character (e.g., @, #, $, !).");

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

