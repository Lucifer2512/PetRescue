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

        private async Task<bool> LoadShelterAsync(Guid id)
        {
            var apiUrl = $"https://localhost:7297/api/shelter/{id}";
            var response = await _apiService.GetAsync<BaseResponseModelFE<ShelterResponseModel>>(apiUrl);

            if (response.Data != null)
            {
                Shelter = response.Data;
                return true;
            }

            return false;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "d290f1ee-6c54-4b01-90e6-d701748f0851" && role != "f3c8d4e5-6b7a-4c9d-8e2f-0a1b2c3d4e5f")
            {
                return RedirectToPage("/AuthorizationError");
            }

            if (id == null || !await LoadShelterAsync(id.Value))
            {
                return NotFound();
            }



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

                if (response == false)
                {
                    ModelState.AddModelError(string.Empty, "There is other information related to this shelter, so this CANNOT BE DELETED.");
                    await LoadShelterAsync(id.Value);
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "There is other information related to this shelter, so this CANNOT BE DELETED.");
                await LoadShelterAsync(id.Value);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
