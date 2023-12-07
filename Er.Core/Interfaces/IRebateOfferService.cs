using Er.Models.Entities;

namespace Er.Core.Interfaces
{
    public interface IRebateOfferService
    {
        RebateOffer Find(Guid rebateOfferId);
        List<RebateOffer> ListAll();
    }
}
