using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace oktmo.Pages.Shared
{
    public class _SearchFormModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                // Redirect to SearchResultPage with the search string as a query parameter
                return Redirect("./Codes/Search?SearchString=" + SearchString);
            }
            else
            {
                return Page();
            }
        }
    }
}
