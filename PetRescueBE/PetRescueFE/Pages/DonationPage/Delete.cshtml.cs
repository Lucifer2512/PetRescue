using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace PetRescueFE.Pages.DonationPage
{
    public class DeleteModel : PageModel
    {
        private readonly DataAccessLayer.Context.PetRescueDbContext _context;

        public DeleteModel(DataAccessLayer.Context.PetRescueDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Donation Donation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Donations == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations.FirstOrDefaultAsync(m => m.DonationId == id);

            if (donation == null)
            {
                return NotFound();
            }
            else
            {
                Donation = donation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Donations == null)
            {
                return NotFound();
            }
            var donation = await _context.Donations.FindAsync(id);

            if (donation != null)
            {
                Donation = donation;
                _context.Donations.Remove(Donation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
