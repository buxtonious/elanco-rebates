using Er.Core.Interfaces;
using Er.Core.Models;
using Er.Core.Validators;
using Er.Models.ValueObjects;
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
        [ValidateAntiForgeryToken]
        public IActionResult Submit(RebateForm model)
        {
            var validationResult = _rebateValidator.Validate(model);

            if (!validationResult.IsValid)
            {
                var errors = new List<Error>();

                foreach (var error in validationResult.Errors)
                {
                    errors.Add(new Error(error.PropertyName, error.ErrorMessage));
                }

                return BadRequest(errors);
            }

            return RedirectToPage("ThankYou");
        }
    }
}
