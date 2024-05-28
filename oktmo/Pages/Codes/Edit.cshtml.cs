using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using oktmo.Data;
using oktmo.Models;

namespace oktmo.Pages.Codes
{
    public class EditModel : PageModel
    {
        private readonly oktmo.Data.ApplicationDbContext _context;

        public EditModel(oktmo.Data.ApplicationDbContext context)
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

            var oktmoentry =  await _context.OktmoEntry.FirstOrDefaultAsync(m => m.Id == id);
            if (oktmoentry == null)
            {
                return NotFound();
            }
            OktmoEntry = oktmoentry;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OktmoEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OktmoEntryExists(OktmoEntry.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OktmoEntryExists(int id)
        {
            return _context.OktmoEntry.Any(e => e.Id == id);
        }
    }
}
