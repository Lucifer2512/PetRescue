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
        public IFormFile? ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "d290f1ee-6c54-4b01-90e6-d701748f0851")
            {
                return RedirectToPage("/AuthorizationError");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var shelterRequest = new ShelterRequestModel
            {
                ShelterName = Shelter.ShelterName,
                ShelterAddress = Shelter.ShelterAddress,
                ShelterPhoneNumber = Shelter.ShelterPhoneNumber,
                UserEmail = Shelter.UserEmail,
                Image = ImageFile != null ? await _apiService.ConvertToByteArrayAsync(ImageFile) : null,
            };

            var apiUrl = "https://localhost:7297/api/shelter";
            try
            {
                var response = await _apiService.PostAsync<ShelterRequestModel, BaseResponseModelFE<ShelterResponseModel>>(apiUrl, shelterRequest);
                if (response.Code != 201)
                {
                    ModelState.AddModelError(string.Empty, "User Email not exists.");
                    return Page();
                }
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
