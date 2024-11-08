using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Shelters;

namespace PetRescueFE.Pages.ShelterPage
{
    public class CreateModel : PageModel
    {
        private readonly ApiService _apiService;

        public CreateModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public ShelterRequestModel Shelter { get; set; } = new ShelterRequestModel();


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var apiUrl = "https://localhost:7297/api/shelter";
            try
            {
                var response = await _apiService.PostAsync<ShelterRequestModel, BaseResponseModelFE<ShelterResponseModel>>(apiUrl, Shelter);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error creating shelter.");
                return Page();
            }


            return RedirectToPage("./Index");
        }
    }
}
