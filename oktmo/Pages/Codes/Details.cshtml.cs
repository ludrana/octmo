using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using oktmo.Data;
using oktmo.Models;

namespace oktmo.Pages.Codes
{
    public class DetailsModel : PageModel
    {
        private readonly oktmo.Data.ApplicationDbContext _context;

        public DetailsModel(oktmo.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public OktmoEntry OktmoEntry { get; set; } = default!;

        public IList<OktmoEntry> OktmoEntryListPrev { get; set; } = default!;
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
                await GetPrevOktmoEntries(oktmoentry);
                OktmoEntryListNext = await _context.OktmoEntry.ToListAsync();
                GetNextOktmoEntries(oktmoentry);
            }

            return Page();
        }

        public async Task GetPrevOktmoEntries(OktmoEntry oktmoEntry)
        {
            var oktmonum = oktmoEntry.Octmo;
            oktmonum = oktmonum.TrimEnd('0');
            if (oktmoEntry.Razdel == "2")
                oktmonum = oktmonum[..^3];
            else
                oktmonum = oktmonum[..^1]; // fix later TODO
            OktmoEntryListPrev = await _context.OktmoEntry.ToListAsync();
            List<OktmoEntry> res = [];
            while (!oktmonum.IsNullOrEmpty())
            {
                oktmonum = oktmonum.TrimEnd('0');
                var num = oktmonum + new String('0', 11 - oktmonum.Length);
                var items = OktmoEntryListPrev.Where(e => e.Octmo == num).ToList();
                res.InsertRange(0, items);
                oktmonum = oktmonum[..^1];
            }
            OktmoEntryListPrev = res;
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
                        var num = oktmoEntry.Ter + i.ToString() + new String('0', 8);
                        res.AddRange(OktmoEntryListNext.Where(e => e.Octmo == num).ToList());
                    }
                }
                if (oktmoEntry.Kod1.Replace("0", "").Length == 1 && res.IsNullOrEmpty())
                {
                    for (int i = 1; i < 99; i++)
                    {
                        var num = oktmoEntry.Ter + oktmoEntry.Kod1[..1] + i.ToString("D2") + new String('0', 6);
                        res.AddRange(OktmoEntryListNext.Where(e => e.Octmo == num).ToList());
                    }
                }
                if (oktmoEntry.Kod2 == "000" && res.IsNullOrEmpty())
                {
                    for (int i = 1; i < 99; i++)
                    {
                        var num = oktmoEntry.Ter + oktmoEntry.Kod1 + i.ToString() + new String('0', 5);
                        res.AddRange(OktmoEntryListNext.Where(e => e.Octmo == num).ToList());
                    }
                }
                if (oktmoEntry.Kod2.Replace("0", "").Length == 1 && res.IsNullOrEmpty())
                {
                    for (int i = 1; i < 99; i++)
                    {
                        var num = oktmoEntry.Ter + oktmoEntry.Kod1 + oktmoEntry.Kod2[..1] + i.ToString("D2") + new String('0', 3);
                        res.AddRange(OktmoEntryListNext.Where(e => e.Octmo == num).ToList());
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
