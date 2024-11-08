using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;

namespace PetRescueFE.Pages.UserPage
{
    public class DeleteModel : PageModel
    {
        private readonly ApiService _apiService;

        public DeleteModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public UserResponseModel User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiUrl = $"https://localhost:7297/api/users/{id}";
            var response = await _apiService.GetAsync<BaseResponseModelFE<UserResponseModel>>(apiUrl);

            if (response.Data == null)
            {
                return NotFound();
            }

            User = response.Data;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiUrl = $"https://localhost:7297/api/users/{id}";

            try
            {
                var response = await _apiService.DeleteAsync(apiUrl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete user.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
