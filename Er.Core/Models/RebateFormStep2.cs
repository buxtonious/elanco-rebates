using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Clinic name is required")]
        [MaxLength(100, ErrorMessage = "Clinic name exceeds maximum length")]
        [DisplayName("Clinic name")]
        public string ClinicName { get; set; }

        [Required(ErrorMessage = "Clinic address line 1 is required")]
        [MaxLength(100, ErrorMessage = "Clinic address line 1 exceeds maximum length")]
        [DisplayName("Clinic address line 1")]
        public string ClinicAddressLine1 { get; set; }

        [MaxLength(100, ErrorMessage = "Clinic address line 2 exceeds maximum length")]
        [DisplayName("Clinic address line 2 (optional)")]
        public string ClinicAddressLine2 { get; set; }

        [Required(ErrorMessage = "Clinic address city is required")]
        [MaxLength(60, ErrorMessage = "Clinic address city exceeds maximum length")]
        [DisplayName("Clinic address city")]
        public string ClinicAddressCity { get; set; }

        [Required(ErrorMessage = "Clinic address country is required")]
        [DisplayName("Clinic address country")]
        public string ClinicAddressCountry { get; set; }

        [Required(ErrorMessage = "Clinic address postcode is required")]
        [MaxLength(16, ErrorMessage = "Clinic address postcode exceeds maximum length")]
        [RegularExpression(@"^([A-Z]{1,2}\d[A-Z\d]? ?\d[A-Z]{2}|GIR ?0A{2})$", ErrorMessage = "Clinic address postcode is not a valid UK postcode")]
        [DisplayName("Clinic address postcode")]
        public string ClinicAddressPostCode { get; set; }

        public List<string> Countries { get; private set; }
    }
}
