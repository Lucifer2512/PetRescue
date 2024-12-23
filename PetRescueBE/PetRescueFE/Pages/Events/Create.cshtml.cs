using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Events;
using PetRescueFE.SignalRealtime;

namespace PetRescueFE.Pages.Events
{
    public class CreateModel : PageModel
    {
        private readonly ApiService _apiService;
        private readonly EventGlobalUtility _utility;
        private readonly IHubContext<NotificationHub> _hubContext;
        public CreateModel(ApiService apiService, EventGlobalUtility utility, IHubContext<NotificationHub> hubContext)
        {
            _apiService = apiService;
            _utility = utility;
            _hubContext = hubContext;
        }

        [BindProperty]
        public IEnumerable<Shelter4EventResponse> Shelters { get; set; } = new List<Shelter4EventResponse>();
        public IFormFile? ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                string? userId = _utility.GetUserId();
                string? role = _utility.GetUserRole();
                if (userId == null || _utility.IsEditable(role)) // do when session is out
                {
                    return RedirectToPage("/Login");
                }
                
                string shelterUrl = EventUrlProfile.BASE_URL_S + EventUrlProfile.GET_SHELTER_BY_USER_ID + userId;

                // Initialize empty Shelters to avoid case of fire
                Shelters = new List<Shelter4EventResponse>();

                var response = await GetShelterList(shelterUrl);
                if (response == null || !response.Any())
                {
                    // Handle case when no shelters are returned
                    ModelState.AddModelError(string.Empty, "No shelters available");
                    return Page();
                }

                Shelters = response;
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error loading shelters: " + ex.Message);
                return Page();
            }
        }

        [BindProperty]
        public EventRequestModel4Create Event { get; set; } = new();


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                string? userId = _utility.GetUserId();
                if (userId == null) // do when session is out
                {
                    return RedirectToPage("/Login");
                }
                if (!ModelState.IsValid)
                {
                    // Repopulate shelter list on validation failure
                    string shelterUrl = EventUrlProfile.BASE_URL_S + EventUrlProfile.GET_SHELTER_BY_USER_ID + userId;
                    var shelters = await GetShelterList(shelterUrl);
                    Shelters = shelters;
                    return Page();
                }

                // Set default time to 00:00 for both start and end dates
                Event.StartDateTime = DateTime.SpecifyKind(Event.StartDateTime.Date, DateTimeKind.Utc);
                Event.EndDateTime = DateTime.SpecifyKind(Event.EndDateTime.Date, DateTimeKind.Utc);

                // Set default status if not provided
                if (string.IsNullOrEmpty(Event.Status))
                {
                    Event.Status = "ACTIVE";
                }

                Event.Image = ImageFile != null ? await _apiService.ConvertToByteArrayAsync(ImageFile) : null;

                await _apiService.PostAsync<EventRequestModel4Create, BaseResponseModelFE<EventResponseModel>>(
                    EventUrlProfile.POST_CREATE,
                    Event
                );
                await SendNotification("New events added, check now!  ", "");

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error creating event: " + ex.Message);

                // Repopulate shelter list on error
                string shelterUrl = EventUrlProfile.BASE_URL_S + EventUrlProfile.GET_SHELTER_BY_USER_ID + _utility.GetUserId();
                var shelters = await GetShelterList(shelterUrl);
                Shelters = shelters;

                return Page();
            }
        }

        /// <summary>
        /// filt or filter or select the shelter list with the user mail.
        /// </summary>
        /// <param name="shelters">List of Shelter4EventResponse</param>
        /// <param name="userMail">string</param>
        /// <returns>List of Shelter4EventResponse</returns>
        private List<Shelter4EventResponse> FilteredShelterWithUserMail(List<Shelter4EventResponse> shelters, string? userMail)
        {
            if (userMail.IsNullOrEmpty())
            {
                // force to return the original list, dirty bastard way
                return shelters;
            }
            var filted = shelters.Where(shelter => shelter.UserEmail == userMail).ToList();
            return filted;
        }

        /// <summary>
        /// try to get the shelter list from api https://localhost:7297/api/shelter
        /// remember to change the url to the correct one.
        /// this one is not recommended to use, because it is vulnerable as data.
        /// btw, it is just a demo
        /// </summary>
        /// <param name="url"> ex: /shelter</param>
        /// <returns>List of Shelter4EventResponse</returns>
        private async Task<List<Shelter4EventResponse>> GetShelterList(string url)
        {
            var response = await _apiService.GetAsync<BaseResponseModelFE<List<Shelter4EventResponse>>>(url);
            return response?.Data ?? new List<Shelter4EventResponse>();
        }
        public async Task SendNotification(string label, string url)
        {
            var message = new
            {
                label = label,
                url = url
            };
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", JsonConvert.SerializeObject(message));
        }
    }
}
