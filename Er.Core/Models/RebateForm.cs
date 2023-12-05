namespace Er.Core.Models
{
    public partial class RebateForm
    {
        public RebateForm(List<string> salutations, List<string> countries)
        {
            PersonalDetails = new RebateFormStep1(salutations, countries);
            ClinicDetails = new RebateFormStep2(countries);
        }

        public RebateFormStep1 PersonalDetails { get; set; }
        public RebateFormStep2 ClinicDetails { get; set; }
        public RebateFormStep3 Preferences { get; set; }
    }
}
