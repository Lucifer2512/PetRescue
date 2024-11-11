using Microsoft.AspNetCore.Mvc;
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


        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "d290f1ee-6c54-4b01-90e6-d701748f0851")
            {
                return RedirectToPage("/AuthorizationError");
            }

            var apiUrl = "https://localhost:7297/api/users";
            var response = await _apiService.GetAsync<BaseResponseModelFE<IList<UserResponseModelFE>>>(apiUrl);

            if (response.Data != null)
            {
                Users = response.Data;
            }

            return Page();
        }
    }
}
