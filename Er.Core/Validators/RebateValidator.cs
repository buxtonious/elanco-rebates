using Er.Core.Helpers;
using Er.Core.Models;
using FluentValidation;

namespace Er.Core.Validators
{
    public class RebateValidator : AbstractValidator<RebateForm>
    {
        private readonly SalutationHelper _salutationHelper;
        private readonly CountryHelper _countryHelper;

        public RebateValidator(SalutationHelper salutationHelper, CountryHelper countryHelper)
        {
            _salutationHelper = salutationHelper;
            _countryHelper = countryHelper;

            ValidatePersonalDetails();
            ValidateClinicDetails();
        }

        private void ValidatePersonalDetails()
        {
            RuleFor(x => x.PersonalDetails.Salutation)
                .NotEmpty()
                .WithMessage("Salutation is required")
                .Must(_salutationHelper.IsInList)
                .WithMessage("Salutation is not a valid salutation");

            RuleFor(x => x.PersonalDetails.FirstName)
                .NotEmpty()
                .WithMessage("First name is required")
                .MaximumLength(60)
                .WithMessage("First name exceeds maximum length of 60 characters");

            RuleFor(x => x.PersonalDetails.LastName)
                .NotEmpty()
                .WithMessage("Last name is required")
                .MaximumLength(60)
                .WithMessage("Last name exceeds maximum length of 60 characters");

            RuleFor(x => x.PersonalDetails.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email is not a valid email address")
                .MaximumLength(60)
                .WithMessage("Email exceeds maximum length of 60 characters");

            RuleFor(x => x.PersonalDetails.PetName)
                .NotEmpty()
                .WithMessage("Pet name is required")
                .MaximumLength(60)
                .WithMessage("Pet name exceeds maximum length of 60 characeters");

            RuleFor(x => x.PersonalDetails.PetBirthday)
                .NotEmpty()
                .Must(x => DateTime.TryParse(x, out _))
                .When(x => !string.IsNullOrEmpty(x.PersonalDetails.PetBirthday))
                .WithMessage("Pet birthday is not a valid date");

            RuleFor(x => x.PersonalDetails.PersonalAddressLine1)
                .NotEmpty()
                .WithMessage("Address line 1 is required")
                .MaximumLength(100)
                .WithMessage("Address line 1 exceeds maximum length of 100 characters");

            RuleFor(x => x.PersonalDetails.PersonalAddressLine2)
                .MaximumLength(100)
                .WithMessage("Address line 2 exceeds maximum length of 100 characters");

            RuleFor(x => x.PersonalDetails.PersonalAddressCity)
                .NotEmpty()
                .WithMessage("Address city is required")
                .MaximumLength(60)
                .WithMessage("Address city exceeds maximum length of 60 characters");

            RuleFor(x => x.PersonalDetails.PersonalAddressCountry)
                .NotEmpty()
                .WithMessage("Address country is required")
                .Must(_countryHelper.IsInList)
                .WithMessage("Address country is not a valid country");

            RuleFor(x => x.PersonalDetails.PersonalAddressPostCode)
                .NotEmpty()
                .WithMessage("Address postcode is required")
                .Matches(@"^([A-Z]{1,2}\d[A-Z\d]? ?\d[A-Z]{2}|GIR ?0A{2})$")
                .WithMessage("Address postcode is not a valid UK postcode");
        }

        private void ValidateClinicDetails()
        {
            RuleFor(x => x.ClinicDetails.ClinicName)
                .NotEmpty()
                .WithMessage("Clinic name is required")
                .MaximumLength(60)
                .WithMessage("Clinic name exceeds the maximum length of 60 characters");

            RuleFor(x => x.ClinicDetails.ClinicAddressLine1)
                .NotEmpty()
                .WithMessage("Clinic address line 1 is required")
                .MaximumLength(100)
                .WithMessage("Clinic address line 1 exceeds maximum length of 100 characters");

            RuleFor(x => x.ClinicDetails.ClinicAddressLine2)
                .MaximumLength(100)
                .WithMessage("Clinic address line 2 exceeds maximum length of 100 characters");

            RuleFor(x => x.ClinicDetails.ClinicAddressCity)
                .NotEmpty()
                .WithMessage("Clinic address city is required")
                .MaximumLength(60)
                .WithMessage("Clinic address city exceeds maximum length of 60 characters");

            RuleFor(x => x.ClinicDetails.ClinicAddressCountry)
                .NotEmpty()
                .WithMessage("Clinic address country is required")
                .Must(_countryHelper.IsInList)
                .WithMessage("Clinic address country is not a valid address country");

            RuleFor(x => x.ClinicDetails.ClinicAddressPostCode)
                .NotEmpty()
                .WithMessage("Clinic address postcode")
                .Matches(@"^([A-Z]{1,2}\d[A-Z\d]? ?\d[A-Z]{2}|GIR ?0A{2})$")
                .WithMessage("Address postcode is not a valid UK postcode");
        }
    }
}
