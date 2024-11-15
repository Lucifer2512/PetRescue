using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PetRescueFE.Pages.Model;
using PetRescueFE.SignalRealtime;
using System.ComponentModel.DataAnnotations;

namespace PetRescueFE.Pages.AdoptionApplicationPage
{
    public class EditModel : PageModel
    {
        private readonly ApiService _apiService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public EditModel(ApiService apiService, IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
            _apiService = apiService;
        }

        [BindProperty]
        public AdoptionApplicationResponseModel AdoptionApplication { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiUrl = $"https://localhost:7297/api/adoptionapplication/{id}";
            var response = await _apiService.GetAsync<BaseResponseModelFE<AdoptionApplicationResponseModel>>(apiUrl);

            if (response.Data == null)
            {
                return NotFound();
            }

            AdoptionApplication = response.Data;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var apiUrl = $"https://localhost:7297/api/adoptionapplication/{AdoptionApplication.ApplicationId}";
            var applicationRequest = new AdoptionApplicationRequestModelForUpdate
            {
                Status = AdoptionApplication.Status,
                Notes = AdoptionApplication.Notes
            };

            var validationContext = new ValidationContext(applicationRequest);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(applicationRequest, validationContext, validationResults, true);

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    ModelState.AddModelError(string.Empty, validationResult.ErrorMessage);
                }
                return Page();
            }

            try
            {
                var response = await _apiService.PutAsync<AdoptionApplicationRequestModelForUpdate, BaseResponseModelFE<AdoptionApplicationResponseModel>>(apiUrl, applicationRequest);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Failed to update shelter.");
                return Page();
            }
            if(AdoptionApplication.Status == "APPROVED")
            {
                var getDetailUrl = $"https://localhost:7297/api/adoptionapplication/onlyid/{AdoptionApplication.ApplicationId}";
                var response = await _apiService.GetAsync<BaseResponseModelFE<AdoptionApplicationOnlyIdResponseModel>>(getDetailUrl);
                await SendNotificationId($"{response.Data.UserId}", "Your application has been approved, check now!  ", "");
            }
            return RedirectToPage("./Index");
        }

        public async Task SendNotificationId(string id, string label, string url)
        {
            var message = new
            {
                label = label,
                url = url
            };
            await _hubContext.Clients.User(id).SendAsync("ReceiveNotification", JsonConvert.SerializeObject(message));
        }
    }
}
