using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Pages.Model;
using Pages.Model.Events;

namespace PetRescueFE.Pages.Events
{
    public class CreateModel : PageModel
    {
        private readonly ApiService _apiService;

        public CreateModel(ApiService apiService)
        {
            _apiService = apiService;
        }
        
        [BindProperty]
        public IEnumerable<Shelter4EventResponse> Shelters { get; set; } = new List<Shelter4EventResponse>();

        public async Task<IActionResult> OnGetAsync()
        {
            try 
            {
                string shelterUrl = EventUrlProfile.BASE_URL_S + EventUrlProfile.GET_SHELTER;
                
                // Initialize empty Shelters to avoid case of fire
                Shelters = new List<Shelter4EventResponse>();
                
                var response = await GetShelterList(shelterUrl);
                if (response == null || !response.Any())
                {
                    // Handle case when no shelters are returned
                    ModelState.AddModelError(string.Empty, "No shelters available");
                    return Page();
                }
                
                // when bug, check this line as user identity is use or not idk
                var userMail = User.Identity?.Name ?? string.Empty;
                var filteredShelters = FilteredShelterWithUserMail(response, userMail);
                
                if (!filteredShelters.Any())
                {
                    ModelState.AddModelError(string.Empty, "No shelters found for current user");
                    return Page();
                }
                
                Shelters = filteredShelters;
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
                if (!ModelState.IsValid)
                {
                    // Repopulate shelter list on validation failure
                    string shelterUrl = EventUrlProfile.BASE_URL_S + EventUrlProfile.GET_SHELTER;
                    var shelters = await GetShelterList(shelterUrl);
                    Shelters = FilteredShelterWithUserMail(shelters, User.Identity?.Name ?? string.Empty);
                    return Page();
                }

                // Ensure dates are in UTC
                Event.StartDateTime = DateTime.SpecifyKind(Event.StartDateTime, DateTimeKind.Utc);
                Event.EndDateTime = DateTime.SpecifyKind(Event.EndDateTime, DateTimeKind.Utc);
                
                // Set default status if not provided
                if (string.IsNullOrEmpty(Event.Status))
                {
                    Event.Status = "ACTIVE";
                }

                await _apiService.PostAsync<EventRequestModel4Create, BaseResponseModel<EventResponseModel>>(
                    EventUrlProfile.POST_CREATE, 
                    Event
                );
                
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error creating event: " + ex.Message);
                
                // Repopulate shelter list on error
                string shelterUrl = EventUrlProfile.BASE_URL_S + EventUrlProfile.GET_SHELTER;
                var shelters = await GetShelterList(shelterUrl);
                Shelters = FilteredShelterWithUserMail(shelters, User.Identity?.Name ?? string.Empty);
                
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
            var response = await _apiService.GetAsync<BaseResponseModel<List<Shelter4EventResponse>>>(url);
            return response?.Data ?? new List<Shelter4EventResponse>();
        }
    }
}
