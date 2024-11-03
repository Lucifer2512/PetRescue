using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;

namespace PetRescueFE.Pages.Events
{
    public class DeleteModel : PageModel
    {
        private readonly DataAccessLayer.Context.PetRescueDbContext _context;

        public DeleteModel(DataAccessLayer.Context.PetRescueDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Event Event { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var eventt = await _context.Events.FirstOrDefaultAsync(m => m.EventId == id);

            if (eventt == null)
            {
                return NotFound();
            }
            else 
            {
                Event = eventt;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }
            var eventt = await _context.Events.FindAsync(id);

            if (eventt != null)
            {
                Event = eventt;
                _context.Events.Remove(Event);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
