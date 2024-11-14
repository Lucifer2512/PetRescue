using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Shelters;

namespace PetRescueFE.Pages.PetPage
{
    public class PetShelterModel : PageModel
    {
        private readonly ApiService _apiService;

        public PetShelterModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public ICollection<PetResponseModelFE> Pets { get; set; } = new List<PetResponseModelFE>();
        public bool IsAdmin { get; private set; }
        public string SearchTerm { get; set; }
        public string ErrorMessage { get; set; }
        public async Task OnGetAsync(Guid? shelterId, string searchTerm)
        {
            var apiUrlShelter = "https://localhost:7297/api/shelter";
            var listShelter = await _apiService.GetAsync<BaseResponseModelFE<IList<ShelterResponseModel>>>(apiUrlShelter);

            var role = HttpContext.Session.GetString("Role");
            string apiUrl = string.Empty;
            if (role == "d290f1ee-6c54-4b01-90e6-d701748f0851" || role == "f3c8d4e5-6b7a-4c9d-8e2f-0a1b2c3d4e5f")
            {
                IsAdmin = true;
                apiUrl = $"https://localhost:7297/api/pet/shelter/{shelterId}?searchTerm={searchTerm}";
            }
            else
            {
                apiUrl = $"https://localhost:7297/api/pet/user-shelter/{shelterId}?searchTerm={searchTerm}";
            }

            var response = await _apiService.GetAsync<BaseResponseModelFE<ICollection<PetResponseModelFE>>>(apiUrl);
            if (response != null && response.Code == 200)
            {
                Pets = response.Data;

                // Add ShelterName to each Pet based on ShelterId
                foreach (var pet in Pets)
                {
                    var shelter = listShelter.Data.FirstOrDefault(s => s.ShelterId == pet.ShelterId);
                    if (shelter != null)
                    {
                        pet.ShelterName = shelter.ShelterName;
                    }
                }
            }
            else
            {
                ErrorMessage = response.Message;
            }
        }
    }
}
