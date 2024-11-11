using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;

namespace PetRescueFE.Pages.UserPage
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public IList<UserResponseModelFE> Users { get; set; } = default!;


        public async Task OnGetAsync()
        {
            var apiUrl = "https://localhost:7297/api/users";
            var response = await _apiService.GetAsync<BaseResponseModelFE<IList<UserResponseModelFE>>>(apiUrl);

            if (response.Data != null)
            {
                Users = response.Data;
            }
        }
    }
}
