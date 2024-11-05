using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;


namespace PetRescueFE.Pages.DonationPage
{
    public class PaymentModel : PageModel
    {
        [BindProperty]
        public decimal Amount { get; set; }
        public string AccountId { get; private set; }
        private HttpClient _httpClient;


        public PaymentModel()
        {
           
        }

        public void OnGet()
        {

        }

       /* public async Task<IActionResult> OnPost()
        {

            IConfiguration config = new ConfigurationBuilder()
                                          .SetBasePath(Directory.GetCurrentDirectory())
                                          .AddJsonFile("appsettings.json", true, true)
                                          .Build();
            string apiUrl = config["API_URL"];
            if (!ModelState.IsValid)
            {
                return Page();
            }
            AccountId = HttpContext.Session.GetString("AccountId");

            var requestBody = new
            {
                amount = Amount,
                fromId = AccountId
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            var request = new HttpRequestMessage(HttpMethod.Post, $"{apiUrl}/GenerateQRBanking/CreateQRCodeandTransaction/accountId={AccountId}&amount={Amount}");
            //request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            //http://localhost:5263/api/GenerateQRBanking/CreateTransaction/accountId=fea2ee07-06ac-44f8-b3bb-2d62be071cb2&amount=5000
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jwtToken = JsonConvert.DeserializeObject<SuccesResponse>(responseContent);
                if (jwtToken.Data == null)
                {
                    TempData["ErrorMessage"] = jwtToken.Message;
                    return Page();
                }
                JObject parsedData = JObject.Parse(jwtToken.Data.ToString());

                // Access the 'url' property
                string url = (string)parsedData["url"];
                return Redirect(url);
            }
            else
            {
                return Page();
            }
             

        }*/

    }
}
