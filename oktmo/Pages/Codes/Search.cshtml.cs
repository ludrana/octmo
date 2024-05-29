using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using oktmo.Models;

namespace oktmo.Pages.Codes
{
    public class SearchModel : PageModel
    {
        private readonly oktmo.Data.ApplicationDbContext _context;

        public SearchModel(oktmo.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OktmoEntry> OktmoEntry { get; set; } = default!;
        public IList<OktmoEntry> OktmoEntryHidden { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var entries = from m in _context.OktmoEntry
                         select m;
            if (SearchString.All(char.IsDigit))
            {
                entries = entries.Where(e => e.Octmo.Contains(SearchString));
            }
            else
            {
                entries = entries.Where(e => e.Name.Contains(SearchString));
            }
            OktmoEntryHidden = await entries.Where(o => o.IsActual == false).ToListAsync();
            OktmoEntry = await entries.Where(o => o.IsActual == true).ToListAsync();
        }
    }
}
