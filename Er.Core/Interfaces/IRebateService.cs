using Er.Core.Models;

namespace Er.Core.Interfaces
{
    public interface IRebateService
    {
        RebateForm BeginForm(Guid rebateOfferId);
        RebateForm RepopulateForm(RebateForm model);
        void SubmitForm(RebateForm model);
    }
}
