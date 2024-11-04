﻿using System;
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
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public IList<ShelterResponseModel> Shelter { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var apiUrl = "https://localhost:7297/api/shelter";
            var response = await _apiService.GetAsync<BaseResponseModel<IList<ShelterResponseModel>>>(apiUrl);

            if (response.Data != null)
            {
                Shelter = response.Data;
            }
        }
    }
}