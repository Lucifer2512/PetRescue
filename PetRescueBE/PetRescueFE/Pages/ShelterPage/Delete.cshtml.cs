using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Shelters;

namespace PetRescueFE.Pages.ShelterPage
{
    public class DeleteModel : PageModel
    {
        private readonly ApiService _apiService;

        public DeleteModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public ShelterResponseModel Shelter { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiUrl = $"https://localhost:7297/api/shelter/{id}";
            var response = await _apiService.GetAsync<BaseResponseModelFE<ShelterResponseModel>>(apiUrl);

            if (response.Data == null)
            {
                return NotFound();
            }

            Shelter = response.Data;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiUrl = $"https://localhost:7297/api/shelter/{id}";

            try
            {
                var response = await _apiService.DeleteAsync(apiUrl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete shelter.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
