using System;
using CrossCutting.ViewModels.Employers;
using FluentValidation;

namespace CrossCutting.ViewModels.Authentication
{
	public class AutenticateRequest
	{
		public string  Document { get; set; }
		public string Password { get; set; }

        public FluentValidation.Results.ValidationResult Validate()
        {
            var validator = new AutenticateRequestValidator();
            return validator.Validate(this);
        }
    }

    public class AutenticateRequestValidator : AbstractValidator<AutenticateRequest>
    {
        public AutenticateRequestValidator()
        {
            RuleFor(r => r.Document)
                .NotEmpty().WithMessage("Document cannot be empty");
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Password cannont be empty");
        }
    }

}

