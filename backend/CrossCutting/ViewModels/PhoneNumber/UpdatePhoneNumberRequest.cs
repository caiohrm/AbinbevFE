using System;
using CrossCutting.ViewModels.Employers;
using FluentValidation;

namespace CrossCutting.ViewModels.PhoneNumber
{
    public class AddPhoneNumberRequest
    {
        public AddPhoneNumberRequest()
        {
        }

        public string CountryCode { get; set; }
        public string RegionCode { get; set; }
        public string Number { get; set; }

        public FluentValidation.Results.ValidationResult Validate()
        {
            var validator = new AddPhoneNumberRequestValidator();
            return validator.Validate(this);
        }
    }

    public class AddPhoneNumberRequestValidator : AbstractValidator<AddPhoneNumberRequest>
    {
        public AddPhoneNumberRequestValidator()
        {
            RuleFor(r => r.CountryCode)
             .MinimumLength(1)
             .MaximumLength(3)
             .WithMessage("Country code must have between 1 and 3 digits");

            RuleFor(r => r.RegionCode)
            .MinimumLength(1)
            .MaximumLength(5)
            .WithMessage("Region Code must have between 1 and 5 letter");

            RuleFor(r => r.Number)
            .MinimumLength(7)
            .MaximumLength(10)
            .WithMessage("Number must have between 7 and 10 letter");
        }


    }
}

