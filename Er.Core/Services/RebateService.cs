using Er.Core.Helpers;
using Er.Core.Interfaces;
using Er.Core.Models;

namespace Er.Core.Services
{
    internal class RebateService : IRebateService
    {
        private readonly IRebateOfferService _rebateOfferService;
        private readonly SalutationHelper _salutationHelper;
        private readonly CountryHelper _countryHelper;

        public RebateService(IRebateOfferService rebateOfferService,
            SalutationHelper salutationHelper, CountryHelper countryHelper)
        {
            _rebateOfferService = rebateOfferService;
            _salutationHelper = salutationHelper;
            _countryHelper = countryHelper;
        }

        public RebateForm BeginForm(Guid rebateOfferId)
        {
            var matchedRebate = _rebateOfferService.Find(rebateOfferId);
            var salutations = _salutationHelper.ListAll();
            var countries = _countryHelper.ListAll();

            return new RebateForm(matchedRebate, salutations, countries);
        }

        public RebateForm RepopulateForm(RebateForm model)
        {
            var rebateOfferId = model.PersonalDetails.SelectedRebate.Id;

            var matchedRebate = _rebateOfferService.Find(rebateOfferId);
            var salutations = _salutationHelper.ListAll();
            var countries = _countryHelper.ListAll();

            model.PersonalDetails.AddSelectedRebate(matchedRebate);
            model.PersonalDetails.AddSalutations(salutations);
            model.PersonalDetails.AddCountries(countries);
            model.ClinicDetails.AddCountries(countries);

            return model;
        }

        public void SubmitForm(RebateForm model)
        {
            throw new NotImplementedException();
        }
    }
}
