using Er.Core.Models;

namespace Er.Core.Interfaces
{
    public interface IRebateService
    {
        RebateForm BeginForm(Guid rebateOfferNId);
        void SubmitForm(RebateForm model);
    }
}
