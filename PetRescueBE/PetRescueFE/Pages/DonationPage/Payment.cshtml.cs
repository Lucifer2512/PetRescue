using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PetRescueFE.Pages.Model;
using System.Net.Http;
using System.Text;


namespace PetRescueFE.Pages.DonationPage
{
    public class PaymentModel : PageModel
    {
        [BindProperty]
        public decimal Amount { get; set; }
        public string AccountId { get; private set; }
        private readonly ApiService _apiService;


        public PaymentModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {


            if (!ModelState.IsValid)
            {
                return Page();
            }
            AccountId = HttpContext.Session.GetString("AccountId");
            DonationRequestModelQRCode donation = new DonationRequestModelQRCode();
            donation.ShelterId = Guid.Parse("2f78ddb6-1b06-4730-a23b-f44fd1d3bfff");
            donation.EventId = Guid.Parse("422916e7-3d1e-4664-a194-d33bdb8a19df");
            donation.UserId = Guid.Parse("3f21226b-30c1-4274-81a6-2ed9d9e0c54c");
            donation.Amount = Amount;
            var response = await _apiService.PostAsync<DonationRequestModelQRCode, BaseResponseModelFE<String>>("donation/donationqrurl", donation); //"`endpoint-url` cho phù hợp"
            if (response.Code == 200)
            {
                if (response.Data == null)
                {
                    TempData["ErrorMessage"] = response.Message;
                    return Page();
                }            
                string url = response.Data.ToString();
                return Redirect(url);
            }
            else
            {
                return Page();
            }


        }

    }
}
