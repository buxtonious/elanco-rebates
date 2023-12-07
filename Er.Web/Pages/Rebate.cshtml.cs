using Er.Core.Interfaces;
using Er.Core.Models;
using Er.Core.Responses;
using Er.Core.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Er.Web.Pages
{
    [ValidateAntiForgeryToken]
    public class RebateModel : PageModel
    {
        private readonly IRebateService _rebateService;
        private readonly RebateValidator _rebateValidator;

        public RebateModel(IRebateService rebateService, RebateValidator rebateValidator)
        {
            _rebateService = rebateService;
            _rebateValidator = rebateValidator;
        }

        [BindProperty]
        public RebateForm FormData { get; set; }

        public void OnGet(Guid id)
        {
            FormData = _rebateService.BeginForm(id);
        }

        public IActionResult OnPost()
        {
            var validationResult = _rebateValidator.Validate(FormData);

            if (!validationResult.IsValid)
            {
                var response = new ErrorResponse();

                foreach (var error in validationResult.Errors)
                {
                    response.Errors.Add(new Error(error.PropertyName, error.ErrorMessage));
                }

                return new JsonResult(response.Errors);
            }

            return RedirectToPage("ThankYou");
        }
    }
}
