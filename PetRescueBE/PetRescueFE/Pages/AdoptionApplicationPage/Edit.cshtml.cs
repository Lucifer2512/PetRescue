using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;

namespace PetRescueFE.Pages.AdoptionApplicationPage
{
    public class EditModel : PageModel
    {
        private readonly DataAccessLayer.Context.PetRescueDbContext _context;

        public EditModel(DataAccessLayer.Context.PetRescueDbContext context)
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

            var adoptionapplication =  await _context.AdoptionApplications.FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (adoptionapplication == null)
            {
                return NotFound();
            }
            AdoptionApplication = adoptionapplication;
           ViewData["PetId"] = new SelectList(_context.Pets, "PetId", "Species");
           ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
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

            _context.Attach(AdoptionApplication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdoptionApplicationExists(AdoptionApplication.ApplicationId))
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

        private bool AdoptionApplicationExists(Guid id)
        {
          return (_context.AdoptionApplications?.Any(e => e.ApplicationId == id)).GetValueOrDefault();
        }
    }
}
