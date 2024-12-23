﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetRescueFE.Pages.Model;
using PetRescueFE.Pages.Model.Shelters;

namespace PetRescueFE.Pages.ShelterPage
{
    public class DetailsModel : PageModel
    {
        private readonly ApiService _apiService;

        public DetailsModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public ShelterResponseModel Shelter { get; set; } = default!;
        public string UserRole { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            UserRole = HttpContext.Session.GetString("Role");

            if (id == null)
            {
                return NotFound();
            }

            var apiUrl = $"https://localhost:7297/api/shelter/{id}";
            var response = await _apiService.GetAsync<BaseResponseModelFE<ShelterResponseModel>>(apiUrl);

            if (response.Data == null)
            {
                return NotFound();
            }

            Shelter = response.Data;
            return Page();
        }
    }
}
