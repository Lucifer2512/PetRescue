using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;
using BusinessLayer.Model.Response;
using BusinessLayer.Models.Response;

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

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiUrl = $"https://localhost:7297/api/shelter/{id}";
            var response = await _apiService.GetAsync<BaseResponseModel<ShelterResponseModel>>(apiUrl);

            if (response.Data == null)
            {
                return NotFound();
            }

            Shelter = response.Data;
            return Page();
        }
    }
}
