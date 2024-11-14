using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Shelters;

namespace PetRescueFE.Pages.ShelterPage
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public IList<ShelterResponseModel> Shelter { get; set; } = default!;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool CanCreate { get; private set; } = true;

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            var role = HttpContext.Session.GetString("Role");
            var userId = HttpContext.Session.GetString("UserId");

            if (role != "d290f1ee-6c54-4b01-90e6-d701748f0851" && role != "f3c8d4e5-6b7a-4c9d-8e2f-0a1b2c3d4e5f")
            {
                return RedirectToPage("/AuthorizationError");
            }

            CurrentPage = pageIndex ?? 1;

            if (role == "f3c8d4e5-6b7a-4c9d-8e2f-0a1b2c3d4e5f")
            {
                var apiUrlForShelterOwner = $"https://localhost:7297/api/shelter/paging/userId/{userId}?index={CurrentPage}&size=3";
                var responseForShelterOwner = await _apiService.GetAsync<BaseResponseModelFE<PaginatedList<ShelterResponseModel>>>(apiUrlForShelterOwner);

                if (responseForShelterOwner.Data != null)
                {
                    Shelter = responseForShelterOwner.Data.Items;
                    TotalPages = responseForShelterOwner.Data.TotalPages;
                    HasPreviousPage = responseForShelterOwner.Data.HasPreviousPage;
                    HasNextPage = responseForShelterOwner.Data.HasNextPage;
                }

                CanCreate = false;

                return Page();
            }

            var apiUrlForAdmin = $"https://localhost:7297/api/shelter/paging?index={CurrentPage}&size=3";
            var responseForAdmin = await _apiService.GetAsync<BaseResponseModelFE<PaginatedList<ShelterResponseModel>>>(apiUrlForAdmin);

            if (responseForAdmin.Data != null)
            {
                Shelter = responseForAdmin.Data.Items;
                TotalPages = responseForAdmin.Data.TotalPages;
                HasPreviousPage = responseForAdmin.Data.HasPreviousPage;
                HasNextPage = responseForAdmin.Data.HasNextPage;
            }

            return Page();
        }
    }
}
