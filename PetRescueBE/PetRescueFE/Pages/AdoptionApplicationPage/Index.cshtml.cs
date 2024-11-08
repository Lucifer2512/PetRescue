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
            var response = await _apiService.GetAsync<BaseResponseModelFE<IList<AdoptionApplicationResponseModel>>>(apiUrl);

            if (response.Data != null)
            {
                AdoptionApplication = response.Data;
            }
        }
    }
}
