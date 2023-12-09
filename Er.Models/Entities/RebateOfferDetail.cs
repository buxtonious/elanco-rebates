namespace Er.Models.Entities
{
    public class RebateOfferDetail
    {
        public string Dosage { get; set; }
        public decimal AmountInPence { get; set; }

        public decimal CalculateAmountInPounds()
        {
            return AmountInPence / 100;
        }
    }
}
