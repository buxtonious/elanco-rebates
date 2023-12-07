using Er.Core.Models;

namespace Er.Core.Interfaces
{
    public interface IRebateService
    {
        RebateForm BeginForm(Guid rebateOfferId);
        void SubmitForm(RebateForm model);
    }
}
