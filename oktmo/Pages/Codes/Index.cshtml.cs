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
    public class IndexModel : PageModel
    {
        private readonly oktmo.Data.ApplicationDbContext _context;

        public IndexModel(oktmo.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OktmoEntry> OktmoEntry { get;set; } = default!;
        public IList<OktmoEntry> OktmoEntryHidden { get; set; } = default!;


        public async Task OnGetAsync()
        {
            List<OktmoEntry> res = await _context.OktmoEntry.ToListAsync();
            OktmoEntryHidden = res.Where(o => o.IsActual == false && o.Kod1 == "000").ToList();
            OktmoEntry = res.Where(e => e.IsActual && e.Kod1 == "000").ToList();
        }
    }
}
