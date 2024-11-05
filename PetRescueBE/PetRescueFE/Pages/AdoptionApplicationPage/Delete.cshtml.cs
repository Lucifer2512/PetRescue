using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;

namespace PetRescueFE.Pages.AdoptionApplicationPage
{
    public class DeleteModel : PageModel
    {
        private readonly DataAccessLayer.Context.PetRescueDbContext _context;

        public DeleteModel(DataAccessLayer.Context.PetRescueDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public AdoptionApplication AdoptionApplication { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.AdoptionApplications == null)
            {
                return NotFound();
            }

            var adoptionapplication = await _context.AdoptionApplications.FirstOrDefaultAsync(m => m.ApplicationId == id);

            if (adoptionapplication == null)
            {
                return NotFound();
            }
            else 
            {
                AdoptionApplication = adoptionapplication;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.AdoptionApplications == null)
            {
                return NotFound();
            }
            var adoptionapplication = await _context.AdoptionApplications.FindAsync(id);

            if (adoptionapplication != null)
            {
                AdoptionApplication = adoptionapplication;
                _context.AdoptionApplications.Remove(AdoptionApplication);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
