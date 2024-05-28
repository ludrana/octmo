using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using oktmo.Data;
using oktmo.Models;

namespace oktmo.Pages.Codes
{
    public class CreateModel : PageModel
    {
        private readonly oktmo.Data.ApplicationDbContext _context;

        public CreateModel(oktmo.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public OktmoEntry OktmoEntry { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OktmoEntry.Add(OktmoEntry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
