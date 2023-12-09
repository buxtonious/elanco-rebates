using Er.Core.Interfaces;
using Er.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Er.Web.Controllers
{
    [Controller]
    public class OfferController : Controller
    {
        private readonly IRebateOfferService _rebateOfferService;

        public OfferController(IRebateOfferService rebateOfferService)
        {
            _rebateOfferService = rebateOfferService;
        }

        [HttpGet]
        public IActionResult Offers()
        {
            var rebateOffers = _rebateOfferService.ListAll();

            return View(new OffersViewModel()
            {
                RebateOffers = rebateOffers
            });
        }

        [HttpPost]
        public IActionResult SelectRebateOffer(Guid rebateOfferId)
        {
            var matchedRebateOffer = _rebateOfferService.Find(rebateOfferId);

            if (matchedRebateOffer == null)
            {
                return RedirectToAction("Offers");
            }

            return RedirectToAction("Claim", "Rebate", new { id = matchedRebateOffer.Id });
        }
    }
}
