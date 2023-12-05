using Er.Core.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Er.Web.Pages
{
    public class RebateModel : PageModel
    {
        public RebateModel()
        {
            var salutations = new List<string>();
            var countries = new List<string>();

            FormData = new RebateForm(salutations, countries);
        }

        public RebateForm FormData { get; set; }

        public void OnGet()
        {
        }
    }
}
