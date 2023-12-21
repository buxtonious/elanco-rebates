using System.ComponentModel;

namespace Er.Core.Models
{
    public partial class RebateFormStep3
    {
        public RebateFormStep3()
        {

        }

        [DisplayName("Payment method")]
        public string PaymentMethod { get; set; }

        [DisplayName("I want to be kept up-to-date by email with the latest news and offers")]
        public bool OptInForEmails { get; set; }

        [DisplayName("I want to be kept up-to-date by post with the latest news and offers")]
        public bool OptInForPost { get; set; }

        [DisplayName("Do you want to create an account with the details you've provided?")]
        public bool AutomaticallyCreateAccount { get; set; }

        [DisplayName("I agree to the privacy policy")]
        public bool AgreeToPrivacyPolicy { get; set; }

        [DisplayName("I agree to the terms and conditions")]
        public bool AgreeToTermsAndConditions { get; set; }
    }
}
