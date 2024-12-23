﻿using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Shelters;
using PetRescueFE.SignalRealtime;

namespace PetRescueFE.Pages.PetPage
{
    public class CreateModel : PageModel
    {
       
        private readonly ApiService _apiService;
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public CreateModel(ApiService apiService, IHubContext<NotificationHub> notificationHubContext)
        {
            _apiService = apiService;
            _notificationHubContext = notificationHubContext;
        }

        [BindProperty]
        public PetAddRequestModelFE Pet { get; set; } = new PetAddRequestModelFE();
        public List<SelectListItem> ShelterOptions { get; set; } = new List<SelectListItem>();

        public IFormFile? ImageFile { get; set; }

        private async Task LoadShelterAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var apiUrl = $"https://localhost:7297/api/shelter/userid/{userId}";
            var response = await _apiService.GetAsync<BaseResponseModelFE<IList<ShelterResponseModel>>>(apiUrl);

            if (response.Data != null)

            {
                ShelterOptions = response.Data.Select(shelter => new SelectListItem
                {
                    Value = shelter.ShelterId.ToString(),
                    Text = shelter.ShelterName
                }).ToList();
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("Role");
            if (string.IsNullOrEmpty(role) || role == "e7b8f3d2-4a2f-4c3b-8f4d-9c5d8a3e1b2c") // User role
            {
                return RedirectToPage("/Login");
            }

            await LoadShelterAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Pet == null)
            {
                await LoadShelterAsync();
                return Page();
            }

            Pet.Image = ImageFile != null ? await _apiService.ConvertToByteArrayAsync(ImageFile) : null;
            // Gọi API để tạo
            // pet mới
            var response = await _apiService.PostAsync<PetAddRequestModelFE, BaseResponseModelFE<PetResponseModelFE>>("https://localhost:7297/api/pet/add", Pet);


            if (response == null || response.Data == null)
            {
                ModelState.AddModelError(string.Empty, response.Message.ToString());
                await LoadShelterAsync();
                return Page();
            }
            var message = $"A new pet '{Pet.Name}' vew a new pet?";
            var petId = response.Data.PetId;
            var detailUrl = $"https://localhost:7132/PetPage/Details?id={petId}";
            await SendNotification(message, detailUrl);


            return RedirectToPage("./Index");
        }
        public async Task SendNotification(string label, string url)
        {
            var message = new
            {
                label = label,
                url = url
            };
            await _notificationHubContext.Clients.All.SendAsync("ReceiveNotification", JsonConvert.SerializeObject(message));
        }
    }
}
