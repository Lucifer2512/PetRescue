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

        public IList<EventResponseModel> Event { get;set; } = default!;

        public async Task<ActionResult> OnGetAsync()
        {
            var data = await TryGetData(EventUrlProfile.BASE_URL_S + EventUrlProfile.GETS);
            if (data is null)
            {
                return NotFound();
            }
            
            Event = data;
            
            return Page();
        }

        private async Task<List<EventResponseModel>> TryGetData(string url)
        {
            var data = await _apiService.GetAsync<BaseResponseModelFE<List<EventResponseModel>>>(url);
            if (data is null)
            {
                return null;
            }
            return data.Data;
        }
    }
}
