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
                .WithMessage("Please select your salutation")
                .Must(_salutationHelper.IsInList)
                .WithMessage("Please select a valid salutation");

            RuleFor(x => x.PersonalDetails.FirstName)
                .NotEmpty()
                .WithMessage("Please enter your first name")
                .MaximumLength(60)
                .WithMessage("First name exceeds maximum length of 60 characters");

            RuleFor(x => x.PersonalDetails.LastName)
                .NotEmpty()
                .WithMessage("Please enter your last name")
                .MaximumLength(60)
                .WithMessage("Last name exceeds maximum length of 60 characters");

            RuleFor(x => x.PersonalDetails.PetName)
                .NotEmpty()
                .WithMessage("Please enter your pet name")
                .MaximumLength(60)
                .WithMessage("Pet name exceeds maximum length of 60 characeters");

            RuleFor(x => x.PersonalDetails.PetBirthday)
                .NotEmpty()
                .Must(x => DateTime.TryParse(x, out _))
                .When(x => string.IsNullOrEmpty(x.PersonalDetails.PetBirthday))
                .WithMessage("Please enter a valid pet birthday");

            RuleFor(x => x.PersonalDetails.PersonalAddressLine1)
                .NotEmpty()
                .WithMessage("Please enter your address line 1")
                .MaximumLength(100)
                .WithMessage("Address line 1 exceeds maximum length of 100 characters");

            RuleFor(x => x.PersonalDetails.PersonalAddressLine2)
                .MaximumLength(100)
                .WithMessage("Address line 2 exceeds maximum length of 100 characters");

            RuleFor(x => x.PersonalDetails.PersonalAddressCity)
                .NotEmpty()
                .WithMessage("Please enter your address city")
                .MaximumLength(60)
                .WithMessage("Address city exceeds maximum length of 60 characteres");

            RuleFor(x => x.PersonalDetails.PersonalAddressCountry)
                .NotEmpty()
                .WithMessage("Please select your address country")
                .Must(_countryHelper.IsInList)
                .WithMessage("Please select a valid address country");

            RuleFor(x => x.PersonalDetails.PersonalAddressPostCode)
                .NotEmpty()
                .WithMessage("Please enter your address postcode")
                .Matches(@"^([A-Z]{1,2}\d[A-Z\d]? ?\d[A-Z]{2}|GIR ?0A{2})$")
                .WithMessage("Please enter a valid UK postcode for your address");
        }

        private void ValidateClinicDetails()
        {
            RuleFor(x => x.ClinicDetails.ClinicName)
                .NotEmpty()
                .WithMessage("Please enter your clinic name")
                .MaximumLength(60)
                .WithMessage("Clinic name exceeds the maximum length of 60 characters");

            RuleFor(x => x.ClinicDetails.ClinicAddressLine1)
                .NotEmpty()
                .WithMessage("Please enter your clinic address line 1")
                .MaximumLength(100)
                .WithMessage("Clinic address line 1 exceeds maximum length of 100 characters");

            RuleFor(x => x.ClinicDetails.ClinicAddressLine2)
                .MaximumLength(100)
                .WithMessage("Clinic address line 2 exceeds maximum length of 100 characters");

            RuleFor(x => x.ClinicDetails.ClinicAddressCity)
                .NotEmpty()
                .WithMessage("Please enter your clinic address city")
                .MaximumLength(60)
                .WithMessage("Clinic address city exceeds maximum length of 60 characteres");

            RuleFor(x => x.ClinicDetails.ClinicAddressCountry)
                .NotEmpty()
                .WithMessage("Please select your clinic address country")
                .Must(_countryHelper.IsInList)
                .WithMessage("Please select a valid clinic address country");

            RuleFor(x => x.ClinicDetails.ClinicAddressPostCode)
                .NotEmpty()
                .WithMessage("Please enter your clinic address postcode")
                .Matches(@"^([A-Z]{1,2}\d[A-Z\d]? ?\d[A-Z]{2}|GIR ?0A{2})$")
                .WithMessage("Please enter a valid UK postcode for your clinic address");
        }
    }
}
