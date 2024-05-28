using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using oktmo.Data;
using oktmo.Models;

namespace oktmo.Pages.Codes
{
    public class DeleteModel : PageModel
    {
        private readonly oktmo.Data.ApplicationDbContext _context;

        public DeleteModel(oktmo.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OktmoEntry OktmoEntry { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oktmoentry = await _context.OktmoEntry.FindAsync(id);
            if (oktmoentry != null)
            {
                OktmoEntry = oktmoentry;
                _context.OktmoEntry.Remove(OktmoEntry);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
