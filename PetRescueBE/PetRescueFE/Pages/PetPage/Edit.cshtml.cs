﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Shelters;

namespace PetRescueFE.Pages.PetPage
{
    public class EditModel : PageModel
    {
        private readonly ApiService _apiService;

        public EditModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public PetUpdateRequestModelFE Pet { get; set; } = default!;

        public List<SelectListItem> ShelterOptions { get; set; } = new List<SelectListItem>();

        public IFormFile? ImageFile { get; set; }

        public string? ExistingImageData { get; set; }

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

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (string.IsNullOrEmpty(role) || role == "e7b8f3d2-4a2f-4c3b-8f4d-9c5d8a3e1b2c") // User role
            {
                return RedirectToPage("/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            // Gọi API để lấy thông tin Pet
            var response = await _apiService.GetAsync<BaseResponseModelFE<PetUpdateRequestModelFE>>($"https://localhost:7297/api/pet/id?id={id}");

            if (response == null || response.Data == null)
            {
                return NotFound();
            }

            // Gọi API để lấy thông tin Pet
            var petResponse = await _apiService.GetAsync<BaseResponseModelFE<PetResponseModelFE>>($"https://localhost:7297/api/pet/id?id={id}");

            if (petResponse == null || petResponse.Data == null)
            {
                return NotFound();
            }

            Pet = response.Data;

            ExistingImageData = petResponse.Data.ImageData;

            await LoadShelterAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadShelterAsync();
                return Page();
            }

            Pet.Image = ImageFile != null ? await _apiService.ConvertToByteArrayAsync(ImageFile) : null;

            // Gọi API để lấy thông tin Pet
            var petResponse = await _apiService.GetAsync<BaseResponseModelFE<PetResponseModelFE>>($"https://localhost:7297/api/pet/id?id={Pet.PetId}");

            if (petResponse == null || petResponse.Data == null)
            {
                return NotFound();
            }

            Pet.Status = petResponse.Data.Status;

            // Gọi API để cập nhật Pet
            var response = await _apiService.PutAsync<PetUpdateRequestModelFE, BaseResponseModelFE<PetResponseModelFE>>(
                $"https://localhost:7297/api/pet/update", Pet);

            if (response == null || response.Code != 200)
            {
                await LoadShelterAsync();
                ModelState.AddModelError(string.Empty, "Failed to update the pet.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
