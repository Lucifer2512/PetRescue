using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;

namespace PetRescueFE.Pages.DonationPage
{
    public class DetailsModel : PageModel
    {
        private readonly ApiService _apiService;
        public DetailsModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public BaseResponseModelFE<DonationReponseModel> Donation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            // Gọi API với phương thức GET
            var data = await _apiService.GetAsync<BaseResponseModelFE<DonationReponseModel>>("donation/31601cfc-fa6b-4a1d-a6c9-54d20fb1a012"); //"`endpoint-url` cho phù hợp"
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                Donation = data;
            }
            return Page();
        }
    }
}
