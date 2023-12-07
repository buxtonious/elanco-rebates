using Er.Models.Entities;

namespace Er.Core.Models
{
    public partial class RebateForm
    {
        public RebateForm()
        {

        }

        public RebateForm(RebateOffer selectedRebate, List<string> salutations,
            List<string> countries)
        {
            PersonalDetails = new RebateFormStep1(selectedRebate, salutations, countries);
            ClinicDetails = new RebateFormStep2(countries);
            Preferences = new RebateFormStep3();
        }

        public RebateFormStep1 PersonalDetails { get; set; }
        public RebateFormStep2 ClinicDetails { get; set; }
        public RebateFormStep3 Preferences { get; set; }
    }
}
