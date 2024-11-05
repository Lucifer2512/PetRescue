using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http.HttpResults;
using PetRescueFE.Pages.Model;
using System.IdentityModel.Tokens.Jwt;

namespace PetRescueFE.Pages.DonationPage
{
    public class CreateModel : PageModel
    {
        private readonly ApiService _apiService;
        public CreateModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty(SupportsGet = true)]
        public Guid EventId { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid ShelterId { get; set; }

        [BindProperty]
        public DonationRequestModel Donation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            // Check if user is logged in and has correct role
            var role = HttpContext.Session.GetString("Role");
            if (role != "e7b8f3d2-4a2f-4c3b-8f4d-9c5d8a3e1b2c") // User role
            {
                return RedirectToPage("/Login");
            }

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostCreateAsync()
        {
            // Get user ID from session token
            var token = HttpContext.Session.GetString("JWTToken");
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;
            var userId = tokenS.Claims.First(claim => claim.Type == "AccountId").Value;

            // Set the IDs from route parameters and session
            Donation.EventId = EventId;
            Donation.ShelterId = ShelterId;
            Donation.UserId = Guid.Parse(userId);

            var data = await _apiService.PostAsync<DonationRequestModel,DonationReponseModel>("donation/createdonation", Donation); //"`endpoint-url` cho phù hợp"

            if (!ModelState.IsValid)
            {
                return Page();
            }          
            return RedirectToPage("./Index");
        }
    }
}
