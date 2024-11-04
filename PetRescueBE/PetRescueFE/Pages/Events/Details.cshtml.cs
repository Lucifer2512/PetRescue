using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Entity;
using Pages.Model;
using Pages.Model.Events;

namespace PetRescueFE.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly ApiService _apiService;

        public DetailsModel( ApiService apiService)
        {
            _apiService = apiService;
        }


        public EventResponseModel Event { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
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
            var data = await _apiService.GetAsync<BaseResponseModel<EventResponseModel>>(url);
            if (data is null)
            {
                return null;
            }
            return data.Data;
        }
    }
}
