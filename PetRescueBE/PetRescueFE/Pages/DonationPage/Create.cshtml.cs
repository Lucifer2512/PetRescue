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

namespace PetRescueFE.Pages.DonationPage
{
    public class CreateModel : PageModel
    {
        private readonly ApiService _apiService;
        public CreateModel(ApiService apiService)
        {
            _apiService = apiService;
        }

       /* public async Task OnGetAsync()
        {
           
            return Page();
        }*/

        [BindProperty]
        public DonationRequestModel Donation { get; set; } = default!;
       


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostCreateAsync()
        {
            Donation.ShelterId =Guid.Parse("2f78ddb6-1b06-4730-a23b-f44fd1d3bfff");
            Donation.EventId = Guid.Parse("422916e7-3d1e-4664-a194-d33bdb8a19df");
            Donation.UserId = Guid.Parse("3f21226b-30c1-4274-81a6-2ed9d9e0c54c");

            var data = await _apiService.PostAsync<DonationRequestModel,DonationReponseModel>("donation/createdonation", Donation); //"`endpoint-url` cho phù hợp"

            if (!ModelState.IsValid)
            {
                return Page();
            }          
            return RedirectToPage("./Index");
        }
    }
}
