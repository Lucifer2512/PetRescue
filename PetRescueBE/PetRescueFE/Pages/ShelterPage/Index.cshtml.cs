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

        public async Task OnGetAsync()
        {
            var apiUrl = "https://localhost:7297/api/shelter";
            var response = await _apiService.GetAsync<BaseResponseModelFE<IList<ShelterResponseModel>>>(apiUrl);

            if (response.Data != null)
            {
                Shelter = response.Data;
            }
        }
    }
}
