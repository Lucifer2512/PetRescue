using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;
using System.Text;


namespace PetRescueFE.Pages.DonationPage
{
    public class PaymentModel : PageModel
    {
        [BindProperty]
        public decimal Amount { get; set; }
        public string UserId { get; private set; }
        [BindProperty]
        public Guid EventId { get; set; }
        [BindProperty]
        public Guid ShelterId { get; set; }
        private readonly ApiService _apiService;


        public PaymentModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public void OnGet(Guid eventId, Guid shelterId)
        {
            EventId = eventId;
            ShelterId = shelterId;
        }

        public async Task<IActionResult> OnPost()
        {


            if (!ModelState.IsValid)
            {
                return Page();
            }
            UserId = HttpContext.Session.GetString("UserId");

            DonationRequestModelQRCode donation = new DonationRequestModelQRCode();
            donation.ShelterId = ShelterId;
            donation.EventId = EventId;
            donation.UserId = Guid.Parse(UserId);
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
