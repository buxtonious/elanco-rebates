using Er.Core.Interfaces;
using Er.Core.Models;
using Er.Core.Validators;
using Er.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Er.Web.Controllers
{
    [Controller]
    public class RebateController : Controller
    {
        private readonly IRebateService _rebateService;
        private readonly RebateValidator _rebateValidator;

        public RebateController(IRebateService rebateService, RebateValidator rebateValidator)
        {
            _rebateService = rebateService;
            _rebateValidator = rebateValidator;
        }

        [HttpGet]
        public IActionResult Claim(Guid id)
        {
            var rebateForm = _rebateService.BeginForm(id);

            return View(new ClaimViewModel()
            {
                FormData = rebateForm
            });
        }

        [HttpPost]
        public IActionResult Upload(RebateForm model)
        {
            _rebateService.RepopulateForm(model);

            return PartialView("Partials/_RebateForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(RebateForm model)
        {
            var validationResult = _rebateValidator.Validate(model);

            if (!validationResult.IsValid)
            {
                _rebateService.RepopulateForm(model);

                foreach (var result in validationResult.Errors)
                {
                    ModelState.AddModelError(result.PropertyName, result.ErrorMessage);
                }

                return PartialView("Partials/_RebateForm", model);
            }

            return RedirectToAction("ThankYou");
        }
    }
}
