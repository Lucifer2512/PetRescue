﻿using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace PetRescueFE.Pages.DonationPage
{
    public class IndexModel : PageModel
    {

        private readonly DataAccessLayer.Context.PetRescueDbContext _context;

        public IndexModel(DataAccessLayer.Context.PetRescueDbContext context)
        {
            _context = context;
        }

        public IList<Donation> Donation { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Donations != null)
            {
                Donation = await _context.Donations
                .Include(d => d.Event)
                .Include(d => d.Shelter)
                .Include(d => d.User).ToListAsync();
            }
        }
    }
}
