using Er.Core.Interfaces;
using Er.Models.Entities;

namespace Er.Core.Services
{
    internal class RebateOfferService : IRebateOfferService
    {
        private readonly List<RebateOffer> rebateOffers;

        public RebateOfferService()
        {
            rebateOffers = GenerateFakeData();
        }

        public RebateOffer Find(Guid rebateOfferId)
        {
            var rebateOffer = rebateOffers
                .FirstOrDefault(x => x.Id.Equals(rebateOfferId));

            return rebateOffer;
        }

        public List<RebateOffer> ListAll()
        {
            var availableOffers = rebateOffers;

            return availableOffers;
        }

        private List<RebateOffer> GenerateFakeData()
        {
            return new List<RebateOffer>()
            {
                new RebateOffer()
                {
                    Id = new Guid("1134F08B-874E-45DD-B627-91A2B36F4926"),
                    Name = "Credelio",
                    Code = "CRED2022",
                    StartDate = DateTime.Parse("31/01/2023"),
                    EndDate = DateTime.Parse("31/12/2023"),
                    SubmissionDeadline = DateTime.Parse("31/12/2023"),
                    OfferDetails = new List<RebateOfferDetail>()
                    {
                        new RebateOfferDetail()
                        {
                            Dosage = "6 doses",
                            AmountInPence = 1500
                        },
                        new RebateOfferDetail()
                        {
                            Dosage = "12 doses",
                            AmountInPence = 3500
                        }
                    }
                }
            };
        }
    }
}
