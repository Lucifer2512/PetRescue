using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Shelters;

namespace PetRescueFE.Pages.ShelterPage
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public IList<ShelterResponseModel> Shelter { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("Role");
            var userId = HttpContext.Session.GetString("UserId");
            
            if (role != "d290f1ee-6c54-4b01-90e6-d701748f0851" && role != "f3c8d4e5-6b7a-4c9d-8e2f-0a1b2c3d4e5f")
            {
                return RedirectToPage("/AuthorizationError");
            }

            if (role == "f3c8d4e5-6b7a-4c9d-8e2f-0a1b2c3d4e5f")
            {
                var apiUrlForShelterOwner = $"https://localhost:7297/api/shelter/userId/{userId}";
                var responseForShelterOwner = await _apiService.GetAsync<BaseResponseModelFE<IList<ShelterResponseModel>>>(apiUrlForShelterOwner);

                if (responseForShelterOwner.Data != null)
                {
                    Shelter = responseForShelterOwner.Data;
                }

                return Page();
            }

            var apiUrlForAdmin = "https://localhost:7297/api/shelter";
            var responseForAdmin = await _apiService.GetAsync<BaseResponseModelFE<IList<ShelterResponseModel>>>(apiUrlForAdmin);

            if (responseForAdmin.Data != null)
            {
                Shelter = responseForAdmin.Data;
            }

            return Page();
        }
    }
}
