using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Events;

namespace PetRescueFE.Pages.Events
{
    public class IndexModel : PageModel
    {

        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public IList<EventResponseModel> Event { get; set; } = default!;

        public bool CanEdit { get; private set; }
        public bool CanDonate { get; private set; }

        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        public async Task<ActionResult> OnGetAsync(int? pageIndex)
        {
            // Check user role from session
            var role = HttpContext.Session.GetString("Role");

            CanEdit = role == "d290f1ee-6c54-4b01-90e6-d701748f0851" || // Admin
                     role == "f3c8d4e5-6b7a-4c9d-8e2f-0a1b2c3d4e5f";    // ShelterOwner

            CanDonate = role == "e7b8f3d2-4a2f-4c3b-8f4d-9c5d8a3e1b2c"; // User

            CurrentPage = pageIndex ?? 1;
            var data = await TryGetData(EventUrlProfile.BASE_URL_S + $"events/p/?Index={CurrentPage}&Size=3");
            if (data is null)
            {
                return NotFound();
            }

            Event = data.Items;
            TotalPages = data.TotalPages;
            HasPreviousPage = data.HasPreviousPage;
            HasNextPage = data.HasNextPage;

            return Page();
        }

        private async Task<PaginatedList<EventResponseModel>> TryGetData(string url)
        {
            var data = await _apiService.GetAsync<BaseResponseModelFE<PaginatedList<EventResponseModel>>>(url);
            if (data is null)
            {
                return null;
            }
            return data.Data;
        }
    }
}
