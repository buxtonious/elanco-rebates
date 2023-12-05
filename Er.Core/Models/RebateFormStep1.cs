using System.ComponentModel;

namespace Er.Core.Models
{
    public partial class RebateFormStep1
    {
        public RebateFormStep1(List<string> salutations, List<string> countries)
        {
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
        public string AddressLine1 { get; set; }

        [DisplayName("Address line 2 (optional)")]
        public string AddressLine2 { get; set; }

        [DisplayName("Address city")]
        public string AddressCity { get; set; }

        [DisplayName("Address country")]
        public string AddressCountry { get; set; }

        [DisplayName("Address postcode")]
        public string AddressPostCode { get; set; }

        [DisplayName("Pet name")]
        public string PetName { get; set; }

        [DisplayName("Pet birthday (optional)")]
        public string PetBirthday { get; set; }

        public List<string> Salutations { get; private set; }
        public List<string> Countries { get; private set; }
    }
}
