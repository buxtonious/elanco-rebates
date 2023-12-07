using Er.Core.Interfaces;
using Er.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Er.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRebateOfferService _rebateOfferService;

        public IndexModel(IRebateOfferService rebateOfferService)
        {
            _rebateOfferService = rebateOfferService;
        }

        public List<RebateOffer> RebateOffers { get; private set; }

        public void OnGet()
        {
            RebateOffers = _rebateOfferService.ListAll();
        }

        public IActionResult OnPost(Guid rebateOfferId)
        {
            var matchedRebate = _rebateOfferService.Find(rebateOfferId);

            if (matchedRebate == null)
            {
                return Page();
            }

            return RedirectToPage("Rebate", new { @id = matchedRebate.Id });
        }
    }
}
