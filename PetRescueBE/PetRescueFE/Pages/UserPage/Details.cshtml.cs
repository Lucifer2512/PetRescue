using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;
using BusinessLayer.Models.Response;

namespace PetRescueFE.Pages.UserPage
{
    public class DetailsModel : PageModel
    {
        private readonly ApiService _apiService;

        public DetailsModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public UserResponseModel User { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiUrl = $"https://localhost:7297/api/users/{id}";
            var response = await _apiService.GetAsync<BaseResponseModel<UserResponseModel>>(apiUrl);

            if (response.Data == null)
            {
                return NotFound();
            }

            User = response.Data;
            return Page();
        }
    }
}
