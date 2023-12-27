using Er.Models.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Er.Core.Models
{
    public partial class RebateFormStep1
    {
        public RebateFormStep1()
        {

        }

        public RebateFormStep1(RebateOffer selectedRebate, List<string> salutations, List<string> countries)
        {
            SelectedRebate = selectedRebate;
            Salutations = salutations;
            Countries = countries;
        }

        [Required(ErrorMessage = "Title is required")]
        [DisplayName("Title")]
        public string Salutation { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "First name exceeds maximum length")]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Last name exceeds maximum length")]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(50, ErrorMessage = "Email exceeds maximum length")]
        [EmailAddress(ErrorMessage = "Email is not a valid email address")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address line 1 is required")]
        [MaxLength(128, ErrorMessage = "Address line 1 exceeds maximum length")]
        [DisplayName("Address line 1")]
        public string PersonalAddressLine1 { get; set; }

        [MaxLength(128, ErrorMessage = "Address line 2 exceeds maximum length")]
        [DisplayName("Address line 2 (optional)")]
        public string PersonalAddressLine2 { get; set; }

        [Required(ErrorMessage = "Address city is required")]
        [MaxLength(50, ErrorMessage = "Address city exceeds maximum length")]
        [DisplayName("Address city")]
        public string PersonalAddressCity { get; set; }

        [Required(ErrorMessage = "Address country is required")]
        [DisplayName("Address country")]
        public string PersonalAddressCountry { get; set; }

        [Required(ErrorMessage = "Address postcode is required")]
        [MaxLength(16, ErrorMessage = "Address postcode exceeds maximum length")]
        [RegularExpression(@"^([A-Z]{1,2}\d[A-Z\d]? ?\d[A-Z]{2}|GIR ?0A{2})$", ErrorMessage = "Address postcode is not a valid UK postcode")]
        [DisplayName("Address postcode")]
        public string PersonalAddressPostCode { get; set; }

        [Required(ErrorMessage = "Pet name is required")]
        [MaxLength(50, ErrorMessage = "Pet name exceeds maximum length")]
        [DisplayName("Pet name")]
        public string PetName { get; set; }

        [MaxLength(50, ErrorMessage = "Pet birthday exceeds maximum length")]
        [RegularExpression(@"^(0[1-9]|[1-2][0-9]|3[0-1])/(0[1-9]|1[0-2])/\d{4}$", ErrorMessage = "Pet birthday is not a valid birthday")]
        [DisplayName("Pet birthday (optional)")]
        public string PetBirthday { get; set; }

        [Required(ErrorMessage = "Receipt is required")]
        [DisplayName("Receipt")]
        public IFormFile UploadedReceipt { get; set; }
        public RebateOffer SelectedRebate { get; set; }

        public List<string> Salutations { get; private set; }
        public List<string> Countries { get; private set; }

        public void AddSelectedRebate(RebateOffer selectedRebate)
        {
            SelectedRebate = selectedRebate;
        }

        public void AddSalutations(List<string> salutations)
        {
            Salutations = salutations;
        }

        public void AddCountries(List<string> countries)
        {
            Countries = countries;
        }
    }
}
