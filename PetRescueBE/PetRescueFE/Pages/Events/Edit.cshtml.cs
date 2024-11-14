using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Events;

namespace PetRescueFE.Pages.Events
{
    public class EditModel : PageModel
    {
        private readonly ApiService _apiService;
        private readonly EventGlobalUtility _utility;

        public EditModel(ApiService apiService, EventGlobalUtility utility)
        {
            _apiService = apiService;
            _utility = utility;
        }

        [BindProperty]
        public EventRequestModel4Update Event { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }  // Add this to bind the ID from route

        public SelectList StatusList { get; set; }

        public IFormFile? ImageFile { get; set; }

        public string? ExistingImageData { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var role = _utility.GetUserRole();
            if (role.IsNullOrEmpty() || _utility.IsEditable(role))
            {
                return RedirectToPage("/Login");
            }

            var response = await TryGetEvent(EventUrlProfile.GET_DETAIL, Id);
            if (response == null)
            {
                return NotFound();
            }

            Event = new EventRequestModel4Update
            {
                Image = ImageFile != null ? await _apiService.ConvertToByteArrayAsync(ImageFile) : null,
                Name = response.Name,
                Description = response.Description,
                StartDateTime = response.StartDateTime,
                EndDateTime = response.EndDateTime,
                Location = response.Location,
                EventType = response.EventType,
                Goal = response.Goal,
                Status = response.Status
            };

            if (response.ImageData != null)
            {
                ExistingImageData = response.ImageData;
            }

            StatusList = new SelectList(Enum.GetNames(typeof(Status)));

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                StatusList = new SelectList(Enum.GetNames(typeof(Status)));
                return Page();
            }

            Event.Image = ImageFile != null ? await _apiService.ConvertToByteArrayAsync(ImageFile) : null;

            try
            {
                var url = $"{EventUrlProfile.PUT_UPDATE}{Id}";
                await _apiService.PutAsync<EventRequestModel4Update, BaseResponseModelFE<object>>(url, Event);

                TempData["SuccessMessage"] = "Event updated successfully";

                return RedirectToPage("/Events/Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating event: {ex.Message}");

                StatusList = new SelectList(Enum.GetNames(typeof(Status)));

                return Page();
            }
        }

        private async Task<EventResponseModel> TryGetEvent(string url, Guid id)
        {
            var response = await _apiService.GetAsync<BaseResponseModelFE<EventResponseModel>>(url + id);
            return response?.Data;
        }
    }
}