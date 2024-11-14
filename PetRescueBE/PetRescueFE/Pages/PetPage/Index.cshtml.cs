using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Shelters;

namespace PetRescueFE.Pages.PetPage
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public ICollection<PetResponseModelFE> Pets { get; set; } = new List<PetResponseModelFE>();
        public bool IsShelter { get; private set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Species { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Gender { get; set; }
        [BindProperty(SupportsGet = true)]
        public Guid? ShelterId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Status { get; set; }

        public List<SelectListItem> ShelterOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> SpeciesOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "All Species" },
            new SelectListItem { Value = "Cat", Text = "Cat" },
            new SelectListItem { Value = "Dog", Text = "Dog" },
            new SelectListItem { Value = "Other", Text = "Other" }
        };

        public List<SelectListItem> GenderOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "All Gender" },
            new SelectListItem { Value = "Male", Text = "Male" },
            new SelectListItem { Value = "Female", Text = "Female" }
        };
        public List<SelectListItem> StatusOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "All Status" },
            new SelectListItem { Value = "ACTIVE", Text = "Active" },
            new SelectListItem { Value = "DELETE", Text = "Deleted" }
        };

        public string ErrorMessage { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        private async Task LoadSheltersAsync()
        {
            var apiUrl = "https://localhost:7297/api/shelter";
            var response = await _apiService.GetAsync<BaseResponseModelFE<IList<ShelterResponseModel>>>(apiUrl);

            if (response?.Data != null)
            {
                ShelterOptions = response.Data.Select(shelter => new SelectListItem
                {
                    Value = shelter.ShelterId.ToString(),
                    Text = shelter.ShelterName
                }).ToList();
                ShelterOptions.Insert(0, new SelectListItem { Value = "", Text = "All Shelters" });
            }
        }

        public async Task OnGetAsync(string? searchTerm, string? species, string? gender, Guid? shelterId, string? status, int? pageIndex)
        {
            await LoadSheltersAsync();
            var role = HttpContext.Session.GetString("Role");
            string apiUrl;
            CurrentPage = pageIndex ?? 1;

            if (role == "f3c8d4e5-6b7a-4c9d-8e2f-0a1b2c3d4e5f")
            {
                IsShelter = true;
                var userId = HttpContext.Session.GetString("UserId");
                apiUrl = $"https://localhost:7297/api/pet/forshelter/{userId}?searchTerm={searchTerm}&species={species}&gender={gender}&status={status}&index={CurrentPage}&size=3";
            }
            else
            {
                apiUrl = $"https://localhost:7297/api/pet/foruser?searchTerm={searchTerm}&species={species}&gender={gender}&shelterId={shelterId}&index={CurrentPage}&size=3";
            }

            var response = await _apiService.GetAsync<BaseResponseModelFE<PaginatedList<PetResponseModelFE>>>(apiUrl);
            if (response?.Code == 200 && response.Data != null)
            {
                Pets = response.Data.Items;
                TotalPages = response.Data.TotalPages;
                HasPreviousPage = response.Data.HasPreviousPage;
                HasNextPage = response.Data.HasNextPage;

                var shelterLookup = ShelterOptions.ToDictionary(s => s.Value, s => s.Text);

                foreach (var pet in Pets)
                {
                    pet.ShelterName = shelterLookup.GetValueOrDefault(pet.ShelterId.ToString());
                }
            }
            else
            {
                ErrorMessage = response?.Message ?? "An error occurred while retrieving the pet data.";
            }
        }
    }
}
