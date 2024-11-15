using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;

namespace PetRescueFE.Pages.AdoptionApplicationPage
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public IList<AdoptionApplicationResponseModel> AdoptionApplication { get; set; } = default!;
        public string StatusFilter { get; set; }

        public async Task<IActionResult> OnGetAsync(string statusFilter = "all")
        {
            StatusFilter = statusFilter;
            var apiUrl = "https://localhost:7297/api/adoptionapplication";
            var role = HttpContext.Session.GetString("Role");
            var userId = HttpContext.Session.GetString("UserId");
            if(role == "" || userId == "")
            {
                return RedirectToPage("/Login");
            }
            var response = new BaseResponseModelFE<IList<AdoptionApplicationResponseModel>>();
            switch (role)
            {
                case "d290f1ee-6c54-4b01-90e6-d701748f0851":      //Administrator
                    response = await _apiService.GetAsync<BaseResponseModelFE<IList<AdoptionApplicationResponseModel>>>($"https://localhost:7297/api/adoptionapplication?status={statusFilter}");
                    break;
                case "f3c8d4e5-6b7a-4c9d-8e2f-0a1b2c3d4e5f":      //ShelterOwner
                    response = await _apiService.GetAsync<BaseResponseModelFE<IList<AdoptionApplicationResponseModel>>>($"https://localhost:7297/api/adoptionapplication/shelter/{userId}?status={statusFilter}");
                    break;
                case "e7b8f3d2-4a2f-4c3b-8f4d-9c5d8a3e1b2c":     //User
                    response = await _apiService.GetAsync<BaseResponseModelFE<IList<AdoptionApplicationResponseModel>>>($"https://localhost:7297/api/adoptionapplication/user/{userId}?status={statusFilter}");
                    break;
            }
            if (response.Data == null)
            {
                return RedirectToPage("/Login");
            }
            AdoptionApplication = response.Data;
            return Page();
        }
    }
}
