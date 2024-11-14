using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Events;

namespace PetRescueFE.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;
        private readonly EventGlobalUtility _utility;

        public IndexModel(ApiService apiService, EventGlobalUtility utility)
        {
            _apiService = apiService;
            _utility = utility;
        }

        public IList<EventResponseModel> Event { get; set; } = default!;
        public string NoEventsMessage { get; set; } = string.Empty;

        public bool CanEdit { get; private set; }
        public bool CanDonate { get; private set; }

        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        public async Task<ActionResult> OnGetAsync(int? pageIndex)
        {
            // Check user role from session
            var role = _utility.GetUserRole();
            string? userId = null;
            var url = EventUrlProfile.BASE_URL_S + EventUrlProfile.GETS_P;

            CanEdit = role == Role4Event.ADMIN || // Admin
                      role == Role4Event.SHELTER_OWNER;    // ShelterOwner

            CanDonate = role == Role4Event.USER; // User

            userId = CanEdit ? _utility.GetUserId() : null;

            CurrentPage = pageIndex ?? 1;
            url += $"?Index={CurrentPage}&Size=3";

            if (userId is not null)
            {
                url += $"&usr={userId}";
            }

            var data = await TryGetData(url);
            if (data is null || !data.Items.Any())
            {
                NoEventsMessage = "No event here, try later";
                Event = new List<EventResponseModel>();
                TotalPages = 1;
                HasPreviousPage = false;
                HasNextPage = false;
            }
            else
            {
                Event = data.Items;
                TotalPages = data.TotalPages;
                HasPreviousPage = data.HasPreviousPage;
                HasNextPage = data.HasNextPage;
            }

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
