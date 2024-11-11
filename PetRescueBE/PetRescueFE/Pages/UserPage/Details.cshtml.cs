using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;

namespace PetRescueFE.Pages.UserPage
{
    public class DetailsModel : PageModel
    {
        private readonly ApiService _apiService;
        public string UserRole { get; private set; }

        public DetailsModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public UserResponseModelFE User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserRole = HttpContext.Session.GetString("Role");

            var apiUrl = $"https://localhost:7297/api/users/{id}";
            var response = await _apiService.GetAsync<BaseResponseModelFE<UserResponseModelFE>>(apiUrl);

            if (response.Data == null)
            {
                return NotFound();
            }

            User = response.Data;
            return Page();
        }
    }
}
