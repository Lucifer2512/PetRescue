using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PetRescueFE.Pages.DonationPage
{
    public class EditModel : PageModel
    {


        public EditModel()
        {

        }

        [BindProperty]
        public Donation Donation { get; set; } = default!;

        /*public async Task<IActionResult> OnGetAsync(Guid? id)
        {
*//*            if (id == null || _context.Donations == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations.FirstOrDefaultAsync(m => m.DonationId == id);
            if (donation == null)
            {
                return NotFound();
            }
            Donation = donation;
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventType");
            ViewData["ShelterId"] = new SelectList(_context.Shelters, "ShelterId", "ShelterAddress");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");*//*
            return Page();
        }*/

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        /* public async Task<IActionResult> OnPostAsync()
         {
             if (!ModelState.IsValid)
             {
                 return Page();
             }

             _context.Attach(Donation).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!DonationExists(Donation.DonationId))
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

         private bool DonationExists(Guid id)
         {
           return (_context.Donations?.Any(e => e.DonationId == id)).GetValueOrDefault();
         }*/
    }
}
