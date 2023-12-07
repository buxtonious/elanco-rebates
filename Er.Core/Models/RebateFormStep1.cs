using Er.Models.Entities;
using System.ComponentModel;

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

        [DisplayName("Title")]
        public string Salutation { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Address line 1")]
        public string PersonalAddressLine1 { get; set; }

        [DisplayName("Address line 2 (optional)")]
        public string PersonalAddressLine2 { get; set; }

        [DisplayName("Address city")]
        public string PersonalAddressCity { get; set; }

        [DisplayName("Address country")]
        public string PersonalAddressCountry { get; set; }

        [DisplayName("Address postcode")]
        public string PersonalAddressPostCode { get; set; }

        [DisplayName("Pet name")]
        public string PetName { get; set; }

        [DisplayName("Pet birthday (optional)")]
        public string PetBirthday { get; set; }

        public RebateOffer SelectedRebate { get; set; }

        public List<string> Salutations { get; private set; }
        public List<string> Countries { get; private set; }
    }
}
