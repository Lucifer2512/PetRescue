using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;
using PetRescueFE.Pages.Model;
using Azure.Core;
using System.Security.Claims;


namespace PetRescueFE.Pages.AdoptionApplicationPage
{
    public class CreateModel : PageModel
    {
        private readonly ApiService _apiService;

        public CreateModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> OnGet()
        {
            var userUrl = "https://localhost:7297/api/users";
            var petUrl = "https://localhost:7297/api/pet";
            var userResponse = await _apiService.GetAsync<BaseResponseModelFE<IEnumerable<UserResponseModelFE>>>(userUrl);
            var petResponse = await _apiService.GetAsync<BaseResponseModelFE<IEnumerable<PetResponseModelFE>>>(petUrl);

            ViewData["PetId"] = new SelectList(petResponse.Data, "PetId", "Name");
            ViewData["UserId"] = new SelectList(userResponse.Data, "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public AdoptionApplicationRequestModel AdoptionApplication { get; set; } = new AdoptionApplicationRequestModel();
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || AdoptionApplication == null)
            {
                return Page();
            }
            var userId = HttpContext.Session.GetString("UserId");
            AdoptionApplication.UserId = Guid.Parse(userId);
            var apiUrl = "https://localhost:7297/api/adoptionapplication";
            try
            {
                var data = await _apiService.PostAsync<AdoptionApplicationRequestModel, BaseResponseModelFE<AdoptionApplicationResponseModel>>(apiUrl, AdoptionApplication);
            }catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error creating application");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
