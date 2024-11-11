using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Shelters;
using System.ComponentModel.DataAnnotations;

namespace PetRescueFE.Pages.ShelterPage
{
    public class EditModel : PageModel
    {
        private readonly ApiService _apiService;

        public EditModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public ShelterResponseModel Shelter { get; set; } = default!;
        public IFormFile? ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "d290f1ee-6c54-4b01-90e6-d701748f0851")
            {
                return RedirectToPage("/AuthorizationError");
            }

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

        public async Task<IActionResult> OnPostAsync()
        {
            var apiUrl = $"https://localhost:7297/api/shelter/{Shelter.ShelterId}";
            var shelterRequest = new ShelterRequestModelForUpdate
            {
                ShelterName = Shelter.ShelterName,
                ShelterAddress = Shelter.ShelterAddress,
                Balance = Shelter.Balance,
                ShelterPhoneNumber = Shelter.ShelterPhoneNumber,
                UserEmail = Shelter.UserEmail,
                Image = ImageFile != null ? await _apiService.ConvertToByteArrayAsync(ImageFile) : null,
            };

            var validationContext = new ValidationContext(shelterRequest);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(shelterRequest, validationContext, validationResults, true);

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    ModelState.AddModelError(string.Empty, validationResult.ErrorMessage);
                }
                return Page();
            }

            try
            {
                var response = await _apiService.PutAsync<ShelterRequestModelForUpdate, BaseResponseModelFE<ShelterResponseModel>>(apiUrl, shelterRequest);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Failed to update shelter.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
