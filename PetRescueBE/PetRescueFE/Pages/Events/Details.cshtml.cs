using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Events;

namespace PetRescueFE.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly ApiService _apiService;
        private readonly EventGlobalUtility _utility;


        public DetailsModel(ApiService apiService, EventGlobalUtility utility)
        {
            _apiService = apiService;
            _utility = utility;
        }


        public EventResponseModel Event { get; set; } = default!;
        public bool CanEdit { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var role = _utility.GetUserRole();
            string? userId = null;
            CanEdit = role == Role4Event.ADMIN || // Admin
                      role == Role4Event.SHELTER_OWNER;    // ShelterOwner
            userId = CanEdit ? _utility.GetUserId() : null;

            var response = await TryGetData(EventUrlProfile.GET_DETAIL + id);
            if (response == null)
            {
                return NotFound();
            }

            Event = response;

            return Page();
        }

        private async Task<EventResponseModel> TryGetData(string url)
        {
            var data = await _apiService.GetAsync<BaseResponseModelFE<EventResponseModel>>(url);
            if (data is null)
            {
                return null;
            }
            return data.Data;
        }
    }
}
