using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pages.Model.Events;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pages.Model;

namespace PetRescueFE.Pages.Events
{
    public class EditModel : PageModel
    {
        private readonly ApiService _apiService;

        public EditModel(ApiService apiService)
        {
            _apiService = apiService;
        }
        
        [BindProperty] 
        public EventRequestModel4Update Event { get; set; } = default!;
        
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }  // Add this to bind the ID from route

        public SelectList StatusList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await TryGetEvent(EventUrlProfile.GET_DETAIL, Id);
            if (response == null)
            {
                return NotFound();
            }
            
            Event = new EventRequestModel4Update
            {
                ImageUrl = response.ImageUrl,
                Name = response.Name,
                Description = response.Description,
                StartDateTime = response.StartDateTime,
                EndDateTime = response.EndDateTime,
                Location = response.Location,
                EventType = response.EventType,
                Goal = response.Goal,
                Status = response.Status
            };
            
            StatusList = new SelectList(Enum.GetNames(typeof(Status)));
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                StatusList = new SelectList(Enum.GetNames(typeof(Status)));
                return Page();
            }
            
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