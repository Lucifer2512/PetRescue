using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetRescueFE.Pages.Model.Events;

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
      public EventResponseModel Event { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {

            return RedirectToPage("./Index");
        }
    }
}
