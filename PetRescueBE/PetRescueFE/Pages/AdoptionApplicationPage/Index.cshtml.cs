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

        public async Task OnGetAsync()
        {
            var apiUrl = "https://localhost:7297/api/adoptionapplication";
            var role = HttpContext.Session.GetString("Role");
            var userId = HttpContext.Session.GetString("UserId");
            var response = new BaseResponseModelFE<IList<AdoptionApplicationResponseModel>>();
            switch (role)
            {
                case "d290f1ee-6c54-4b01-90e6-d701748f0851":      //Administrator
                    response = await _apiService.GetAsync<BaseResponseModelFE<IList<AdoptionApplicationResponseModel>>>("https://localhost:7297/api/adoptionapplication");
                    break;
                case "f3c8d4e5-6b7a-4c9d-8e2f-0a1b2c3d4e5f":      //ShelterOwner
                    response = await _apiService.GetAsync<BaseResponseModelFE<IList<AdoptionApplicationResponseModel>>>($"https://localhost:7297/api/adoptionapplication/shelter/{userId}");
                    break;
                case "e7b8f3d2-4a2f-4c3b-8f4d-9c5d8a3e1b2c":     //User
                    response = await _apiService.GetAsync<BaseResponseModelFE<IList<AdoptionApplicationResponseModel>>>($"https://localhost:7297/api/adoptionapplication/user/{userId}");
                    break;
            }
            if (response.Data != null)
            {
                AdoptionApplication = response.Data;
            }
        }
    }
}
