using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;
using Pages.Model;
using Azure;
using BusinessLayer.Models.Response;

namespace PetRescueFE.Pages.UserPage
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public IList<UserResponseModel> Users { get;set; } = default!;


        public async Task OnGetAsync()
        {
            var apiUrl = "https://localhost:7297/api/users";
            var response = await _apiService.GetAsync<BusinessLayer.Models.Response.BaseResponseModel<IList<UserResponseModel>>>(apiUrl);

            if (response.Data != null)
            {
                Users = response.Data;
            }
        }
    }
}
