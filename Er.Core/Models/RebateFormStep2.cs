using System.ComponentModel;

namespace Er.Core.Models
{
    public partial class RebateFormStep2
    {
        public RebateFormStep2()
        {

        }

        public RebateFormStep2(List<string> countries)
        {
            Countries = countries;
        }

        [DisplayName("Clinic name")]
        public string ClinicName { get; set; }

        [DisplayName("Clinic address line 1")]
        public string ClinicAddressLine1 { get; set; }

        [DisplayName("Clinic address line 2 (optional)")]
        public string ClinicAddressLine2 { get; set; }

        [DisplayName("Clinic address city")]
        public string ClinicAddressCity { get; set; }

        [DisplayName("Clinic address country")]
        public string ClinicAddressCountry { get; set; }

        [DisplayName("Clinic address postcode")]
        public string ClinicAddressPostCode { get; set; }

        public List<string> Countries { get; private set; }
    }
}
