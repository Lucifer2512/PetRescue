using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PetRescueFE.Pages.Model;
using PetRescueFE.SignalRealtime;
using System.IdentityModel.Tokens.Jwt;


namespace PetRescueFE.Pages.AdoptionApplicationPage
{
    public class CreateModel : PageModel
    {
        private readonly ApiService _apiService;

        public CreateModel(ApiService apiService)
        {
            _apiService = apiService;
        }
        [BindProperty]
        public AdoptionApplicationRequestModel AdoptionApplication { get; set; } = new AdoptionApplicationRequestModel();
        public async Task<IActionResult> OnGet(Guid id)
        {
            AdoptionApplication.PetId = id;
            
            return Page();
        }

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
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error creating application");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
