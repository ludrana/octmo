using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using oktmo.Models;

namespace oktmo.Pages.Codes
{
    public class RestoreModel : PageModel
    {
        private readonly oktmo.Data.ApplicationDbContext _context;

        public RestoreModel(oktmo.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OktmoEntry OktmoEntry { get; set; } = default!;
        public IList<OktmoEntry> OktmoEntryListNext { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oktmoentry = await _context.OktmoEntry.FirstOrDefaultAsync(m => m.Id == id);

            if (oktmoentry == null)
            {
                return NotFound();
            }
            else
            {
                OktmoEntry = oktmoentry;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var oktmoEntryToRestore = await _context.OktmoEntry.FindAsync(OktmoEntry.Id);

            if (oktmoEntryToRestore == null)
            {
                return NotFound();
            }

            oktmoEntryToRestore.IsActual = true;

            _context.OktmoEntry.Update(oktmoEntryToRestore);

            OktmoEntryListNext = await _context.OktmoEntry.ToListAsync();
            GetNextOktmoEntries(oktmoEntryToRestore);
            foreach (var entry in OktmoEntryListNext)
            {
                entry.IsActual = true; // Mark the entry as deleted
                _context.OktmoEntry.Update(entry);
            }

            await _context.SaveChangesAsync();

            return Redirect("./Details?id=" + OktmoEntry.Id);
        }

        public void GetNextOktmoEntries(OktmoEntry oktmoEntry)
        {
            if (oktmoEntry.Razdel == "2")
            {
                OktmoEntryListNext = [];
                return;
            }
            else
            {
                List<OktmoEntry> res = [];
                if (oktmoEntry.Kod1 == "000")
                {
                    for (int i = 1; i < 10; i++)
                    {
                        res.AddRange(OktmoEntryListNext.Where(e => e.Ter == oktmoEntry.Ter).ToList());
                    }
                }
                if (oktmoEntry.Kod1.Replace("0", "").Length == 1 && res.IsNullOrEmpty())
                {
                    for (int i = 1; i < 99; i++)
                    {
                        res.AddRange(OktmoEntryListNext.Where(e => e.Ter == oktmoEntry.Ter && e.Kod1.StartsWith(oktmoEntry.Kod1[..1])).ToList());
                    }
                }
                if (oktmoEntry.Kod2 == "000" && res.IsNullOrEmpty())
                {
                    for (int i = 1; i < 99; i++)
                    {
                        res.AddRange(OktmoEntryListNext.Where(e => e.Ter == oktmoEntry.Ter && e.Kod1 == oktmoEntry.Kod1).ToList());
                    }
                }
                if (oktmoEntry.Kod2.Replace("0", "").Length == 1 && res.IsNullOrEmpty())
                {
                    for (int i = 1; i < 99; i++)
                    {
                        res.AddRange(OktmoEntryListNext.Where(e => e.Ter == oktmoEntry.Ter && e.Kod1 == oktmoEntry.Kod1 && e.Kod2.StartsWith(oktmoEntry.Kod2[..1])).ToList());
                    }
                }
                if (res.IsNullOrEmpty())
                {
                res.AddRange(OktmoEntryListNext.Where(e => e.Octmo.StartsWith(oktmoEntry.Octmo[..8]) & e.Razdel == "2").ToList());
                }
                
                OktmoEntryListNext = res;
            }
        }
    }
}
